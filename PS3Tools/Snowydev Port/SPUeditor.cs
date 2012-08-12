using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Snowydev_Port
{
    public partial class SPUeditor : Form
    {
        public SPUeditor()
        {
            InitializeComponent();
        }

        private void SPUeditor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to request a file to open.
            OpenFileDialog openFile1 = new OpenFileDialog();

            // Initialize the OpenFileDialog to look for RTF files.
            openFile1.DefaultExt = "*.spu";
            openFile1.Filter = "SPU Files|*.spu";

            // Determine whether the user selected a file from the OpenFileDialog.
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                // Load the contents of the file into the RichTextBox.
                SPU.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to request a file to open.
            OpenFileDialog openFile1 = new OpenFileDialog();

            // Initialize the OpenFileDialog to look for RTF files.
            openFile1.DefaultExt = "*.SPU";
            openFile1.Filter = "SPU Files|*.SPU";

            // Determine whether the user selected a file from the OpenFileDialog.
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile1.FileName.Length > 0)
            {
                // Load the contents of the file into the RichTextBox.
                SPU.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to request a path and file name to save to.
   SaveFileDialog saveFile1 = new SaveFileDialog();

   // Initialize the SaveFileDialog to specify the RTF extension for the file.
   saveFile1.DefaultExt = "*.SPU";
   saveFile1.Filter = "SPU Emulator Files|*.SPU";

   // Determine if the user selected a file name from the saveFileDialog.
   if(saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
      saveFile1.FileName.Length > 0) 
   {
      // Save the contents of the RichTextBox into the file.
      SPU.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
   }
}
                        }
                    }
                
                

