using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PKG_Manager
{
    public partial class tut : Form
    {
        public tut()
        {
            InitializeComponent();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String curdir = Directory.GetCurrentDirectory();
            TreeNode node = treeView1.SelectedNode;
            webBrowser1.AllowNavigation = true;
            if (node.Text == "About")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/about.html"));
            }
            else if (node.Text == "Contents")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/contents.html"));
            }
            else if (node.Text == "Confs")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/conf.html"));
            }
            else if (node.Text == "unPKG")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/unpkg.html"));
            }
            else if (node.Text == "unSELF")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/unself.html"));
            }
            else if (node.Text == "enSELF")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/enself.html"));
            }
            else if (node.Text == "enPKG")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/enpkg.html"));
            }
            else if (node.Text == "conVERT")
            {
                webBrowser1.Navigate(new Uri(curdir + @"/tuts/convert.html"));
            }
        }
    }
}
