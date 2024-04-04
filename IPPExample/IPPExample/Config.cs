﻿using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;

namespace IPPExample
{
    public partial class Config : Form
    {
        public string Path { get; set; }
        public bool DesktopShortcut { get; set; }
        public Config()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog cmn = new CommonOpenFileDialog();
            cmn.InitialDirectory = "C:";
            cmn.IsFolderPicker = true;
            if (cmn.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textBox1.Text = cmn.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Path = textBox1.Text;
            DesktopShortcut = checkBox1.Checked;
        }

        private void Config_Load(object sender, EventArgs e)
        {
            textBox1.Text = Path;
            checkBox1.Checked = DesktopShortcut;
        }
    }
}
