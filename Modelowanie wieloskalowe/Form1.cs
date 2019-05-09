using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Modelowanie_wieloskalowe
{
    public partial class Form1 : Form
    {

        int[,] tablica;
        int licznik = 0;
        Bitmap DrawArea;
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            //DrawArea = new Bitmap(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            pictureBox1.Image = DrawArea;
            


        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            



        }

        public class Siatka
        {
            static int value = 1;
           

            public int[,] metoda90(int m, int n )
            {
                int[,] array2Da = new int[m, n];
                array2Da[0, n/2] = value;

                for (int i = 0; i < m - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0 && (array2Da[i, n-1] != array2Da[i, j + 1]))
                            array2Da[i + 1, j] = value;
                        else if (j == n-1 && (array2Da[i, j - 1] != array2Da[i, 0]))
                            array2Da[i + 1, j] = value;
                        else if (j != 0 && j != n-1 && (array2Da[i, j - 1] != array2Da[i, j + 1]))
                            array2Da[i + 1, j] = value;
                    }
                }
                return array2Da;
            }

            public int[,] metoda30(int m, int n)
            {
                int[,] array2Da = new int[m, n];
                array2Da[0, n/2] = value;

                for (int i = 0; i < m - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0 && ((array2Da[i, n-1] == 1 && array2Da[i, j] == 0 &&array2Da[i, j + 1] == 0) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 1) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 0) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 1)))
                            array2Da[i + 1, j] = value;
                        else if (j == n-1 && ((array2Da[i, j-1] == 1 && array2Da[i, j] == 0 && array2Da[i, 0] == 0) || (array2Da[i, j-1] == 0 && array2Da[i, j] == 1 && array2Da[i, 0] == 1) || (array2Da[i, j-1] == 0 && array2Da[i, j] == 1 && array2Da[i, 0] == 0) || (array2Da[i, j-1] == 0 && array2Da[i, j] == 0 && array2Da[i, 0] == 1)))
                            array2Da[i + 1, j] = value;
                        else if (j != 0 && j != n-1 && ((array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, j+1] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, j+1] == 1) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, j+1] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 0 && array2Da[i, j+1] == 1)))
                            array2Da[i + 1, j] = value;
                    }
                }
                return array2Da;
            }

            public int[,] metoda60(int m, int n)
            {
                int[,] array2Da = new int[m, n];
                array2Da[0, n/2] = value;

                for (int i = 0; i < m - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0 && ((array2Da[i, n-1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 1) || (array2Da[i, n-1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 0) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 1) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 0)))
                            array2Da[i + 1, j] = value;
                        else if (j == n-1 && ((array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, 0] == 1) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, 0] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, 0] == 1) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, 0] == 0)))
                            array2Da[i + 1, j] = value;
                        else if (j != 0 && j != n-1 && ((array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 1) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 1) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 0)))
                            array2Da[i + 1, j] = value;
                    }
                }
                return array2Da;
            }

            public int[,] metoda120(int m, int n)
            {
                int[,] array2Da = new int[m, n];
                array2Da[0, n/2] = value;

                for (int i = 0; i < m - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0 && ((array2Da[i, n-1] == 1 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 0) || (array2Da[i, n-1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 1) || (array2Da[i, n-1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 0) || (array2Da[i, n-1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 1)))
                            array2Da[i + 1, j] = value;
                        else if (j == n-1 && ((array2Da[i, j - 1] == 1 && array2Da[i, j] == 1 && array2Da[i, 0] == 0) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, 0] == 1) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, 0] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, 0] == 1)))
                            array2Da[i + 1, j] = value;
                        else if (j != 0 && j != n-1 && ((array2Da[i, j - 1] == 1 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 0) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 1) || (array2Da[i, j - 1] == 1 && array2Da[i, j] == 0 && array2Da[i, j + 1] == 0) || (array2Da[i, j - 1] == 0 && array2Da[i, j] == 1 && array2Da[i, j + 1] == 1)))
                            array2Da[i + 1, j] = value;
                    }
                }
                return array2Da;
            }

            public int[,] metoda225(int m, int n)
            {
                int[,] array2Da = new int[m, n];
                array2Da[0, n/2] = value;

                for (int i = 0; i < m - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j == 0 && ((array2Da[i, n-1] == 1 && ((array2Da[i, j] + array2Da[i, j + 1])==1 )) || ((array2Da[i, n-1] + array2Da[i, j] + array2Da[i, j + 1]) == 0) || ((array2Da[i, n - 1] + array2Da[i, j] + array2Da[i, j + 1]) == 3)))
                            array2Da[i + 1, j] = value;
                        else if (j == n-1 && ((array2Da[i, j-1] == 1 && ((array2Da[i, j] + array2Da[i, 0]) == 1)) || ((array2Da[i, j-1] + array2Da[i, j] + array2Da[i, 0]) == 0) || ((array2Da[i, j - 1] + array2Da[i, j] + array2Da[i, 0]) == 3)))
                            array2Da[i + 1, j] = value;
                        else if (j != 0 && j != n-1 && ((array2Da[i, j-1] == 1 && ((array2Da[i, j] + array2Da[i, j + 1]) == 1)) || ((array2Da[i, j-1] + array2Da[i, j] + array2Da[i, j + 1]) == 0) || ((array2Da[i, j - 1] + array2Da[i, j] + array2Da[i, j + 1]) == 3)))
                            array2Da[i + 1, j] = value;
                    }
                }
                return array2Da;
            }

            public int[,] gra(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i=0; i<m; i++)
                {
                    for(int j=0; j<n; j++)
                    {
                        if(tab[i,j]==0)
                        {
                            if ((j == 0 || j == n-1) && i != 0 && i != m - 1)
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[i + k, (j + l + n) % n];
                                        if (licznik == 3)
                                            tab1[i, j] = 1;
                                        else
                                            tab1[i, j] = 0;
                                    }
                                }
                            }
                            else if((i == 0 || i == m-1 ) && j != 0 && j != n-1)
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[(i + k + m) % m, j + l];
                                        if (licznik == 3)
                                            tab1[i, j] = 1;
                                        else
                                            tab1[i, j] = 0;
                                    }
                                }
                            }
                            else if((i == 0 && j==0) || ( i == 0 && j == n-1) || ( i == m-1 && j == 0 ) || ( i == m-1 && j == n-1))
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[(i + k + m) % m, (j + l + n) % n];
                                        if (licznik == 3)
                                            tab1[i, j] = 1;
                                        else
                                            tab1[i, j] = 0;
                                    }
                                }
                            }
                            else
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[i + k, j + l];
                                        if (licznik == 3)
                                            tab1[i, j] = 1;
                                        else
                                            tab1[i, j] = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((j == 0 || j == n - 1) && i != 0 && i != m - 1)
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[i + k, (j + l + n) % n];
                                        if (licznik - 1 > 3 || licznik - 1 < 2)
                                            tab1[i, j] = 0;
                                        else
                                            tab1[i, j] = 1;
                                    }
                                }
                            }
                            else if ((i == 0 || i == m - 1) && j != 0 && j != n - 1)
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[(i + k + m) % m, j + l];
                                        if (licznik - 1 > 3 || licznik - 1 < 2)
                                            tab1[i, j] = 0;
                                        else
                                            tab1[i, j] = 1;
                                    }
                                }
                            }
                            else if ((i == 0 && j == 0) || (i == 0 && j == n - 1) || (i == m - 1 && j == 0) || (i == m - 1 && j == n - 1))
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[(i + k + m) % m, (j + l + n) % n];
                                        if (licznik - 1 > 3 || licznik - 1 < 2)
                                            tab1[i, j] = 0;
                                        else
                                            tab1[i, j] = 1;
                                    }
                                }
                            }
                            else
                            {
                                int licznik = 0;
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        licznik += tab[i + k, j + l];
                                        if (licznik-1> 3 || licznik -1 <2)
                                            tab1[i, j] = 0;
                                        else
                                            tab1[i, j] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("");
                /*if (tab[m - 2, m - 2] == 1)
                    tab[m - 2, m - 2] = 0;
                else
                    tab[m - 2, m - 2] = 1;*/
                return tab1;
            }
        }
        

        private void InitializeComboBox()
        {
            string[] lista = new string[] { "metoda 30", "metoda 60", "metoda 90", "metoda 120", "metoda 225" };
            comboBox1.Items.AddRange(lista);
            this.Controls.Add(this.comboBox1);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);

            string[] lista2 = new string[] { "Niezmienny", "Glider", "Reczna definicja", "Oscylator", "Losowy" };
            comboBox2.Items.AddRange(lista2);
            this.Controls.Add(this.comboBox2);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            Color color1 = Color.Black;
            Color color2 = Color.White;
            SolidBrush blackBrush = new SolidBrush(color1);
            SolidBrush whiteBrush = new SolidBrush(color2);
            Siatka s = new Siatka();
            MouseEventArgs me = (MouseEventArgs)e;
            int x = me.Location.X;
            int y = me.Location.Y;

            int r1 = int.Parse(textBox1.Text);
            int r2 = int.Parse(textBox2.Text);
            float r1_f = (float)r1;
            float r2_f = (float)r2;

            /*int[,] tab = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tab[i, j] = 0;*/
            
            

            float size_x = (float)pictureBox1.Size.Width / r1_f;
            float size_y = (float)pictureBox1.Size.Height / r2_f;
            if (size_x < size_y)
                size_y = size_x;
            else
                size_x = size_y;


            x = me.Location.X;
            y = me.Location.Y;

            float j_f = x / size_x;
            float i_f = y / size_y;
            int j_i = (int)j_f;
            int i_i = (int)i_f;
            /*string qwe = j_i.ToString();
            string qwer = i_i.ToString();
            MessageBox.Show(qwe);
            MessageBox.Show(qwer);*/
            if (tablica[i_i, j_i] == 1)
                tablica[i_i, j_i] = 0;
            else
                tablica[i_i, j_i] = 1;
            

            
            g.Clear(Color.DarkGray);
           
            for (int i = 0; i < r2; i++)
            {
                for (int j = 0; j < r1; j++)
                {
                    if (tablica[i, j] == 1)
                        g.FillRectangle(blackBrush, j * size_x, i * size_y, size_x, size_y);
                    else
                        g.FillRectangle(whiteBrush, j * size_x, i * size_y, size_x, size_y);
                }
            }
            pictureBox1.Image = DrawArea;
            licznik++;
            if(licznik == 10)
            {
                g.Clear(Color.DarkGray);
                for (int k = 0; k < 10; k++)
                {
                    for (int i = 0; i < r2; i++)
                    {
                        for (int j = 0; j < r1; j++)
                        {
                            if (tablica[i, j] == 1)
                                g.FillRectangle(blackBrush, j * size_x, i * size_y, size_x, size_y);
                            else
                                g.FillRectangle(whiteBrush, j * size_x, i * size_y, size_x, size_y);
                        }
                    }
                    pictureBox1.Image = DrawArea;
                    tablica = s.gra(tablica, r2, r1);
                    Thread.Sleep(1000);
                }
                g.Dispose();
                licznik= 0;
            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int r1 = int.Parse(textBox1.Text);
            int r2 = int.Parse(textBox2.Text);
            float r1_f = (float)r1;
            float r2_f = (float)r2;

            /*if (r1 > 100)
                r1 = 100;
            if (r2 > 120)
                r2 = 120;
            int wiel = pictureBox1.Size.Width / r1;
            int wiel2 = pictureBox1.Size.Height / r2;*/

            float wiel_f = (float)pictureBox1.Size.Width / r1_f;
            float wiel2_f = (float)pictureBox1.Size.Height / r2_f;
            if (wiel_f < wiel2_f)
                wiel2_f = wiel_f;
            else
                wiel_f = wiel2_f;
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            
            Color color1 = Color.Black;
            Color color2 = Color.White;
            SolidBrush blackBrush = new SolidBrush(color1);
            SolidBrush whiteBrush = new SolidBrush(color2);
            Siatka s = new Siatka();
            
            string tekst2 = "30";
            string tekst3 = "60";
            string tekst4 = "90";
            string tekst5 = "120";
            string tekst6 = "225";
            string tekst = comboBox1.SelectedItem.ToString();
            
            if (tekst == "metoda 30")
            {
                g.Clear(Color.DarkGray);
                MessageBox.Show(tekst2);
                MessageBox.Show(wiel_f.ToString());
                MessageBox.Show(wiel2_f.ToString());
                int[,] tab = new int[r2, r1];
                tab = s.metoda30(r2, r1);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tab[i, j] == 1)
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
                
            }
                
            else if (tekst == "metoda 60")
            {
                g.Clear(Color.DarkGray);
                MessageBox.Show(tekst3);
                MessageBox.Show(wiel_f.ToString());
                MessageBox.Show(wiel2_f.ToString());
                int[,] tab = new int[r2, r1];
                tab = s.metoda60(r2, r1);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tab[i, j] == 1)
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
                
            }
                
            else if (tekst == "metoda 90")
            {
                g.Clear(Color.DarkGray);
                MessageBox.Show(tekst4);
                MessageBox.Show(wiel_f.ToString());
                MessageBox.Show(wiel2_f.ToString());
                int[,] tab = new int[r2, r1];
                tab = s.metoda90(r2, r1);
                for(int i=0; i<r2; i++)
                {
                    for(int j=0; j<r1; j++)
                    {
                        if(tab[i,j] == 1 )
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
                
            }
                
            else if (tekst == "metoda 120")
            {
                g.Clear(Color.DarkGray);
                MessageBox.Show(tekst5);
                MessageBox.Show(wiel_f.ToString());
                MessageBox.Show(wiel2_f.ToString());
                int[,] tab = new int[r2, r1];
                tab = s.metoda120(r2, r1);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tab[i, j] == 1)
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
               
            }
                
            else if (tekst == "metoda 225")
            {
                g.Clear(Color.DarkGray);
                MessageBox.Show(tekst6);
                MessageBox.Show(wiel_f.ToString());
                MessageBox.Show(wiel2_f.ToString());
                int[,] tab = new int[r2, r1];
                tab = s.metoda225(r2, r1);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tab[i, j] == 1)
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
                

            }

            pictureBox1.Image = DrawArea;
            g.Dispose();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);

            int r1 = int.Parse(textBox1.Text);
            int r2 = int.Parse(textBox2.Text);
            float r1_f = (float)r1;
            float r2_f = (float)r2;
            float wiel_f = (float)pictureBox1.Size.Width / r1_f;
            float wiel2_f = (float)pictureBox1.Size.Height / r2_f;
            if (wiel_f < wiel2_f)
                wiel2_f = wiel_f;
            else
                wiel_f = wiel2_f;
            //int wiel = (int)wiel_f;
            //int wiel2 = (int)wiel2_f;
            int[,] tab = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tab[i, j] = 0;
            
            

            Color color1 = Color.Black;
            Color color2 = Color.White;
            SolidBrush blackBrush = new SolidBrush(color1);
            SolidBrush whiteBrush = new SolidBrush(color2);
            Siatka s = new Siatka();

            string tekst = comboBox2.SelectedItem.ToString();

            if (tekst == "Niezmienny")
            {
                g.Clear(Color.DarkGray);
                tab[0, 1] = 1; tab[0, 2] = 1; tab[1, 0] = 1; tab[1, 3] = 1; tab[2, 1] = 1; tab[2, 2] = 1;

                for (int k = 0; k < 10; k++)
                {
                    
                    for (int i = 0; i < r2; i++)
                    {
                        for (int j = 0; j < r1; j++)
                        {
                            if (tab[i, j] == 1)
                                g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                            else
                                g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        }
                    }
                    pictureBox1.Image = DrawArea;
                    tab = s.gra(tab, r2, r1);
                    Thread.Sleep(1000);
                    
                }
            }
            else if (tekst == "Glider")
            {
                g.Clear(Color.DarkGray);
                tab[10, 11] = 1; tab[10, 12] = 1; tab[11, 10] = 1; tab[11, 11] = 1; tab[12, 12] = 1;
                for (int k = 0; k < 10; k++)
                {
                    for (int i = 0; i < r2; i++)
                    {
                        for (int j = 0; j < r1; j++)
                        {
                            if (tab[i, j] == 1)
                                g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                            else
                                g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        }
                    }
                    pictureBox1.Image = DrawArea;
                    tab = s.gra(tab, r2, r1);
                    Thread.Sleep(1000);
                }
            }
            else if (tekst == "Reczna definicja")
            {
                tablica = new int[r2, r1];
                for (int i = 0; i < r2; i++)
                    for (int j = 0; j < r1; j++)
                        tablica[i, j] = 0;
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tablica[i, j] == 1)
                            g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        else
                            g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                    }
                }
                pictureBox1.Image = DrawArea;
                //pictureBox1_Click(sender, e);

            }
            else if (tekst == "Oscylator")
            {
                g.Clear(Color.DarkGray);
                tab[0, 1] = 1; tab[1, 1] = 1; tab[2, 1] = 1;
                for (int k = 0; k < 10; k++)
                {
                    for (int i = 0; i < r2; i++)
                    {
                        for (int j = 0; j < r1; j++)
                        {
                            if (tab[i, j] == 1)
                                g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                            else
                                g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        }
                    }
                    pictureBox1.Image = DrawArea;
                    tab = s.gra(tab, r2, r1);
                    Thread.Sleep(1000);
                }
            }
            else if (tekst == "Losowy")
            {
                g.Clear(Color.DarkGray);
                Random rand = new Random();
                for (int i = 0; i < r2 * r1 / 3; i++)
                    tab[rand.Next(r2), rand.Next(r1)] = 1;
                for (int k = 0; k < 10; k++)
                {
                    for (int i = 0; i < r2; i++)
                    {
                        for (int j = 0; j < r1; j++)
                        {
                            if (tab[i, j] == 1)
                                g.FillRectangle(blackBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                            else
                                g.FillRectangle(whiteBrush, j * wiel_f, i * wiel2_f, wiel_f, wiel2_f);
                        }
                    }
                    pictureBox1.Image = DrawArea;
                    tab = s.gra(tab, r2, r1);
                    Thread.Sleep(1000);
                }
            }

            pictureBox1.Image = DrawArea;
            g.Dispose();

        }

       
    }

    


}
