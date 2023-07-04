
namespace Release_Manager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Createevidence_rep = new System.Windows.Forms.Button();
            this.txt_Apppath = new System.Windows.Forms.TextBox();
            this.btn_Saveevidence_rep = new System.Windows.Forms.Button();
            this.btn_Viewevidence_rep = new System.Windows.Forms.Button();
            this.cbo_App = new System.Windows.Forms.ComboBox();
            this.lbl_Application = new System.Windows.Forms.Label();
            this.lbl_Apppath = new System.Windows.Forms.Label();
            this.txt_Changeid = new System.Windows.Forms.TextBox();
            this.lbl_Changeid = new System.Windows.Forms.Label();
            this.lbl_Note = new System.Windows.Forms.Label();
            this.txt_Note = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Createevidence_rep
            // 
            this.btn_Createevidence_rep.Location = new System.Drawing.Point(395, 212);
            this.btn_Createevidence_rep.Name = "btn_Createevidence_rep";
            this.btn_Createevidence_rep.Size = new System.Drawing.Size(221, 23);
            this.btn_Createevidence_rep.TabIndex = 0;
            this.btn_Createevidence_rep.Text = "create evidence report";
            this.btn_Createevidence_rep.UseVisualStyleBackColor = true;
            this.btn_Createevidence_rep.Click += new System.EventHandler(this.btn_Createevidence_rep_Click);
            // 
            // txt_Apppath
            // 
            this.txt_Apppath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Apppath.Location = new System.Drawing.Point(104, 48);
            this.txt_Apppath.Name = "txt_Apppath";
            this.txt_Apppath.ReadOnly = true;
            this.txt_Apppath.Size = new System.Drawing.Size(512, 20);
            this.txt_Apppath.TabIndex = 1;
            this.txt_Apppath.TextChanged += new System.EventHandler(this.txt_Apppath_TextChanged);
            // 
            // btn_Saveevidence_rep
            // 
            this.btn_Saveevidence_rep.Location = new System.Drawing.Point(395, 270);
            this.btn_Saveevidence_rep.Name = "btn_Saveevidence_rep";
            this.btn_Saveevidence_rep.Size = new System.Drawing.Size(221, 23);
            this.btn_Saveevidence_rep.TabIndex = 2;
            this.btn_Saveevidence_rep.Text = "save evidence report";
            this.btn_Saveevidence_rep.UseVisualStyleBackColor = true;
            this.btn_Saveevidence_rep.Click += new System.EventHandler(this.btn_Saveevidence_rep_Click);
            // 
            // btn_Viewevidence_rep
            // 
            this.btn_Viewevidence_rep.Location = new System.Drawing.Point(395, 241);
            this.btn_Viewevidence_rep.Name = "btn_Viewevidence_rep";
            this.btn_Viewevidence_rep.Size = new System.Drawing.Size(221, 23);
            this.btn_Viewevidence_rep.TabIndex = 3;
            this.btn_Viewevidence_rep.Text = "view evidence report";
            this.btn_Viewevidence_rep.UseVisualStyleBackColor = true;
            this.btn_Viewevidence_rep.Click += new System.EventHandler(this.btn_Viewevidence_rep_Click);
            // 
            // cbo_App
            // 
            this.cbo_App.BackColor = System.Drawing.Color.AliceBlue;
            this.cbo_App.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_App.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbo_App.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbo_App.Location = new System.Drawing.Point(104, 11);
            this.cbo_App.Name = "cbo_App";
            this.cbo_App.Size = new System.Drawing.Size(512, 21);
            this.cbo_App.TabIndex = 4;
            this.cbo_App.SelectedIndexChanged += new System.EventHandler(this.cbo_App_SelectedIndexChanged);
            // 
            // lbl_Application
            // 
            this.lbl_Application.AutoSize = true;
            this.lbl_Application.Location = new System.Drawing.Point(12, 14);
            this.lbl_Application.Name = "lbl_Application";
            this.lbl_Application.Size = new System.Drawing.Size(62, 13);
            this.lbl_Application.TabIndex = 5;
            this.lbl_Application.Text = "Application:";
            // 
            // lbl_Apppath
            // 
            this.lbl_Apppath.AutoSize = true;
            this.lbl_Apppath.Location = new System.Drawing.Point(12, 51);
            this.lbl_Apppath.Name = "lbl_Apppath";
            this.lbl_Apppath.Size = new System.Drawing.Size(86, 13);
            this.lbl_Apppath.TabIndex = 6;
            this.lbl_Apppath.Text = "Application path:";
            // 
            // txt_Changeid
            // 
            this.txt_Changeid.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_Changeid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Changeid.Location = new System.Drawing.Point(104, 91);
            this.txt_Changeid.Name = "txt_Changeid";
            this.txt_Changeid.Size = new System.Drawing.Size(512, 20);
            this.txt_Changeid.TabIndex = 7;
            this.txt_Changeid.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lbl_Changeid
            // 
            this.lbl_Changeid.AutoSize = true;
            this.lbl_Changeid.Location = new System.Drawing.Point(12, 94);
            this.lbl_Changeid.Name = "lbl_Changeid";
            this.lbl_Changeid.Size = new System.Drawing.Size(61, 13);
            this.lbl_Changeid.TabIndex = 8;
            this.lbl_Changeid.Text = "Change ID:";
            // 
            // lbl_Note
            // 
            this.lbl_Note.AutoSize = true;
            this.lbl_Note.Location = new System.Drawing.Point(13, 131);
            this.lbl_Note.Name = "lbl_Note";
            this.lbl_Note.Size = new System.Drawing.Size(33, 13);
            this.lbl_Note.TabIndex = 9;
            this.lbl_Note.Text = "Note:";
            // 
            // txt_Note
            // 
            this.txt_Note.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_Note.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Note.Location = new System.Drawing.Point(104, 124);
            this.txt_Note.Multiline = true;
            this.txt_Note.Name = "txt_Note";
            this.txt_Note.Size = new System.Drawing.Size(512, 67);
            this.txt_Note.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(628, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(34, 367);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(21, 19);
            this.fileToolStripMenuItem.Text = "...";
            this.fileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.OpenSettings);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-1, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 12;
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.Info;
            this.statusBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBox.Location = new System.Drawing.Point(15, 299);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(601, 56);
            this.statusBox.TabIndex = 36;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(662, 367);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbo_App);
            this.Controls.Add(this.txt_Note);
            this.Controls.Add(this.lbl_Note);
            this.Controls.Add(this.lbl_Changeid);
            this.Controls.Add(this.txt_Changeid);
            this.Controls.Add(this.lbl_Apppath);
            this.Controls.Add(this.lbl_Application);
            this.Controls.Add(this.btn_Viewevidence_rep);
            this.Controls.Add(this.btn_Saveevidence_rep);
            this.Controls.Add(this.txt_Apppath);
            this.Controls.Add(this.btn_Createevidence_rep);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Release Manager tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Createevidence_rep;
        private System.Windows.Forms.TextBox txt_Apppath;
        private System.Windows.Forms.Button btn_Saveevidence_rep;
        private System.Windows.Forms.Button btn_Viewevidence_rep;
        private System.Windows.Forms.ComboBox cbo_App;
        private System.Windows.Forms.Label lbl_Application;
        private System.Windows.Forms.Label lbl_Apppath;
        private System.Windows.Forms.TextBox txt_Changeid;
        private System.Windows.Forms.Label lbl_Changeid;
        private System.Windows.Forms.Label lbl_Note;
        private System.Windows.Forms.TextBox txt_Note;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox statusBox;
    }
}

