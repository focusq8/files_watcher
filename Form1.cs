using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FileWatcher
{
    public partial class Form1 : Form
    {
        // ── Constants & fields ────────────────────────────────────────────────
        private readonly string settingsFilePath = Path.Combine(Application.StartupPath, "Setting.ini");
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int MaxTitleLength = 16;
        private const int MaxLogEntries  = 500;
        private const int DedupMs        = 500;   // ignore same event on same file within this window

        private struct LogEntry { public string Action, FilePath; public DateTime Time; }

        private readonly List<LogEntry>            allLogs     = new List<LogEntry>();
        private readonly List<FileSystemWatcher>   watchers    = new List<FileSystemWatcher>();
        private readonly Dictionary<string, DateTime> lastEvents = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> activeExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private int  countCreated, countDeleted, countChanged, countRenamed;
        private bool darkMode;

        // ── P/Invoke: placeholder text for TextBox ────────────────────────────
        [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
        private void SetPlaceholder(TextBox tb, string hint) => SendMessage(tb.Handle, 0x1501, (IntPtr)1, hint);

        // ── Init ──────────────────────────────────────────────────────────────
        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadSettingsFile();
            SetPlaceholder(textBox3, "Search in log...");
            SetPlaceholder(textBox2, "e.g. txt");
            SetPlaceholder(textBoxPath, "Select an item to view the full path...");
            UpdateStatusBar(false);

            // Tooltips
            toolTip1.SetToolTip(checkBox1,  "Randomize the window title every 3 seconds");
            toolTip1.SetToolTip(checkBox2,  "Watch all file types (ignore extension list)");
            toolTip1.SetToolTip(textBox2,   "Type a file extension then use Add Extension > Add");
            toolTip1.SetToolTip(listBox2,   "Watched extensions — right-click to remove");
            toolTip1.SetToolTip(listBox3,   "Watched paths — right-click to remove");
            toolTip1.SetToolTip(textBox3,   "Filter the log by text (action, path or time)");
            toolTip1.SetToolTip(chkCreated, "Show / hide Created events");
            toolTip1.SetToolTip(chkDeleted, "Show / hide Deleted events");
            toolTip1.SetToolTip(chkChanged, "Show / hide Changed events");
            toolTip1.SetToolTip(chkRenamed, "Show / hide Renamed events");
            toolTip1.SetToolTip(textBoxPath,"Full path of the selected log entry");
        }

        // ── Settings ──────────────────────────────────────────────────────────
        private void ReadSettingsFile()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                    listBox2.Items.AddRange(File.ReadAllLines(settingsFilePath));
                else
                    File.CreateText(settingsFilePath).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot read settings file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveExtensionsToFile()
        {
            try
            {
                using (var sw = new StreamWriter(settingsFilePath))
                    foreach (var ext in listBox2.Items.Cast<string>())
                        sw.WriteLine(ext);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save extensions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Duplicate filter ──────────────────────────────────────────────────
        private bool IsDuplicate(string action, string path)
        {
            var key = action + "|" + path;
            var now = DateTime.Now;
            lock (lastEvents)
            {
                // Fix #1: clean up stale entries to prevent unbounded memory growth
                var stale = lastEvents
                    .Where(kv => (now - kv.Value).TotalSeconds > 5)
                    .Select(kv => kv.Key).ToList();
                foreach (var k in stale) lastEvents.Remove(k);

                if (lastEvents.TryGetValue(key, out var last) &&
                    (now - last).TotalMilliseconds < DedupMs)
                    return true;
                lastEvents[key] = now;
                return false;
            }
        }

        // ── Extension filter ──────────────────────────────────────────────────
        private bool MatchesExtensionFilter(string filePath)
        {
            if (activeExtensions.Count == 0) return true;
            return activeExtensions.Contains(Path.GetExtension(filePath));
        }

        private void RebuildExtensionFilter()
        {
            activeExtensions.Clear();
            if (!checkBox2.Checked)
                foreach (var item in listBox2.Items.Cast<string>())
                    activeExtensions.Add(item.TrimStart('*'));
        }

        // ── Watcher management ────────────────────────────────────────────────
        private FileSystemWatcher CreateWatcher(string path)
        {
            var w = new FileSystemWatcher(path)
            {
                IncludeSubdirectories = true,
                Filter       = "*.*",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite,
                InternalBufferSize = 65536
            };
            w.Deleted += (s, ev) => { if (!IsDuplicate("Deleted", ev.FullPath) && MatchesExtensionFilter(ev.FullPath)) OnWatcherEvent("Deleted", ev.FullPath); };
            w.Created += (s, ev) => { if (!IsDuplicate("Created", ev.FullPath) && MatchesExtensionFilter(ev.FullPath)) OnWatcherEvent("Created", ev.FullPath); };
            w.Changed += (s, ev) => { if (!IsDuplicate("Changed", ev.FullPath) && MatchesExtensionFilter(ev.FullPath)) OnWatcherEvent("Changed", ev.FullPath); };
            w.Renamed += (s, ev) => { if (!IsDuplicate("Renamed", ev.FullPath) && MatchesExtensionFilter(ev.FullPath)) OnWatcherEvent("Renamed", ev.FullPath); };
            return w;
        }

        private void OnWatcherEvent(string action, string filePath)
        {
            LogEvent(action, filePath);
            Invoke(new Action(() => { Show(); WindowState = FormWindowState.Normal; Activate(); }));
        }

        private void StopAllWatchers()
        {
            foreach (var w in watchers) { w.EnableRaisingEvents = false; w.Dispose(); }
            watchers.Clear();
        }

        // ── Log ───────────────────────────────────────────────────────────────
        private void LogEvent(string action, string filePath)
        {
            var entry = new LogEntry { Action = action, FilePath = filePath, Time = DateTime.Now };
            lock (allLogs)
            {
                allLogs.Add(entry);
                if (allLogs.Count > MaxLogEntries) allLogs.RemoveAt(0);
            }
            // Fix #3: use Interlocked to safely increment from background threads
            switch (action)
            {
                case "Created": System.Threading.Interlocked.Increment(ref countCreated); break;
                case "Deleted": System.Threading.Interlocked.Increment(ref countDeleted); break;
                case "Changed": System.Threading.Interlocked.Increment(ref countChanged); break;
                case "Renamed": System.Threading.Interlocked.Increment(ref countRenamed); break;
            }
            Invoke(new Action(() => { RefreshLog(); UpdateStats(); }));
        }

        private void RefreshLog()
        {
            var search    = textBox3.Text.ToLower().Trim();
            bool fc = chkCreated.Checked, fd = chkDeleted.Checked,
                 fch = chkChanged.Checked, fr = chkRenamed.Checked;
            bool anyFilter = fc || fd || fch || fr;

            List<LogEntry> snapshot;
            lock (allLogs) { snapshot = new List<LogEntry>(allLogs); }

            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach (var log in snapshot)
            {
                bool matchFilter = !anyFilter ||
                    (fc  && log.Action == "Created") || (fd  && log.Action == "Deleted") ||
                    (fch && log.Action == "Changed") || (fr  && log.Action == "Renamed");
                bool matchSearch = string.IsNullOrEmpty(search) ||
                    log.Action.ToLower().Contains(search) ||
                    log.FilePath.ToLower().Contains(search) ||
                    log.Time.ToString("yyyy-MM-dd HH:mm:ss").Contains(search);

                if (!matchFilter || !matchSearch) continue;

                var item = new ListViewItem(log.Action);
                item.SubItems.Add(log.FilePath);
                item.SubItems.Add(log.Time.ToString("yyyy-MM-dd HH:mm:ss"));
                item.ForeColor = GetActionColor(log.Action);
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
            if (listView1.Items.Count > 0)
                listView1.EnsureVisible(listView1.Items.Count - 1);
        }

        private void UpdateStats()
        {
            lblStats.Text = $"  Created: {countCreated}   Deleted: {countDeleted}   Changed: {countChanged}   Renamed: {countRenamed}";
        }

        private void UpdateStatusBar(bool running)
        {
            if (running)
            {
                lblStatus.Text      = $"  Running  ({watchers.Count} path{(watchers.Count != 1 ? "s" : "")})  ";
                lblStatus.ForeColor = darkMode ? Color.FromArgb(100, 220, 100) : Color.FromArgb(0, 130, 0);
            }
            else
            {
                lblStatus.Text      = "  Stopped  ";
                lblStatus.ForeColor = darkMode ? Color.FromArgb(150, 150, 150) : Color.FromArgb(100, 100, 100);
            }
        }

        // ── Dark Mode ─────────────────────────────────────────────────────────
        private Color GetActionColor(string action)
        {
            if (darkMode)
            {
                switch (action)
                {
                    case "Created": return Color.FromArgb(100, 220, 100);
                    case "Deleted": return Color.FromArgb(255, 100, 100);
                    case "Changed": return Color.FromArgb(100, 180, 255);
                    case "Renamed": return Color.FromArgb(255, 200, 80);
                }
                return Color.FromArgb(220, 220, 220);
            }
            switch (action)
            {
                case "Created": return Color.FromArgb(0, 140, 0);
                case "Deleted": return Color.FromArgb(180, 0, 0);
                case "Changed": return Color.FromArgb(0, 80, 200);
                case "Renamed": return Color.FromArgb(160, 80, 0);
            }
            return SystemColors.WindowText;
        }

        private void ApplyTheme()
        {
            var bg      = darkMode ? Color.FromArgb(30, 30, 30)  : SystemColors.Control;
            var fg      = darkMode ? Color.FromArgb(220, 220, 220) : SystemColors.ControlText;
            var inputBg = darkMode ? Color.FromArgb(45, 45, 45)  : SystemColors.Window;
            var menuBg  = darkMode ? Color.FromArgb(40, 40, 40)  : SystemColors.Control;

            BackColor = bg;
            ForeColor = fg;
            ApplyToControls(Controls, bg, fg, inputBg);
            menuStrip1.BackColor = menuBg;
            menuStrip1.ForeColor = fg;
            ApplyMenuColors(menuStrip1.Items, menuBg, fg);
            statusStrip1.BackColor = menuBg;
            statusStrip1.ForeColor = fg;
            lblStats.ForeColor     = fg;
            UpdateStatusBar(watchers.Any(w => w.EnableRaisingEvents));
            RefreshLog();
        }

        private void ApplyToControls(Control.ControlCollection controls, Color bg, Color fg, Color inputBg)
        {
            foreach (Control c in controls)
            {
                if (c is MenuStrip) continue;
                if (c is TextBox || c is ListBox || c is ListView)
                {
                    c.BackColor = inputBg;
                    c.ForeColor = fg;
                }
                else
                {
                    c.BackColor = bg;
                    c.ForeColor = fg;
                }
                if (c.Controls.Count > 0)
                    ApplyToControls(c.Controls, bg, fg, inputBg);
            }
        }

        private void ApplyMenuColors(ToolStripItemCollection items, Color bg, Color fg)
        {
            foreach (ToolStripItem item in items)
            {
                item.BackColor = bg;
                item.ForeColor = fg;
                if (item is ToolStripMenuItem mi && mi.DropDownItems.Count > 0)
                    ApplyMenuColors(mi.DropDownItems, bg, fg);
            }
        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkMode = darkModeToolStripMenuItem.Checked;
            ApplyTheme();
        }

        // ── ListView helpers ──────────────────────────────────────────────────
        private string GetSelectedPath()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            return listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxPath.Text = listView1.SelectedItems.Count > 0
                ? listView1.SelectedItems[0].SubItems[1].Text
                : string.Empty;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var path = GetSelectedPath();
            if (path == null) return;
            try
            {
                // Fix #5: always route through explorer.exe — never Shell-execute an arbitrary path
                if (File.Exists(path) || Directory.Exists(path))
                    Process.Start("explorer.exe", "\"" + path + "\"");
            }
            catch (Exception ex) { MessageBox.Show($"Failed to open. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = listView1.SelectedItems.Count == 0;
        }

        // ── UI events ─────────────────────────────────────────────────────────
        private void timer1_Tick(object sender, EventArgs e)
        {
            var rng = new Random();
            Text = new string(Enumerable.Range(0, rng.Next(1, MaxTitleLength))
                .Select(_ => Chars[rng.Next(Chars.Length)]).ToArray());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) timer1.Start();
            else
            {
                timer1.Stop();
                Text = watchers.Any(w => w.EnableRaisingEvents) ? "Files Watcher [Running]" : "Files Watcher";
            }
        }

        private void All_Extenions_CheckedChanged(object sender, EventArgs e)
        {
            RebuildExtensionFilter();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)          => RefreshLog();
        private void FilterCheckBox_CheckedChanged(object sender, EventArgs e) => RefreshLog();

        private void choosePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fb = new FolderBrowserDialog())
            {
                fb.SelectedPath = @"C:\";
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    if (!listBox3.Items.Contains(fb.SelectedPath))
                        listBox3.Items.Add(fb.SelectedPath);
                    else
                        MessageBox.Show("This path is already in the watch list.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void removePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null) listBox3.Items.Remove(listBox3.SelectedItem);
            else MessageBox.Show("Select a path to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox3.Items.Count == 0)
                {
                    MessageBox.Show("Please choose at least one path to watch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                RebuildExtensionFilter();
                foreach (var path in listBox3.Items.Cast<string>())
                {
                    var w = CreateWatcher(path);
                    w.EnableRaisingEvents = true;
                    watchers.Add(w);
                }
                StartToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
                if (!checkBox1.Checked) Text = "Files Watcher [Running]";
                UpdateStatusBar(true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StopAllWatchers();
                StartToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
                if (!checkBox1.Checked) Text = "Files Watcher";
                UpdateStatusBar(false);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (allLogs) { allLogs.Clear(); }
            listView1.Items.Clear();
            countCreated = countDeleted = countChanged = countRenamed = 0;
            UpdateStats();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ext = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(ext)) { MessageBox.Show("Please add an extension.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            var full = "*." + ext;
            if (listBox2.Items.Contains(full)) { MessageBox.Show("This extension already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            listBox2.Items.Add(full);
            textBox2.Clear();
            SaveExtensionsToFile();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null) { listBox2.Items.Remove(listBox2.SelectedItem); SaveExtensionsToFile(); }
            else MessageBox.Show("Select an extension to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Fix #2: prefix fields that start with formula characters to prevent CSV injection
        private static string SanitizeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return field;
            char c = field[0];
            if (c == '=' || c == '+' || c == '-' || c == '@' || c == '\t' || c == '\r')
                return "'" + field;
            return field;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0) { MessageBox.Show("Cannot save, log is empty."); return; }
            try
            {
                using (var dlg = new SaveFileDialog())
                {
                    dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    dlg.Filter = "Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    bool csv = dlg.FilterIndex == 2;
                    string sep = csv ? "," : "\t";
                    var header = csv ? "Type,Path,Time" : "Type\tPath\tTime";
                    var lines = new[] { header }.Concat(
                        listView1.Items.Cast<ListViewItem>().Select(i =>
                        {
                            var f0 = csv ? SanitizeCsvField(i.SubItems[0].Text) : i.SubItems[0].Text;
                            var f1 = csv ? SanitizeCsvField(i.SubItems[1].Text) : i.SubItems[1].Text;
                            var f2 = csv ? SanitizeCsvField(i.SubItems[2].Text) : i.SubItems[2].Text;
                            return $"{f0}{sep}{f1}{sep}{f2}";
                        }));
                    File.WriteAllLines(dlg.FileName, lines);
                    MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Failed to save file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = GetSelectedPath();
            if (path == null) { MessageBox.Show("Please select an item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            try { Process.Start(Path.GetDirectoryName(path)); }
            catch (Exception ex) { MessageBox.Show($"Failed to open folder. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = GetSelectedPath();
            if (path == null) { MessageBox.Show("Please select an item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            try { Process.Start("explorer.exe", "\"" + path + "\""); }
            catch (Exception ex) { MessageBox.Show($"Failed to open file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void deleteFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var path = GetSelectedPath();
            if (path == null) { MessageBox.Show("Please select a file to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            try
            {
                if (MessageBox.Show($"Are you sure you want to delete:\n{path}", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                // Fix #4: removed TOCTOU IsFileLocked pre-check — let File.Delete throw directly if locked
                File.Delete(path);
                MessageBox.Show("File deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException) { MessageBox.Show("File is in use and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show($"Failed to delete file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = GetSelectedPath();
            if (path == null) { MessageBox.Show("Please select an item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (File.Exists(path) || Directory.Exists(path))
            { Clipboard.SetText(path); MessageBox.Show("Path copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            else MessageBox.Show("The selected path does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.ShowBalloonTip(1000, "Files Watcher", "Has Been Minimized", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) => Show();
    }
}
