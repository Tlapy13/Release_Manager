
namespace Release_Manager
{
    partial class Form2
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.AddedChk = new System.Windows.Forms.CheckBox();
            this.ChangedChk = new System.Windows.Forms.CheckBox();
            this.DeletedChk = new System.Windows.Forms.CheckBox();
            this.UnchangedChk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(-1, 23);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(900, 242);
            this.textBox2.TabIndex = 4;
            // 
            // AddedChk
            // 
            this.AddedChk.AutoSize = true;
            this.AddedChk.Checked = true;
            this.AddedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddedChk.Location = new System.Drawing.Point(12, 4);
            this.AddedChk.Name = "AddedChk";
            this.AddedChk.Size = new System.Drawing.Size(81, 17);
            this.AddedChk.TabIndex = 5;
            this.AddedChk.Text = "Added Files";
            this.AddedChk.UseVisualStyleBackColor = true;
            this.AddedChk.CheckedChanged += new System.EventHandler(this.NewChk_CheckedChanged);
            // 
            // ChangedChk
            // 
            this.ChangedChk.AutoSize = true;
            this.ChangedChk.Checked = true;
            this.ChangedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChangedChk.Location = new System.Drawing.Point(116, 4);
            this.ChangedChk.Name = "ChangedChk";
            this.ChangedChk.Size = new System.Drawing.Size(93, 17);
            this.ChangedChk.TabIndex = 6;
            this.ChangedChk.Text = "Changed Files";
            this.ChangedChk.UseVisualStyleBackColor = true;
            this.ChangedChk.CheckedChanged += new System.EventHandler(this.ChangedChk_CheckedChanged);
            // 
            // DeletedChk
            // 
            this.DeletedChk.AutoSize = true;
            this.DeletedChk.Checked = true;
            this.DeletedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DeletedChk.Location = new System.Drawing.Point(226, 4);
            this.DeletedChk.Name = "DeletedChk";
            this.DeletedChk.Size = new System.Drawing.Size(87, 17);
            this.DeletedChk.TabIndex = 7;
            this.DeletedChk.Text = "Deleted Files";
            this.DeletedChk.UseVisualStyleBackColor = true;
            this.DeletedChk.CheckedChanged += new System.EventHandler(this.DeletedChk_CheckedChanged);
            // 
            // UnchangedChk
            // 
            this.UnchangedChk.AutoSize = true;
            this.UnchangedChk.Checked = true;
            this.UnchangedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UnchangedChk.Location = new System.Drawing.Point(329, 4);
            this.UnchangedChk.Name = "UnchangedChk";
            this.UnchangedChk.Size = new System.Drawing.Size(106, 17);
            this.UnchangedChk.TabIndex = 8;
            this.UnchangedChk.Text = "Unchanged Files";
            this.UnchangedChk.UseVisualStyleBackColor = true;
            this.UnchangedChk.CheckedChanged += new System.EventHandler(this.UnchangedChk_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 264);
            this.Controls.Add(this.UnchangedChk);
            this.Controls.Add(this.DeletedChk);
            this.Controls.Add(this.ChangedChk);
            this.Controls.Add(this.AddedChk);
            this.Controls.Add(this.textBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Deployment Report";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox AddedChk;
        private System.Windows.Forms.CheckBox ChangedChk;
        private System.Windows.Forms.CheckBox DeletedChk;
        private System.Windows.Forms.CheckBox UnchangedChk;
    }
}