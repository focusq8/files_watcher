using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class Form1 : Form
    {
        private readonly string settingsFilePath = Path.Combine(Application.StartupPath, "Setting.ini");
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int MaxTitleLength = 16;
        public Form1()
        {
            InitializeComponent();
        }
        private void ReadSettingsFile()
        {
            try
            {
                if (File.Exists(settingsFilePath))
                    listBox2.Items.AddRange(File.ReadAllLines(settingsFilePath));
                else
                    // Create the file if it does not exist
                    File.CreateText(settingsFilePath).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot create or read the settings file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveExtensionsToFile()
        {
            // Use the 'using' statement to ensure the StreamWriter is properly disposed of
            try
            {
                // Initialize the StreamWriter within a using block to ensure proper disposal
                using (var streamWriter = new StreamWriter("Setting.ini"))
                {
                    // Write each extension to the file
                    foreach (var extension in listBox2.Items.Cast<string>())
                    {
                        streamWriter.WriteLine(extension);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show($"Failed to save extensions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ApplyFilter()
        {
            if (listBox2.Items.Count > 0)
            {
                var extensions = string.Join(";", listBox2.Items.Cast<string>());
                fileSystemWatcherEx1.Filter = extensions;
            }
            else
            {
                MessageBox.Show("Setting.ini is empty. Please add extensions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadSettingsFile();
            ApplyFilter();
        }
        private void fileSystemWatcherEx1_OnDeleted(object sender, FileSystemEventArgs e)
        {
            string action = e.ChangeType.ToString();
            listBox1.Items.Add($"{action}: *{e.FullPath}");

            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void fileSystemWatcherEx1_OnRenamed(object sender,RenamedEventArgs e)
        {
            string action = e.ChangeType.ToString();
            listBox1.Items.Add($"{action}: *{e.FullPath}");

            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void fileSystemWatcherEx1_OnCreated(object sender, FileSystemEventArgs e)
        {
            string action = e.ChangeType.ToString();
            listBox1.Items.Add($"{action}: *{e.FullPath}");

            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var random = new Random();
            var randomString = new string(Enumerable.Range(0, random.Next(1, MaxTitleLength))
                .Select(_ => Chars[random.Next(Chars.Length)]).ToArray());
            Text = randomString;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                timer1.Start();
            else
                timer1.Stop();
            Text = "Files Watcher";           
        }
        private void All_Extenions_CheckedChanged(object sender, EventArgs e)
        {
            fileSystemWatcherEx1.Filter = checkBox2.Checked ? "*.*" : string.Empty;
            if (!checkBox2.Checked)
                ApplyFilter();
        }
        private void choosePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = @"C:\";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = folderBrowser.SelectedPath;
                    fileSystemWatcherEx1.Path = textBox1.Text;
                }
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programmed using C# By BoSalem Alazmi\n\n\n\nThank you (Qassam Sniper , Maged Khoja)",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.Items.Clear();
            }
            else
            {
                MessageBox.Show("Cannot clear, ListBox is empty.");
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var extension = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(extension))
            {
                MessageBox.Show("Please add an extension.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            listBox2.Items.Add("*." + extension);
            textBox2.Clear();
            SaveExtensionsToFile();
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem != null)
                {
                    listBox2.Items.Remove(listBox2.SelectedItem);
                    SaveExtensionsToFile();
                }
                else
                {
                    MessageBox.Show("Select an extension to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to remove extension. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = listBox1.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Please select a folder to open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var path = selectedItem.Split('*')[1].Trim();
                var directoryPath = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    Process.Start(directoryPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open folder. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = listBox1.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Please select a file to open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var path = selectedItem.Split('*')[1].Trim();
                Process.Start("explorer", path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = listBox1.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedItem))
                {
                    MessageBox.Show("Please select a file to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var path = selectedItem.Split('*')[1].Trim();
                // Check if file is in use
                if (IsFileLocked(path))
                {
                    MessageBox.Show("The file is currently in use by another process and cannot be deleted. Please close the file and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Delete the file
                File.Delete(path);
                MessageBox.Show("File deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsFileLocked(string filePath)
        {
            FileStream stream = null;
            try
            {
                // Attempt to open the file with exclusive access
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is locked
                return true;
            }
            finally
            {
                // Close the file stream if it was successfully opened
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return false;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Cannot save, ListBox is empty.");
                return;
            }
            try
            {
                using (var saveTxt = new SaveFileDialog())
                {
                    saveTxt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    saveTxt.Filter = "Text Files (*.txt)|*.txt";
                    if (saveTxt.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllLines(saveTxt.FileName, listBox1.Items.Cast<string>());
                        MessageBox.Show("File saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save file. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected item as a string
                var selectedItem = listBox1.SelectedItem as string;
                if (string.IsNullOrEmpty(selectedItem))
                {
                    // Notify user if no item is selected
                    MessageBox.Show("Please select a file to copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Extract the full path from the selected item
                var path = selectedItem.Split('*')[1].Trim();
                // Check if the file exists
                if (File.Exists(path))
                {
                    // Copy the full path (including file name) to the clipboard
                    Clipboard.SetText(path);
                    MessageBox.Show("File path copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Directory.Exists(path))
                {
                    // Handle the case where the path itself is a directory
                    Clipboard.SetText(path);
                    MessageBox.Show("Directory path copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The selected path does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                MessageBox.Show($"Failed to copy path. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length > 0)
                {
                    fileSystemWatcherEx1.IncludeSubdirectories = true;
                    fileSystemWatcherEx1.EnableRaisingEvents = true;
                    fileSystemWatcherEx1.EnableRaisingEvents = true;

                    StartToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = true;
                }

                else
                    MessageBox.Show("Please Choose Path To Watch Files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fileSystemWatcherEx1.EnableRaisingEvents = false;
                StartToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Move(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.ShowBalloonTip(1000, "Files Watcher", "Has Been Minimized", ToolTipIcon.Info);
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }
    }
}