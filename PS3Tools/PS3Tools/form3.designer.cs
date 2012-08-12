namespace PKG_Manager
{
    partial class tut
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("About");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Contents");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Confs");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("unPKG");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("unSELF");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("enSELF");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("enPKG");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("conVERT");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Tools", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Help", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode9});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(18, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "About";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Contents";
            treeNode3.Name = "Node7";
            treeNode3.Text = "Confs";
            treeNode4.Name = "Node2";
            treeNode4.Text = "unPKG";
            treeNode5.Name = "Node3";
            treeNode5.Text = "unSELF";
            treeNode6.Name = "Node4";
            treeNode6.Text = "enSELF";
            treeNode7.Name = "Node5";
            treeNode7.Text = "enPKG";
            treeNode8.Name = "Node6";
            treeNode8.Text = "conVERT";
            treeNode9.Name = "Node9";
            treeNode9.Text = "Tools";
            treeNode10.Name = "Node0";
            treeNode10.Text = "Help";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView1.Size = new System.Drawing.Size(183, 447);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(207, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(498, 447);
            this.webBrowser1.TabIndex = 1;
            // 
            // tut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 471);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "tut";
            this.Text = "Tutorials";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}