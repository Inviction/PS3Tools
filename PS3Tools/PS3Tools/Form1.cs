using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Snowydev_Port
{
    public partial class Form1 : Form
    {
     
        string[] filesToOpen = new string[2];
           public Form1()
        {
            InitializeComponent();
        }

        private static byte[] RIFKEY = {(byte) 0xDA,(byte) 0x7D,(byte) 0x4B,(byte) 0x5E,
            (byte) 0x49,(byte) 0x9A,(byte) 0x4F,(byte) 0x53,(byte) 0xB1,(byte) 0xC1,
            (byte) 0xA1,(byte) 0x4A,(byte) 0x74,(byte) 0x84,(byte) 0x44,(byte) 0x3B};

        private static byte[] getKey(String rifIn, String actIn, String idps)
        {
            if (rifIn == null || actIn == null) return null;
            byte[] result = null;
            FileStream rifFile = File.Open(rifIn, FileMode.Open);
        

            byte[] rif0x40 = new byte[0x10];
            byte[] rif0x50 = new byte[0x10];
            byte[] encrif0x40 = new byte[0x10];
            byte[] encrif0x50 = new byte[0x10];
            rifFile.Seek(0x40, SeekOrigin.Begin);
            rifFile.Read(encrif0x40, 0, encrif0x40.Length);
            rifFile.Read(encrif0x50, 0, encrif0x50.Length);
            rifFile.Close();
            ToolsImpl.aesecbDecrypt(RIFKEY, encrif0x40, 0x00, rif0x40, 0, 0x10);  //Decryp firzt 0x10 bytes of RIF
            //System.out.println("rif0x40= " + ConversionUtils.getHexString(rif0x40));
            long index = ConversionUtils.be32(rif0x40, 0xC); //
            if (index < 0x80)
            {
                byte[] actDat = decryptACTDAT(actIn, idps);
                byte[] datKey = new byte[0x10];
                result = new byte[0x10];
                ConversionUtils.arraycopy(actDat, (int)index * 16, datKey, 0, 0x10);
                ToolsImpl.aesecbDecrypt(datKey, encrif0x50, 0, result, 0, 0x10);
            }
            return result;
        }

        private static byte[] decryptACTDAT(String actIn, String IDPSFile)
        {
            FileStream actFile = File.Open(actIn, FileMode.Open);
            byte[] actdat = new byte[0x800];
            byte[] result = new byte[actdat.Length];
            actFile.Seek(0x10, SeekOrigin.Begin);
            actFile.Read(actdat, 0, actdat.Length);
            actFile.Close();
            byte[] key = getPerConsoleKey(IDPSFile);
            ToolsImpl.aesecbDecrypt(key, actdat, 0, result, 0, actdat.Length);
            return result;
        }
        static byte[] ACTDAT_KEY = { (byte)0x5E, (byte)0x06, (byte)0xE0, (byte)0x4F, (byte)0xD9, (byte)0x4A, (byte)0x71, (byte)0xBF, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x01 };

        private static byte[] getPerConsoleKey(String IDPSFile)
        {
            FileStream raf = File.Open(IDPSFile, FileMode.Open);
            byte[] idps = new byte[0x10];
            raf.Read(idps, 0, idps.Length);
            raf.Close();
            byte[] result = new byte[0x10];
            ToolsImpl.aesecbEncrypt(idps, ACTDAT_KEY, 0, result, 0, ACTDAT_KEY.Length);
            return result;
        }

        private void setEnableAllButtons(bool enabled)
        {
            this.button1.Enabled = enabled;
            this.button2.Enabled = enabled;
            this.button3.Enabled = enabled;
            this.button4.Enabled = enabled;
            this.button5.Enabled = enabled;
            this.button6.Enabled = enabled;
            this.button7.Enabled = enabled;
            this.button8.Enabled = enabled;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            setEnableAllButtons(false);
            String inFile = textBox5.Text;
            String outFile = textBox6.Text;
            byte[] devKLic = ConversionUtils.getByteArray("52c0b5ca76d6134bb45fc66ca637f2c1");
            //            byte[] keyFromRif = getKey("ff8.rif", "act.dat", "idps");
            byte[] keyFromRif = getKey(textBox2.Text, textBox3.Text, textBox4.Text);

            EDAT instance = new EDAT();
            instance.decryptFile(inFile, outFile, devKLic, keyFromRif);
            setEnableAllButtons(true);
            Debugbox.Text += "RIF KEY = (byte)0xDA, (byte)0x7D, (byte)0x4B, (byte)0x5E, (byte)0x49, (byte)0x9A, (byte)0x4F, (byte)0x53, (byte)0xB1, (byte)0xC1, (byte)0xA1, (byte)0x4A, (byte)0x74, (byte)0x84, (byte)0x44, (byte)0x3B" + Environment.NewLine + "/n/r";
            Debugbox.Text += "ACTDAT KEY =(byte)0x5E, (byte)0x06, (byte)0xE0, (byte)0x4F, (byte)0xD9, (byte)0x4A, (byte)0x71, (byte)0xBF, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x00, (byte)0x01" + Environment.NewLine;
            Debugbox.Text += "Byte array = 52c0b5ca76d6134bb45fc66ca637f2c1" + Environment.NewLine;
            SystemSounds.Beep.Play();
            MessageBox.Show("Succcess check selected save folder");

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setEnableAllButtons(false);
            String inFile = textBox5.Text;
            String outFile = textBox6.Text;
            byte[] devKLic = ConversionUtils.getByteArray("52c0b5ca76d6134bb45fc66ca637f2c1");
            byte[] keyFromRif = (new RAP()).getKey(this.textBox1.Text);

            EDAT instance = new EDAT();
            instance.decryptFile(inFile, outFile, devKLic, keyFromRif);
            setEnableAllButtons(true);
        }
        
        private void load(string file)
        {
            if (System.IO.Path.GetExtension(file).Equals(".EDAT"))
            {
                filesToOpen = System.IO.File.ReadAllLines(file);
                textBox5.Text = filesToOpen[1];

            }
            else
                Debugbox.Text += "Unrecognized File type?";
            SystemSounds.Beep.Play();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Reset();
            DialogResult openFile = this.openFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox1.Text = this.openFileDialog1.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Reset();
            DialogResult openFile = this.openFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox2.Text = this.openFileDialog1.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Reset();
            DialogResult openFile = this.openFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox3.Text = this.openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Reset();
            DialogResult openFile = this.openFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox4.Text = this.openFileDialog1.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Reset();
            DialogResult openFile = this.openFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox5.Text = this.openFileDialog1.FileName;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Reset();
            DialogResult openFile = this.saveFileDialog1.ShowDialog();
            if (openFile == DialogResult.OK)
                this.textBox6.Text = this.saveFileDialog1.FileName;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public byte[] calculatedKey { get; set; }

        public byte[] rif0x40 { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
