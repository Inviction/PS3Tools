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
    public partial class LocalStorageWatcher : Form
    {
        SPU spu;
        public LocalStorageWatcher(SPU spu)
        {
            InitializeComponent();

            this.spu = spu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            LoadingScreen ls = new LoadingScreen();
            ls.progressBar1.Maximum = spu.LocalStorage.Length;
            ls.progressBar1.Value = 0;
            ls.label1.Text = "Process Local Storage Watcher...";
            ls.Show();
            string MemString = "";
            string offset = "00000000";
            for (int i = 0; i < spu.LocalStorage.Length; i++)
            {
                if ((i & 0xF) == 0 && i != 0)
                {
                    if (MemString != "")
                    {
                        listBox1.Items.Add(offset + ":" + MemString);
                    }
                    MemString = "";
                    offset = "00000000" + i.ToString("X");
                    offset = offset.Substring(offset.Length - 8);
                }
                string val = ("00" + spu.LocalStorage[i].ToString("X"));
                MemString += " 0x" + val.Substring(val.Length - 2);
                if ((i & 0xFFF) == 0)
                {
                    ls.progressBar1.Value = i;
                    ls.Refresh();
                }
            }
            if (MemString != "")
            {
                listBox1.Items.Add(offset + ":" + MemString);
            }
            ls.Hide();
            MessageBox.Show("Local Storage watcher Refreshed.");
        }

        private void LocalStorageWatcher_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
