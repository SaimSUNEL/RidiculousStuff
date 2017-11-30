using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections ;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HaarObjectMarker
{
    public partial class Form1 : Form
    {

        private Form2 form2 = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = this.folderBrowserDialog1.SelectedPath;
                this.files = Directory.GetFiles(textBox1.Text);

            }
            else return;

            //User might want to change the directory for the positive samples...
            this.file_index = -1;

            form2 = new Form2( this.liste  );

            int count = 0 ;
            foreach (string file_name in this.files)
            {
                //MessageBox.Show(file_name);
                if (file_name.Contains(".jpg") || file_name.Contains(".png") || file_name.Contains(".bmp")) count ++;
                
            }

           // MessageBox.Show("File count : " + count);

            this.unprocessed_count = count;
            this.processed_count = 0;
            label3.Text = "" + processed_count;
            label4.Text = "" + unprocessed_count;
        }

        private String[] files;
        private int file_index =  -1 ;


        private void button3_Click(object sender, EventArgs e)
        {
            
            
            
            
            if ( textBox1.Text == "" )
            {

                MessageBox.Show("Please select and folder");
                return; 
            }


            if ( listBox1.Items.Count > 0  && label2.Text == "Not Added" )
            {

                MessageBox.Show("You have forgotten to add the line to list....");
                return;

            }




            label2.Text = "Not Added";
            listBox1.Items.Clear();





            this.file_index += 1;
            if (this.file_index >= this.files.Length)
            {
                this.file_index = 0;


            }
            string file_name = this.files[this.file_index] ;

            if( file_name.Contains( ".jpg" ) || file_name.Contains( ".png" ) || file_name.Contains( ".bmp" ) );
            else return ;
            //MessageBox.Show(file_name);

            if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();
            this.pictureBox1.Image = Image.FromFile( file_name );

            if (this.copy_image != null)
                copy_image.Dispose();
            copy_image = ( Image )pictureBox1.Image.Clone();
            this.pictureBox1.Width = this.pictureBox1.Image.Width;
            this.pictureBox1.Height = this.pictureBox1.Image.Height;
            this.label1.Text = file_name;

            string[] id = file_name.Split('\\');
            string fil = id[id.Length - 1];


            temp_ = false;

            foreach (string s in this.liste)
            {
                if (s.Contains(fil))
                    temp_ = true;
            }

            if (temp_ == true )
            {
                this.label5.Text = "This file has been processed...";
            }
            else
                this.label5.Text = "This file has not been processed...";


        }

        private bool temp_;

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" )
            {

                MessageBox.Show("Please select and folder");
                return;
            }
            label2.Text = "Not Added";
            listBox1.Items.Clear();

            this.file_index -= 1;
            if (this.file_index <  0 )
            {
                this.file_index = this.files.Length -1 ;


            }

            string file_name = this.files[this.file_index];

            if (file_name.Contains(".jpg") || file_name.Contains(".png") || file_name.Contains(".bmp")) ;
            else return;
            //MessageBox.Show(file_name);

            if (this.pictureBox1.Image != null) this.pictureBox1.Image.Dispose();
            this.pictureBox1.Image = Image.FromFile(file_name);

            if (this.copy_image != null)
                copy_image.Dispose();
            copy_image = (Image)pictureBox1.Image.Clone();
           
            this.pictureBox1.Width = this.pictureBox1.Image.Width;
            this.pictureBox1.Height = this.pictureBox1.Image.Height;
            this.label1.Text = file_name;
            
            string[] id = file_name.Split('\\');
            string fil = id[id.Length - 1];


            temp_ = false;
            foreach (string s in liste)
                if (s.Contains(fil))
                    temp_ = true;


            if (temp_ == true )
            {
                this.label5.Text = "This file has been processed...";
            }
            else
                this.label5.Text = "This file has not been processed...";

        }

       
        


        private int mouse_start_x, mouse_start_y, mouse_end_x, mouse_end_y;
        private bool drag = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_start_x = e.Location.X ;
            this.mouse_start_y = e.Location.Y;
            drag = true;


        }
        private Image copy_image = null;
        private Pen whitePen = new Pen(Color.Green ,3); 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ( drag == true )
            {


                Graphics graphics = Graphics.FromImage((Image)pictureBox1.Image);
                
                Rectangle rect = new Rectangle(mouse_start_x, mouse_start_y, e.X - mouse_start_x ,e.Y - mouse_start_y );

                graphics.DrawImage(this.copy_image, new Point(0, 0));

                for (int a = 0; a < this.listBox1.Items.Count; a++ )
                {

                    string eleman = this.listBox1.Items[a].ToString() ;
                    string[] elemanlar = eleman.Split(' ');

//foreach( string v in elemanlar )
  //                  MessageBox.Show ( v );

                    if (a == this.listBox1.SelectedIndex)
                        graphics.DrawRectangle(mark, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));

                    else
                        graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));

                   // graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));
                   

                }
                graphics.DrawRectangle(whitePen, rect);
                pictureBox1.Refresh();

                // Calling Dispose() is like calling the destructor of the respective object.
                // Dispose() clears all resources associated with the object, but the object still remains in memory
                // until the system garbage-collects it.
                graphics.Dispose();

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            mouse_end_x = e.X;
            mouse_end_y = e.Y;
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1  .  Items  .  Add  ( mouse_start_x + " "+mouse_start_y  +" " + ( mouse_end_x - mouse_start_x ) + " " + (mouse_end_y - mouse_start_y)     );
            this.comboBox1.SelectedIndex = listBox1.Items.Count;
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private Pen mark =  new Pen(Color.Red , 5 );  

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item to delete");
                return;


            }
            
            listBox1.Items.Remove( listBox1.Items[ listBox1.SelectedIndex ]  ) ;
            this.comboBox1.SelectedIndex = this.listBox1.Items.Count; 

            Graphics graphics = Graphics.FromImage((Image)pictureBox1.Image);

            
            graphics.DrawImage(this.copy_image, new Point(0, 0));

            for (int c = 0;c < this.listBox1.Items.Count; c++)
            {

                string eleman = this.listBox1.Items[c].ToString();
                string[] elemanlar = eleman.Split(' ');

                //foreach( string v in elemanlar )
                //                  MessageBox.Show ( v );

                if (c == this.listBox1.SelectedIndex)
                    graphics.DrawRectangle(mark, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));

                else
                    graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));

               // graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));


            }
           
            pictureBox1.Refresh();

            // Calling Dispose() is like calling the destructor of the respective object.
            // Dispose() clears all resources associated with the object, but the object still remains in memory
            // until the system garbage-collects it.
            graphics.Dispose();



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Graphics graphics = Graphics.FromImage((Image)pictureBox1.Image);


            graphics.DrawImage(this.copy_image, new Point(0, 0));

            for (int c = 0; c < this.listBox1.Items.Count; c++)
            {

                string eleman = this.listBox1.Items[c].ToString();
                string[] elemanlar = eleman.Split(' ');

                //foreach( string v in elemanlar )
                //                  MessageBox.Show ( v );

                if ( c == this.listBox1.SelectedIndex )
                    graphics.DrawRectangle(mark, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));

                else
                graphics.DrawRectangle(whitePen, new Rectangle(Convert.ToInt32(elemanlar[0]), Convert.ToInt32(elemanlar[1]), Convert.ToInt32(elemanlar[2]), Convert.ToInt32(elemanlar[3])));


            }

            pictureBox1.Refresh();

            // Calling Dispose() is like calling the destructor of the respective object.
            // Dispose() clears all resources associated with the object, but the object still remains in memory
            // until the system garbage-collects it.
            graphics.Dispose();


        }


        private ArrayList liste = new ArrayList();
        private int processed_count = 0, unprocessed_count = 0;

        private void button6_Click(object sender, EventArgs e)
        {
            
            if ( listBox1.Items.Count == 0 )
            {

                MessageBox.Show("You need to select at least one region to add...");
                return;

            }


            string[] id = label1.Text.Split('\\');
            string fil = id [ id.Length -1 ] ;
            foreach ( string s in liste)
                if (s.Contains( fil )){
                    MessageBox.Show ( "Bu dosya daha önce eklenmiş...." );
                    return ;
                } 
            
            string yazi = textBox1.Text;
            string [] bolgeler = yazi.Split( '\\' );
            string line = bolgeler[   bolgeler.Length - 1    ];
            line += "/";
           
            line += id [ id.Length -1 ] ;
            line += " " ;
            line += this.comboBox1.Text  ;
            
            for ( int _i = 0 ; _i < listBox1.Items.Count ; _i++ )
            {
                line += " " +  listBox1.Items[ _i ].ToString();
    
            }
           
            //MessageBox.Show( line );
            liste.Add ( line ) ;

            label2.Text = "Added";
            this.processed_count += 1 ;
            this.unprocessed_count -= 1 ;

            label3.Text = "" + processed_count;
            label4.Text = "" + unprocessed_count;

            StreamWriter temp_file = null ;
            //In every 20 images we will save the Arraylist for data recovery....
            if ( liste.Count %20 == 0 )
            {

                temp_file = new StreamWriter("temp.txt");

                foreach (string lin in liste)
                    temp_file.WriteLine(lin);


                temp_file.Close();
            }






        }

        private void dosyayaKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form2 == null)
                this.form2 = new Form2(this.liste);

            try
            {
                this.form2.Show();

            }
            catch( ObjectDisposedException  )
                {

                    this.form2 = null ;
                    this.form2 = new Form2 ( this.liste ) ;
                    this.form2.Show ( );
                }



            }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "Not added";


        }
        private Negatives form3 = null;

        private void negativeIndexFileToolStripMenuItem_Click(object sender, EventArgs e)
        {


            form3 = new Negatives();

            try
            {
                form3.Show();
            }
            catch ( Exception )
                {
                    form3 = null;
                    form3 = new Negatives();
                    form3.Show();


                }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
