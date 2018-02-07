using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox3.Text))
            {
                MessageBox.Show("请输入版本号");
                return;
            }
            if (Directory.Exists(this.textBox1.Text))
            {
                string[] fileNames = Directory.GetFiles(this.textBox1.Text);
                if (fileNames.Length > 0)
                {
                    string str = "";
                    foreach (string fileName in fileNames)
                    {
                        FileInfo fi = new FileInfo(fileName);
                        str += "<File Ver=\"" + this.textBox3.Text + "\" Name= \"" + fi.Name + "\" />\n";
                    }
                    this.textBox2.Text = str;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.textBox1.Text))
            {
                string[] fileNames = Directory.GetFiles(this.textBox1.Text);
                if (fileNames.Length > 0)
                {
                    string str = "";
                    int i = 0;
                    foreach (string fileName in fileNames)
                    {
                        i++;
                        FileInfo fi = new FileInfo(fileName);
                        str += "insert into PlayLists(Code,Name,BeginTime,EndTime,IsEnabled) values('" + this.textBox6.Text + i.ToString().PadLeft(2, '0') + "','" + fi.Name + "','" + this.textBox4.Text + "','" + this.textBox5.Text + "',1)\n";
                    }
                    this.textBox2.Text = str;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (Directory.Exists(this.textBox1.Text))
            //{
                DXInfo.Models.ExcelLoad el = new DXInfo.Models.ExcelLoad();
                el.InitConf(this.textBox1.Text);
            //}
        }
    }
}
