using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class Form1 : Form
    {
        //read all Extensions from file Setting.ini
        private readonly string read_file_extension = Application.StartupPath + "\\Setting.ini";
        private readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly int MAXMIUM_TITLE_LENGTH = 16;
        public Form1()
        {
            InitializeComponent();
        }
        private void read_file()
        {
            try
            {
                if (File.Exists(read_file_extension))
                    listBox2.Items.AddRange(File.ReadAllLines(read_file_extension));
                else
                    File.CreateText("Setting.ini");
            }

            catch (Exception)
            {
                MessageBox.Show("Can not create file","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void extension_file()
        {
            // use using to close StreamWriter when finished
            using (StreamWriter streamWriter = new StreamWriter("Setting.ini"))
                try
                {
                    foreach (object add_extension in listBox2.Items)
                    {                        
                        streamWriter.WriteLine(add_extension.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
        }
        private void choose_filter()
        // upload all Extensions from file Setting.ini in listBox2
        {
            if (listBox2.Items.Count > 0)
            {
                string extensions = string.Empty;
                foreach (object Item in listBox2.Items)
                    extensions += Item + ";";
                string add_filter = extensions.Remove(extensions.Length - 1);
                fileSystemWatcherEx1.Filter = add_filter;
            }
            else
                MessageBox.Show("Setting.ini is empty Please Add Extensions ! ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            read_file();
            choose_filter();
        }
        private void fileSystemWatcherEx1_OnDeleted(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add($"Deleted:    *{e.FullPath}\n");
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void fileSystemWatcherEx1_OnRenamed(object sender,RenamedEventArgs e)
        {
            listBox1.Items.Add($"Renamed:    *{e.FullPath}\n");
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void fileSystemWatcherEx1_OnCreated(object sender, FileSystemEventArgs e)
        {
            listBox1.Items.Add($"Created:    *{e.FullPath}\n");
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // random alphabet            
            var stringChars = string.Empty;
            var random = new Random();
            int length = random.Next(1, MAXMIUM_TITLE_LENGTH);
            for (int i = 0; i < length; i++)
                stringChars += chars[random.Next(chars.Length)];
            Text = stringChars;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                timer1.Start();
            else
                timer1.Stop();
            Text = "Files Watcher";           
        }
        private void All_Extenions_CheckedChanged(object sender, EventArgs e)
        {
            // choose All Extenions 
            if (checkBox2.Checked == true)

                fileSystemWatcherEx1.Filter = "*.*";

            else if (checkBox2.Checked == false)
                choose_filter();
        }
        private void choosePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_browser = new FolderBrowserDialog();
            string selectedFolder = @"C:\";
            folder_browser.SelectedPath = selectedFolder;
            if (folder_browser.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folder_browser.SelectedPath;
                fileSystemWatcherEx1.Path = textBox1.Text;
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programmed using C# By BoSalem Alazmi\n\n\n\nThank you (Qassam Sniper , Maged Khoja)",
                string.Empty,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count > 0)
                {
                    listBox1.Items.Clear();
                }
                else
                    MessageBox.Show("Can Not Clear, ListBox Is Empty");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrEmpty(textBox2.Text))
                MessageBox.Show("Please Add Extension","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            else
            {
                string text = textBox2.Text;
                string text2 = "*." + text;
                listBox2.Items.Add(text2);
                textBox2.Clear();

            }
            extension_file();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                object remove_file = listBox2.SelectedItem;
                listBox2.Items.Remove(remove_file);
                extension_file();
            }

            catch (Exception error)
            {
                MessageBox.Show(error.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }      
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(listBox1.Text))
                    MessageBox.Show("please select the Folder To open it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string choose_folder = listBox1.SelectedItem.ToString();
                    string path = choose_folder.Split('*')[1];
                    Path.GetInvalidPathChars().ToList().ForEach(c => path = path.Replace(c.ToString(), ""));
                    string directoryPathOnly = Path.GetDirectoryName(path);
                    Process.Start(directoryPathOnly);
                }               
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(listBox1.Text))
                    MessageBox.Show("please select the File To open it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string choose_folder = listBox1.SelectedItem.ToString();
                    string path = choose_folder.Split('*')[1];
                    Process.Start("explorer", path);
                }                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteFileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                // delete file
                if (string.IsNullOrEmpty(listBox1.Text))
                    MessageBox.Show("please select the file to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    string choose_folder = listBox1.SelectedItem.ToString();
                    string path = choose_folder.Split('*')[1];
                    File.Delete(path);
                }
            }
            catch (Exception error)

            {
                
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count > 0)
                {
                    SaveFileDialog save_txt = new SaveFileDialog();
                    save_txt.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    save_txt.Filter = "TEXT (*.txt)|*.txt";
                    save_txt.ShowDialog();
                    using (StreamWriter writer = new StreamWriter(save_txt.FileName))
                        foreach (object saver in listBox1.Items)
                        {    
                            writer.Write(saver.ToString());
                        }                  
                    MessageBox.Show("Done saved It", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Can Not Save, ListBox Is Empty");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string choose_folder = listBox1.SelectedItem.ToString();
                string path = choose_folder.Split('*')[1];
                Path.GetInvalidPathChars().ToList().ForEach(c => path = path.Replace(c.ToString(), ""));
                string directoryPathOnly = Path.GetDirectoryName(path);
                Clipboard.SetText(directoryPathOnly);
                MessageBox.Show("Copied!",string.Empty,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }            
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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