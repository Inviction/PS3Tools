using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Snowydev_Port;

namespace SPU_simulation
{
    public partial class formspu : Form
    {
        public formspu()
        {
            InitializeComponent();
            ls = new LoadingScreen();
            spu = new SPU();
        }

        bool run;
        SPU spu;
        LoadingScreen ls;

        public void updateUI()
        {
            string ip = "00000000" + spu.IP.ToString("X");
            toolStripStatusLabel1.Text = "IP: 0x" + ip.Substring(ip.Length - 8);
            int i = (int) (spu.IP >> 2);
            int len = i + 100;
            listbox1.Items.Clear();
            for (; i < len; i++)
            {
                int offset = i << 2;
                bool breakPoint = SPUBreakpoints.Instance.isBreakPoint(offset);

                string offsetString = ("00000000" + offset.ToString("X"));
                offsetString = offsetString.Substring(offsetString.Length - 8);
                this.listbox1.Items.Add(((breakPoint) ? "[B]0x" : "[-]0x") + offsetString + ": " + spu.LocalStorageCommands[i].fullCommand + "\t\t" + spu.LocalStorageCommands[i].functionName);
            }

            listbox2.Items.Clear();
            for (int r = 0; r < 128; r++)
            {
                for (int rb = 0; rb < 4; rb++)
                {
                    string registerString = "00000000" + spu.Register[r, rb].ToString("X");
                    this.listbox2.Items.Add(SPUOpcodeTreeNodeData.getRegisterString(r) + "[" + rb + "]: 0x" + registerString.Substring(registerString.Length - 8));
                }
            }
            Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            spu.LocalStorageCommands[spu.IP >> 2].execute(spu);
            spu.IP += 4;
            updateUI();
        }

        private void runHandler()
        {
            run = true;
            while (run)
            {
                if (spu.LocalStorageCommands[spu.IP >> 2].execute(spu) != 0)
                    return;
                spu.IP += 4;
                if (SPUBreakpoints.Instance.isOneTimeBreakPoint((int)spu.IP, true) || SPUBreakpoints.Instance.isBreakPoint((int)spu.IP))
                {
                    return;
                }
            }
            spu.IP += 4;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(runHandler));
            thread.Start();
            toolStripButton1.Enabled = toolStripButton2.Enabled = toolStripButton3.Enabled = toolStripButton4.Enabled = toolStripButton5.Enabled
                = toolStripButton6.Enabled = toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = true;
            while (thread.IsAlive)
            {
                updateUI();
                Application.DoEvents();
            }
            toolStripButton1.Enabled = toolStripButton2.Enabled = toolStripButton3.Enabled = toolStripButton4.Enabled = toolStripButton5.Enabled
                = toolStripButton6.Enabled = toolStripButton7.Enabled = true;
            toolStripButton8.Enabled = false;
            updateUI();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           
            // Initialize the OpenFileDialog to look for SPU files.
            openFileDialog1.Reset(); //resets it
            openFileDialog1.DefaultExt = "*.SPU";
            openFileDialog1.Filter = "SPU Files|*.SPU";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//opens the dialog with OK button
            {
                spu = new SPU();
                string scriptFile = openFileDialog1.FileName;//open scriptfile with name
                FileLoader.LoadScriptFile(scriptFile, spu);//loads it
                spu.buildLocalStorageCommands();//the buildlocal storage function
                findKnownFunctions();//looks for known functions in folder
                toolStripButton1.Enabled = toolStripButton2.Enabled = toolStripButton4.Enabled = toolStripButton5.Enabled
                    = toolStripButton6.Enabled = toolStripButton7.Enabled = true; //enables other toolstrip items that were disable
                toolStripButton8.Enabled = false;
                updateUI();
                Emulating.Text = "Emulating";
                {
                    if (Emulating.Text == "Emulating...");
                    {
                        MessageBox.Show("EMULATING", "EMULATING",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            LocalStorageWatcher mw = new LocalStorageWatcher(spu);
            mw.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            CodeWatcher cw = new CodeWatcher(spu, this);
            cw.Show();
        }

        private void findKnownFunctions()
        {
            string[] files = Directory.GetFiles("./KnownFunctions/");
            ls.progressBar1.Maximum = spu.LocalStorageCommands.Length;
            ls.progressBar1.Value = 0;
            ls.Show();
            foreach (string file in files)
            {
                ls.label1.Text = "Searching '" + file + "'...";
                SPUKnownFunction func = new SPUKnownFunction(file);
                func.findYourself(spu, ls);
                ls.Refresh();
            }
            ls.Hide();
            updateUI();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SPUBreakpoints.Instance.OneTimeCodeBreakPoints.Add((int)(spu.IP + 4));
            toolStripButton2_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (listbox1.SelectedIndex >= 0)
            {
                int offset = (listbox1.SelectedIndex << 2);
                offset += (int) spu.IP;
                char[] splitter = new char[1];
                splitter[0] = ']';
                string val = ((string)listbox1.SelectedItem).Split(splitter)[1];
                if (SPUBreakpoints.Instance.isBreakPoint(offset))
                {
                    SPUBreakpoints.Instance.CodeBreakPoints.Remove(offset);
                    listbox1.Items[listbox1.SelectedIndex] = "[-]" + val;
                }
                else
                {
                    SPUBreakpoints.Instance.CodeBreakPoints.Add(offset);
                    listbox1.Items[listbox1.SelectedIndex] = "[B]" + val;
                }
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            run = false;
        }

        private void formspu_Load(object sender, EventArgs e)
        {

        }

        private void listbox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openfile_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void listbox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
          
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            SPUeditor mw = new SPUeditor();
            mw.Show();
        }
    }
}
