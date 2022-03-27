namespace DuplicateFilesInFolders
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
            this.searchButton = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.richTextBoxFolderPaths = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cancelAsyncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(12, 112);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(222, 62);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(396, 137);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(55, 13);
            this.resultLabel.TabIndex = 2;
            this.resultLabel.Tag = "";
            this.resultLabel.Text = "                ";
            // 
            // richTextBoxFolderPaths
            // 
            this.richTextBoxFolderPaths.Location = new System.Drawing.Point(13, 13);
            this.richTextBoxFolderPaths.Name = "richTextBoxFolderPaths";
            this.richTextBoxFolderPaths.Size = new System.Drawing.Size(833, 93);
            this.richTextBoxFolderPaths.TabIndex = 3;
            this.richTextBoxFolderPaths.Text = "";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // cancelAsyncButton
            // 
            this.cancelAsyncButton.Enabled = false;
            this.cancelAsyncButton.Location = new System.Drawing.Point(240, 112);
            this.cancelAsyncButton.Name = "cancelAsyncButton";
            this.cancelAsyncButton.Size = new System.Drawing.Size(150, 62);
            this.cancelAsyncButton.TabIndex = 4;
            this.cancelAsyncButton.Text = "Cancel";
            this.cancelAsyncButton.UseVisualStyleBackColor = true;
            this.cancelAsyncButton.Click += new System.EventHandler(this.cancelAsyncButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 186);
            this.Controls.Add(this.cancelAsyncButton);
            this.Controls.Add(this.richTextBoxFolderPaths);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.searchButton);
            this.Name = "Form1";
            this.Text = "Duplicate Files in Folders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.RichTextBox richTextBoxFolderPaths;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button cancelAsyncButton;
    }
}

