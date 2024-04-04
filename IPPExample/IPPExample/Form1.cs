using System;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace IPPExample
{
    public partial class Form1 : Form
    {
        string path = IPPExample.Properties.Resources.DefaultInstallPath;
        bool desktopshortcut = bool.Parse(IPPExample.Properties.Resources.DesktopShortcut);
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Config c = new Config();
            c.Path = path;
            c.DesktopShortcut = desktopshortcut;
            if (c.ShowDialog() == DialogResult.OK)
            {
                path = c.Path;
                desktopshortcut = c.DesktopShortcut;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string pn = IPPExample.Properties.Resources.ProductName;
            this.Text = pn + " Installer";
            label1.Text = pn + " Installer";
            label2.Text = "Install " + pn + " onto your computer\nClick Install to install, or configure settings in Configure.";
            label3.Text = IPPExample.Properties.Resources.Copyright;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = Path.GetPathRoot(path) + "Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\ipprogram.zip";
            System.IO.File.WriteAllBytes(temp, IPPExample.Properties.Resources.programtest);
            Directory.CreateDirectory(path);
            ZipFile.ExtractToDirectory(temp, path);
            if (desktopshortcut)
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\" + IPPExample.Properties.Resources.ProductName + ".lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = IPPExample.Properties.Resources.Description;
                shortcut.TargetPath = path + "\\" + IPPExample.Properties.Resources.ProgramFile;
                shortcut.Save();
            }
            if (MessageBox.Show("Installation Complete. Do you want to start the application?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(path + "\\" + IPPExample.Properties.Resources.ProgramFile);
            }
            Application.Exit();
        }
    }
}
