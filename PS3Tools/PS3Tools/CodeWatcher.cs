using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPU_simulation
{
    public partial class CodeWatcher : Form
    {
        SPU spu;
        LoadingScreen ls;
        formspu CodeListing;
        public CodeWatcher(SPU spu, formspu CodeListing)
        {
            this.spu = spu;
            this.CodeListing = CodeListing;
            ls = new LoadingScreen();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ls.label1.Text = "Building Code Section of Local Storage...";
            ls.progressBar1.Maximum = spu.LocalStorageCommands.Length;
            ls.progressBar1.Value = 0;
            ls.Show();
            this.listBox1.Items.Clear();
            for (int i = 0; i < spu.LocalStorageCommands.Length; i++)
            {
                int offset = i << 2;
                bool breakPoint = SPUBreakpoints.Instance.isBreakPoint(offset);

                string offsetString = ("00000000" + offset.ToString("X"));
                offsetString = offsetString.Substring(offsetString.Length - 8);
                this.listBox1.Items.Add(((breakPoint) ? "[B]0x" : "[-]0x") + offsetString + ": " + spu.LocalStorageCommands[i].fullCommand + "\t\t" + spu.LocalStorageCommands[i].functionName);
                if ((i & 0xFF) == 0)
                {
                    ls.progressBar1.Value = i;
                    ls.Refresh();
                }
            }
            ls.Hide();
            MessageBox.Show("CodeWatcher refreshed");
        }

        private void CodeWatcher_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                int offset = (listBox1.SelectedIndex << 2);
                char[] splitter = new char[1];
                splitter[0] = ']';
                string val = ((string)listBox1.SelectedItem).Split(splitter)[1];
                if (SPUBreakpoints.Instance.isBreakPoint(offset))
                {
                    SPUBreakpoints.Instance.CodeBreakPoints.Remove(offset);
                    listBox1.Items[listBox1.SelectedIndex] = "[-]" + val;
                }
                else
                {
                    SPUBreakpoints.Instance.CodeBreakPoints.Add(offset);
                    listBox1.Items[listBox1.SelectedIndex] = "[B]" + val;
                }
                CodeListing.updateUI();
            }
//            button1_Click(sender, e);
        }
    }
}
