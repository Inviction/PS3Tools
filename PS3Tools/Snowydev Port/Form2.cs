using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
//using Functions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using PKG_Manager;
using SPU_simulation;
using System.Media;

namespace Snowydev_Port
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txtResults_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedItem == "PUP unpacker")
            {
                txtCommand.Text = "pupunpack PS3UPDAT.PUP PS3UPDAT";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd PUP Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("PUP UNPACK operation finished.");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox1.SelectedItem == "Dev_Flash unpacker")
            {
                txtCommand.Text = "Dev_flash PS3UPDAT.PUP";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd PUP Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Dev_flash operation Finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox2.SelectedItem == "Decrypt Core_os")
            {
                txtCommand.Text = "fwpkg d CORE_OS_PACKAGE.pkg decrypted_core_os_package";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Core_os Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Core_os decryption operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox2.SelectedItem == "Extract Core_os")
            {
                txtCommand.Text = "discore decrypted_core_os_package";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = false;
                // we create the cmd window here because with out it the program will not respond and force close itself. // wierd error? 
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Core_os Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Core_os Extraction operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox2.SelectedItem == "Encrypt Core_os")
            {
                txtCommand.Text = "fwpkg e decrypted_core_os_package encrypted_core_os_package";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Core_os Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Core_os Encryption operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox2.SelectedItem == "HexDump it")
            {
               SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                MessageBox.Show("Feature removed");
            }
            if (ComboBox2.SelectedItem == "Core_os image info")
            {
                txtCommand.Text = "cos -i CORE_OS_PACKAGE.pkg";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Core_os Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Core_os Info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
                
            }
        }


        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox3.SelectedItem == "lv0")
            {
                txtCommand.Text = "readself lv0";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("lv0 readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox3.SelectedItem == "lv1ldr")
            {
                txtCommand.Text = "readself lv1ldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("lv1ldr readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox3.SelectedItem == "lv2ldr")
            {
                txtCommand.Text = "readself lv2ldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("lv2ldr readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox3.SelectedItem == "isoldr")
            {
                txtCommand.Text = "readself isoldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("isoldr readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox3.SelectedItem == "appldr")
            {
                txtCommand.Text = "readself appldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("appldr readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox3.SelectedItem == "EBOOT.BIN")
            {
                txtCommand.Text = "readself EBOOT.BIN";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd readself Tools");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("EBOOT readself operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox4.SelectedItem == "Fix Tar 3.72 and lower")
            {
                txtCommand.Text = "fix_tar update_files.tar";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Fix_tar Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Fix_tar operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");

            }
            else if (ComboBox4.SelectedItem == "Fix Tar 3.72  debug")
            {
                txtCommand.Text = "fix_tar_debug update_files.tar";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Fix_tar Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Fix_tar operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox4.SelectedItem == "Fix Tar 3.72 up (retail and debug)")
            {
                txtCommand.Text = "fix_tar_v3 update_files.tar";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd Fix_tar Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Fix_tar operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox6.SelectedItem == "lv0")
            {
                txtCommand.Text = "scetool -i lv0";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox6.SelectedItem == "lv1ldr")
            {
                txtCommand.Text = "scetool -i lv1ldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox6.SelectedItem == "lv2ldr")
            {
                txtCommand.Text = "scetool -i lv2ldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox6.SelectedItem == "isoldr")
            {
                txtCommand.Text = "scetool -i isoldr";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (ComboBox6.SelectedItem == "appldr")
            {
                txtCommand.Text = "scetool -i appldr";
                    Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (ComboBox6.SelectedItem == "EBOOT.BIN")
            {
                txtCommand.Text = "scetool -i EBOOT.BIN";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("sce info operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.ShowDialog();
        }

        private void txtResults_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtResults_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormMain f2 = new FormMain();
            f2.ShowDialog();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RCO.SelectedItem == "Dump category_friend_")
            {
                txtCommand.Text = "rcomage dump explore_category_friend.rco friend.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump explore_category_sysconf")
            {
                txtCommand.Text = "rcomage Dump explore_category_sysconf.rco sysconf.xml --resdir RCO";
            }
            if (RCO.SelectedItem == "Dump category_game")
            {
                txtCommand.Text = "rcomage dump explore_category_game.rco game.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump category_music")
            {
                txtCommand.Text = "rcomage Dump explore_category_music.rco music.xml --resdir RCO";
            }
            if (RCO.SelectedItem == "Dump category_network")
            {
                txtCommand.Text = "rcomage Dump explore_category_network.rco network.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump category_photo")
            {
                txtCommand.Text = "rcomage Dump explore_category_photo.rco photo.xml --resdir RCO";
            }
            if (RCO.SelectedItem == "Dump category_psn")
            {
                txtCommand.Text = "rcomage Dump explore_category_psn.rco psn.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump category_tv")
            {
                txtCommand.Text = "rcomage Dump explore_category_tv.rco tv.xml --resdir RCO";
            }
            if (RCO.SelectedItem == "Dump category_user")
            {
                txtCommand.Text = "rcomage Dump explore_category_user.rco user.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump category_video")
            {
                txtCommand.Text = "rcomage Dump explore_category_video.rco video.xml --resdir RCO";
            }
        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (SFO.SelectedItem == "SFO reader")
            {
                txtCommand.Text = "SFOread PARAM.SFO";
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void RCO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RCO.SelectedItem == "Dump category_friend")
            {
                txtCommand.Text = "rcomage dump explore_category_friend.rco friend.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (RCO.SelectedItem == "Dump category_sysconf")
            {
                txtCommand.Text = "rcomage Dump explore_category_sysconf.rco sysconf.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (RCO.SelectedItem == "Dump category_game")
            {
                txtCommand.Text = "rcomage dump explore_category_game.rco game.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (RCO.SelectedItem == "Dump category_music")
            {
                txtCommand.Text = "rcomage Dump explore_category_music.rco music.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (RCO.SelectedItem == "Dump category_network")
            
            {
                txtCommand.Text = "rcomage Dump explore_category_network.rco network.xml --resdir RCO";
            }
            else if (RCO.SelectedItem == "Dump category_photo")
            {
                txtCommand.Text = "rcomage Dump explore_category_photo.rco photo.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (RCO.SelectedItem == "Dump category_psn")
            {
                txtCommand.Text = "rcomage Dump explore_category_psn.rco psn.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (RCO.SelectedItem == "Dump category_tv")
            {
                txtCommand.Text = "rcomage Dump explore_category_tv.rco tv.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (RCO.SelectedItem == "Dump category_user")
            {
                txtCommand.Text = "rcomage Dump explore_category_user.rco user.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            else if (RCO.SelectedItem == "Dump category_video")
            {
                txtCommand.Text = "rcomage Dump explore_category_video.rco video.xml --resdir RCO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd RCO Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("RCO operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }

        private void groupBox5_Enter_1(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if (C2D.SelectedItem == "CEX 2 DEX")
            {
                txtCommand.Text = "CEX2DEX eid_root_key.bin CEXFLASH.bin DEXFLASH.bin";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd C2D Tool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("C2D Operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormMain f2 = new FormMain();
            f2.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 f2 = new Form1();
            f2.Show();
        }

        private void comboBoxPKG_SelectedIndexChanged_3(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formspu f2 = new formspu();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void SFO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SFO.SelectedItem == "SFOreader")
            {
                txtCommand.Text = "SFOreader PARAM.SFO";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd SFOReader");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("SFO Reading operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
        }

        private void EBO_SelectedIndexChanged_3(object sender, EventArgs e)
        {
            if (EBO.SelectedItem == "Decrypt EBOOT")
            {
                txtCommand.Text = "scetool -d EBOOT.BIN EBOOT.ELF";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Decryption of SCE file operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (EBO.SelectedItem == "Show keys")
            {
                txtCommand.Text = "scetool -k";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Show keys operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
            }
            if (EBO.SelectedItem == "Decrypt + resign")
            {
                txtCommand.Text = "fix";
                Process myprocess = new Process();
                System.Diagnostics.ProcessStartInfo StartInfo = new System.Diagnostics.ProcessStartInfo();
                StartInfo.FileName = "cmd";
                //starts cmd window
                StartInfo.RedirectStandardInput = true;
                StartInfo.RedirectStandardOutput = true;
                StartInfo.UseShellExecute = false;
                //required to redirect
                StartInfo.CreateNoWindow = true;
                //<---- creates no window, obviously
                myprocess.StartInfo = StartInfo;
                myprocess.Start();
                System.IO.StreamReader SR = myprocess.StandardOutput;
                System.IO.StreamWriter SW = myprocess.StandardInput;
                SW.WriteLine("cd scetool");
                SW.WriteLine(txtCommand.Text);
                //the command you wish to run.....
                SW.WriteLine("exit");
                //exits command prompt window
                txtResults.Text = SR.ReadToEnd();
                //returns results of the command window
                SW.Close();
                SR.Close();
                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                Clipboard.SetText(txtResults.Text); //set text to clipBoard
                progressBar1.PerformStep();
                int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                MessageBox.Show("Decryption + resign of EBOOT operation finished");
                // Copy the selected text to the Clipboard.
                MessageBox.Show("output log copy to clipboard!!");
       
            }
        }
    }
}







        

