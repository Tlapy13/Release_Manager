namespace Release_Manager
{
    partial class Settings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.folderNameTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderPathTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.logPathLabel = new System.Windows.Forms.Label();
            this.logPathTextBox = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.solutionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solutionPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solutionsConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.logPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BrowseLogFolder = new System.Windows.Forms.Button();
            this.browseConfigPath = new System.Windows.Forms.Button();
            this.configPathLabel = new System.Windows.Forms.Label();
            this.configPathTextbox = new System.Windows.Forms.TextBox();
            this.browseSlnButton = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.Logs = new System.Windows.Forms.Button();
            this.configPathDialog = new System.Windows.Forms.OpenFileDialog();
            this.solutionPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BrowseXMLsFolder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.xmlPathTextBox = new System.Windows.Forms.TextBox();
            this.xmlPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.xmlPathDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.solutionsConfigBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // folderNameTextbox
            // 
            this.folderNameTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.folderNameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.folderNameTextbox.Location = new System.Drawing.Point(95, 40);
            this.folderNameTextbox.Name = "folderNameTextbox";
            this.folderNameTextbox.Size = new System.Drawing.Size(455, 20);
            this.folderNameTextbox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Folder name:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Save changes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SaveChanges);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(441, 388);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(109, 23);
            this.cancel.TabIndex = 11;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.Cancel);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(556, 388);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Reset settings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ResetSettings);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Folder path:";
            // 
            // folderPathTextbox
            // 
            this.folderPathTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.folderPathTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.folderPathTextbox.Location = new System.Drawing.Point(95, 66);
            this.folderPathTextbox.Name = "folderPathTextbox";
            this.folderPathTextbox.Size = new System.Drawing.Size(455, 20);
            this.folderPathTextbox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Solution list configuration";
            // 
            // logPathLabel
            // 
            this.logPathLabel.AutoSize = true;
            this.logPathLabel.Location = new System.Drawing.Point(12, 338);
            this.logPathLabel.Name = "logPathLabel";
            this.logPathLabel.Size = new System.Drawing.Size(52, 13);
            this.logPathLabel.TabIndex = 22;
            this.logPathLabel.Text = "Log path:";
            this.logPathLabel.MouseHover += new System.EventHandler(this.ShowTooltip);
            // 
            // logPathTextBox
            // 
            this.logPathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logPathTextBox.Location = new System.Drawing.Point(95, 336);
            this.logPathTextBox.Name = "logPathTextBox";
            this.logPathTextBox.Size = new System.Drawing.Size(455, 20);
            this.logPathTextBox.TabIndex = 21;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(556, 238);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 23);
            this.button5.TabIndex = 26;
            this.button5.Text = "Remove solution";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.RemoveSolution);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.solutionNameDataGridViewTextBoxColumn,
            this.solutionPathDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.solutionsConfigBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(95, 92);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(570, 140);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectDataGridViewRow);
            // 
            // solutionNameDataGridViewTextBoxColumn
            // 
            this.solutionNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.solutionNameDataGridViewTextBoxColumn.DataPropertyName = "SolutionName";
            this.solutionNameDataGridViewTextBoxColumn.HeaderText = "Folder name";
            this.solutionNameDataGridViewTextBoxColumn.Name = "solutionNameDataGridViewTextBoxColumn";
            this.solutionNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // solutionPathDataGridViewTextBoxColumn
            // 
            this.solutionPathDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.solutionPathDataGridViewTextBoxColumn.DataPropertyName = "SolutionPath";
            this.solutionPathDataGridViewTextBoxColumn.HeaderText = "Folder path";
            this.solutionPathDataGridViewTextBoxColumn.Name = "solutionPathDataGridViewTextBoxColumn";
            // 
            // solutionsConfigBindingSource
            // 
            this.solutionsConfigBindingSource.DataSource = typeof(Release_Manager.SolutionsConfig);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "General";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(441, 238);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 29;
            this.button4.Text = "Add solution";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.AddSolution);
            // 
            // BrowseLogFolder
            // 
            this.BrowseLogFolder.Location = new System.Drawing.Point(556, 336);
            this.BrowseLogFolder.Name = "BrowseLogFolder";
            this.BrowseLogFolder.Size = new System.Drawing.Size(109, 20);
            this.BrowseLogFolder.TabIndex = 30;
            this.BrowseLogFolder.Text = "Browse";
            this.BrowseLogFolder.UseVisualStyleBackColor = true;
            this.BrowseLogFolder.Click += new System.EventHandler(this.SelectLogPath);
            // 
            // browseConfigPath
            // 
            this.browseConfigPath.Location = new System.Drawing.Point(556, 362);
            this.browseConfigPath.Name = "browseConfigPath";
            this.browseConfigPath.Size = new System.Drawing.Size(109, 20);
            this.browseConfigPath.TabIndex = 33;
            this.browseConfigPath.Text = "Browse";
            this.browseConfigPath.UseVisualStyleBackColor = true;
            this.browseConfigPath.Click += new System.EventHandler(this.SelectConfigPath);
            // 
            // configPathLabel
            // 
            this.configPathLabel.AutoSize = true;
            this.configPathLabel.Location = new System.Drawing.Point(12, 364);
            this.configPathLabel.Name = "configPathLabel";
            this.configPathLabel.Size = new System.Drawing.Size(64, 13);
            this.configPathLabel.TabIndex = 32;
            this.configPathLabel.Text = "Config path:";
            // 
            // configPathTextbox
            // 
            this.configPathTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.configPathTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configPathTextbox.Location = new System.Drawing.Point(95, 362);
            this.configPathTextbox.Name = "configPathTextbox";
            this.configPathTextbox.Size = new System.Drawing.Size(455, 20);
            this.configPathTextbox.TabIndex = 31;
            // 
            // browseSlnButton
            // 
            this.browseSlnButton.Location = new System.Drawing.Point(556, 40);
            this.browseSlnButton.Name = "browseSlnButton";
            this.browseSlnButton.Size = new System.Drawing.Size(109, 46);
            this.browseSlnButton.TabIndex = 34;
            this.browseSlnButton.Text = "Browse";
            this.browseSlnButton.UseVisualStyleBackColor = true;
            this.browseSlnButton.Click += new System.EventHandler(this.SelectSolutionFile);
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.Info;
            this.statusBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBox.Location = new System.Drawing.Point(16, 419);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(649, 13);
            this.statusBox.TabIndex = 35;
            // 
            // Logs
            // 
            this.Logs.Location = new System.Drawing.Point(95, 388);
            this.Logs.Name = "Logs";
            this.Logs.Size = new System.Drawing.Size(109, 23);
            this.Logs.TabIndex = 36;
            this.Logs.Text = "Logs";
            this.Logs.UseVisualStyleBackColor = true;
            this.Logs.Click += new System.EventHandler(this.OpenLogsFolder);
            // 
            // BrowseXMLsFolder
            // 
            this.BrowseXMLsFolder.Location = new System.Drawing.Point(556, 310);
            this.BrowseXMLsFolder.Name = "BrowseXMLsFolder";
            this.BrowseXMLsFolder.Size = new System.Drawing.Size(109, 20);
            this.BrowseXMLsFolder.TabIndex = 39;
            this.BrowseXMLsFolder.Text = "Browse";
            this.BrowseXMLsFolder.UseVisualStyleBackColor = true;
            this.BrowseXMLsFolder.Click += new System.EventHandler(this.SelectXMLPath);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 312);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "XML path:";
            // 
            // xmlPathTextBox
            // 
            this.xmlPathTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.xmlPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xmlPathTextBox.Location = new System.Drawing.Point(95, 310);
            this.xmlPathTextBox.Name = "xmlPathTextBox";
            this.xmlPathTextBox.Size = new System.Drawing.Size(455, 20);
            this.xmlPathTextBox.TabIndex = 37;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(677, 441);
            this.Controls.Add(this.BrowseXMLsFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.xmlPathTextBox);
            this.Controls.Add(this.Logs);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.browseSlnButton);
            this.Controls.Add(this.browseConfigPath);
            this.Controls.Add(this.configPathLabel);
            this.Controls.Add(this.configPathTextbox);
            this.Controls.Add(this.BrowseLogFolder);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.logPathLabel);
            this.Controls.Add(this.logPathTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPathTextbox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.folderNameTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Settings";
            this.Text = "Release Manager Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsClosed);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ClearStatusBoxTextWhenClicked);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.solutionsConfigBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderNameTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox folderPathTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label logPathLabel;
        private System.Windows.Forms.TextBox logPathTextBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource solutionsConfigBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.FolderBrowserDialog logPathBrowserDialog;
        private System.Windows.Forms.Button BrowseLogFolder;
        private System.Windows.Forms.Button browseConfigPath;
        private System.Windows.Forms.Label configPathLabel;
        private System.Windows.Forms.TextBox configPathTextbox;
        private System.Windows.Forms.Button browseSlnButton;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.Button Logs;
        private System.Windows.Forms.OpenFileDialog configPathDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn solutionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn solutionPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.FolderBrowserDialog solutionPathBrowserDialog;
        private System.Windows.Forms.Button BrowseXMLsFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xmlPathTextBox;
        private System.Windows.Forms.FolderBrowserDialog xmlPathBrowserDialog;
        private System.Windows.Forms.OpenFileDialog xmlPathDialog;
    }
}