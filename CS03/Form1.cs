using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            copyFile();
        }

        private void addAutoStartUp()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("CS03 Auto Startup", "\"" + Application.ExecutablePath + "\"");
                }

                MessageBox.Show("Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void getCurrentUserName()
        {
            string n = Environment.UserName;
            MessageBox.Show(n);
        }


        private void copyFile()
        {
            
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string to = "";
            bool isExisting = false;

            try
            {
                string from = path + "\\CS03Demo.txt";

                string current_user = Environment.UserName;
                to = "C:\\Users\\" + current_user + "\\AppData\\Local\\Temp\\CS03Demo.txt";

                if (!File.Exists(to))
                {
                    isExisting = false;
                }
                else
                {
                    isExisting = true;
                }

                File.Copy(from, to, true);

                MessageBox.Show("Success");

            }
            catch (ExecutionEngineException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {

                MessageBox.Show("isExisting: "+ isExisting);
                
                if (File.Exists(to))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(to);
                    }
                    catch (ExecutionEngineException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

    }
}
