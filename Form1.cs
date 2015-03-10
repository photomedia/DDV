using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;
using Microsoft.DeepZoomTools;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.InteropServices;

/*

Copyright (c) 2014, Tomasz Neugebauer
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

3. Neither the name of the author nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

 * OpenSeadragon license: OpenSeadragon-license.txt
 * DeepZoomTools.dll license:  DeepZoomToolsDLL-license.txt
 * PHP license: php-license.txt
 * Civetweb license: civetweb-license.txt
 * jQuery license: jQuery-MIT-license.txt
 * BioJS license: biojs-apache-license.txt
 */


namespace DDV
{

	

	public class Form1 : System.Windows.Forms.Form
    {
        private IContainer components;
		private System.Windows.Forms.OpenFileDialog fDlgSourceSequence;
		private Random RandomClass = new Random();
		private System.Windows.Forms.Label lblSourceSequence;
		private System.Windows.Forms.Button btnBrowseSelectFASTA;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblDataLength;
		private static string m_strSourceFile;
        private static string m_strSourceBitmapFile;
        private System.Windows.Forms.Label lblSequenceName;
        private SaveFileDialog saveDialog;
        public ProgressBar progressBar1;
        private Label lblProgressText;
        private Label lblDNAViewer;
		public string read;
        public string EndOfSequence = "";
        public string refseq = "";
        public string gi = "";
        public string DDVseqID = "";
        public string m_strFinalDestinationFolder = "";

        //BitmapData glbl_bmd;
        //Bitmap glbl_b;
        private RichTextBox resultLogTextBox;
        private Label label5;
        private TextBox txtGI;
        private Button btnDownloadFASTA;
        private Label lblRefSeq;
        private Label label6;
        private Label label8;
        private Label lblSourceBitmapFilename;
        private Process m_prcCivetweb;
        private FolderBrowserDialog fDlgFinalDestination;
        private Button btnFinalDestinationFolder;
        private GroupBox groupBox3;
        private Label lblOutputPath;
        private Button btnGeneratedIntefaces;
        private LinkLabel lnkLatestInterface;
        private TabPage tabPage1;
        private Button btnReadSequenceProperties;
        private Label label3;
        private Label label2;
        private Label label4;
        private TextBox textBoxTileSize;
        private TextBox txtBoxY;
        private Button btnProcessBitmapDeepZoom;
        private Label label1;
        private Button btnGenerateImage;
        private TabControl tabControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolTip toolTip1;
        private Label label7;
        private Label label9;
        private CheckBox chckIncludeDensity;
        private OpenFileDialog dlgImageFileSet;
        private RichTextBox txtBoxFASTAStats;


        protected const string _newline = "\r\n";

   		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			m_strSourceFile="";
            m_strSourceBitmapFile = "";

