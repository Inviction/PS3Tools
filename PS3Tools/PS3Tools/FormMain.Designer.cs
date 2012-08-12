namespace PKG_Manager
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.DoButton = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileDirBrowser = new FormControls.FileDirBrowser();
            this.quotes = new System.Windows.Forms.Label();
            this.slash = new System.Windows.Forms.Label();
            this.enter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.Highlight;
            this.progressBar.Location = new System.Drawing.Point(-1, 126);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(266, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 23;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // DoButton
            // 
            this.DoButton.BackColor = System.Drawing.Color.Black;
            this.DoButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoButton.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoButton.ForeColor = System.Drawing.Color.LimeGreen;
            this.DoButton.Location = new System.Drawing.Point(12, 97);
            this.DoButton.Name = "DoButton";
            this.DoButton.Size = new System.Drawing.Size(243, 23);
            this.DoButton.TabIndex = 26;
            this.DoButton.Text = "Decrypt PKG file";
            this.DoButton.UseVisualStyleBackColor = false;
            this.DoButton.Click += new System.EventHandler(this.DoButton_Click_1);
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.Black;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtResults.Location = new System.Drawing.Point(262, 1);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(290, 144);
            this.txtResults.TabIndex = 27;
            this.txtResults.TextChanged += new System.EventHandler(this.txtResults_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Quartz MS", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Location = new System.Drawing.Point(73, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 35);
            this.label1.TabIndex = 28;
            this.label1.Text = "PsDev";
            // 
            // fileDirBrowser
            // 
            this.fileDirBrowser.BackColor = System.Drawing.Color.Black;
            this.fileDirBrowser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fileDirBrowser.BrowseBackColor = System.Drawing.Color.Empty;
            this.fileDirBrowser.BrowseDialogTitle = "";
            this.fileDirBrowser.BrowseFor = FormControls.FileDirBrowser.BrowseType.FileOpen;
            this.fileDirBrowser.BrowseForeColor = System.Drawing.Color.Empty;
            this.fileDirBrowser.Cursor = System.Windows.Forms.Cursors.Default;
            this.fileDirBrowser.Description = "PS3Tools GUI Edition";
            this.fileDirBrowser.DescriptionColor = System.Drawing.Color.Empty;
            this.fileDirBrowser.FileDescription = "All files";
            this.fileDirBrowser.FileExtension = "*PKG";
            this.fileDirBrowser.FileName = "*";
            this.fileDirBrowser.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileDirBrowser.ForeColor = System.Drawing.Color.LimeGreen;
            this.fileDirBrowser.InitDirUseLastDir = true;
            this.fileDirBrowser.InitialDirectory = System.Environment.SpecialFolder.Desktop;
            this.fileDirBrowser.Location = new System.Drawing.Point(12, 47);
            this.fileDirBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fileDirBrowser.Name = "fileDirBrowser";
            this.fileDirBrowser.PathBackColor = System.Drawing.Color.Empty;
            this.fileDirBrowser.PathFileDir = "browse or drag and drop";
            this.fileDirBrowser.PathForeColor = System.Drawing.Color.Empty;
            this.fileDirBrowser.Size = new System.Drawing.Size(243, 49);
            this.fileDirBrowser.TabIndex = 24;
            this.fileDirBrowser.Load += new System.EventHandler(this.fileDirBrowser_Load_1);
            // 
            // quotes
            // 
            this.quotes.AutoSize = true;
            this.quotes.Location = new System.Drawing.Point(270, 65);
            this.quotes.Name = "quotes";
            this.quotes.Size = new System.Drawing.Size(12, 13);
            this.quotes.TabIndex = 117;
            this.quotes.Text = "\"";
            this.quotes.Visible = false;
            // 
            // slash
            // 
            this.slash.AutoSize = true;
            this.slash.Location = new System.Drawing.Point(278, 73);
            this.slash.Name = "slash";
            this.slash.Size = new System.Drawing.Size(12, 13);
            this.slash.TabIndex = 118;
            this.slash.Text = "\\";
            this.slash.Visible = false;
            // 
            // enter
            // 
            this.enter.AutoSize = true;
            this.enter.Location = new System.Drawing.Point(273, 65);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(7, 13);
            this.enter.TabIndex = 119;
            this.enter.Text = "\r\n";
            this.enter.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 120;
            this.label2.Text = "label2";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(553, 145);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.enter);
            this.Controls.Add(this.slash);
            this.Controls.Add(this.quotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.DoButton);
            this.Controls.Add(this.fileDirBrowser);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "PS3Tools GUI Edition";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private FormControls.FileDirBrowser fileDirBrowser;
        private System.Windows.Forms.Button DoButton;
        internal System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label quotes;
        private System.Windows.Forms.Label slash;
        private System.Windows.Forms.Label enter;
        private System.Windows.Forms.Label label2;
    }
}