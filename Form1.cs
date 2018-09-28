using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MyWIFI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            passValid.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pass.TextLength < 9)
            {
                passValid.ForeColor = Color.Red;
                passValid.Text = "short pass (min 9 sumbols)";
            }
            else
            {
                passValid.Text = null;

                string commands = @"netsh wlan set hostednetwork mode=allow ssid=" + ssid.Text + " key=" + pass.Text +
                    "\n netsh wlan start hostednetwork";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        StandardOutputEncoding = Encoding.GetEncoding(866)
                    }
                };
                process.Start();

                using (StreamWriter pWriter = process.StandardInput)
                {
                    if (pWriter.BaseStream.CanWrite)
                    {
                        foreach (var line in commands.Split('\n'))
                            pWriter.WriteLine(line);
                    }
                }

                StreamReader read = process.StandardOutput;
                textBox1.Text = read.ReadToEnd();
                process.WaitForExit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string commands = @"netsh wlan stop hostednetwork";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866)
                }
            };
            process.Start();

            using (StreamWriter pWriter = process.StandardInput)
            {
                if (pWriter.BaseStream.CanWrite)
                {
                    foreach (var line in commands.Split('\n'))
                        pWriter.WriteLine(line);
                }
            }

            StreamReader read = process.StandardOutput;
            textBox1.Text = read.ReadToEnd();
            process.WaitForExit();
        }

        private void pass_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void ssid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