            btnProcessBitmapDeepZoom.Enabled = false;
            checkEnvironment();
            launchCivetweb();
            SetFinalDestinationFolder(@Directory.GetCurrentDirectory() + "\\output\\");
            btnGeneratedIntefaces.Enabled = true;
            
          
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            
             if (m_prcCivetweb != null)
                {
                    if (!m_prcCivetweb.HasExited)
                    {
                        killCivetweb();
                    }
                }
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );

           
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fDlgSourceSequence = new System.Windows.Forms.OpenFileDialog();
            this.lblSourceSequence = new System.Windows.Forms.Label();
            this.btnBrowseSelectFASTA = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDownloadFASTA = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGI = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSourceBitmapFilename = new System.Windows.Forms.Label();
            this.lblDataLength = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoxFASTAStats = new System.Windows.Forms.RichTextBox();
            this.lblRefSeq = new System.Windows.Forms.Label();
            this.lblSequenceName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgressText = new System.Windows.Forms.Label();
            this.lblDNAViewer = new System.Windows.Forms.Label();
            this.resultLogTextBox = new System.Windows.Forms.RichTextBox();
            this.fDlgFinalDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFinalDestinationFolder = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lnkLatestInterface = new System.Windows.Forms.LinkLabel();
            this.btnGeneratedIntefaces = new System.Windows.Forms.Button();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chckIncludeDensity = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnReadSequenceProperties = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTileSize = new System.Windows.Forms.TextBox();
            this.txtBoxY = new System.Windows.Forms.TextBox();
            this.btnProcessBitmapDeepZoom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateImage = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dlgImageFileSet = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSourceSequence
            // 
            this.lblSourceSequence.Location = new System.Drawing.Point(171, 73);
            this.lblSourceSequence.Name = "lblSourceSequence";
            this.lblSourceSequence.Size = new System.Drawing.Size(478, 40);
            this.lblSourceSequence.TabIndex = 6;
            this.lblSourceSequence.Text = "Source sequence filename";
            // 
            // btnBrowseSelectFASTA
            // 
            this.btnBrowseSelectFASTA.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnBrowseSelectFASTA.Location = new System.Drawing.Point(4, 82);
            this.btnBrowseSelectFASTA.Name = "btnBrowseSelectFASTA";
            this.btnBrowseSelectFASTA.Size = new System.Drawing.Size(157, 24);
            this.btnBrowseSelectFASTA.TabIndex = 5;
            this.btnBrowseSelectFASTA.Text = "Browse/Select FASTA file ->";
            this.btnBrowseSelectFASTA.UseVisualStyleBackColor = false;
            this.btnBrowseSelectFASTA.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnDownloadFASTA);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtGI);
            this.groupBox1.Controls.Add(this.lblSourceSequence);
            this.groupBox1.Controls.Add(this.btnBrowseSelectFASTA);
            this.groupBox1.Location = new System.Drawing.Point(4, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 130);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gene Sequence Source File";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(17, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(391, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Enter GI and download FASTA file from NIH or browse for local source FASTA file";
            // 
            // btnDownloadFASTA
            // 
            this.btnDownloadFASTA.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnDownloadFASTA.Location = new System.Drawing.Point(264, 43);
            this.btnDownloadFASTA.Name = "btnDownloadFASTA";
            this.btnDownloadFASTA.Size = new System.Drawing.Size(194, 23);
            this.btnDownloadFASTA.TabIndex = 37;
            this.btnDownloadFASTA.Text = "Download FASTA file from NIH";
            this.btnDownloadFASTA.UseVisualStyleBackColor = false;
            this.btnDownloadFASTA.Click += new System.EventHandler(this.button13_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "DNA data for GI:";
            // 
            // txtGI
            // 
            this.txtGI.Location = new System.Drawing.Point(128, 45);
            this.txtGI.Name = "txtGI";
            this.txtGI.Size = new System.Drawing.Size(129, 20);
            this.txtGI.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Image file:";
            this.label8.Visible = false;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblSourceBitmapFilename
            // 
            this.lblSourceBitmapFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSourceBitmapFilename.Location = new System.Drawing.Point(64, 132);
            this.lblSourceBitmapFilename.Name = "lblSourceBitmapFilename";
            this.lblSourceBitmapFilename.Size = new System.Drawing.Size(396, 24);
            this.lblSourceBitmapFilename.TabIndex = 38;
            this.lblSourceBitmapFilename.Text = "filename";
            this.lblSourceBitmapFilename.Visible = false;
            // 
            // lblDataLength
            // 
            this.lblDataLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDataLength.Location = new System.Drawing.Point(6, 59);
            this.lblDataLength.Name = "lblDataLength";
            this.lblDataLength.Size = new System.Drawing.Size(464, 35);
            this.lblDataLength.TabIndex = 15;
            this.lblDataLength.Text = "Number of base pairs/data length";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.txtBoxFASTAStats);
            this.groupBox2.Controls.Add(this.lblRefSeq);
            this.groupBox2.Controls.Add(this.lblSequenceName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblDataLength);
            this.groupBox2.Location = new System.Drawing.Point(663, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(481, 5040);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sequence Properties";
            // 
            // txtBoxFASTAStats
            // 
            this.txtBoxFASTAStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxFASTAStats.BackColor = System.Drawing.SystemColors.Control;
            this.txtBoxFASTAStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxFASTAStats.Location = new System.Drawing.Point(11, 153);
            this.txtBoxFASTAStats.Name = "txtBoxFASTAStats";
            this.txtBoxFASTAStats.Size = new System.Drawing.Size(459, 277);
            this.txtBoxFASTAStats.TabIndex = 35;
            this.txtBoxFASTAStats.Text = "";
            // 
            // lblRefSeq
            // 
            this.lblRefSeq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefSeq.Location = new System.Drawing.Point(125, 12);
            this.lblRefSeq.Name = "lblRefSeq";
            this.lblRefSeq.Size = new System.Drawing.Size(345, 37);
            this.lblRefSeq.TabIndex = 36;
            // 
            // lblSequenceName
            // 
            this.lblSequenceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSequenceName.Location = new System.Drawing.Point(8, 94);
            this.lblSequenceName.Name = "lblSequenceName";
            this.lblSequenceName.Size = new System.Drawing.Size(462, 48);
            this.lblSequenceName.TabIndex = 16;
            this.lblSequenceName.Text = "Name of sequence";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "RefSeq";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(59, 304);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(477, 23);
            this.progressBar1.TabIndex = 21;
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = true;
            this.lblProgressText.Location = new System.Drawing.Point(6, 348);
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(28, 13);
            this.lblProgressText.TabIndex = 24;
            this.lblProgressText.Text = "Log:";
            // 
            // lblDNAViewer
            // 
            this.lblDNAViewer.Location = new System.Drawing.Point(6, 304);
            this.lblDNAViewer.Name = "lblDNAViewer";
            this.lblDNAViewer.Size = new System.Drawing.Size(51, 15);
            this.lblDNAViewer.TabIndex = 25;
            this.lblDNAViewer.Text = "Progress:";
            // 
            // resultLogTextBox
            // 
            this.resultLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.resultLogTextBox.Location = new System.Drawing.Point(37, 328);
            this.resultLogTextBox.Name = "resultLogTextBox";
            this.resultLogTextBox.Size = new System.Drawing.Size(616, 287);
            this.resultLogTextBox.TabIndex = 29;
            this.resultLogTextBox.Text = "";
            // 
            // btnFinalDestinationFolder
            // 
            this.btnFinalDestinationFolder.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnFinalDestinationFolder.Location = new System.Drawing.Point(11, 62);
            this.btnFinalDestinationFolder.Name = "btnFinalDestinationFolder";
            this.btnFinalDestinationFolder.Size = new System.Drawing.Size(87, 34);
            this.btnFinalDestinationFolder.TabIndex = 32;
            this.btnFinalDestinationFolder.Text = "Browse Output Folder";
            this.btnFinalDestinationFolder.UseVisualStyleBackColor = false;
            this.btnFinalDestinationFolder.Click += new System.EventHandler(this.btnFinalDestinationFolder_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lblSourceBitmapFilename);
            this.groupBox3.Controls.Add(this.lnkLatestInterface);
            this.groupBox3.Controls.Add(this.btnGeneratedIntefaces);
            this.groupBox3.Controls.Add(this.lblOutputPath);
            this.groupBox3.Controls.Add(this.btnFinalDestinationFolder);
            this.groupBox3.Location = new System.Drawing.Point(663, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 170);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // lnkLatestInterface
            // 
            this.lnkLatestInterface.AutoSize = true;
            this.lnkLatestInterface.Enabled = false;
            this.lnkLatestInterface.Location = new System.Drawing.Point(168, 26);
            this.lnkLatestInterface.Name = "lnkLatestInterface";
            this.lnkLatestInterface.Size = new System.Drawing.Size(80, 13);
            this.lnkLatestInterface.TabIndex = 42;
            this.lnkLatestInterface.TabStop = true;
            this.lnkLatestInterface.Text = "Latest interface";
            this.lnkLatestInterface.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLatestInterface_LinkClicked);
            // 
            // btnGeneratedIntefaces
            // 
            this.btnGeneratedIntefaces.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGeneratedIntefaces.Enabled = false;
            this.btnGeneratedIntefaces.Location = new System.Drawing.Point(11, 19);
            this.btnGeneratedIntefaces.Name = "btnGeneratedIntefaces";
            this.btnGeneratedIntefaces.Size = new System.Drawing.Size(151, 27);
            this.btnGeneratedIntefaces.TabIndex = 41;
            this.btnGeneratedIntefaces.Text = "Open Generated Interfaces";
            this.btnGeneratedIntefaces.UseVisualStyleBackColor = false;
            this.btnGeneratedIntefaces.Click += new System.EventHandler(this.btnGeneratedIntefaces_Click);
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutputPath.Location = new System.Drawing.Point(97, 63);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(363, 58);
            this.lblOutputPath.TabIndex = 40;
            this.lblOutputPath.Text = "Output Path";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage1.Controls.Add(this.chckIncludeDensity);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btnReadSequenceProperties);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxTileSize);
            this.tabPage1.Controls.Add(this.txtBoxY);
            this.tabPage1.Controls.Add(this.btnProcessBitmapDeepZoom);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnGenerateImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(645, 112);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Generate DNA Visualization";
            // 
            // chckIncludeDensity
            // 
            this.chckIncludeDensity.AutoSize = true;
            this.chckIncludeDensity.Checked = true;
            this.chckIncludeDensity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckIncludeDensity.Location = new System.Drawing.Point(174, 24);
            this.chckIncludeDensity.Name = "chckIncludeDensity";
            this.chckIncludeDensity.Size = new System.Drawing.Size(133, 17);
            this.chckIncludeDensity.TabIndex = 40;
            this.chckIncludeDensity.Text = "include density service";
            this.chckIncludeDensity.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(6, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(600, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "After selecting sequence, Read Sequence Properties and Generate Image and Interfa" +
    "ce, then Process Image with Deepzoom";
            // 
            // btnReadSequenceProperties
            // 
            this.btnReadSequenceProperties.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReadSequenceProperties.Enabled = false;
            this.btnReadSequenceProperties.Location = new System.Drawing.Point(13, 29);
            this.btnReadSequenceProperties.Name = "btnReadSequenceProperties";
            this.btnReadSequenceProperties.Size = new System.Drawing.Size(70, 52);
            this.btnReadSequenceProperties.TabIndex = 32;
            this.btnReadSequenceProperties.Text = "Read Sequence Properties";
            this.btnReadSequenceProperties.UseVisualStyleBackColor = false;
            this.btnReadSequenceProperties.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Recommended: 144 for bacteria, 256 for human";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(416, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Tile Size (Deep Zoom)  1-1048";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Recommended: 3000 for bacteria, 20000 for human";
            // 
            // textBoxTileSize
            // 
            this.textBoxTileSize.Location = new System.Drawing.Point(419, 59);
            this.textBoxTileSize.Name = "textBoxTileSize";
            this.textBoxTileSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxTileSize.TabIndex = 31;
            this.textBoxTileSize.Text = "144";
            // 
            // txtBoxY
            // 
            this.txtBoxY.Location = new System.Drawing.Point(171, 62);
            this.txtBoxY.Name = "txtBoxY";
            this.txtBoxY.Size = new System.Drawing.Size(100, 20);
            this.txtBoxY.TabIndex = 30;
            this.txtBoxY.Text = "3000";
            // 
            // btnProcessBitmapDeepZoom
            // 
            this.btnProcessBitmapDeepZoom.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnProcessBitmapDeepZoom.Location = new System.Drawing.Point(314, 33);
            this.btnProcessBitmapDeepZoom.Name = "btnProcessBitmapDeepZoom";
            this.btnProcessBitmapDeepZoom.Size = new System.Drawing.Size(97, 44);
            this.btnProcessBitmapDeepZoom.TabIndex = 30;
            this.btnProcessBitmapDeepZoom.Text = "Process Image with Deepzoom";
            this.btnProcessBitmapDeepZoom.UseVisualStyleBackColor = false;
            this.btnProcessBitmapDeepZoom.Click += new System.EventHandler(this.button12_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Image Height";
            // 
            // btnGenerateImage
            // 
            this.btnGenerateImage.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGenerateImage.Enabled = false;
            this.btnGenerateImage.Location = new System.Drawing.Point(96, 28);
            this.btnGenerateImage.Name = "btnGenerateImage";
            this.btnGenerateImage.Size = new System.Drawing.Size(69, 54);
            this.btnGenerateImage.TabIndex = 26;
            this.btnGenerateImage.Text = "Generate Image and Interface";
            this.btnGenerateImage.UseVisualStyleBackColor = false;
            this.btnGenerateImage.Click += new System.EventHandler(this.button9_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 163);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(653, 138);
            this.tabControl1.TabIndex = 31;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1156, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1156, 627);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.resultLogTextBox);
            this.Controls.Add(this.lblDNAViewer);
            this.Controls.Add(this.lblProgressText);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "DNA Data Visualization Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		 private static void checkEnvironment(){

             string strDotNetVersion = Environment.Version.ToString();
             int iDotNetVersion = Environment.Version.Major;

             if (iDotNetVersion < 4)
             {
                 MessageBox.Show("Error: Attempt to run with .NET Framework "+strDotNetVersion+" \nThis application requires .NET Framework Version 4 or higher. \nTo run this software, please donwload and install .NET Framework 4 at http://www.microsoft.com/en-ca/download/details.aspx?id=17851");
             }

        }

		public string ConvertToDigits(string strTGACN)
		{

            /* AGCT
             *  T="0"
		        A="1"
		        G="2"
		        C="3"
             * 
             * ----------------------------
             * 
             * T (or U)..........Thymine (or Uracil) 
             * 
             * R.................A or G 
             * Y.................C or T 
             * S.................G or C 
             * W.................A or T 
             * K.................G or T 
             * M.................A or C 
             * B.................C or G or T 
             * D.................A or G or T 
             * H.................A or C or T 
             * V.................A or C or G 
             * N.................any base 
             * . or -............gap   * 
             */
            
            //now convert all to digits:

			strTGACN=strTGACN.Replace("T", T);
			strTGACN=strTGACN.Replace("A", A);
			strTGACN=strTGACN.Replace("G", G);
			strTGACN=strTGACN.Replace("C", C);

            strTGACN = strTGACN.Replace("N", N);
            strTGACN = strTGACN.Replace("V", V);
            strTGACN = strTGACN.Replace("H", H);
            strTGACN = strTGACN.Replace("D", D);
            strTGACN = strTGACN.Replace("B", B);
            strTGACN = strTGACN.Replace("M", M);
            strTGACN = strTGACN.Replace("K", K);
            strTGACN = strTGACN.Replace("W", W);
            strTGACN = strTGACN.Replace("S", S);
            strTGACN = strTGACN.Replace("Y", Y);
            strTGACN = strTGACN.Replace("R", R);
            strTGACN = strTGACN.Replace("T", T);
            strTGACN = strTGACN.Replace("A", A);
            strTGACN = strTGACN.Replace("G", G);
            strTGACN = strTGACN.Replace("C", C);
			
			return strTGACN;
		}

		public string ConvertToTGACN(string strDigits)
		{
			
			//strDigits.Replace("0", "N");

			strDigits=strDigits.Replace(T, "T");
			strDigits=strDigits.Replace(A, "A");
			strDigits=strDigits.Replace(G, "G");
			strDigits=strDigits.Replace(C, "C");
			return strDigits;
		}

		public string CleanInputFile(string strFile)
		{
			strFile=strFile.Replace("0", "");
			strFile=strFile.Replace("1", "");
			strFile=strFile.Replace("2", "");
			strFile=strFile.Replace("3", "");
			strFile=strFile.Replace("4", "");
			strFile=strFile.Replace("5", "");
			strFile=strFile.Replace("6", "");
			strFile=strFile.Replace("7", "");
			strFile=strFile.Replace("8", "");
			strFile=strFile.Replace("9", "");
			strFile=strFile.Replace(" ", "");
			return strFile.ToUpper();
		}

		

		private void button2_Click_1(object sender, System.EventArgs e)
		{
			DialogResult dr = fDlgSourceSequence.ShowDialog();

			if (dr == DialogResult.OK)
			{
                if (fDlgSourceSequence.FileName == "") { 
                    MessageBox.Show("Please select source file.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    return; 
                }
                
                //copy the file over to the output folder as "sequence.fasta"
                string strDestination = @Directory.GetCurrentDirectory() + "\\output\\sequence.fasta";
                //check if file already in correct place, if not, move it into output folder
                if (!(fDlgSourceSequence.FileName == strDestination))
                {
                    //if file exists, delete it
                    if (File.Exists(strDestination))
                    {
                        File.Delete(strDestination);
                    }
                    //if output Directory not there, make it
                    if (!(Directory.Exists(@Directory.GetCurrentDirectory() + "\\output")))
                    {
                        Directory.CreateDirectory(@Directory.GetCurrentDirectory() + "\\output");
                    }
                    //copy the file
                    File.Copy(fDlgSourceSequence.FileName, strDestination);
                    MessageBoxClear();
                    MessageBoxShow("Copied selected sequence to output folder.");
                }
                fDlgSourceSequence.FileName = strDestination;
                SetSourceSequence(fDlgSourceSequence.FileName);
                txtGI.Text = "";
                CleanInterfaceNewSequence();
                BitmapClear();
                
			}

			

		}

        public static void SetMyPalette(ref Bitmap b)
        {
            ColorPalette pal = b.Palette;
            for (int i = 0; i < 16; i++)
            { 
                pal.Entries[i] = Color.FromArgb(255, 240, 240, 240); 
            }
            pal.Entries[1] = Color.FromArgb(255, 255, 0, 0); //A
            pal.Entries[2] = Color.FromArgb(255, 0, 255, 0); //G
            pal.Entries[3] = Color.FromArgb(255, 250, 240, 114);//T
            pal.Entries[4] = Color.FromArgb(255, 0, 0, 255);//C
            pal.Entries[5] = Color.FromArgb(255, 30, 30, 30);//N
            pal.Entries[6] = Color.FromArgb(255, 60, 60, 60);//R
            pal.Entries[7] = Color.FromArgb(255, 70, 70, 70);//Y
            pal.Entries[8] = Color.FromArgb(255, 80, 80, 80);//S
            pal.Entries[9] = Color.FromArgb(255, 90, 90, 90);//W
            pal.Entries[10] = Color.FromArgb(255, 100, 100, 100);//K
            pal.Entries[11] = Color.FromArgb(255, 110, 110, 110);//M
            pal.Entries[12] = Color.FromArgb(255, 120, 120, 120);//B
            pal.Entries[13] = Color.FromArgb(255, 130, 130, 130);//D
            pal.Entries[14] = Color.FromArgb(255, 140, 140, 140);//H
            pal.Entries[15] = Color.FromArgb(255, 150, 150, 150);//V

            pal.Entries[16] = Color.FromArgb(255, 0, 0, 0);//unknown - error
              
          
            b.Palette = pal;
        }


        public unsafe void UnsafeSetPixel(int x, int y, byte c, ref BitmapData bmd)
        {
           // BitmapData bmd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
          //                ImageLockMode.ReadWrite, b.PixelFormat);
            byte* p = (byte*)bmd.Scan0.ToPointer();
            int offset = y * bmd.Stride + x;
            p[offset] = c;
        }


		       


       

        public void Write1BaseToBMPUncompressed4X(int intStart, int intEnd, ref Bitmap Tex, int x, int y, ref BitmapData bmd)
        {

    
            /*
             * R.................A or G 
             * Y.................C or T 
             * S.................G or C 
             * W.................A or T 
             * K.................G or T 
             * M.................A or C 
             * B.................C or G or T 
             * D.................A or G or T 
             * H.................A or C or T 
             * V.................A or C or G 
             * N.................any base 
             * 
             * 
             */

            string first = null;

           
            byte bytePaletteIndex = 0;

            first = read.Substring(intStart, 1);
           

            if (first == A)
            {
                ipA++;
                bytePaletteIndex = 1;
            }
            else if (first == G)
            {
                ipG++;
                bytePaletteIndex = 2;
            }
            else if (first == T)
            {
                ipT++;
                bytePaletteIndex = 3;
            }
            else if (first == C)
            {
                ipC++;
                bytePaletteIndex = 4;
            }
            //logical FASTA probabilities
            //R.................A or G 
            else if (first == R)
            {
                bytePaletteIndex = 6;
                ipR++;
            }
            //Y.................C or T 
            else if (first == Y)
            {
                bytePaletteIndex = 7;
                ipY++;
            }
            // S.................G or C 
            else if (first == S)
            {
                bytePaletteIndex = 8;
                ipS++;
            }
            // W.................A or T 
            else if (first == W)
            {
                bytePaletteIndex = 9;
                ipW++;
            }
            // K.................G or T 
            else if (first == K)
            {
               
                bytePaletteIndex = 10;
                ipK++;
            }
            // M.................A or C 
            else if (first == M)
            {
                
                bytePaletteIndex = 11;
                ipM++;
            }
            // B.................C or G or T 
            else if (first == B)
            {
                
                bytePaletteIndex = 12;
                ipB++;
            }
            // D.................A or G or T 
            else if (first == D)
            {
               
                bytePaletteIndex = 13;
                ipD++;
            }
            // H.................A or C or T 
            else if (first == H)
            {
                
                bytePaletteIndex = 14;
                ipH++;
            }
            // V.................A or C or G 
            else if (first == V)
            {
               
                bytePaletteIndex = 15;
                ipV++;
            }
            // N.................any base 
            else if (first == N)
            {
                
                ipN++;
                bytePaletteIndex = 5;
            }

            else
            {
                
                bytePaletteIndex = 16;
                ipUnknown++;

            }

            //end

            
            UnsafeSetPixel(x, y, bytePaletteIndex, ref bmd);
            UnsafeSetPixel(x, y+1, bytePaletteIndex, ref bmd);
            UnsafeSetPixel(x+1, y+1, bytePaletteIndex, ref bmd);
            UnsafeSetPixel(x+1, y, bytePaletteIndex, ref bmd);

        }

     
    
        public void WriteToBMPUncompressed4X(int intStart, int intEnd, ref Bitmap Tex, int x, int y, ref BitmapData bmd)
        {
            
            for (int i = 0; i < 10; i++)
            {
                Write1BaseToBMPUncompressed4X(intStart + i, intStart + i + 1, ref Tex, x + (i*2), y, ref bmd);
            }
        }

        public void WriteToBMPVariableLengthUncompressed4X(int intStart, int intEnd, ref Bitmap Tex, int x, int y, int iLength, ref BitmapData bmd)
        {
            

            for (int i = 0; i < iLength; i++)
            {
                Write1BaseToBMPUncompressed4X(intStart + i, intStart + i + 1, ref Tex, x + (i * 2), y, ref bmd);
            }
        }


       

        public int iLineLength = 70;

		public string T="0";
		public string A="1";
		public string G="2";
		public string C="3";

        public string R = "R";
        public string Y = "Y";
        public string S = "S";
        public string W = "W";
        public string K = "K";
        public string M = "M";
        public string B = "B";
        public string D = "D";
        public string H = "H";
        public string V = "V";
        public string N = "N";

        public int iT = 0;
        public int iA = 0;
        public int iG = 0;
        public int iC = 0;

        public int ipT = 0;
        public int ipA = 0;
        public int ipG = 0;
        public int ipC = 0;

        public int iR = 0;
        public int iY = 0;
        public int iS = 0;
        public int iW = 0;
        public int iK = 0;
        public int iM = 0;
        public int iB = 0;
        public int iD = 0;
        public int iH = 0;
        public int iV = 0;
        public int iN = 0;
        public int iUnknown = 0;

        public int ipR = 0;
        public int ipY = 0;
        public int ipS = 0;
        public int ipW = 0;
        public int ipK = 0;
        public int ipM = 0;
        public int ipB = 0;
        public int ipD = 0;
        public int ipH = 0;
        public int ipV = 0;
        public int ipN = 0;
        public int ipUnknown = 0;
       

        private int populateInfo()
        {
            if (m_strSourceFile == "") { 
                MessageBox.Show("Please select source file.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                return (0); 
            }

            //check if FASTA file exists
            if (!File.Exists(m_strSourceFile))
            {
                MessageBox.Show("Could not locate FASTA file to process, please specify source file.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (0); 
            }
          
            StreamReader streamFASTAFile = File.OpenText(m_strSourceFile);

            int i = 0;
            bool end = false;
            bool bOnce = false;
            int DataLengthCounter = 0;
            int iActualLineLength = 0;


            string firstLetter = null;


            while (((read = streamFASTAFile.ReadLine()) != null) && !end)
            {
                if (read == "")
                { //skip 
                }
                else
                {

                    firstLetter = read.Substring(0, 1);

                    if (firstLetter == ">")
                    {
                        if (i > 1) { end = true; }
                        else
                        {
                            lblSequenceName.Text = read;
                            lblSequenceName.Text = lblSequenceName.Text.Substring(1, lblSequenceName.Text.Length-1);
                            lblSequenceName.Refresh();
                            //ref|NC_007414.1|
                            //[^\]
                            refseq = Regex.Match(lblSequenceName.Text, @"ref\|(.*?)\|").Groups[1].Value;
                            if (refseq == "") { 
                                MessageBoxShow("Refseq not found in sequence file.");
                                
                            }
                            else { 
                               //string refseqAcc = Regex.Match(refseq, @"^([^\.]*)\.").Groups[1].Value;
                               lblRefSeq.Text = refseq;
                               MessageBoxShow("Refseq present in sequence data. ");
                            }
                            gi = Regex.Match(lblSequenceName.Text, @"gi\|(.*?)\|").Groups[1].Value;
                            if (gi == "") {
                                DDVseqID="DDV"+DateTime.Now.ToString("yyyyMMddHHmmss");
                                MessageBoxShow("GI not found in sequence file.  Generated the following DDV ID for this sequence: "+DDVseqID);

                            }
                        }

                    }

                    else
                    {
                        read = CleanInputFile(read);

                        //for accounting:
                        iT = iT + CountOccurencesOfChar(read, 'T');
                        iA = iA + CountOccurencesOfChar(read, 'A');
                        iG = iG + CountOccurencesOfChar(read, 'G');
                        iC = iC + CountOccurencesOfChar(read, 'C');
                        iR = iR + CountOccurencesOfChar(read, 'R');
                        iY = iY + CountOccurencesOfChar(read, 'Y');
                        iS = iS + CountOccurencesOfChar(read, 'S');
                        iW = iW + CountOccurencesOfChar(read, 'W');
                        iK = iK + CountOccurencesOfChar(read, 'K');
                        iM = iM + CountOccurencesOfChar(read, 'M');
                        iB = iB + CountOccurencesOfChar(read, 'B');
                        iD = iD + CountOccurencesOfChar(read, 'D');
                        iH = iH + CountOccurencesOfChar(read, 'H');
                        iV = iV + CountOccurencesOfChar(read, 'V');
                        iN = iN + CountOccurencesOfChar(read, 'N');
                        if (!bOnce) { iActualLineLength = read.Length; bOnce = true; }

                        DataLengthCounter = DataLengthCounter + read.Trim().Length;
                       
                        i++;
                    }
                }
            }
            

            lblDataLength.Text = "Nucleotides:" + DataLengthCounter.ToString();
            lblDataLength.Text = lblDataLength.Text + " | Line Length: " + iActualLineLength.ToString();
            lblDataLength.Refresh();
            if (iLineLength.ToString() != iActualLineLength.ToString())
            {
                MessageBoxShow("Error: Line length in FASTA file does not equal to "+iLineLength.ToString());
                MessageBoxShow("This software requires FASTA file to use line length " + iLineLength.ToString());
                streamFASTAFile.Close();
                return (0);
            }
            MessageBoxShow("Retrieved basic sequence properties from FASTA.");
            streamFASTAFile.Close();
            return (DataLengthCounter);

        }


        private void MessageBoxClear()
        {
            resultLogTextBox.Clear();
            resultLogTextBox.Refresh();
        }
        private void MessageBoxShow(string strMessage)
        {
            //Log progress on the interface generator.

            resultLogTextBox.Text = resultLogTextBox.Text + "\n" + strMessage;
            resultLogTextBox.Refresh();

        }

        private void SaveBMP(ref Bitmap bmp, string strPath) // now 'ref' parameter
        {
            try
            {
                bmp.Save(strPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                Bitmap bitmap = (Bitmap) bmp.Clone();
                bmp.Dispose();
                bitmap.Save(strPath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }


        private void InitializeMakeBitmap()
        {

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            resultLogTextBox.Clear();
            EndOfSequence = "";

            string strFastaStats = "";
            int intMagnification = 2;

            iT = 0; iA = 0; iG = 0; iC = 0; iR = 0; iY = 0; iS = 0; iW = 0; iK = 0; iM = 0; iB = 0;
            iD = 0; iH = 0; iV = 0; iN = 0;

            ipT = 0; ipA = 0; ipG = 0; ipC = 0; ipR = 0; ipY = 0; ipS = 0; ipW = 0; ipK = 0; ipM = 0; ipB = 0;
            ipD = 0; ipH = 0; ipV = 0; ipN = 0;

            lblProgressText.Text = "";
            lblProgressText.Refresh();
            progressBar1.Value = 0;
            progressBar1.Update();
            progressBar1.Refresh();
            int counter = 0;
            if (m_strSourceFile == "") { MessageBox.Show("Please select source file.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (!File.Exists(m_strSourceFile)) { MessageBox.Show("Could not find source FASTA file"+m_strSourceFile+". Please select source file", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            FileInfo TheFile = new FileInfo(m_strSourceFile);

            int total = populateInfo();

            if (total == 0)
            {
                MessageBoxShow("Error.  Invalid FASTA file.  ");
                return;
            }

            int y = 0;
            try
            {
                y = Convert.ToInt32(txtBoxY.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Please put only numbers in Image height");
            }

            //int x = (((total / y) / 60 * 2)) + (total / y) + 64 * 2;
            //int x = 100+((((total / (y/intMagnification)) / (iLineLength*intMagnification)) * 4) + (total / (y/intMagnification)) + ((iLineLength+4)*intMagnification)) * intMagnification;

            int iPaddingBetweenColumns = 4;
            int iColumnWidth = (iLineLength * intMagnification) + iPaddingBetweenColumns;
            MessageBoxShow("iColumnWidth: " + iColumnWidth);
            int iNucleotidesPerColumn = iLineLength * y / intMagnification;
            MessageBoxShow("iNucleotidesPerColumn: " + iNucleotidesPerColumn);
            int numColumns = (int)Math.Ceiling((double)total / iNucleotidesPerColumn);
            MessageBoxShow("numColumns: " + numColumns);
            int x = (numColumns * iColumnWidth) - iPaddingBetweenColumns; //last column has no padding.
            MessageBoxShow("x: " + x);

            if ((x > Math.Pow(2, 16)))
            {
                throw new System.Exception("Width of image (x) would be greater than 65536 (2^16). \n Try setting a larger Image height for this data so that the image width becomes smaller.");
            }
            if ((y > Math.Pow(2, 16)))
            {
                throw new System.Exception("Image height is set to be greater than the maximum 65536 (2^16). Try setting a smaller height for this data and try again.");
        
            }


            // Bitmap B = new Bitmap(x, y);
            string strMessage = "Initializing PNG width=" + x + " height=" + y;
            MessageBoxShow(strMessage);

            Bitmap b = new Bitmap(x, y, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            SetMyPalette(ref b);
            BitmapData bmd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),ImageLockMode.ReadWrite, b.PixelFormat);


            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    UnsafeSetPixel(i, j, (byte)0, ref bmd);
                }
            }
            
            MessageBoxShow("PNG Initialized");

            // Lets get the length so that when we are reading we know
            // when we have hit a "milestone" and to update the progress bar.
            FileInfo fileSize = new FileInfo(m_strSourceFile);
            long size = fileSize.Length;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = (int)size;
            
            BackgroundWorker workerBMPPainter = new BackgroundWorker();
            workerBMPPainter.WorkerReportsProgress = true;
            workerBMPPainter.WorkerSupportsCancellation = true;
            workerBMPPainter.ProgressChanged += (u, args) =>
            {
                  progressBar1.Value = args.ProgressPercentage; 
            };
            workerBMPPainter.DoWork += (u, args) =>
            {
                BackgroundWorker worker = u as BackgroundWorker;
                //----------------------------convert data to pixels---------------------------------
                int boundX = b.Width;
                int boundY = b.Height - 1;
                //int intMagnification = 2;


                int i = 0;
                counter = 0;
                bool end = false;
                string firstLetter = null;

                int x_pointer = 0;
                int y_pointer = 0;
                int progress = 0;

                StreamReader streamFASTAFile = File.OpenText(m_strSourceFile);

                progress = progress + 1;
                worker.ReportProgress(progress);

                while (((read = streamFASTAFile.ReadLine()) != null) && !end)
                {
                    if (read == "")
                    { //skip 
                    }
                    else
                    {
                        progress += (int)read.Length;
                        worker.ReportProgress(progress);

                        firstLetter = read.Substring(0, 1);

                        if (firstLetter == ">")
                        {

                        }

                        else
                        {
                            read = CleanInputFile(read);
                            read = ConvertToDigits(read);


                            //7 times 10 = 1 line
                            for (int j = 1; j <= (iLineLength / 10); j++)
                            {
                                //read in multiples of 10
                                if (read.Length >= (j * 10))
                                {
                                    //Tex.Write(strFront);
                                    //split further into 3 
                                    WriteToBMPUncompressed4X((j - 1) * 10, j * 10, ref b, x_pointer, y_pointer, ref bmd);
                                    x_pointer = x_pointer + 10 * intMagnification;
                                    //DataLengthCounter=DataLengthCounter+24;
                                }
                                else if (EndOfSequence == "")
                                {
                                    EndOfSequence = ConvertToTGACN(read.Substring((j - 1) * 10, read.Length - ((j - 1) * 10)));
                                    WriteToBMPVariableLengthUncompressed4X((j - 1) * 10, j * 10, ref b, x_pointer, y_pointer, EndOfSequence.Length, ref bmd);
                                    x_pointer = x_pointer + EndOfSequence.Length * intMagnification;
                                    break;
                                }

                            }
                            x_pointer = x_pointer - iLineLength * intMagnification;


                            i++;
                            y_pointer = y_pointer + intMagnification;
                            if (y_pointer >= boundY)
                            {
                                x_pointer = x_pointer + (iLineLength * intMagnification) + 4;
                                y_pointer = 0;
                            }

                            if (x_pointer >= boundX)
                            {
                                x_pointer = 0;
                                counter++;
                                end = true;
                                //this would be an unexpected error, throw an exception
                                throw new System.Exception("Unexpected error while converting data.  Attempt to paint a pixel outside of image bounds. Please review the parameters and ensure the data is in FASTA format, with 70 nucleotides per line. ");
                            }


                        }

                    }
                }

                //close the sequence file
                streamFASTAFile.Close();
               
                //----------------------------end of convert data to pixels--------------------------
            };
            workerBMPPainter.RunWorkerAsync();
            workerBMPPainter.RunWorkerCompleted += (u, args) =>
            {
                MessageBoxShow("Completed mapping DNA to pixels.  Saving result...");

                b.UnlockBits(bmd);
                bmd = null;
                string strResultFileName = "";
                if (gi != "")
                {
                    strResultFileName = gi + ".png";
                }
                else
                {
                    strResultFileName = DDVseqID + ".png";
                }

                //if file exists, delete
                if (File.Exists(strResultFileName))
                {
                    MessageBoxShow("Removing temp version of PNG file.");
                    File.Delete(strResultFileName);
                }
                try
                {
                    b.Save(strResultFileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch
                {
                    throw new System.Exception("Error.  Could not save PNG file.  ");
                }
                b.Dispose();
                MessageBoxShow("File saved " + strResultFileName);

                string sourceFile = strResultFileName;
                string destinationFile = TheFile.DirectoryName + Path.DirectorySeparatorChar + strResultFileName;
                MoveWithReplace(sourceFile, destinationFile);
                MessageBoxShow("File moved to -> " + destinationFile);
                BitmapSet(destinationFile);


                if (counter != 0) { MessageBoxShow("Error: Number of Times Max Width Reached:" + counter.ToString()); }
                int iTotal = iA + iT + iG + iC + iR + iY + iS + iW + iK + iM + iB + iD + iH + iV + iN;
                int ipTotal = ipA + ipT + ipG + ipC + ipR + ipY + ipS + ipW + ipK + ipM + ipB + ipD + ipH + ipV + ipN;
                strFastaStats = "FASTA Stats for " + lblSequenceName.Text +
                "\n\nA:" + iA + " processed: " + ipA +
                "\nT:" + iT + " processed: " + ipT +
                "\nG:" + iG + " processed: " + ipG +
                "\nC:" + iC + " processed: " + ipC +
                "\nR:" + iR + " processed: " + ipR +
                " | Y:" + iY + " processed: " + ipY +
                " | S:" + iS + " processed: " + ipS +
                " | W:" + iW + " processed: " + ipW +
                " | K:" + iK + " processed: " + ipK +
                " | M:" + iM + " processed: " + ipM +
                " | B:" + iB + " processed: " + ipB +
                " | D:" + iD + " processed: " + ipD +
                " | H:" + iH + " processed: " + ipH +
                " | V:" + iV + " processed: " + ipV +
                "\nN:" + iN + " processed: " + ipN +
                "\nUnknown:" + iUnknown + " processed: " + ipUnknown +
                "\niTotal:" + iTotal + " ipTotal: " + ipTotal +
                "\n---------------------------------------\n";
                txtBoxFASTAStats.Text = strFastaStats;
                strFastaStats += resultLogTextBox.Text;
               

                FileInfo f = new FileInfo(m_strFinalDestinationFolder + "//sequence.fasta");
                long direct_data_file_length = f.Length;

                //embed.html
                strResultFileName = "embed.html";
                FileInfo t = new FileInfo(strResultFileName);
                StreamWriter Tex = t.CreateText();
                StringWriter wr = new StringWriter();
                string embedHTML = @"
<!DOCTYPE html>
<html lang='en'>
<head>
<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
<title>DNA Data Visualization : " + lblSequenceName.Text +
                @"</title>
<script src='../../openseadragon.min.js' type='text/javascript'></script>
<script type='text/javascript' src='../../jquery-1.7.min.js'></script>
<script src='../../openseadragon-scalebar.js' type='text/javascript'></script>

<script type='text/javascript'>
	        var originalImageWidth= " + x + ";" +
                @"
            var originalImageHeight= " + y + ";" +
                @"
            var pixelSize = 2;
            var ColumnPadding = 4;
            var iLineLength = 70;
            var usa='refseq_fetch:" + lblRefSeq.Text + "';" +
                @"          
            var ipTotal = " + ipTotal + ";" +
                @"
            var direct_data_file='sequence.fasta';" +
                @"
            var direct_data_file_length=" + direct_data_file_length + ";" +
                @"
            var sbegin='1';
            var send=ipTotal.toString(); 
            </script>
<script src='../../nucleotideNumber.js' type='text/javascript'></script>
<script src='../../nucleicDensity.js' type='text/javascript'></script>";
                embedHTML = embedHTML + @"<link rel='stylesheet' type='text/css' href='../../seadragon.css' />
    <!-- BIOJS css -->
	<script language='JavaScript' type='text/javascript' src='../../Biojs.js'></script>
	<!-- component code -->
	<script language='JavaScript' type='text/javascript' src='../../Biojs.Sequence.js'></script>
</head>

<body>
<h2 class='mainTitle'>Data Visualization - DNA</h2>
<span style='float:left;'>Menu:&nbsp;</span>
	<ul class='selectChromosome'>
	<li><a href='../'>Select Visualization</a></li>
	 </ul>
<h2 class='mainTitle'><strong>" + lblSequenceName.Text +
                            @"</strong> 
 </h2>

<div id='container'>
</div>

<p class='legendHeading'><strong>Legend:</strong><br /></p><div style='margin-left:50px;'>";
                if (iA != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-A.png' />"; }
                if (iT != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-T.png' />"; }
                if (iG != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-G.png' />"; }
                if (iC != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-C.png' />"; }
                if (iR != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-R.png' />"; }
                if (iY != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-Y.png' />"; }
                if (iS != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-S.png' />"; }
                if (iW != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-W.png' />"; }
                if (iK != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-K.png' />"; }
                if (iM != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-M.png' />"; }
                if (iB != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-B.png' />"; }
                if (iD != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-D.png' />"; }
                if (iH != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-H.png' />"; }
                if (iV != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-V.png' />"; }
                if (iN != 0) { embedHTML = embedHTML + @"<img src='../../LEGEND-N.png' />"; }
                embedHTML = embedHTML + @"<img src='../../LEGEND-bg.png' /></div>

<script type='text/javascript'>
	        outputTable();";
                if (chckIncludeDensity.Checked == true) { embedHTML = embedHTML + @"outputDensityUI();"; }
                else { embedHTML = embedHTML + @"outputStatusResultUI();"; }
                embedHTML = embedHTML + @"</script>

<div class='legend-details'>
<h3>Data Source:</h3>

<a href='sequence.fasta'>FASTA file</a><br />";


                if (gi != "")
                {
                    embedHTML = embedHTML + @"
NCBI (gi): <a href='http://www.ncbi.nlm.nih.gov/nuccore/" + gi + @"'>http://www.ncbi.nlm.nih.gov/nuccore/" + gi + @"</a><br />";
                }
                else
                    {
                        embedHTML = embedHTML + @"
Custom/local sequence (DDV seq ID): "+DDVseqID+"<br />";
                    } 

                embedHTML = embedHTML + @"

<h3>Notes</h3>
This DNA data visualization interface was generated with <a href='https://bitbucket.org/tneugebauer/ddv'>DDV</a><br />Date Visualization Created:" + DateTime.Now.ToString("d/MM/yyyy") + @"
<script type='text/javascript'>
	        otherCredits();
</script>
</div>
</body>

</html>
            ";
                Tex.Write(embedHTML);
                MessageBoxShow("File generated " + strResultFileName);
                wr.Close();
                Tex.Close();

                //move file
                sourceFile = strResultFileName;
                destinationFile = TheFile.DirectoryName + Path.DirectorySeparatorChar + strResultFileName;
                MoveWithReplace(sourceFile, destinationFile);
                MessageBoxShow("File moved to -> " + destinationFile);

                //end of embed.html
                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

                MessageBoxShow("Completed.");
                MessageBoxShow("Image and interface files generated. Click on Process Image with Deepzoom for the final step.");
                //enable deepzoomprocessing
                btnProcessBitmapDeepZoom.Enabled = true;

                
            };
           
        }

        //Generates Bitmap from Data
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeMakeBitmap();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Path is a null reference.");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("The caller does not have the " +
                    "required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }
            

        }

       

        public static int CountOccurencesOfChar(string instance, char c)
        {
            int result = 0;
            foreach (char curChar in instance)
            {
                if (c == curChar)
                {
                    result++;
                }
            }
            return result;
        }

        public static void CopyDirectory(string source, string destination)
        {
            if (destination[destination.Length - 1] != Path.DirectorySeparatorChar)
            {
                destination += Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            var entries = Directory.GetFileSystemEntries(source);
            foreach (var e in entries)
            {
                if (Directory.Exists(e))
                {
                    CopyDirectory(e, destination + Path.GetFileName(e));
                }
                else
                {
                    File.Copy(e, destination + Path.GetFileName(e), true);
                }
            }
        }

        public static void MoveWithReplace(string sourceFileName, string destFileName)
        {
            //check if the source is not the same as destination
            if (sourceFileName != destFileName)
            {
                //first, delete target file if exists, as File.Move() does not support overwrite
                if (File.Exists(destFileName))
                {
                    File.Delete(destFileName);
                }

                File.Move(sourceFileName, destFileName);
            }

        }

        public static void CopyFileWithReplaceIfNewer(string sourceFileName, string destFileName)
        {
            //check if the source is not the same as destination
            if (sourceFileName != destFileName)
            {
                FileInfo file = new FileInfo(sourceFileName);
                FileInfo destFile = new FileInfo(destFileName);
                if (destFile.Exists)
                {
                    if (file.LastWriteTime > destFile.LastWriteTime)
                    {
                        //MOVE File with Overwrite since it is newer
                        File.Copy(sourceFileName, destFileName, true);
                    }
                }
                else
                {
                    //DestFile does not exist, so just move it there
                    File.Copy(sourceFileName, destFileName);
                }
            }

        }

        public static void CopyFileNoReplace(string sourceFileName, string destFileName)
        {
            if (!File.Exists(destFileName))
            {
                File.Copy(sourceFileName, destFileName, false);
            } 
        }

        public static void MoveDirectory(string source, string destination)
        {
            CopyDirectory(source, destination);
            Directory.Delete(source, true);
        }

        //Process Bitmap with DeepZoomTools.dll
        //calls on the DeepZoomTools.dll to generate the deepzoom tiles
        //Tile size default is 256, but user can change it using txtTileSize
        //After it is complete, it moves all of the files into proper subfolders
        //renames "embed.html" file to index.html
        private void button12_Click(object sender, EventArgs e)
        {
            MessageBoxClear();
            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 40;
            progressBar1.Value += 1;

            //move source image into source folder
            // Remove path from the file name.
            string fName = System.IO.Path.GetFileName(m_strSourceBitmapFile);
            string fNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(m_strSourceBitmapFile);
            string fPathName = System.IO.Path.GetDirectoryName(m_strSourceBitmapFile);
            //string finalDestinationPath = m_strFinalDestinationFolder; 
            string fImageDestinationPath = @Directory.GetCurrentDirectory()+"\\source\\"; ;
            string destinationFile = fImageDestinationPath+fName;
            if (!Directory.Exists(fImageDestinationPath)){
             MessageBoxShow("Source folder does not exist, creating");
                Directory.CreateDirectory(fImageDestinationPath);
            }
            MessageBoxShow("Moving "+fName+" from "+fPathName+" to "+destinationFile);
            // To move source image to source folder:
            if (!File.Exists(m_strSourceBitmapFile)) { MessageBox.Show("Error: could not locate source image file.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);return; }
            MoveWithReplace(m_strSourceBitmapFile, destinationFile);
            
            //default tile size is 256, but check interface for user entered value
            int txtTileSize = 256;
            try
            {
                txtTileSize = Convert.ToInt32(textBoxTileSize.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Please put only numbers (1-1048) in Tile size");
            }

            //Deep Zoom processing
            MessageBoxShow("Attempting to initialize DeepZoomTools.dll.");
            
            try
            {
                
                //This runs Deep Zoom Tools as a separate thread in backgroundworker
                List<object> DZparams = new List<object>();
                DZparams.Add(destinationFile);
                DZparams.Add(fNameNoExtension);
                DZparams.Add(txtTileSize);

                BackgroundWorker bwDZ = new BackgroundWorker();
                bwDZ.DoWork += new DoWorkEventHandler(DeepZoomDLL_doWork);
                bwDZ.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CopyFilesIntoOutputFolder);
                bwDZ.RunWorkerAsync(DZparams);
                 

                /* this would run deep zoom tools.dll in the same thread.
                ImageCreator imageCreator = new ImageCreator();
                //CollectionCreator collectionCreator = new CollectionCreator();
                imageCreator.TileSize = txtTileSize;
                imageCreator.TileOverlap = 1;
                imageCreator.TileFormat = Microsoft.DeepZoomTools.ImageFormat.Png;
                imageCreator.Create("source\\" + fName, "output");
                Microsoft.DeepZoomTools.Image img = new Microsoft.DeepZoomTools.Image(fName);
                */

                /* this runs DZConvert.exe as a process 
                Process compiler = new Process();
                compiler.StartInfo.CreateNoWindow = true;
                compiler.StartInfo.FileName = "Dzconvert.exe";
                compiler.StartInfo.Arguments = "source\\" + fName + " output /tf:png /ts:"+txtTileSize+" /ov:1";
                compiler.StartInfo.UseShellExecute = false;
                compiler.StartInfo.RedirectStandardOutput = true;
                compiler.StartInfo.RedirectStandardError = true;
                MessageBoxShow("Calling function: " + compiler.StartInfo.FileName + compiler.StartInfo.Arguments);
                compiler.Start();
                MessageBoxShow(compiler.StandardOutput.ReadToEnd());
                MessageBoxShow(compiler.StandardError.ReadToEnd());
                compiler.WaitForExit();
                */
                
                progressBar1.Value += 2;
                progressBar1.Update();
                progressBar1.Refresh();

                MessageBoxShow("Processing with DeepZoomTools.dll, please wait...");

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;
                
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Path is a null reference.");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("The caller does not have the " +
                    "required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }
        }

        private void CopyFilesIntoOutputFolder(object sender, EventArgs e)
        {
            string fName = System.IO.Path.GetFileName(m_strSourceBitmapFile);
            string fNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(m_strSourceBitmapFile);
            string fPathName = System.IO.Path.GetDirectoryName(m_strSourceBitmapFile);
            string finalDestinationPath = m_strFinalDestinationFolder;

            progressBar1.Value += 2;
            progressBar1.Update();
            progressBar1.Refresh();

            MessageBoxShow("Finished Processing with DeepZoomTools.dll.  Preparing final folders.");

            try
            {
            //moving generated folders
                string strSource=@Directory.GetCurrentDirectory() + "\\output\\" + fNameNoExtension + "_files";
                string strDestination = @Directory.GetCurrentDirectory() + "\\output\\"+fNameNoExtension+"\\GeneratedImages\\dzc_output_files";
                MessageBoxShow("Moving " + strSource + " to " + strDestination);
                MoveDirectory(strSource, strDestination);

                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //moving generated xml file
                strSource = @Directory.GetCurrentDirectory() + "\\output\\" + fNameNoExtension + ".xml";
                strDestination = @Directory.GetCurrentDirectory() + "\\output\\" + fNameNoExtension + "\\GeneratedImages\\dzc_output.xml";
                MessageBoxShow("Moving " + strSource + " to " + strDestination);
                //File.Move(strSource, strDestination);
                MoveWithReplace(strSource, strDestination);

                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                // To move generated "embed" file to output folder, rename it to index.html
                strSource = fPathName+"\\embed.html";
                strDestination = @Directory.GetCurrentDirectory() + "\\output\\" + fNameNoExtension + "\\index.html";
                MessageBoxShow("Moving " + strSource + " to " + strDestination);
                MoveWithReplace(strSource, strDestination);

                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();


                //moving generated folders 
                strSource = @Directory.GetCurrentDirectory() + "\\output\\" + fNameNoExtension;
                if (gi != "") { strDestination = finalDestinationPath + "dnadata\\nuccore" + gi; }
                else { strDestination = finalDestinationPath + "dnadata\\" + DDVseqID; }
                MessageBoxShow("Moving Results" + strSource + " to " + strDestination);
                MoveDirectory(strSource, strDestination);

                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //moving [img] folder into place
                strSource = @Directory.GetCurrentDirectory() + "\\img";
                if (gi != "") { strDestination = finalDestinationPath + "dnadata\\nuccore" + gi + "\\img"; }
                else { strDestination = finalDestinationPath + "dnadata\\" + DDVseqID + "\\img"; }
                MessageBoxShow("Copying images" + strSource + " to " + strDestination);
                if (!(Directory.Exists(strDestination)))
                {
                    CopyDirectory(strSource, strDestination);
                    MessageBoxShow("Done.");
                }
                else
                {
                    MessageBoxShow("Folder already exists, skipping");
                }
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\openseadragon.min.js"; ;
                strDestination = finalDestinationPath + "openseadragon.min.js";
                MessageBoxShow("Copying scripts" + strSource + " to " + strDestination);
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\openseadragon-scalebar.js"; ;
                strDestination = finalDestinationPath + "openseadragon-scalebar.js";
                MessageBoxShow("Copying scripts" + strSource + " to " + strDestination);
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\openseadragon.min.js.map"; ;
                strDestination = finalDestinationPath + "openseadragon.min.js.map";
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\Biojs.js"; ;
                strDestination = finalDestinationPath + "Biojs.js";
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\Biojs.Sequence.js"; ;
                strDestination = finalDestinationPath + "Biojs.Sequence.js";
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                // To move donwloaded sequence file to final folder
                strSource = fPathName + "\\sequence.fasta";
                if (File.Exists(strSource))
                {
                    if (gi != "") { strDestination = finalDestinationPath + "dnadata\\nuccore" + gi + "\\sequence.fasta"; }
                    else { strDestination = finalDestinationPath + "dnadata\\" + DDVseqID + "\\sequence.fasta"; }
                    MessageBoxShow("Copying " + strSource + " to " + strDestination);
                    File.Copy(strSource, strDestination, true);
                }

                // To move donwloaded sequence-info.xml to final folder
                strSource = fPathName + "\\sequence-info.xml";
                if (File.Exists(strSource))
                {
                    if (gi != "") { strDestination = finalDestinationPath + "dnadata\\nuccore" + gi + "\\sequence-info.xml"; }
                    else { strDestination = finalDestinationPath + "dnadata\\" + DDVseqID + "\\sequence-info.xml"; }
                    MessageBoxShow("Moving " + strSource + " to " + strDestination);
                    File.Copy(strSource, strDestination, true);
                    File.Delete(strSource);
                }

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\seadragon.css"; ;
                strDestination = finalDestinationPath + "seadragon.css";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\nucleotideNumber.js";
                strDestination = finalDestinationPath + "nucleotideNumber.js";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileWithReplaceIfNewer(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\jquery-1.7.min.js";
                strDestination = finalDestinationPath + "jquery-1.7.min.js";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\nucleicDensity.js";
                strDestination = finalDestinationPath + "nucleicDensity.js";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\d3.v3.js";
                strDestination = finalDestinationPath + "d3.v3.js";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\loading.gif";
                strDestination = finalDestinationPath + "loading.gif";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-A.png";
                strDestination = finalDestinationPath + "LEGEND-A.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-B.png";
                strDestination = finalDestinationPath + "LEGEND-B.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-bg.png";
                strDestination = finalDestinationPath + "LEGEND-bg.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-C.png";
                strDestination = finalDestinationPath + "LEGEND-C.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-D.png";
                strDestination = finalDestinationPath + "LEGEND-D.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-G.png";
                strDestination = finalDestinationPath + "LEGEND-G.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-H.png";
                strDestination = finalDestinationPath + "LEGEND-H.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-K.png";
                strDestination = finalDestinationPath + "LEGEND-K.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-M.png";
                strDestination = finalDestinationPath + "LEGEND-M.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-N.png";
                strDestination = finalDestinationPath + "LEGEND-N.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-R.png";
                strDestination = finalDestinationPath + "LEGEND-R.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-S.png";
                strDestination = finalDestinationPath + "LEGEND-S.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-T.png";
                strDestination = finalDestinationPath + "LEGEND-T.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-V.png";
                strDestination = finalDestinationPath + "LEGEND-V.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-W.png";
                strDestination = finalDestinationPath + "LEGEND-W.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\LEGEND-Y.png";
                strDestination = finalDestinationPath + "LEGEND-Y.png";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                //copy shared files into output folder
                strSource = @Directory.GetCurrentDirectory() + "\\density.php";
                strDestination = finalDestinationPath + "density.php";
                MessageBoxShow("Copying " + strSource + " to " + strDestination);
                CopyFileNoReplace(strSource, strDestination);
                progressBar1.Value += 1;
                progressBar1.Update();
                progressBar1.Refresh();

                MessageBoxShow("Completed. ");

                //Enable link to Generated Interfaces
                btnGeneratedIntefaces.Enabled = true;
                

                //Go to current result
                string strResultURL = "";
                if (gi != "")
                {
                    strResultURL = "http://localhost:1818/dnadata/nuccore" + fNameNoExtension + "/index.html";
                }
                else
                {
                    strResultURL = "http://localhost:1818/dnadata/" + fNameNoExtension + "/index.html";
     
                }
                MessageBoxShow("Opening Result "+strResultURL);
                lnkLatestInterface.Text = strResultURL;
                lnkLatestInterface.Enabled = true;
                try
                {
                    System.Diagnostics.Process.Start(strResultURL);
                }
                catch
                    (
                     System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;
                
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Path is a null reference.");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("The caller does not have the " +
                    "required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Download FASTA file from NIH
            if (txtGI.Text == "")
            {
                MessageBox.Show("Please enter valid GI or select local data file.");
                return;
            }
            MessageBoxClear();
             // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;
            WebClient Client = new WebClient();
            
            string strDownload = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=nucleotide&rettype=fasta&id=" + txtGI.Text;
            string strDownloadInfo = "http://eutils.ncbi.nlm.nih.gov/entrez/eutils/efetch.fcgi?db=nucleotide&rettype=docsum&id=" + txtGI.Text;;
            string strDestinationInfo = @Directory.GetCurrentDirectory() + "\\output\\sequence-info.xml";
            string strDestination = @Directory.GetCurrentDirectory() + "\\output\\sequence.fasta";
            //if file exists, delete it
            if (File.Exists(strDestinationInfo))
            {
                File.Delete(strDestinationInfo);
                MessageBoxShow("Deleting cached sequence info...");
            }
            //if file exists, delete it
            if (File.Exists(strDestination)){
                File.Delete(strDestination);
                MessageBoxShow("Deleting cached sequence...");
            }
            //if output Directory not there, make it
            if (!(Directory.Exists(@Directory.GetCurrentDirectory() + "\\output"))){
                Directory.CreateDirectory(@Directory.GetCurrentDirectory() + "\\output");
            }
            MessageBoxShow("Attempting to download file...please wait");
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            btnGenerateImage.Enabled = true;
            btnReadSequenceProperties.Enabled = true;
            try
            {
                progressBar1.Value = 2;
                MessageBoxShow("Attempting download of docsummary.");
                WebClient client = new WebClient();
                client.DownloadFile(strDownloadInfo, strDestinationInfo);
                MessageBoxShow("Downloaded docsummary:");
                string strDocSummary = File.ReadAllText(strDestinationInfo);
                MessageBoxShow(strDocSummary);
                MessageBoxShow("Downloading FASTA sequence. Please wait...");
                int bytesLength = 0;
                XmlReaderSettings xmlreadersettings = new XmlReaderSettings();
                xmlreadersettings.DtdProcessing = DtdProcessing.Ignore;
                using (XmlReader reader = XmlReader.Create(new StringReader(strDocSummary), xmlreadersettings)) 
                {
                    string s = "";
                    while (s != "Length")
                    {
                        reader.ReadToFollowing("Item");
                        reader.MoveToAttribute("Name");
                        s = reader.Value;
                    }
                    reader.MoveToContent();
                    bytesLength = reader.ReadElementContentAsInt();
                    //MessageBoxShow(bytesLength.ToString());
                }
                progressBar1.Maximum = bytesLength+1000;
	            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
	            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                // Starts the download
                client.DownloadFileAsync(new Uri(strDownload), strDestination);	 
                // Reset UI
                btnDownloadFASTA.Enabled = false;
                DisableGenerateUI();
            }
            catch (Exception ex)
            {
                if (strDownload == "")
                {
                    MessageBox.Show("Please enter valid URL or select local data file");
                }
                else
                {
                    MessageBox.Show("Downloading failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
                }

            }
            
            
        }

        void DisableGenerateUI()
        {
                btnProcessBitmapDeepZoom.Enabled = false;
                btnGenerateImage.Enabled = false;
                btnReadSequenceProperties.Enabled = false;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if ((int)e.BytesReceived < progressBar1.Maximum)
            {
                progressBar1.Value = (int)e.BytesReceived;
                progressBar1.Update();
                progressBar1.Refresh();
            }
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBoxShow("Download Completed.");
            btnDownloadFASTA.Enabled = true;
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
            //Clear variables
            BitmapClear();
            string strDestination = @Directory.GetCurrentDirectory() + "\\output\\sequence.fasta";
            SetSourceSequence(strDestination);
        }
        private void SetSourceSequence(string strDestination){
            lblSourceSequence.Text = strDestination;
            m_strSourceFile = lblSourceSequence.Text;
            CleanInterfaceNewSequence();
            btnGenerateImage.Enabled = true;
            btnReadSequenceProperties.Enabled = true;
         }

        private void CleanInterfaceNewSequence()
        {
            lblDataLength.Text = "";
            lblSequenceName.Text = "";
            txtBoxFASTAStats.Text = "";
            lblRefSeq.Text = "";
            progressBar1.Value = 0;
        }

        private void BitmapClear()
        {
            m_strSourceBitmapFile = "";
            lblSourceBitmapFilename.Text = "";    
            //CLEAR functions associated with bitmap
            btnProcessBitmapDeepZoom.Enabled = false;
        }

        private void BitmapSet(string destinationFile)
        {
            m_strSourceBitmapFile = destinationFile;
            lblSourceBitmapFilename.Text = destinationFile;
            MessageBoxShow("Image file set to -> " + destinationFile + " for processing.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set cursor as wait 
            Cursor.Current = Cursors.WaitCursor;
            int length = populateInfo();
            if (length == 0)
            {
                btnGenerateImage.Enabled = false;
                MessageBoxShow("Cannot generate image from this FASTA file.");
            }
            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

     
       
        private void launchCivetweb(){


            //Run Civetweb for localhost to be created in output folder
            //for nucleic_density applications
            try
            {
            //make sure output folder exists, if it doesn't, create it
            //initialize Civetweb is set to use this location as default localhost
            if (!Directory.Exists("output")) { Directory.CreateDirectory("output"); }

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(Civetweb_doWork);
            bw.RunWorkerAsync();
            

            MessageBoxShow("Launching Civetweb");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Path is a null reference.");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("The caller does not have the " +
                    "required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }

        }

        private void DeepZoomDLL_doWork(object sender, DoWorkEventArgs e)
        {

                List <object> genericlist = e.Argument as List <object>;
                string destinationFile = (string) genericlist[0];
                string fNameNoExtension = (string) genericlist[1];
                int txtTileSize = (int) genericlist[2];

                //MessageBox.Show(fNameNoExtension);

                ImageCreator ic = new ImageCreator();
                ic.TileSize = txtTileSize;
                ic.TileOverlap = 1;
                ic.TileFormat = Microsoft.DeepZoomTools.ImageFormat.Png;
                //ic.UseOptimizations = true;
                ic.Create(destinationFile, "output\\" + fNameNoExtension);

               
        }

        private void Civetweb_doWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                m_prcCivetweb = new Process();
                m_prcCivetweb.StartInfo.CreateNoWindow = true;
                m_prcCivetweb.StartInfo.FileName = "civetweb.exe";
                m_prcCivetweb.StartInfo.UseShellExecute = false;
                //m_prcCivetweb.StartInfo.RedirectStandardOutput = true;
                //m_prcCivetweb.StartInfo.RedirectStandardError = true;
                MessageBoxShow("Calling function: " + m_prcCivetweb.StartInfo.FileName + m_prcCivetweb.StartInfo.Arguments);
                m_prcCivetweb.Start();
                m_prcCivetweb.WaitForExit();
            
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("Error: The caller does not have the required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error: ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }
               
           
        }

        private void killCivetweb()
        {
            
            try
            {
                m_prcCivetweb.Kill();
                m_prcCivetweb.Dispose();

             }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Path is a null reference.");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("The caller does not have the " +
                    "required permission.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("ArgumentException Error has occurred.  ");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Error.  IO Exception has occurred.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Processing failed with the following error message: " + Environment.NewLine + Environment.NewLine + ex.Message, ex.GetType().Name);
            }
        }

        private void btnFinalDestinationFolder_Click(object sender, EventArgs e)
        {
            if (m_strFinalDestinationFolder != "")
            {
                Process.Start(m_strFinalDestinationFolder);
            }
            else
            {
                MessageBox.Show("Output folder not set.");
            }
            /*
            DialogResult dr = fDlgFinalDestination.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (fDlgFinalDestination.SelectedPath == "")
                {
                    MessageBox.Show("Please select destination folder.  The generated interface files will be placed under this folder.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SetFinalDestinationFolder(fDlgFinalDestination.SelectedPath);
            }
            */


        }

        private void SetFinalDestinationFolder(string path)
        {
            m_strFinalDestinationFolder = path;
            lblOutputPath.Text = m_strFinalDestinationFolder;
        }

        private void btnGeneratedIntefaces_Click(object sender, EventArgs e)
        {
            //Go to current result
            string strResultURL = "http://localhost:1818/dnadata/";
            MessageBoxShow("Opening Result " + strResultURL);
            try
            {
                System.Diagnostics.Process.Start(strResultURL);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void lnkLatestInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Go to current result
            string strResultURL = lnkLatestInterface.Text;
            MessageBoxShow("Opening Result " + strResultURL);
            try
            {
                System.Diagnostics.Process.Start(strResultURL);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgImageFileSet.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (dlgImageFileSet.FileName != ""){
                    BitmapSet(dlgImageFileSet.FileName);
                    btnProcessBitmapDeepZoom.Enabled=true;
                }
            }
        }
         

     
	}
}

            