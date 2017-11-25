using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;

namespace HaarObjectMarker
{
    public partial class Negatives : Form
    {
        public Negatives()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           if (  this.folderBrowserDialog1.ShowDialog ( ) == DialogResult.OK )
           {

               this.label1.Text = this.folderBrowserDialog1.SelectedPath;
               string [] names  = Directory.GetFiles(label1.Text );
               //Use might change the selected folder before saving...Refresh the listbox content...
               listBox1.Items.Clear();
               //After choosing the directory , we are taking the file names and adding it to listBox

               foreach (string el in names)
               {
                   string[] sep = el.Split('\\');
                   //If the file is BMP , JPG or PNG then add it to list....
                   if (sep[sep.Length - 1].Contains(".jpg") || sep[sep.Length - 1].Contains(".png") || sep[sep.Length - 1].Contains(".bmp") )
                   listBox1.Items.Add( sep [ sep.Length -2] + "/" + sep[ sep.Length -1 ] );
               }
               

           }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( textBox1.Text == "" )
            {
                MessageBox.Show("Please write the file name");
                return;
            }
            if ( listBox1.Items.Count == 0 )
            {
                MessageBox.Show("There is no content to write to file...");
                return;
            }

            
            //The index file will be saved to the same directory with the images...
            StreamWriter dosya = new StreamWriter(label1.Text + "\\" + textBox1.Text );
            for (int _i = 0 ; _i < listBox1.Items.Count ; _i++ )
            {
                dosya.WriteLine( listBox1.Items[ _i].ToString () );
                
            }

            dosya.Close();

        }
    }
}
