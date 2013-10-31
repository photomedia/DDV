using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace GENE
{

	

	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(88, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(224, 56);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 269);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

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

		private void button1_Click(object sender, System.EventArgs e)
		{
			//StreamReader s = File.OpenText("hs_phase0.fna");
			StreamReader s = File.OpenText("hs_oat.fna");
			string read = null;
			string first = null;
			string second = null;
			string third = null;
			string fourth = null;
			string fifth = null;
			string sixth = null;
			string hex = null;
			FileInfo t = new FileInfo("output.html");
			StreamWriter Tex =t.CreateText();
			StringWriter wr = new StringWriter();

			string strTable="<html><head></head><body><table style=\"float:left;margin-right:10px;\" cellspacing=\"2\" border=\"1\" align=\"left\">";
			string strRow="<tr>";
			string strFront="<td style=\"background-color:#";
			string strBack="\">";
			string strFront2="<a title=\"";
			string strBack2="\">&nbsp;</a></td>";
			string strRowEnd="</tr>";
			string strTableEnd="</table></body></html>";

				Tex.Write(strTable);
			
			int i=0;
			bool end=false;

			string firstLetter=null;

			while (((read = s.ReadLine()) != null)&&!end)
			{
				
				read=ConvertToDigits(read);
				Tex.Write(strRow);


				firstLetter=read.Substring(0, 1);

				if (firstLetter==">")
				{
					if (i>1){end=true;}
					else
					{
						Tex.Write("</tr></table><br clear=\"left\"><br>");
						Tex.Write("<p>"+read+"</p>");
						Tex.Write(strTable);
					}
					
				}

				else
				{


					if (read.Length>=10)
					{
						first=read.Substring(0, 10);
						hex = BaseConverter.ToBase(first, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);	
						Tex.Write(strBack);
						Tex.Write(strFront2);
						first=ConvertToTGACN(first);
						Tex.Write(first);	
						Tex.Write(strBack2);
					}
					
					if (read.Length>=20)
					{
						second=read.Substring(10, 10);
						hex = BaseConverter.ToBase(second, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);		
						Tex.Write(strBack);	
						Tex.Write(strFront2);
						second=ConvertToTGACN(second);
						Tex.Write(second);	
						Tex.Write(strBack2);
					}
					if (read.Length>=30)
					{
						third=read.Substring(20, 10);
						hex = BaseConverter.ToBase(third, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);		
						Tex.Write(strBack);
						Tex.Write(strFront2);
						third=ConvertToTGACN(third);
						Tex.Write(third);	
						Tex.Write(strBack2);
					}
					if (read.Length>=40)
					{
						fourth=read.Substring(30, 10);
						hex = BaseConverter.ToBase(fourth, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);		
						Tex.Write(strBack);	
						Tex.Write(strFront2);
						fourth=ConvertToTGACN(fourth);
						Tex.Write(fourth);	
						Tex.Write(strBack2);
					}

					if (read.Length>=50)
					{
						fifth=read.Substring(40, 10);
						hex = BaseConverter.ToBase(fifth, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);		
						Tex.Write(strBack);	
						Tex.Write(strFront2);
						fifth=ConvertToTGACN(fifth);
						Tex.Write(fifth);	
						Tex.Write(strBack2);
					}

					if (read.Length>=60)
					{
						sixth=read.Substring(50, 10);
						hex = BaseConverter.ToBase(sixth, 4, 16);
						//hex=Correct(hex);
						Tex.Write(strFront);
						Tex.Write(hex);		
						Tex.Write(strBack);	
						Tex.Write(strFront2);
						sixth=ConvertToTGACN(sixth);
						Tex.Write(sixth);	
						Tex.Write(strBack2);
					}
	
					Tex.Write(strRowEnd);
					Tex.Write(wr.NewLine);
					i++;
					if ((i%50)==0){Tex.Write("</tr></table><table style=\"float:left;margin-right:10px;\" cellspacing=\"2\" border=\"1\" align=\"left\"><tr>");}
				}

			}
			Tex.Write(strTableEnd);


			s.Close();
			Tex.Close();

				
		
		}

		public string Correct(string strCorrect)
		{
			
		
			if (strCorrect.Length==3){strCorrect=strCorrect+"000";}
			if (strCorrect.Length==4){strCorrect=strCorrect+"00";}
			if (strCorrect.Length==5){strCorrect=strCorrect+"0";}
			if (strCorrect.Length>6){strCorrect=strCorrect.Substring(0, 6);}
		
		 	return strCorrect;
		}

		public string ConvertToDigits(string strTGACN)
		{	
			//Create a new Random class in C#
			Random RandomClass = new Random();
			int RandomNumber = RandomClass.Next(0, 3);
			strTGACN.Replace("N", RandomNumber.ToString());


			strTGACN.Replace("T", "0");
			strTGACN.Replace("A", "1");
			strTGACN.Replace("G", "2");
			strTGACN.Replace("C", "3");
			
			return strTGACN;
		}

		public string ConvertToTGACN(string strDigits)
		{
			
			//strDigits.Replace("0", "N");

			strDigits.Replace("0", "T");
			strDigits.Replace("1", "A");
			strDigits.Replace("2", "G");
			strDigits.Replace("3", "C");
			return strDigits;
		}
	}
}
