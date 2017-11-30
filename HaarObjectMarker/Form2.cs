using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;



namespace HaarObjectMarker
{
    public partial class Form2 : Form
    {
        private ArrayList lines;
        public Form2( ArrayList lines )
        {
            InitializeComponent();
            this.lines = lines;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();


            foreach (string eleman in this.lines)
                listBox1.Items.Add(eleman);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = this.label1.Text +  "\\" + textBox1.Text;
            StreamWriter dosya = new StreamWriter(filename);

            foreach ( string a in this.lines )
            {
                dosya.WriteLine(a);



            }

            dosya.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( this.folderBrowserDialog1.ShowDialog() == DialogResult.OK )
            {
                label1.Text = this.folderBrowserDialog1.SelectedPath;

            }







        }

       

       

        
    }
}
