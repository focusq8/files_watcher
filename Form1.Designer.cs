
using System;

namespace FileWatcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.listView1                      = new System.Windows.Forms.ListView();
            this.colType                        = new System.Windows.Forms.ColumnHeader();
            this.colPath                        = new System.Windows.Forms.ColumnHeader();
            this.colTime                        = new System.Windows.Forms.ColumnHeader();
            this.groupBox1                      = new System.Windows.Forms.GroupBox();
            this.listBox2                       = new System.Windows.Forms.ListBox();
            this.textBox2                       = new System.Windows.Forms.TextBox();
            this.label2                         = new System.Windows.Forms.Label();
            this.checkBox2                      = new System.Windows.Forms.CheckBox();
            this.groupBox2                      = new System.Windows.Forms.GroupBox();
            this.listBox3                       = new System.Windows.Forms.ListBox();
            this.panelSep                       = new System.Windows.Forms.Panel();
            this.textBox3                       = new System.Windows.Forms.TextBox();
            this.textBoxPath                    = new System.Windows.Forms.TextBox();
            this.checkBox1                      = new System.Windows.Forms.CheckBox();
            this.chkCreated                     = new System.Windows.Forms.CheckBox();
            this.chkDeleted                     = new System.Windows.Forms.CheckBox();
            this.chkChanged                     = new System.Windows.Forms.CheckBox();
            this.chkRenamed                     = new System.Windows.Forms.CheckBox();
            this.timer1                         = new System.Windows.Forms.Timer(this.components);
            this.toolTip1                       = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1                     = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.StartToolStripMenuItem         = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.choosePathToolStripMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.addExtensionToolStripMenuItem  = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem        = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem         = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.darkModeToolStripMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.label4                         = new System.Windows.Forms.Label();
            this.statusStrip1                   = new System.Windows.Forms.StatusStrip();
            this.lblStats                       = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpring                      = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus                      = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1              = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderToolStripMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileToolStripMenuItem1   = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2              = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removePathToolStripMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1                    = new System.Windows.Forms.NotifyIcon(this.components);

            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();

            // ── listView1 ─────────────────────────────────────────────────────
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colType, this.colPath, this.colTime });
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect    = true;
            this.listView1.GridLines        = true;
            this.listView1.BorderStyle      = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Location         = new System.Drawing.Point(128, 76);
            this.listView1.Name             = "listView1";
            this.listView1.Size             = new System.Drawing.Size(567, 359);
            this.listView1.TabIndex         = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View             = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick             += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged    += new System.EventHandler(this.listView1_SelectedIndexChanged);

            this.colType.Text  = "Type";
            this.colType.Width = 80;
            this.colPath.Text  = "Path";
            this.colPath.Width = 352;
            this.colTime.Text  = "Time";
            this.colTime.Width = 130;

            // ── groupBox1 – Extensions ────────────────────────────────────────
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Bottom)));
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Location = new System.Drawing.Point(2, 40);
            this.groupBox1.Name     = "groupBox1";
            this.groupBox1.Size     = new System.Drawing.Size(118, 220);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop  = false;
            this.groupBox1.Text     = "Extensions";

            // listBox2 inside groupBox1
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.BorderStyle       = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location          = new System.Drawing.Point(5, 90);
            this.listBox2.Name              = "listBox2";
            this.listBox2.Size              = new System.Drawing.Size(107, 122);
            this.listBox2.TabIndex          = 2;

            // textBox2 inside groupBox1
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location    = new System.Drawing.Point(5, 52);
            this.textBox2.Name        = "textBox2";
            this.textBox2.Size        = new System.Drawing.Size(107, 21);
            this.textBox2.TabIndex    = 6;

            // label2 inside groupBox1
            this.label2.AutoSize  = true;
            this.label2.Location  = new System.Drawing.Point(5, 36);
            this.label2.Name      = "label2";
            this.label2.TabIndex  = 17;
            this.label2.Text      = "Add:";

            // checkBox2 inside groupBox1
            this.checkBox2.AutoSize  = true;
            this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox2.Location  = new System.Drawing.Point(5, 16);
            this.checkBox2.Name      = "checkBox2";
            this.checkBox2.TabIndex  = 14;
            this.checkBox2.Text      = "All Extensions";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.All_Extenions_CheckedChanged);

            // ── groupBox2 – Paths ─────────────────────────────────────────────
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.listBox3);
            this.groupBox2.Location = new System.Drawing.Point(2, 268);
            this.groupBox2.Name     = "groupBox2";
            this.groupBox2.Size     = new System.Drawing.Size(118, 188);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop  = false;
            this.groupBox2.Text     = "Paths";

            // listBox3 inside groupBox2
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox3.BorderStyle       = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox3.ContextMenuStrip  = this.contextMenuStrip2;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location          = new System.Drawing.Point(5, 16);
            this.listBox3.Name              = "listBox3";
            this.listBox3.Size              = new System.Drawing.Size(107, 165);
            this.listBox3.TabIndex          = 20;

            // ── panelSep – vertical divider ───────────────────────────────────
            this.panelSep.Anchor    = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSep.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSep.Location  = new System.Drawing.Point(122, 26);
            this.panelSep.Name      = "panelSep";
            this.panelSep.Size      = new System.Drawing.Size(1, 438);
            this.panelSep.TabIndex  = 42;

            // ── textBox3 – search ─────────────────────────────────────────────
            this.textBox3.Anchor      = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location    = new System.Drawing.Point(178, 27);
            this.textBox3.Name        = "textBox3";
            this.textBox3.Size        = new System.Drawing.Size(512, 21);
            this.textBox3.TabIndex    = 21;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);

            // ── textBoxPath – full path display ───────────────────────────────
            this.textBoxPath.Anchor      = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPath.Location    = new System.Drawing.Point(128, 440);
            this.textBoxPath.Name        = "textBoxPath";
            this.textBoxPath.ReadOnly    = true;
            this.textBoxPath.Size        = new System.Drawing.Size(567, 21);
            this.textBoxPath.TabIndex    = 31;
            this.textBoxPath.TabStop     = false;

            // ── checkBox1 – Random Title (on form, above groupBox1) ───────────
            this.checkBox1.AutoSize  = true;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox1.Location  = new System.Drawing.Point(5, 23);
            this.checkBox1.Name      = "checkBox1";
            this.checkBox1.TabIndex  = 10;
            this.checkBox1.Text      = "Rnd Title";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);

            // ── Filter checkboxes ─────────────────────────────────────────────
            this.chkCreated.AutoSize  = true;
            this.chkCreated.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkCreated.Location  = new System.Drawing.Point(128, 52);
            this.chkCreated.Name      = "chkCreated";
            this.chkCreated.TabIndex  = 22;
            this.chkCreated.Text      = "Created";
            this.chkCreated.UseVisualStyleBackColor = true;
            this.chkCreated.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);

            this.chkDeleted.AutoSize  = true;
            this.chkDeleted.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkDeleted.Location  = new System.Drawing.Point(270, 52);
            this.chkDeleted.Name      = "chkDeleted";
            this.chkDeleted.TabIndex  = 23;
            this.chkDeleted.Text      = "Deleted";
            this.chkDeleted.UseVisualStyleBackColor = true;
            this.chkDeleted.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);

            this.chkChanged.AutoSize  = true;
            this.chkChanged.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkChanged.Location  = new System.Drawing.Point(412, 52);
            this.chkChanged.Name      = "chkChanged";
            this.chkChanged.TabIndex  = 24;
            this.chkChanged.Text      = "Changed";
            this.chkChanged.UseVisualStyleBackColor = true;
            this.chkChanged.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);

            this.chkRenamed.AutoSize  = true;
            this.chkRenamed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkRenamed.Location  = new System.Drawing.Point(554, 52);
            this.chkRenamed.Name      = "chkRenamed";
            this.chkRenamed.TabIndex  = 25;
            this.chkRenamed.Text      = "Renamed";
            this.chkRenamed.UseVisualStyleBackColor = true;
            this.chkRenamed.CheckedChanged += new System.EventHandler(this.FilterCheckBox_CheckedChanged);

            // ── timer1 ────────────────────────────────────────────────────────
            this.timer1.Interval = 3000;
            this.timer1.Tick    += new System.EventHandler(this.timer1_Tick);

            // ── menuStrip1 ────────────────────────────────────────────────────
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.fileToolStripMenuItem,
                this.choosePathToolStripMenuItem,
                this.addExtensionToolStripMenuItem,
                this.saveToolStripMenuItem,
                this.clearToolStripMenuItem,
                this.viewToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name     = "menuStrip1";
            this.menuStrip1.Size     = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 15;

            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.StartToolStripMenuItem, this.stopToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";

            this.StartToolStripMenuItem.Name   = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.Size   = new System.Drawing.Size(98, 22);
            this.StartToolStripMenuItem.Text   = "Start";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);

            this.stopToolStripMenuItem.Name   = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size   = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text   = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);

            this.choosePathToolStripMenuItem.Name   = "choosePathToolStripMenuItem";
            this.choosePathToolStripMenuItem.Size   = new System.Drawing.Size(86, 20);
            this.choosePathToolStripMenuItem.Text   = "Choose Path";
            this.choosePathToolStripMenuItem.Click += new System.EventHandler(this.choosePathToolStripMenuItem_Click);

            this.addExtensionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.addToolStripMenuItem, this.removeToolStripMenuItem });
            this.addExtensionToolStripMenuItem.Name = "addExtensionToolStripMenuItem";
            this.addExtensionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.addExtensionToolStripMenuItem.Text = "Add Extension";

            this.addToolStripMenuItem.Name   = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size   = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text   = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);

            this.removeToolStripMenuItem.Name   = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size   = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text   = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);

            this.saveToolStripMenuItem.Name   = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size   = new System.Drawing.Size(43, 20);
            this.saveToolStripMenuItem.Text   = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);

            this.clearToolStripMenuItem.Name   = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size   = new System.Drawing.Size(46, 20);
            this.clearToolStripMenuItem.Text   = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);

            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.darkModeToolStripMenuItem });
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";

            this.darkModeToolStripMenuItem.CheckOnClick = true;
            this.darkModeToolStripMenuItem.Name         = "darkModeToolStripMenuItem";
            this.darkModeToolStripMenuItem.Size         = new System.Drawing.Size(130, 22);
            this.darkModeToolStripMenuItem.Text         = "Dark Mode";
            this.darkModeToolStripMenuItem.Click       += new System.EventHandler(this.darkModeToolStripMenuItem_Click);

            // ── label4 – Search ───────────────────────────────────────────────
            this.label4.AutoSize  = true;
            this.label4.Location  = new System.Drawing.Point(128, 30);
            this.label4.Name      = "label4";
            this.label4.TabIndex  = 26;
            this.label4.Text      = "Search:";

            // ── StatusStrip ───────────────────────────────────────────────────
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStats, this.lblSpring, this.lblStatus });
            this.statusStrip1.Location   = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name       = "statusStrip1";
            this.statusStrip1.Size       = new System.Drawing.Size(700, 22);
            this.statusStrip1.TabIndex   = 30;
            this.statusStrip1.SizingGrip = false;

            this.lblStats.Name      = "lblStats";
            this.lblStats.Text      = "  Created: 0   Deleted: 0   Changed: 0   Renamed: 0";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblSpring.Name   = "lblSpring";
            this.lblSpring.Spring = true;

            this.lblStatus.Name        = "lblStatus";
            this.lblStatus.Text        = "  Stopped  ";
            this.lblStatus.ForeColor   = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;

            // ── contextMenuStrip1 – log ───────────────────────────────────────
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.openFolderToolStripMenuItem,
                this.openFileToolStripMenuItem,
                this.deleteFileToolStripMenuItem1,
                this.copyPathToolStripMenuItem });
            this.contextMenuStrip1.Name     = "contextMenuStrip1";
            this.contextMenuStrip1.Size     = new System.Drawing.Size(140, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);

            this.openFolderToolStripMenuItem.Name   = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size   = new System.Drawing.Size(139, 22);
            this.openFolderToolStripMenuItem.Text   = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);

            this.openFileToolStripMenuItem.Name   = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size   = new System.Drawing.Size(139, 22);
            this.openFileToolStripMenuItem.Text   = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);

            this.deleteFileToolStripMenuItem1.Name   = "deleteFileToolStripMenuItem1";
            this.deleteFileToolStripMenuItem1.Size   = new System.Drawing.Size(139, 22);
            this.deleteFileToolStripMenuItem1.Text   = "Delete File";
            this.deleteFileToolStripMenuItem1.Click += new System.EventHandler(this.deleteFileToolStripMenuItem1_Click);

            this.copyPathToolStripMenuItem.Name   = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Size   = new System.Drawing.Size(139, 22);
            this.copyPathToolStripMenuItem.Text   = "Copy Path";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.copyPathToolStripMenuItem_Click);

            // ── contextMenuStrip2 – paths ─────────────────────────────────────
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.removePathToolStripMenuItem });
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(145, 26);

            this.removePathToolStripMenuItem.Name   = "removePathToolStripMenuItem";
            this.removePathToolStripMenuItem.Size   = new System.Drawing.Size(144, 22);
            this.removePathToolStripMenuItem.Text   = "Remove Path";
            this.removePathToolStripMenuItem.Click += new System.EventHandler(this.removePathToolStripMenuItem_Click);

            // ── notifyIcon1 ───────────────────────────────────────────────────
            this.notifyIcon1.Icon              = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text              = "Files Watcher";
            this.notifyIcon1.Visible           = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);

            // ── Form1 ─────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode  = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate   = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize     = new System.Drawing.Size(700, 490);
            this.Font           = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Controls.Add(this.panelSep);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkRenamed);
            this.Controls.Add(this.chkChanged);
            this.Controls.Add(this.chkDeleted);
            this.Controls.Add(this.chkCreated);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon           = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip  = this.menuStrip1;
            this.MaximizeBox    = false;
            this.MinimumSize    = new System.Drawing.Size(600, 420);
            this.Name           = "Form1";
            this.Text           = "Files Watcher";
            this.Load          += new System.EventHandler(this.Form1_Load);
            this.Move          += new System.EventHandler(this.Form1_Move);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ListView              listView1;
        private System.Windows.Forms.ColumnHeader          colType;
        private System.Windows.Forms.ColumnHeader          colPath;
        private System.Windows.Forms.ColumnHeader          colTime;
        private System.Windows.Forms.GroupBox              groupBox1;
        private System.Windows.Forms.GroupBox              groupBox2;
        private System.Windows.Forms.Panel                 panelSep;
        private System.Windows.Forms.ListBox               listBox2;
        private System.Windows.Forms.ListBox               listBox3;
        private System.Windows.Forms.TextBox               textBox2;
        private System.Windows.Forms.TextBox               textBox3;
        private System.Windows.Forms.TextBox               textBoxPath;
        private System.Windows.Forms.CheckBox              checkBox1;
        private System.Windows.Forms.CheckBox              checkBox2;
        private System.Windows.Forms.CheckBox              chkCreated;
        private System.Windows.Forms.CheckBox              chkDeleted;
        private System.Windows.Forms.CheckBox              chkChanged;
        private System.Windows.Forms.CheckBox              chkRenamed;
        private System.Windows.Forms.Timer                 timer1;
        private System.Windows.Forms.ToolTip               toolTip1;
        private System.Windows.Forms.MenuStrip             menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem     fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     StartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     choosePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     addExtensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     darkModeToolStripMenuItem;
        private System.Windows.Forms.Label                 label2;
        private System.Windows.Forms.Label                 label4;
        private System.Windows.Forms.StatusStrip           statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel  lblStats;
        private System.Windows.Forms.ToolStripStatusLabel  lblSpring;
        private System.Windows.Forms.ToolStripStatusLabel  lblStatus;
        private System.Windows.Forms.ContextMenuStrip      contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem     openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem     deleteFileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem     copyPathToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip      contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem     removePathToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon            notifyIcon1;
    }
}
