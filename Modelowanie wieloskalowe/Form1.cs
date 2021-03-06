﻿using System;
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
        int[,] poprzednia_tablica;
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
        bool mc;
        bool von_Nemann_mc;
        bool moor_mc;
        bool vonNeumann;
        bool moore;
        bool pentagonalne_lewe;
        bool pentagonalne_prawe;
        bool pentagonalne_gorne;
        bool pentagonalne_dolne;
        bool pentagonalne_losowe;
        bool heksagonalne_prawe;
        bool heksagonalne_lewe;
        bool heksagonalne_losowe;
        bool promien;
        double[] gestosc_dyslokacji;
        double krytyczna_dyslokacja;
        bool zrekrystalizowany;
        public int[,] tablica_energii;
        SolidBrush czarny = new SolidBrush(Color.Black);
        SolidBrush bialy = new SolidBrush(Color.White);
        SolidBrush zolty = new SolidBrush(Color.Yellow);
        Random random = new Random();
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
                            max_wartosc = 0;
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_moor_periodyczne(int[,] tab, int m, int n)
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
                        if (tab[i, j] == 0)
                        {
                            for (int r = -1; r < 2; r++)
                                for (int t = -1; t < 2; t++)
                                {
                                    licznik = 0;
                                    aktualna_wartosc = tab[(i + r +m)%m, (j + t+n)%n];
                                    for (int k = -1; k < 2; k++)
                                    {
                                        for (int l = -1; l < 2; l++)
                                        {
                                            if ((aktualna_wartosc == tab[(i + k+m)%m, (j + l+n)%n] )&& tab[(i+k+m)%m,(j+l+n)%n]!=0)
                                                licznik++;
                                        }
                                    }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = aktualna_wartosc;
                                    }
                                    licznik = 0;
                                }

                            tab1[i, j] = max_wartosc;
                            max_wartosc = 0;
                            max_licznik = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_moor_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[9];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = 0; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = 0;
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = 0; zbior[7] = 0; zbior[8] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = 0; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = 0;
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] =0; zbior[6] = 0; zbior[7] = 0; zbior[8] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = 0; zbior[7] = 0; zbior[8] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i-1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i-1, j + 1]; zbior[3] = tab[i, j-1];zbior[4] = tab[i, j];zbior[5] = tab[i, j + 1];zbior[6] = tab[i + 1, j - 1];zbior[7] = tab[i + 1, j];zbior[8] = tab[i + 1, j + 1];
                            }
                            for (int l = 0; l < 9; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 9; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_lewe_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;


                int aktualna_wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (tab[i, j] == 0)
                        {
                            for (int r = -1; r < 2; r++)
                                for (int t = 0; t < 2; t++)
                                {
                                    licznik = 0;
                                    aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                    for (int k = -1; k < 2; k++)
                                    {
                                        for (int l = 0; l < 2; l++)
                                        {
                                            if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                licznik++;
                                        }
                                    }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = aktualna_wartosc;
                                    }
                                    licznik = 0;
                                }

                            tab1[i, j] = max_wartosc;
                            max_wartosc = 0;
                            max_licznik = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_lewe_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[6];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i-1,j]; zbior[1] = tab[i - 1, j+1]; zbior[2] = tab[i,j]; zbior[3] = tab[i, j+1]; zbior[4] = tab[i+1,j]; zbior[5] = tab[i + 1, j+1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] =0; zbior[4] = tab[i + 1, j ]; zbior[5] = 0;
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = tab[i, j+1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j+1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j ]; zbior[1] = tab[i - 1, j+1]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j+1]; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i,j]; zbior[3] = tab[i, j+1]; zbior[4] = tab[i+1,j]; zbior[5] = tab[i + 1, j+1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j ]; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = tab[i-1,j]; zbior[1] = tab[i - 1, j+1]; zbior[2] = tab[i,j]; zbior[3] = tab[i, j+1]; zbior[4] = 0; zbior[5] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j+1]; zbior[2] = tab[i, j ]; zbior[3] = tab[i, j+1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j+1];
                            }
                            for (int l = 0; l < 6; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 6; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_prawe_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                int aktualna_wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (tab[i, j] == 0)
                        {
                            for (int r = -1; r < 2; r++)
                                for (int t = -1; t < 1; t++)
                                {
                                    licznik = 0;
                                    aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                    for (int k = -1; k < 2; k++)
                                    {
                                        for (int l = -1; l < 1; l++)
                                        {
                                            if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                licznik++;
                                        }
                                    }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = aktualna_wartosc;
                                    }
                                    licznik = 0;
                                }

                            tab1[i, j] = max_wartosc;
                            max_wartosc = 0;
                            max_licznik = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_prawe_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[6];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                            }
                            for (int l = 0; l < 6; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 6; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_gorne_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] =0;

                int aktualna_wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (tab[i, j] == 0)
                        {
                            for (int r = 0; r < 2; r++)
                                for (int t = -1; t < 2; t++)
                                {
                                    licznik = 0;
                                    aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                    for (int k = 0; k < 2; k++)
                                    {
                                        for (int l = -1; l < 2; l++)
                                        {
                                            if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                licznik++;
                                        }
                                    }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = aktualna_wartosc;
                                    }
                                    licznik = 0;
                                }

                            tab1[i, j] = max_wartosc;
                            max_wartosc = 0;
                            max_licznik = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_gorne_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[6];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i, j]; zbior[2] = tab[i, j+1]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i , j-1]; zbior[1] = tab[i,j]; zbior[2] = 0; zbior[3] = tab[i+1,j-1]; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i,j-1]; zbior[1] = tab[i,j]; zbior[2] = tab[i, j+1]; zbior[3] = tab[i+1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i, j-1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j+1]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i,j]; zbior[2] = tab[i, j+1]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = tab[i,j-1]; zbior[1] = tab[i,j]; zbior[2] = 0; zbior[3] = tab[i+1,j-1]; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i, j-1]; zbior[1] = tab[i,j]; zbior[2] = 0; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i, j]; zbior[2] = tab[i, j+1]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i, j-1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j+1]; zbior[3] = tab[i+1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                            }
                            for (int l = 0; l < 6; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 6; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_dolne_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                int aktualna_wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (tab[i, j] == 0)
                        {
                            for (int r = -1; r < 1; r++)
                                for (int t = -1; t < 2; t++)
                                {
                                    licznik = 0;
                                    aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                    for (int k = -1; k < 1; k++)
                                    {
                                        for (int l = -1; l < 2; l++)
                                        {
                                            if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                licznik++;
                                        }
                                    }
                                    if (licznik > max_licznik)
                                    {
                                        max_licznik = licznik;
                                        max_wartosc = aktualna_wartosc;
                                    }
                                    licznik = 0;
                                }

                            tab1[i, j] = max_wartosc;
                            max_wartosc = 0;
                            max_licznik = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_dolne_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[6];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i-1, j]; zbior[2] = tab[i-1, j + 1]; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i-1, j - 1]; zbior[1] = tab[i-1, j]; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0;
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i-1, j - 1]; zbior[1] = tab[i-1, j]; zbior[2] = tab[i-1, j + 1]; zbior[3] = tab[i,j-1]; zbior[4] = tab[i,j]; zbior[5] = tab[i,j+1];
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0;zbior[1]= 0; zbior[2] = 0; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i , j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i-1, j - 1]; zbior[1] = tab[i-1, j]; zbior[2] = 0; zbior[3] = tab[i,j-1]; zbior[4] = tab[i,j]; zbior[5] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i-1, j]; zbior[2] = tab[i-1, j + 1]; zbior[3] = 0; zbior[4] = tab[i,j]; zbior[5] = tab[i,j+1];
                            }
                            else
                            {
                                zbior[0] = tab[i-1, j - 1]; zbior[1] = tab[i-1, j]; zbior[2] = tab[i-1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                            }
                            for (int l = 0; l < 6; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 6; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_losowe_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;
                Random rand = new Random();


                int aktualna_wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (tab[i, j] == 0)
                        {
                            switch(rand.Next(4))
                            {
                                case 0:
                                    for (int r = -1; r < 2; r++)
                                        for (int t = 0; t < 2; t++)
                                        {
                                            licznik = 0;
                                            aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                            for (int k = -1; k < 2; k++)
                                            {
                                                for (int l = 0; l < 2; l++)
                                                {
                                                    if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                        licznik++;
                                                }
                                            }
                                            if (licznik > max_licznik)
                                            {
                                                max_licznik = licznik;
                                                max_wartosc = aktualna_wartosc;
                                            }
                                            licznik = 0;
                                        }

                                    tab1[i, j] = max_wartosc;
                                    max_wartosc = 0;
                                    max_licznik = 0;
                                    break;
                                case 1:
                                    for (int r = -1; r < 2; r++)
                                        for (int t = -1; t < 1; t++)
                                        {
                                            licznik = 0;
                                            aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                            for (int k = -1; k < 2; k++)
                                            {
                                                for (int l = -1; l < 1; l++)
                                                {
                                                    if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                        licznik++;
                                                }
                                            }
                                            if (licznik > max_licznik)
                                            {
                                                max_licznik = licznik;
                                                max_wartosc = aktualna_wartosc;
                                            }
                                            licznik = 0;
                                        }

                                    tab1[i, j] = max_wartosc;
                                    max_wartosc = 0;
                                    max_licznik = 0;
                                    break;
                                case 2:
                                    for (int r = 0; r < 2; r++)
                                        for (int t = -1; t < 2; t++)
                                        {
                                            licznik = 0;
                                            aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                            for (int k = 0; k < 2; k++)
                                            {
                                                for (int l = -1; l < 2; l++)
                                                {
                                                    if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                        licznik++;
                                                }
                                            }
                                            if (licznik > max_licznik)
                                            {
                                                max_licznik = licznik;
                                                max_wartosc = aktualna_wartosc;
                                            }
                                            licznik = 0;
                                        }

                                    tab1[i, j] = max_wartosc;
                                    max_wartosc = 0;
                                    max_licznik = 0;
                                    break;
                                case 3:
                                    for (int r = -1; r < 1; r++)
                                        for (int t = -1; t < 2; t++)
                                        {
                                            licznik = 0;
                                            aktualna_wartosc = tab[(i + r + m) % m, (j + t + n) % n];
                                            for (int k = -1; k < 1; k++)
                                            {
                                                for (int l = -1; l < 2; l++)
                                                {
                                                    if ((aktualna_wartosc == tab[(i + k + m) % m, (j + l + n) % n]) && tab[(i + k + m) % m, (j + l + n) % n] != 0)
                                                        licznik++;
                                                }
                                            }
                                            if (licznik > max_licznik)
                                            {
                                                max_licznik = licznik;
                                                max_wartosc = aktualna_wartosc;
                                            }
                                            licznik = 0;
                                        }

                                    tab1[i, j] = max_wartosc;
                                    max_wartosc = 0;
                                    max_licznik = 0;
                                    break;

                            }
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }
                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_pentagonalne_losowe_absorbujace(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                Random rand = new Random();
                int wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                int[] zbior;
                zbior = new int[6];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        switch(rand.Next(4))
                        {
                            case 0:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j + 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    for (int l = 0; l < 6; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 6; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                            case 1:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i + 1, j - 1]; zbior[5] = tab[i + 1, j];
                                    }
                                    for (int l = 0; l < 6; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 6; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                            case 2:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = 0; zbior[3] = tab[i + 1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = 0; zbior[3] = tab[i + 1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = 0; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = 0; zbior[4] = 0; zbior[5] = 0;
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j - 1]; zbior[4] = tab[i + 1, j]; zbior[5] = tab[i + 1, j + 1];
                                    }
                                    for (int l = 0; l < 6; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 6; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                            case 3:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0;
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = 0; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1];
                                    }
                                    for (int l = 0; l < 6; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 6; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                        }
                        
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_heksagonalne_lewe_periodyczne(int[,] tab, int m, int n)
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
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, n - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j - 1]; zbior[6] = tab[0, j];
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, n - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[0, j - 1]; zbior[6] = tab[0, j];
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, n - 1]; zbior[6] = tab[0, j];
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j ]; zbior[1] = tab[i - 1, j+1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j+1]; zbior[5] = tab[i+1, j - 1]; zbior[6] = tab[i + 1, j]; 
                            }
                            for (int l = 0; l < 7; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 7; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_heksagonalne_lewe_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = tab[i + 1, j];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0; zbior[6] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                            }
                            for (int l = 0; l < 7; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 7; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_heksagonalne_prawe_periodyczne(int[,] tab, int m, int n)
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
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, n - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i,0]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, 0];
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[m - 1, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j]; zbior[6] = tab[0, j + 1];
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = tab[m - 1, n - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = tab[m - 1, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, 0];
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i,0]; zbior[5] = tab[0, j]; zbior[6] = tab[0, 0];
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = tab[i - 1, n- 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j]; zbior[6] = tab[0, j + 1];
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j-1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j+1];
                            }
                            for (int l = 0; l < 7; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 7; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }


            public int[,] sprawdz_warunki_brzegowe_heksagonalne_prawe_absorbujace(int[,] tab, int m, int n)
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
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        wartosc = 0; licznik = 0; max_licznik = 0;
                        if (tab[i, j] == 0)
                        {
                            if (j == 0 & i != 0 && i != m - 1)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (j == n - 1 && i != 0 && i != m - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j]; zbior[6] = 0;
                            }
                            else if (i == 0 && j != 0 && j != n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (i == m - 1 && j != 0 && j != n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                            }
                            else if (i == 0 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            else if (i == 0 && j == n - 1)
                            {
                                zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j]; zbior[6] = 0;
                            }
                            else if (i == m - 1 && j == n - 1)
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0; zbior[6] = 0;
                            }
                            else if (i == m - 1 && j == 0)
                            {
                                zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                            }
                            else
                            {
                                zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                            }
                            for (int l = 0; l < 7; l++)
                            {
                                wartosc = zbior[l];
                                for (int k = 0; k < 7; k++)
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
                            max_wartosc = 0;
                        }
                        else
                            tab1[i, j] = tab[i, j];
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_heksagonalne_losowe_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                Random rand = new Random();
                int wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                int[] zbior;
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        switch(rand.Next(2))
                        {
                            case 0:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, n - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j - 1]; zbior[6] = tab[0, j];
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, n - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = tab[m - 1, j]; zbior[1] = tab[m - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, 0]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[0, j - 1]; zbior[6] = tab[0, j];
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, n - 1]; zbior[6] = tab[0, j];
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    for (int l = 0; l < 7; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 7; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                            case 1:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, n - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, 0];
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[m - 1, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j]; zbior[6] = tab[0, j + 1];
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = tab[m - 1, n - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = tab[m - 1, j - 1]; zbior[1] = tab[m - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, 0];
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, 0]; zbior[5] = tab[0, j]; zbior[6] = tab[0, 0];
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = tab[i - 1, n - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, n - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[0, j]; zbior[6] = tab[0, j + 1];
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    for (int l = 0; l < 7; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 7; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                        }
                        
                    }
                }

                return tab1;
            }

            public int[,] sprawdz_warunki_brzegowe_heksagonalne_losowe_absorbujace(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        tab1[i, j] = 0;

                Random rand = new Random();
                int wartosc = 0;
                int max_wartosc = 0;
                int licznik = 0;
                int max_licznik = 0;
                int[] zbior;
                zbior = new int[7];
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        switch(rand.Next(2))
                        {
                            case 0:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j]; zbior[1] = tab[i - 1, j + 1]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j - 1]; zbior[6] = tab[i + 1, j];
                                    }
                                    for (int l = 0; l < 7; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 7; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                            case 1:
                                wartosc = 0; licznik = 0; max_licznik = 0;
                                if (tab[i, j] == 0)
                                {
                                    if (j == 0 & i != 0 && i != m - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (j == n - 1 && i != 0 && i != m - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j]; zbior[6] = 0;
                                    }
                                    else if (i == 0 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (i == m - 1 && j != 0 && j != n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else if (i == 0 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    else if (i == 0 && j == n - 1)
                                    {
                                        zbior[0] = 0; zbior[1] = 0; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = tab[i + 1, j]; zbior[6] = 0;
                                    }
                                    else if (i == m - 1 && j == n - 1)
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = 0; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else if (i == m - 1 && j == 0)
                                    {
                                        zbior[0] = 0; zbior[1] = tab[i - 1, j]; zbior[2] = 0; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = 0; zbior[6] = 0;
                                    }
                                    else
                                    {
                                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j - 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j + 1]; zbior[5] = tab[i + 1, j]; zbior[6] = tab[i + 1, j + 1];
                                    }
                                    for (int l = 0; l < 7; l++)
                                    {
                                        wartosc = zbior[l];
                                        for (int k = 0; k < 7; k++)
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
                                    max_wartosc = 0;
                                }
                                else
                                    tab1[i, j] = tab[i, j];
                                break;
                        }
                        
                    }
                }

                return tab1;
            }

            public int[,] monte_carlo_von_neumann_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int p = 0; p < m; p++)
                    for (int u = 0; u < n; u++)
                        tab1[p, u] = 0;

                int ilosc_punktow = m * n;
                int ilosc_punktow_for = ilosc_punktow;
                int[] tablica_punktow = new int[ilosc_punktow];
                for (int b = 0; b < ilosc_punktow; b++)
                    tablica_punktow[b] = b;

                Random rand = new Random();
                int x = 0;
                int i = 0, j = 0, energia_przed = 0, energia_po = 0, roznica_energii = 0;
                int poprzedni_kolor = 0;
                int[] zbior;
                zbior = new int[4];

                void funkcja_przed()
                {
                    if (zbior[0] != tab[i, j])
                        energia_przed++;
                    if (zbior[1] != tab[i, j])
                        energia_przed++;
                    if (zbior[2] != tab[i, j])
                        energia_przed++;
                    if (zbior[3] != tab[i, j])
                        energia_przed++;
                }
                void funkcja_po()
                {
                    if (zbior[0] != tab[i, j])
                        energia_po++;
                    if (zbior[1] != tab[i, j])
                        energia_po++;
                    if (zbior[2] != tab[i, j])
                        energia_po++;
                    if (zbior[3] != tab[i, j])
                        energia_po++;
                }

                for (int r=0; r<ilosc_punktow_for; r++)
                {
                    x = tablica_punktow[rand.Next(ilosc_punktow)];
                    i = x / m;
                    j = x % n;
                    poprzedni_kolor = tab[i, j];
                    energia_po = 0;
                    energia_przed = 0;
                    roznica_energii = 0;

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

                    funkcja_przed();
                    switch (rand.Next(4))
                    {
                        case 0:
                            tab[i, j] = zbior[0];
                            break;
                        case 1:
                            tab[i, j] = zbior[1];
                            break;
                        case 2:
                            tab[i, j] = zbior[2];
                            break;
                        case 3:
                            tab[i, j] = zbior[3];
                            break;
                    }
                    funkcja_po();
                    roznica_energii = energia_po - energia_przed;
                    if (roznica_energii <= 0)
                        tab[i, j] = tab[i, j];
                    else
                    {
                        if(rand.Next(100)  <  (Math.Exp(-((double)roznica_energii)/0.6))*100.0)
                            tab[i, j] = tab[i, j];
                        else
                            tab[i, j] = poprzedni_kolor;
                    }
                        
                    
                          

                    tablica_punktow[x] = tablica_punktow[ilosc_punktow - 1];
                    ilosc_punktow--;
                }
                return tab;
            }

            public int[,] monte_carlo_von_neumann_absorbujace(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int p = 0; p < m; p++)
                    for (int u = 0; u < n; u++)
                        tab1[p, u] = 0;

                int ilosc_punktow = m * n;
                int ilosc_punktow_for = ilosc_punktow;
                int[] tablica_punktow = new int[ilosc_punktow];
                for (int b = 0; b < ilosc_punktow; b++)
                    tablica_punktow[b] = b;

                Random rand = new Random();
                int x = 0;
                int i = 0, j = 0, energia_przed = 0, energia_po = 0, roznica_energii = 0;
                int poprzedni_kolor = 0;
                int[] zbior;
                zbior = new int[4];

                void funkcja_przed()
                {
                    if (zbior[0] != tab[i, j])
                        energia_przed++;
                    if (zbior[1] != tab[i, j])
                        energia_przed++;
                    if (zbior[2] != tab[i, j])
                        energia_przed++;
                    if (zbior[3] != tab[i, j])
                        energia_przed++;
                }
                void funkcja_po()
                {
                    if (zbior[0] != tab[i, j])
                        energia_po++;
                    if (zbior[1] != tab[i, j])
                        energia_po++;
                    if (zbior[2] != tab[i, j])
                        energia_po++;
                    if (zbior[3] != tab[i, j])
                        energia_po++;
                }

                for (int r = 0; r < ilosc_punktow_for; r++)
                {
                    x = tablica_punktow[rand.Next(ilosc_punktow)];
                    i = x / m;
                    j = x % n;
                    poprzedni_kolor = tab[i, j];
                    energia_po = 0;
                    energia_przed = 0;
                    roznica_energii = 0;

                    if (j == 0 & i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                    }
                    else if (j == n - 1 && i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i + 1, j];
                    }
                    else if (i == 0 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                    }
                    else if (i == m - 1 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i, j];
                    }
                    else if (i == 0 && j == 0)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                    }
                    else if (i == 0 && j == n - 1)
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i + 1, j];
                    }
                    else if (i == m - 1 && j == n - 1)
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j];
                    }
                    else if (i == m - 1 && j == 0)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i, j];
                    }
                    else
                    {
                        zbior[0] = tab[i, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j + 1]; zbior[3] = tab[i + 1, j];
                    }

                    funkcja_przed();
                    switch (rand.Next(4))
                    {
                        case 0:
                            tab[i, j] = zbior[0];
                            break;
                        case 1:
                            tab[i, j] = zbior[1];
                            break;
                        case 2:
                            tab[i, j] = zbior[2];
                            break;
                        case 3:
                            tab[i, j] = zbior[3];
                            break;
                    }
                    funkcja_po();
                    roznica_energii = energia_po - energia_przed;
                    if (roznica_energii <= 0)
                        tab[i, j] = tab[i, j];
                    else
                    {
                        if (rand.Next(100) < (Math.Exp(-((double)roznica_energii) / 0.6)) * 100)
                            tab[i, j] = tab[i, j];
                        else
                            tab[i, j] = poprzedni_kolor;
                    }


                    tablica_punktow[x] = tablica_punktow[ilosc_punktow - 1];
                    ilosc_punktow--;
                }

                return tab;
            }

            public int[,] monte_carlo_moor_periodyczne(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int p = 0; p < m; p++)
                    for (int u = 0; u < n; u++)
                        tab1[p, u] = 0;

                int ilosc_punktow = m * n;
                int ilosc_punktow_for = ilosc_punktow;
                int[] tablica_punktow = new int[ilosc_punktow];
                for (int b = 0; b < ilosc_punktow; b++)
                    tablica_punktow[b] = b;

                Random rand = new Random();
                int x = 0;
                int i = 0, j = 0, energia_przed = 0, energia_po = 0, roznica_energii = 0;
                int poprzedni_kolor = 0;
                int[] zbior;
                zbior = new int[9];

                void funkcja_przed()
                {
                    if (zbior[0] != tab[i, j])
                        energia_przed++;
                    if (zbior[1] != tab[i, j])
                        energia_przed++;
                    if (zbior[2] != tab[i, j])
                        energia_przed++;
                    if (zbior[3] != tab[i, j])
                        energia_przed++;
                    if (zbior[4] != tab[i, j])
                        energia_przed++;
                    if (zbior[5] != tab[i, j])
                        energia_przed++;
                    if (zbior[6] != tab[i, j])
                        energia_przed++;
                    if (zbior[7] != tab[i, j])
                        energia_przed++;
                    if (zbior[8] != tab[i, j])
                        energia_przed++;
                }
                void funkcja_po()
                {
                    if (zbior[0] != tab[i, j])
                        energia_po++;
                    if (zbior[1] != tab[i, j])
                        energia_po++;
                    if (zbior[2] != tab[i, j])
                        energia_po++;
                    if (zbior[3] != tab[i, j])
                        energia_po++;
                    if (zbior[4] != tab[i, j])
                        energia_po++;
                    if (zbior[5] != tab[i, j])
                        energia_po++;
                    if (zbior[6] != tab[i, j])
                        energia_po++;
                    if (zbior[7] != tab[i, j])
                        energia_po++;
                    if (zbior[8] != tab[i, j])
                        energia_po++;
                }

                for (int r = 0; r < ilosc_punktow_for; r++)
                {
                    x = tablica_punktow[rand.Next(ilosc_punktow)];
                    i = x / m;
                    j = x % n;
                    poprzedni_kolor = tab[i, j];
                    energia_po = 0;
                    energia_przed = 0;
                    roznica_energii = 0;

                    if (j == 0 & i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i-1,n-1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i,n-1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i+1,n-1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (j == n - 1 && i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i-1,0]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i,0]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i+1,0];
                    }
                    else if (i == 0 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[m-1,j-1]; zbior[1] = tab[m-1,j]; zbior[2] = tab[m-1,j+1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (i == m - 1 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[0,j-1]; zbior[7] = tab[0,j]; zbior[8] = tab[0,j+1];
                    }
                    else if (i == 0 && j == 0)
                    {
                        zbior[0] = tab[m-1,n-1]; zbior[1] = tab[m-1,j]; zbior[2] = tab[m-1,j+1]; zbior[3] = tab[i,n-1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i,n-1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (i == 0 && j == n - 1)
                    {
                        zbior[0] = tab[m-1,j-1]; zbior[1] = tab[m-1,j]; zbior[2] = tab[m-1,0]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i,0]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i+1,0];
                    }
                    else if (i == m - 1 && j == n - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i-1,0]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i,0]; zbior[6] = tab[0,j-1]; zbior[7] = tab[0,j]; zbior[8] = tab[0,0];
                    }
                    else if (i == m - 1 && j == 0)
                    {
                        zbior[0] = tab[i-1,n-1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i,n-1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[0,n-1]; zbior[7] = tab[0,j]; zbior[8] = tab[0,j+1];
                    }
                    else
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }

                    funkcja_przed();
                    switch (rand.Next(9))
                    {
                        case 0:
                            tab[i, j] = zbior[0];
                            break;
                        case 1:
                            tab[i, j] = zbior[1];
                            break;
                        case 2:
                            tab[i, j] = zbior[2];
                            break;
                        case 3:
                            tab[i, j] = zbior[3];
                            break;
                        case 4:
                            tab[i, j] = zbior[4];
                            break;
                        case 5:
                            tab[i, j] = zbior[5];
                            break;
                        case 6:
                            tab[i, j] = zbior[6];
                            break;
                        case 7:
                            tab[i, j] = zbior[7];
                            break;
                        case 8:
                            tab[i, j] = zbior[8];
                            break;
                    }
                    funkcja_po();
                    roznica_energii = energia_po - energia_przed;
                    if (roznica_energii <= 0)
                        tab[i, j] = tab[i, j];
                    else
                    {
                        if (rand.Next(100) < (Math.Exp(-((double)roznica_energii) / 0.6)) * 100)
                            tab[i, j] = tab[i, j];
                        else
                            tab[i, j] = poprzedni_kolor;
                    }

                    tablica_punktow[x] = tablica_punktow[ilosc_punktow - 1];
                    ilosc_punktow--;
                }

                return tab;
            }

            public int[,] monte_carlo_moor_absorbujace(int[,] tab, int m, int n)
            {
                int[,] tab1 = new int[m, n];
                for (int p = 0; p < m; p++)
                    for (int u = 0; u < n; u++)
                        tab1[p, u] = 0;

                int ilosc_punktow = m * n;
                int ilosc_punktow_for = ilosc_punktow;
                int[] tablica_punktow = new int[ilosc_punktow];
                for (int b = 0; b < ilosc_punktow; b++)
                    tablica_punktow[b] = b;

                Random rand = new Random();
                int x = 0;
                int i = 0, j = 0, energia_przed = 0, energia_po = 0, roznica_energii = 0;
                int poprzedni_kolor = 0;
                int[] zbior;
                zbior = new int[9];

                void funkcja_przed()
                {
                    if (zbior[0] != tab[i, j])
                        energia_przed++;
                    if (zbior[1] != tab[i, j])
                        energia_przed++;
                    if (zbior[2] != tab[i, j])
                        energia_przed++;
                    if (zbior[3] != tab[i, j])
                        energia_przed++;
                    if (zbior[4] != tab[i, j])
                        energia_przed++;
                    if (zbior[5] != tab[i, j])
                        energia_przed++;
                    if (zbior[6] != tab[i, j])
                        energia_przed++;
                    if (zbior[7] != tab[i, j])
                        energia_przed++;
                    if (zbior[8] != tab[i, j])
                        energia_przed++;
                }
                void funkcja_po()
                {
                    if (zbior[0] != tab[i, j])
                        energia_po++;
                    if (zbior[1] != tab[i, j])
                        energia_po++;
                    if (zbior[2] != tab[i, j])
                        energia_po++;
                    if (zbior[3] != tab[i, j])
                        energia_po++;
                    if (zbior[4] != tab[i, j])
                        energia_po++;
                    if (zbior[5] != tab[i, j])
                        energia_po++;
                    if (zbior[6] != tab[i, j])
                        energia_po++;
                    if (zbior[7] != tab[i, j])
                        energia_po++;
                    if (zbior[8] != tab[i, j])
                        energia_po++;
                }

                for (int r = 0; r < ilosc_punktow_for; r++)
                {
                    x = tablica_punktow[rand.Next(ilosc_punktow)];
                    i = x / m;
                    j = x % n;
                    poprzedni_kolor = tab[i, j];
                    energia_po = 0;
                    energia_przed = 0;
                    roznica_energii = 0;

                    if (j == 0 & i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i,j]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i, j]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (j == n - 1 && i != 0 && i != m - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i, j];
                    }
                    else if (i == 0 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (i == m - 1 && j != 0 && j != n - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i, j]; zbior[7] = tab[i, j]; zbior[8] = tab[i, j];
                    }
                    else if (i == 0 && j == 0)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i, j]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }
                    else if (i == 0 && j == n - 1)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i, j];
                    }
                    else if (i == m - 1 && j == n - 1)
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i, j]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j]; zbior[6] = tab[i, j]; zbior[7] = tab[i, j]; zbior[8] = tab[i, j];
                    }
                    else if (i == m - 1 && j == 0)
                    {
                        zbior[0] = tab[i, j]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i, j]; zbior[7] = tab[i, j]; zbior[8] = tab[i, j];
                    }
                    else
                    {
                        zbior[0] = tab[i - 1, j - 1]; zbior[1] = tab[i - 1, j]; zbior[2] = tab[i - 1, j + 1]; zbior[3] = tab[i, j - 1]; zbior[4] = tab[i, j]; zbior[5] = tab[i, j + 1]; zbior[6] = tab[i + 1, j - 1]; zbior[7] = tab[i + 1, j]; zbior[8] = tab[i + 1, j + 1];
                    }

                    funkcja_przed();
                    switch (rand.Next(9))
                    {
                        case 0:
                            tab[i, j] = zbior[0];
                            break;
                        case 1:
                            tab[i, j] = zbior[1];
                            break;
                        case 2:
                            tab[i, j] = zbior[2];
                            break;
                        case 3:
                            tab[i, j] = zbior[3];
                            break;
                        case 4:
                            tab[i, j] = zbior[4];
                            break;
                        case 5:
                            tab[i, j] = zbior[5];
                            break;
                        case 6:
                            tab[i, j] = zbior[6];
                            break;
                        case 7:
                            tab[i, j] = zbior[7];
                            break;
                        case 8:
                            tab[i, j] = zbior[8];
                            break;
                    }
                    funkcja_po();
                    roznica_energii = energia_po - energia_przed;
                    if (roznica_energii <= 0)
                        tab[i, j] = tab[i, j];
                    else
                    {
                        if (rand.Next(100) < (Math.Exp(-((double)roznica_energii) / 0.6)) * 100)
                            tab[i, j] = tab[i, j];
                        else
                            tab[i, j] = poprzedni_kolor;
                    }


                    tablica_punktow[x] = tablica_punktow[ilosc_punktow - 1];
                    ilosc_punktow--;
                }

                return tab;
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

            string[] lista4 = new string[] { "Von Neumann", "Moore", "Pentagonalne Lewe", "Pentagonalne Prawe", "Pentagonalne Gorne", "Pentagonalne Dolne", "Pentagonalne Losowe", "Heksagonalne Lewe", "Heksagonalne Prawe", "Heksagonalne Losowe", "Z promieniem" };
            comboBox4.Items.AddRange(lista4);
            this.Controls.Add(this.comboBox4);
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(comboBox4_SelectedIndexChanged);
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
            while(mc)
            {
                rysuj_monte_carlo();
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            rozrost_ziaren = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tablica_energii = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tablica_energii[i, j] = 0;
            poprzednia_tablica = new int[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    poprzednia_tablica[i, j] = 0;
            mc = true;
            if (mc)
            {
                Thread th = new Thread(nowy_watek);
                th.Start();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            von_Nemann_mc = true;
            moor_mc = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            von_Nemann_mc = false;
            moor_mc = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mc = false;
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mc = true;
            if (mc)
            {
                Thread th = new Thread(nowy_watek);
                th.Start();
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            //energia
            mc = false;
            Graphics grap;
            grap = Graphics.FromImage(DrawArea);
            for(int i=0; i<r2; i++)
            {
                for(int j=0; j<r1; j++)
                {
                    if(tablica_energii[i,j] == 0)
                        grap.FillRectangle(bialy, j * size_x, i * size_y, size_x, size_y);
                    else
                        grap.FillRectangle(czarny, j * size_x, i * size_y, size_x, size_y);
                }
            }
            pictureBox1.Image = DrawArea;
            grap.Dispose();


        }

        private void button11_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            pobierz_dane();
            //rekrystalizacja
            double[,] tablica_dyslokacji = new double[r2, r1];
            for (int i = 0; i < r2; i++)
                for (int j = 0; j < r1; j++)
                    tablica_dyslokacji[i, j] = 0;
            double[] tablica_ro = new double[r2];
            gestosc_dyslokacji = new double[r2];
            double t = 0.0;
            for (int i = 0; i < r2; i++)
            {
                tablica_ro[i] = 0.0;
                gestosc_dyslokacji[i] = 0.0;
            }
            double A = 86710969050178.5;
            double B = 9.41268203527779;
            krytyczna_dyslokacja = 4.21584E+12 / (r2 * r1);

            for (int i=0; i<r2; i++)
            {
                tablica_ro[i] = A / B + (1 - (A / B)) * (Math.Pow(Math.E, B * (-1) * t));
                t = t + 0.001;
            }
            double[] tablica_deltaRo = new double[r2];
            for (int i = 0; i < r2; i++)
                tablica_deltaRo[i] = 0.0;
            for (int i = 0; i < r2; i++)
            {
                if (i == r2 - 1)
                    tablica_deltaRo[i] = tablica_ro[0] - tablica_ro[i];
                else
                    tablica_deltaRo[i] = tablica_ro[i + 1] - tablica_ro[i];
            }
            for (int i = 0; i < r2; i++)
                gestosc_dyslokacji[i] = tablica_deltaRo[i] / (r2 * r1);

            for (int r=0;r<r2; r++)
                for (int i = 0; i < r2; i++)
                    for (int j = 0; j < r1; j++)
                        tablica_dyslokacji[i, j] += gestosc_dyslokacji[r] * 0.7;
            double[] pozostalo = new double[r2];
            for (int i = 0; i < r2; i++)
                pozostalo[i] = tablica_deltaRo[i] * 0.3 / 10.0;


            for(int i=0; i<r2; i++)
            {
                for (int z = 0; z < 10;)
                {
                    int val = rand.Next(100);
                    int x = rand.Next(r2-1);
                    int y = rand.Next(r1-1);
                    if (val < 80 && tablica_energii[x, y] == 1)
                    {
                        tablica_dyslokacji[x, y] += pozostalo[i];
                        z++;
                    }
                    else if (val > 80 && tablica_energii[x, y] == 0)
                    {
                        tablica_dyslokacji[x, y] += pozostalo[i];
                        z++;
                    }



                }
            }


            
            
            Graphics graphh;
            graphh = Graphics.FromImage(DrawArea);
            for (int i=0; i<r2; i++)
            {
                for(int j=0; j<r1; j++)
                {
                    if (tablica_dyslokacji[i, j] > krytyczna_dyslokacja && tablica_energii[i,j]==1)
                        graphh.FillRectangle(zolty, j * size_x, i * size_y, size_x, size_y);
                       // MessageBox.Show("xx");
                   

                }
            }
            pictureBox1.Image = DrawArea;
            graphh.Dispose();




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
                {
                    if (vonNeumann)
                        tablica = s.sprawdz_warunki_brzeogwe_vonNeymana_periodyczne(tablica, r2, r1);
                    else if (moore)
                        tablica = s.sprawdz_warunki_brzegowe_moor_periodyczne(tablica, r2, r1);
                    else if (pentagonalne_lewe)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_lewe_periodyczne(tablica, r2, r1);
                    else if (pentagonalne_prawe)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_prawe_periodyczne(tablica, r2, r1);
                    else if (pentagonalne_gorne)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_gorne_periodyczne(tablica, r2, r1);
                    else if (pentagonalne_dolne)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_dolne_periodyczne(tablica, r2, r1);
                    else if (pentagonalne_losowe)
                    {
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_losowe_periodyczne(tablica, r2, r1); 
                    }
                    else if (heksagonalne_lewe)
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_lewe_periodyczne(tablica, r2, r1);
                    else if (heksagonalne_prawe)
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_prawe_periodyczne(tablica, r2, r1);
                    else if(heksagonalne_losowe)
                    {
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_losowe_periodyczne(tablica, r2, r1);
                    }
                }
                else//absorbujace
                {
                    if (vonNeumann)
                        tablica = s.sprawdz_warunki_brzeogwe_vonNeymana_absorbujace(tablica, r2, r1);
                    else if (moore)
                        tablica = s.sprawdz_warunki_brzegowe_moor_absorbujace(tablica, r2, r1);
                    else if (pentagonalne_lewe)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_lewe_absorbujace(tablica, r2, r1);
                    else if (pentagonalne_prawe)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_prawe_absorbujace(tablica, r2, r1);
                    else if (pentagonalne_gorne)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_gorne_absorbujace(tablica, r2, r1);
                    else if (pentagonalne_dolne)
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_dolne_absorbujace(tablica, r2, r1);
                    else if (pentagonalne_losowe)
                    {
                        
                        tablica = s.sprawdz_warunki_brzegowe_pentagonalne_losowe_absorbujace(tablica, r2, r1);
                    }
                    else if (heksagonalne_lewe)
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_lewe_absorbujace(tablica, r2, r1);
                    else if (heksagonalne_prawe)
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_prawe_absorbujace(tablica, r2, r1);
                    else if (heksagonalne_losowe)
                    {
                        tablica = s.sprawdz_warunki_brzegowe_heksagonalne_losowe_absorbujace(tablica, r2, r1);
                    }
                }
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

            string sasiadztwo = comboBox4.SelectedItem.ToString();
            if (sasiadztwo == "Von Neumann")
                vonNeumann = true;
            else if (sasiadztwo == "Moore")
                moore = true;
            else if (sasiadztwo == "Pentagonalne Lewe")
                pentagonalne_lewe = true;
            else if (sasiadztwo == "Pentagonalne Prawe")
                pentagonalne_prawe = true;
            else if (sasiadztwo == "Pentagonalne Gorne")
                pentagonalne_gorne = true;
            else if (sasiadztwo == "Pentagonalne Dolne")
                pentagonalne_dolne = true;
            else if (sasiadztwo == "Pentagonalne Losowe")
                pentagonalne_losowe = true;
            else if (sasiadztwo == "Heksagonalne Lewe")
                heksagonalne_lewe = true;
            else if (sasiadztwo == "Heksagonalne Prawe")
                heksagonalne_prawe = true;
            else if (sasiadztwo == "Heksagonalne Losowe")
                heksagonalne_losowe = true;
            else if (sasiadztwo == "Z promieniem ")
                promien = true;

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
                int dodano = 0;
                bool poprawne = true;
                double odleglosc = 0.0;
                double d = 0.0;
                g.Clear(Color.DarkGray);
                int promien = int.Parse(textBox3.Text);
                int ilosc = int.Parse(textBox4.Text);
                Random rand = new Random();
                
                for (int k = 1; k < ilosc + 1; k++)
                {
                    odleglosc = 0.0;
                    int a = rand.Next(r2);
                    int b = rand.Next(r1);
                    poprawne = true;
                    if (tablica[a, b] == 0)
                    {
                        for(int i=0; i<r2; i++)
                        {
                            for(int j=0; j<r1; j++)
                            {
                                if(tablica[i,j]!=0)
                                {
                                    d = (i*size_y - a*size_y) * (i*size_y - a*size_y) + (j*size_x - b*size_x) * (j*size_x - b*size_x);
                                    odleglosc = Math.Sqrt(d);
                                    if (odleglosc > 2 * promien*size_x)
                                        poprawne = true;
                                    else
                                    {
                                        poprawne = false;
                                    }
                                }
                                if (poprawne == false)
                                    break;
                            }
                            if (poprawne == false)
                            {
                                if(k>1)
                                    k--;
                                break;
                            }
                        }
                        if (poprawne)
                        {
                            tablica[a, b] = k;
                            dodano++;
                        }
                        
                        //
                           // k--;
                    }
                    
                }
                MessageBox.Show(dodano.ToString());
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

        public void rysuj_monte_carlo()
        {
            pobierz_dane();
            for (int c = 0; c < r2; c++)
                for (int v = 0; v < r1; v++)
                    poprzednia_tablica[c, v] = tablica[c, v];
            lock (g)
            {
                Graphics mon;
                mon = Graphics.FromImage(DrawArea);
                for (int i = 0; i < r2; i++)
                {
                    for (int j = 0; j < r1; j++)
                    {
                        for (int k = 0; k < 1000; k++)
                        {
                            if (tablica[i, j] == k)
                                mon.FillRectangle(solidBrushes[k], j * size_x, i * size_y, size_x, size_y);
                        }
                    }
                }
                if(periodyczne)
                {
                    if (von_Nemann_mc)
                        tablica = s.monte_carlo_von_neumann_periodyczne(tablica, r2, r1);
                    else
                        tablica = s.monte_carlo_moor_periodyczne(tablica, r2, r1);
                }
                else
                {
                    if (von_Nemann_mc)
                        tablica = s.monte_carlo_von_neumann_absorbujace(tablica, r2, r1);
                    else
                        tablica = s.monte_carlo_moor_absorbujace(tablica, r2, r1);
                }
                for(int b=0; b<r2; b++)
                {
                    for(int z=0; z<r1; z++)
                    {
                        if (poprzednia_tablica[b, z] != tablica[b, z])
                            tablica_energii[b, z] = 1;
                    }
                }
                pictureBox1.Image = DrawArea;
                mon.Dispose();
            }
        }
    }

    


}
