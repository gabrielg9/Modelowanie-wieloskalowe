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
using System.Timers;
using System.Media;



namespace Modelowanie_wieloskalowe
{
    public partial class Form1 : Form
    {

        int[,] tablica;
        int val = 1;
        private Graphics g;
        Bitmap DrawArea;
        int r1, r2;
        float r1_f, r2_f, size_x, size_y;
        Siatka s;
        bool stan_gry;
        bool button_GOL_click;
        bool periodyczne;
        bool absorbujace;
        bool rozrost_ziaren;
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush brownBrush = new SolidBrush(Color.Brown);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush pinkBrush = new SolidBrush(Color.Pink);
        SolidBrush colorBrush = new SolidBrush(Color.Aqua);
        SolidBrush[] solidBrushes;


        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            g = Graphics.FromImage(DrawArea);

            

        }
        
        private void pobierz_dane()
        {
            r1 = int.Parse(textBox1.Text);
            r2 = int.Parse(textBox2.Text);
            r1_f = (float)r1;
            r2_f = (float)r2;
            size_x = pictureBox1.Size.Width / r1_f;
            size_y = pictureBox1.Size.Height / r2_f;
            if (size_x < size_y)
                size_y = size_x;
            else
                size_x = size_y;

            s = new Siatka();

        }

