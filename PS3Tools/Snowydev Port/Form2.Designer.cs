namespace Snowydev_Port
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.ComboBox3 = new System.Windows.Forms.ComboBox();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.ComboBox4 = new System.Windows.Forms.ComboBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.ComboBox6 = new System.Windows.Forms.ComboBox();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.RCO = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SFO = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.C2D = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.EBO = new System.Windows.Forms.ComboBox();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBox1
            // 
            this.ComboBox1.BackColor = System.Drawing.Color.Black;
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox1.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox1.ForeColor = System.Drawing.Color.LimeGreen;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "PUP unpacker",
            "Dev_Flash unpacker"});
            this.ComboBox1.Location = new System.Drawing.Point(6, 19);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(121, 22);
            this.ComboBox1.TabIndex = 4;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox1.Controls.Add(this.ComboBox1);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.ForeColor = System.Drawing.Color.White;
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(140, 52);
            this.GroupBox1.TabIndex = 10;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "PUP Tools";
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox2.Controls.Add(this.ComboBox3);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.ForeColor = System.Drawing.Color.White;
            this.GroupBox2.Location = new System.Drawing.Point(158, 12);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(140, 52);
            this.GroupBox2.TabIndex = 16;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "readself Tool";
            // 
            // ComboBox3
            // 
            this.ComboBox3.BackColor = System.Drawing.Color.Black;
            this.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox3.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox3.ForeColor = System.Drawing.Color.LimeGreen;
            this.ComboBox3.FormattingEnabled = true;
            this.ComboBox3.Items.AddRange(new object[] {
            "lv0",
            "lv1ldr",
            "lv2ldr",
            "isoldr",
            "appldr",
            "EBOOT.BIN"});
            this.ComboBox3.Location = new System.Drawing.Point(6, 19);
            this.ComboBox3.Name = "ComboBox3";
            this.ComboBox3.Size = new System.Drawing.Size(121, 22);
            this.ComboBox3.TabIndex = 4;
            this.ComboBox3.SelectedIndexChanged += new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
            // 
            // ComboBox2
            // 
            this.ComboBox2.BackColor = System.Drawing.Color.Black;
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox2.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox2.ForeColor = System.Drawing.Color.LimeGreen;
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Items.AddRange(new object[] {
            "Decrypt Core_os",
            "Extract Core_os",
            "Encrypt Core_os",
            "Core_os image info",
            "HexDump it"});
            this.ComboBox2.Location = new System.Drawing.Point(6, 19);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(121, 22);
            this.ComboBox2.TabIndex = 4;
            this.ComboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // GroupBox3
            // 
            this.GroupBox3.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox3.Controls.Add(this.ComboBox2);
            this.GroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox3.ForeColor = System.Drawing.Color.White;
            this.GroupBox3.Location = new System.Drawing.Point(12, 70);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(140, 52);
            this.GroupBox3.TabIndex = 17;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Core_os Tool";
            // 
            // ComboBox4
            // 
            this.ComboBox4.BackColor = System.Drawing.Color.Black;
            this.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox4.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox4.ForeColor = System.Drawing.Color.LimeGreen;
            this.ComboBox4.FormattingEnabled = true;
            this.ComboBox4.Items.AddRange(new object[] {
            "Fix Tar 3.72 and lower",
            "Fix Tar 3.72  debug",
            "Fix Tar 3.72 up (retail and debug)"});
            this.ComboBox4.Location = new System.Drawing.Point(6, 19);
            this.ComboBox4.Name = "ComboBox4";
            this.ComboBox4.Size = new System.Drawing.Size(121, 22);
            this.ComboBox4.TabIndex = 4;
            this.ComboBox4.SelectedIndexChanged += new System.EventHandler(this.ComboBox4_SelectedIndexChanged);
            // 
            // GroupBox4
            // 
            this.GroupBox4.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox4.Controls.Add(this.ComboBox4);
            this.GroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox4.ForeColor = System.Drawing.Color.White;
            this.GroupBox4.Location = new System.Drawing.Point(158, 70);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(140, 52);
            this.GroupBox4.TabIndex = 18;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Fix Tar Tool";
            // 
            // ComboBox6
            // 
            this.ComboBox6.BackColor = System.Drawing.Color.Black;
            this.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboBox6.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox6.ForeColor = System.Drawing.Color.LimeGreen;
            this.ComboBox6.FormattingEnabled = true;
            this.ComboBox6.Items.AddRange(new object[] {
            "lv0",
            "lv1ldr",
            "lv2ldr",
            "appldr",
            "isoldr",
            "EBOOT.BIN"});
            this.ComboBox6.Location = new System.Drawing.Point(6, 19);
            this.ComboBox6.Name = "ComboBox6";
            this.ComboBox6.Size = new System.Drawing.Size(121, 22);
            this.ComboBox6.TabIndex = 4;
            this.ComboBox6.SelectedIndexChanged += new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            // 
            // GroupBox6
            // 
            this.GroupBox6.BackColor = System.Drawing.Color.Transparent;
            this.GroupBox6.Controls.Add(this.ComboBox6);
            this.GroupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox6.ForeColor = System.Drawing.Color.White;
            this.GroupBox6.Location = new System.Drawing.Point(12, 128);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(140, 52);
            this.GroupBox6.TabIndex = 19;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "SCE Info Tool";
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(903, 140);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(10, 20);
            this.txtCommand.TabIndex = 21;
            this.txtCommand.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Black;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.LimeGreen;
            this.button2.Location = new System.Drawing.Point(126, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "EDAT TOOL";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.Black;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtResults.Location = new System.Drawing.Point(330, 0);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(662, 334);
            this.txtResults.TabIndex = 23;
            this.txtResults.Text = "Welcome to PS3Tools GUI Edition v3.3 by PSDev \r\n\r\nFollow me on twitter @RealPSDev" +
                " Thanks";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.LimeGreen;
            this.button3.Location = new System.Drawing.Point(12, 302);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "PKG decrypter";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // RCO
            // 
            this.RCO.BackColor = System.Drawing.Color.Black;
            this.RCO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RCO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RCO.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RCO.ForeColor = System.Drawing.Color.LimeGreen;
            this.RCO.FormattingEnabled = true;
            this.RCO.Items.AddRange(new object[] {
            "Dump category_friend",
            "Dump category_sysconf",
            "Dump category_game",
            "Dump category_music",
            "Dump category_network",
            "Dump category_photo",
            "Dump category_psn",
            "Dump category_tv",
            "Dump category_user",
            "Dump category_video"});
            this.RCO.Location = new System.Drawing.Point(6, 19);
            this.RCO.Name = "RCO";
            this.RCO.Size = new System.Drawing.Size(171, 22);
            this.RCO.TabIndex = 4;
            this.RCO.SelectedIndexChanged += new System.EventHandler(this.RCO_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.RCO);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(56, 244);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 52);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "RCO tool";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter_1);
            // 
            // SFO
            // 
            this.SFO.BackColor = System.Drawing.Color.Black;
            this.SFO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SFO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SFO.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFO.ForeColor = System.Drawing.Color.LimeGreen;
            this.SFO.FormattingEnabled = true;
            this.SFO.Items.AddRange(new object[] {
            "SFOreader"});
            this.SFO.Location = new System.Drawing.Point(6, 19);
            this.SFO.Name = "SFO";
            this.SFO.Size = new System.Drawing.Size(121, 22);
            this.SFO.TabIndex = 4;
            this.SFO.SelectedIndexChanged += new System.EventHandler(this.SFO_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.SFO);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.White;
            this.groupBox7.Location = new System.Drawing.Point(158, 128);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(140, 52);
            this.groupBox7.TabIndex = 26;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SFO tool";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(-6, 340);
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(998, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 27;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.Transparent;
            this.groupBox8.Controls.Add(this.C2D);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.White;
            this.groupBox8.Location = new System.Drawing.Point(158, 186);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(140, 52);
            this.groupBox8.TabIndex = 28;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "C2D tool";
            // 
            // C2D
            // 
            this.C2D.BackColor = System.Drawing.Color.Black;
            this.C2D.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C2D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.C2D.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.C2D.ForeColor = System.Drawing.Color.LimeGreen;
            this.C2D.FormattingEnabled = true;
            this.C2D.Items.AddRange(new object[] {
            "CEX 2 DEX"});
            this.C2D.Location = new System.Drawing.Point(6, 19);
            this.C2D.Name = "C2D";
            this.C2D.Size = new System.Drawing.Size(121, 22);
            this.C2D.TabIndex = 4;
            this.C2D.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged_2);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Black;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.LimeGreen;
            this.button4.Location = new System.Drawing.Point(225, 302);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 23);
            this.button4.TabIndex = 30;
            this.button4.Text = "SPU Emu";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.Transparent;
            this.groupBox9.Controls.Add(this.EBO);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.White;
            this.groupBox9.Location = new System.Drawing.Point(12, 186);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(140, 52);
            this.groupBox9.TabIndex = 32;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "scetool";
            // 
            // EBO
            // 
            this.EBO.BackColor = System.Drawing.Color.Black;
            this.EBO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EBO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EBO.Font = new System.Drawing.Font("Quartz MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EBO.ForeColor = System.Drawing.Color.LimeGreen;
            this.EBO.FormattingEnabled = true;
            this.EBO.Items.AddRange(new object[] {
            "Show keys",
            "Decrypt EBOOT",
            "Decrypt + resign"});
            this.EBO.Location = new System.Drawing.Point(6, 19);
            this.EBO.Name = "EBO";
            this.EBO.Size = new System.Drawing.Size(121, 22);
            this.EBO.TabIndex = 4;
            this.EBO.SelectedIndexChanged += new System.EventHandler(this.EBO_SelectedIndexChanged_3);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(992, 362);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.GroupBox6);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "PS3Tools GUI Edition By PsDev";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.ComboBox ComboBox3;
        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.ComboBox ComboBox4;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.ComboBox ComboBox6;
        internal System.Windows.Forms.GroupBox GroupBox6;
        internal System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button button3;
        internal System.Windows.Forms.ComboBox RCO;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.ComboBox SFO;
        internal System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ProgressBar progressBar1;
        internal System.Windows.Forms.GroupBox groupBox8;
        internal System.Windows.Forms.ComboBox C2D;
        private System.Windows.Forms.Button button4;
        internal System.Windows.Forms.GroupBox groupBox9;
        internal System.Windows.Forms.ComboBox EBO;

    }
}