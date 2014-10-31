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
using System.Reflection;


namespace DDV
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string fileVersion=Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.label1DDVversion.Text = this.label1DDVversion.Text + "version "+fileVersion;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//DeepZoomToolsDLL-license.txt");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//php-license.txt");

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//civetweb-license.txt");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//OpenSeadragon-license.txt");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//DDV-license.txt");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//jquery-MIT-license.txt");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//biojs-apache-license.txt");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@Directory.GetCurrentDirectory() + "//d3-license.txt");

        }
    }
}