        private void wyznacz_kolory()
        {
            Random rand = new Random();
            int r, g, b;
            solidBrushes = new SolidBrush[1000];
            solidBrushes[0] = new SolidBrush(Color.White);
            for (int i = 1; i < 1000; i++)
            {
                r = rand.Next(255);
                g = rand.Next(255);
                b = rand.Next(255);
                solidBrushes[i] = new SolidBrush(Color.FromArgb(r,g,b));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
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
                return tab1;
            }

            public int[,] sprawdz_warunki_brzeogwe_vonNeymana_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                int wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                int[] zbior;
                zbior = new int[4];
                for(int i=0; i<m; i++)
                {
                    for(int j=0; j<n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i, n - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, 0]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[0, j];
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = tab[i, n - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, 0]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, 0]; zbior[3] = tab[0, j];
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = tab[i, n - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[0, j];
                            }
                            else
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            for(int l=0;l<4;l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 4; k++)
                                {
                                    if (wartosc == zbior[k] && zbior[k] != 0)
                                    {
                                        licznik++;
                                    }
                                }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = wartosc;
                                    }
                                    licznik = 0;
                                
                            }
                            tab1[i, j] = max_wartosc;                       
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzeogwe_vonNeymana_absorbujace(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                int wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                int[] zbior;
                zbior = new int[4];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = 0; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                            }
                            for (int l = 0; l < 4; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 4; k++)
                                {
                                    if (wartosc == zbior[k] && zbior[k] != 0)
                                    {
                                        licznik++;
                                    }
                                }
                                if (licznik > max_licznik)
                                {
                                    max_licznik = licznik;
                                    max_wartosc = wartosc;
                                }
                                licznik = 0;

                            }
                            tab1[i, j] = max_wartosc;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_moor(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                int aktualna_wartosc=0;
                int max_wartosc=0;
                int licznik = 0;
                int max_licznik = 0;
                for(int i=0; i<m; i++)
                {
                    for(int j=0; j<n; j++)
                    {
                        for(int r=-1; r<2; r++)
                            for(int t=-1; t<2; t++)
                            {
                                licznik = 0;
                                aktualna_wartosc = tab[i + r, j + t];
                                max_wartosc = tab[i + r, j + r];
                                for (int k = -1; k < 2; k++)
                                {
                                    for (int l = -1; l < 2; l++)
                                    {
                                        if (aktualna_wartosc == tab[i + k, j + l])
                                            licznik++;
                                    }
                                }
                                if (licznik > max_licznik)
                                {
                                    max_licznik = licznik;
                                    max_wartosc = aktualna_wartosc;
                                }
                            }
                        
                         tab1[i, j] = max_wartosc;
                    }
                }

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

            string[] lista3 = new string[] { "Jednorodne", "Z promieniem", "Losowe", "Wyklinanie" };
            comboBox3.Items.AddRange(lista3);
            this.Controls.Add(this.comboBox3);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pobierz_dane();
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            
            MouseEventArgs me = (MouseEventArgs)e;
            int x = me.Location.X;
            int y = me.Location.Y;

            x = me.Location.X;
            y = me.Location.Y;

            float j_f = x / size_x;
            float i_f = y / size_y;
            int j_i = (int)j_f;
            int i_i = (int)i_f;
            
            
            if(button_GOL_click)
            {
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

                /*if (stan_gry)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }*/
            }
            else//rozrost ziaren 1
            {
                tablica[i_i, j_i] = val;
                val++;
                g.Clear(Color.DarkGray);

                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        for (int k = 0; k < 1000; k++)
                            if (tablica[i, j] == k)
                                g.FillRectangle(solidBrushes[k], j * size_x, i * size_y, size_x, size_y);
                            
                    }
                }
                pictureBox1.Image = DrawArea;
                g.Dispose();
                
                /*if (rozrost_ziaren)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }*/
            }

        }

        private void rysuj_automaty_kom(int [,] tab, int opcja)
        {
            pobierz_dane();
            Graphics g;
            g = Graphics.FromImage(DrawArea);

            MessageBox.Show(size_x.ToString());
            MessageBox.Show(size_y.ToString());
            
            switch (opcja)
            {
                case 1:
                    tab = s.metoda30(r2, r1);
                    break;
                case 2:
                    tab = s.metoda60(r2, r1);
                    break;
                case 3:
                    tab = s.metoda90(r2, r1);
                    break;
                case 4:
                    tab = s.metoda120(r2, r1);
                    break;
                case 5:
                    tab = s.metoda225(r2, r1);
                    break;

            }
            g.Clear(Color.DarkGray);
            for (int i = 0; i < r2; i++)
            {
                for (int j = 0; j < r1; j++)
                {
                    if (tab[i, j] == 1)
                        g.FillRectangle(blackBrush, j * size_x, i * size_y, size_x, size_y);
                    else
                        g.FillRectangle(whiteBrush, j * size_x, i * size_y, size_x, size_y);
                }
            }
            pictureBox1.Image = DrawArea;
            g.Dispose();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            pobierz_dane();
            int opcja = 0;

            string tekst1 = "30";
            string tekst2 = "60";
            string tekst3 = "90";
            string tekst4 = "120";
            string tekst5 = "225";
            string tekst = comboBox1.SelectedItem.ToString();
            
            if (tekst == "metoda 30")
            {
                MessageBox.Show(tekst1);                
                int[,] tab = new int[r2, r1];
                opcja = 1;
                rysuj_automaty_kom(tab, opcja);
            }
                
            else if (tekst == "metoda 60")
            {
                MessageBox.Show(tekst2);              
                int[,] tab = new int[r2, r1];
                opcja = 2;
                rysuj_automaty_kom(tab, opcja);                 
            }
                
            else if (tekst == "metoda 90")
            {
                MessageBox.Show(tekst3);
                int[,] tab = new int[r2, r1];
                opcja = 3;
                rysuj_automaty_kom(tab, opcja);                             
            }
                
            else if (tekst == "metoda 120")
            {
                MessageBox.Show(tekst4);
                int[,] tab = new int[r2, r1];
                opcja = 4;
                rysuj_automaty_kom(tab, opcja);
            }
                
            else if (tekst == "metoda 225")
            {
                MessageBox.Show(tekst5);
                int[,] tab = new int[r2, r1];
                opcja = 5;
                rysuj_automaty_kom(tab, opcja);                
            }
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

        private void nowy_watek()
        {
            while(stan_gry)
            {
                rysuj_gra_w_zycie();
                Thread.Sleep(1000);
            }
            while(rozrost_ziaren)
            {
                rysuj_ziarna();
                Thread.Sleep(1000);
            }
        }

        private void rysuj_gra_w_zycie()
        {
            pobierz_dane();
            lock(g)
            {
                Graphics gr;
                gr = Graphics.FromImage(DrawArea);
                
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        if (tablica[i, j] == 1)
                            gr.FillRectangle(blackBrush, j * size_x, i * size_y, size_x, size_y);
                        else
                            gr.FillRectangle(whiteBrush, j * size_x, i * size_y, size_x, size_y);
                    }
                }
                tablica = s.gra(tablica, r2, r1);
                pictureBox1.Image = DrawArea;
                gr.Dispose();
            }

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stan_gry = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stan_gry = true;
            if (stan_gry)
            {
                Thread th = new Thread(nowy_watek);
                th.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pobierz_dane();
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            stan_gry = true;
            button_GOL_click = true;

            tablica = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tablica[i, j] = 0;

            string tekst = comboBox2.SelectedItem.ToString();

            if (tekst == "Niezmienny")
            {
                g.Clear(Color.DarkGray);
                tablica[0, 1] = 1; tablica[0, 2] = 1; tablica[1, 0] = 1; tablica[1, 3] = 1; tablica[2, 1] = 1; tablica[2, 2] = 1;

                if(stan_gry)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
            }
            else if (tekst == "Glider")
            {
                g.Clear(Color.DarkGray);
                tablica[10, 11] = 1; tablica[10, 12] = 1; tablica[11, 10] = 1; tablica[11, 11] = 1; tablica[12, 12] = 1;
                if(stan_gry)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
            }
            else if (tekst == "Reczna definicja")
            {
                stan_gry = false;
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
                g.Dispose();
            }
            else if (tekst == "Oscylator")
            {
                g.Clear(Color.DarkGray);
                tablica[0, 1] = 1; tablica[1, 1] = 1; tablica[2, 1] = 1;
                if(stan_gry)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
            }
            else if (tekst == "Losowy")
            {
                g.Clear(Color.DarkGray);
                Random rand = new Random();
                for (int i = 0; i < r2 * r1 / 3; i++)
                    tablica[rand.Next(r2), rand.Next(r1)] = 1;
                if(stan_gry)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            periodyczne = true;
            absorbujace = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            periodyczne = false;
            absorbujace = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rozrost_ziaren = true;
            if (rozrost_ziaren)
            {
                Thread th = new Thread(nowy_watek);
                th.Start();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rysuj_ziarna()
        {
            lock(g)
            {
                Graphics grp;
                grp = Graphics.FromImage(DrawArea);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            if (tablica[i, j] == k)
                                grp.FillRectangle(solidBrushes[k], j * size_x, i * size_y, size_x, size_y);
                        }
                    }
                }
                if (periodyczne)
                    tablica = s.sprawdz_warunki_brzeogwe_vonNeymana_periodyczne(tablica, r2, r1);
                else//absorbujace
                    tablica = s.sprawdz_warunki_brzeogwe_vonNeymana_absorbujace(tablica, r2, r1);
                pictureBox1.Image = DrawArea;
                grp.Dispose();
            }
                        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pobierz_dane();
            wyznacz_kolory();
            button_GOL_click = false;
            rozrost_ziaren = true;
            Graphics g;
            g = Graphics.FromImage(DrawArea);

            tablica = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tablica[i, j] = 0;

            string tekst = comboBox3.SelectedItem.ToString();
            if(tekst == "Jednorodne")
            {
                g.Clear(Color.DarkGray);
                int ilosc_wiersz_i = int.Parse(textBox3.Text);
                int ilosc_kolumna_i = int.Parse(textBox4.Text);
                float ilosc_wiersz = (float)ilosc_wiersz_i;
                float ilosc_kolumna = (float)ilosc_kolumna_i;
                float odstep_wiersz_f = r1_f / ilosc_wiersz;
                int odstep_wiersz = (int)Math.Ceiling(odstep_wiersz_f);
                //MessageBox.Show(odstep_wiersz.ToString());
                float odstep_kolumna_f = r2_f / ilosc_kolumna;
                int odstep_kolumna = (int)Math.Ceiling(odstep_kolumna_f);
                //MessageBox.Show(odstep_kolumna.ToString());
                int ilosc_zarodkow = ilosc_wiersz_i * ilosc_kolumna_i;
                int val = 1;
                for (int i = 0; i < r2; i += odstep_kolumna)
                    for (int j = 0; j < r1; j += odstep_wiersz)
                    {
                        tablica[(odstep_kolumna / 2) + i,(odstep_wiersz / 2) + j] = val;
                        val++;
                    }
                if (rozrost_ziaren)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }

                
            }
            else if(tekst == "Z promieniem")
            {
                g.Clear(Color.DarkGray);
                int promien = int.Parse(textBox3.Text);
                int ilosc = int.Parse(textBox4.Text);
                Random rand = new Random();
                for (int i = 1; i < ilosc + 1; i++)
                {
                    int a = rand.Next(r2);
                    int b = rand.Next(r1);
                    if (tablica[a, b] == 0)
                        tablica[a, b] = i;
                }
                if (rozrost_ziaren)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
                

            }
            else if(tekst == "Losowe")
            {
                g.Clear(Color.DarkGray);
                Random rand = new Random();
                int ilosc = int.Parse(textBox4.Text);
                for(int i =1; i<ilosc+1; i++)
                {
                    int a = rand.Next(r2);
                    int b = rand.Next(r1);
                    if (tablica[a, b] == 0)
                        tablica[a, b] = i;
                }
                if (rozrost_ziaren)
                {
                    Thread th = new Thread(nowy_watek);
                    th.Start();
                }
                
            }
            else//wyklinanie
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
                g.Dispose();
            }
        }
    }

    


}
