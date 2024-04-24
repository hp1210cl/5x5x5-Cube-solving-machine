using cs.min2phase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace min2phaseNET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel[][] facelet=new StackPanel[6][];
        Color[] COLORS = { Colors.White, Colors.Red, Colors.Green, Colors.Yellow, Colors.Orange, Colors.Blue };
        int curColor = 0;
        private int maxDepth = 21, maxTime = 5;
        bool useSeparator = true;
        bool showString = false;
        bool inverse = true;
        bool showLength = true;
        Search search = new Search();

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            String r = Tools.randomCube();
            txtInfo.Text = r;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    switch (r[9 * i + j])
                    {
                        case 'U':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[0]);
                            break;
                        case 'R':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[1]);
                            break;
                        case 'F':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[2]);
                            break;
                        case 'D':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[3]);
                            break;
                        case 'L':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[4]);
                            break;
                        case 'B':
                            facelet[i][j].Background = new SolidColorBrush(COLORS[5]);
                            break;
                    }
                }
            }
            txtMaxMoves.Text = 21.ToString();
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder(54);

            for (int i = 0; i < 54; i++)
                s.Insert(i, 'B');// default initialization

            for (int i = 0; i < 6; i++)
                // read the 54 facelets
                for (int j = 0; j < 9; j++)
                {
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[0][4].Background).Color)
                        s[9 * i + j] ='U';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[1][4].Background).Color)
                        s[9 * i + j]= 'R';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[2][4].Background).Color)
                        s[9 * i + j]= 'F';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[3][4].Background).Color)
                        s[9 * i + j]= 'D';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[4][4].Background).Color)
                        s[9 * i + j]= 'L';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == ((SolidColorBrush)facelet[5][4].Background).Color)
                        s[9 * i + j]= 'B';
                }

            String cubeString = s.ToString();
            if (showString)
            {
                MessageBox.Show("Cube Definiton String: " + cubeString);
            }
            int mask = 0;
            mask |= useSeparator ? Search.USE_SEPARATOR : 0;
            mask |= inverse ? Search.INVERSE_SOLUTION : 0;
            mask |= showLength ? Search.APPEND_LENGTH : 0;
            Stopwatch timer =  Stopwatch.StartNew();
            long t;
            String result = search.getsolution(cubeString, maxDepth, 100, 0, mask); ;
            long n_probe = search.numberOfProbes();
            // ++++++++++++++++++++++++ Call Search.solution method from package org.kociemba.twophase ++++++++++++++++++++++++
            while (result.StartsWith("Error 8") && (timer.ElapsedMilliseconds) < maxTime*1000 )
            {
                result = search.next(100, 0, mask);
                n_probe += search.numberOfProbes();
            }
            timer.Stop();
            t = timer.ElapsedMilliseconds;

            // +++++++++++++++++++ Replace the error messages with more meaningful ones in your language ++++++++++++++++++++++
            if (result.Contains("Error"))
            {
                switch (result[result.Length - 1])
                {
                    case '1':
                        result = "There are not exactly nine facelets of each color!";
                        break;
                    case '2':
                        result = "Not all 12 edges exist exactly once!";
                        break;
                    case '3':
                        result = "Flip error: One edge has to be flipped!";
                        break;
                    case '4':
                        result = "Not all 8 corners exist exactly once!";
                        break;
                    case '5':
                        result = "Twist error: One corner has to be twisted!";
                        break;
                    case '6':
                        result = "Parity error: Two corners or two edges have to be exchanged!";
                        break;
                    case '7':
                        result = "No solution exists for the given maximum move number!";
                        break;
                    case '8':
                        result = "Timeout, no solution found within given maximum time!";
                        break;
                }
                MessageBox.Show(result, t.ToString() + " ms | " + n_probe + " probes");
            }
            else
            {
                int solLen = (result.Length - (useSeparator ? 3 : 0) - (showLength ? 4 : 0)) / 3;
                txtMaxMoves.Text=(solLen - 1).ToString();
                txtInfo.Text = $"{result}\n{txtInfo.Text}";
                txtInfo.Select(0, result.Length);
            }
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            inverse = (bool)checkBoxInv.IsChecked;
            useSeparator = (bool)checkBoxUseSep.IsChecked;
            showString = (bool)checkBoxShowStr.IsChecked;
            showLength = (bool)checkBoxShowLen.IsChecked;
        }

        private void txtMaxMoves_TextChanged(object sender, TextChangedEventArgs e)
        {
            maxDepth = Convert.ToInt32(txtMaxMoves.Text);
        }

        private void txtTimeout_TextChanged(object sender, TextChangedEventArgs e)
        {
            maxTime = Convert.ToInt32(txtTimeout.Text);
        }

        private void cx_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            curColor = Convert.ToInt32(b.Name.Substring(1));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Background = Brushes.AntiqueWhite;

            for (int i = 0; i < 6; i++)
            {
                facelet[i] = new StackPanel[9];
            }
            Grid[] faceletGrids = { gridU, gridR, gridF, gridD, gridL, gridB };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    facelet[i][j] = new StackPanel();
                    facelet[i][j].Background = new SolidColorBrush(Colors.Gray);
                    facelet[i][j].Margin = new Thickness(1);
                    faceletGrids[i].Children.Add(facelet[i][j]);
                    Grid.SetRow(facelet[i][j], j / 3);
                    Grid.SetColumn(facelet[i][j], j % 3);
                    facelet[i][j].MouseUp += MainWindow_MouseUp;
                }
            }

            checkBoxInv.Click += checkBox_Click;
            checkBoxShowLen.Click += checkBox_Click;
            checkBoxShowStr.Click += checkBox_Click;
            checkBoxUseSep.Click += checkBox_Click;

            try
            {
                using(FileStream filestream=new FileStream("twophase.data", FileMode.Open))
                {
                    Tools.initFrom(filestream);
                }
            }
            catch(FileNotFoundException e)
            {

            }
            if (!Search.isInited())
            {
                Search.init();
                try
                {
                    using (FileStream filestream = new FileStream("twophase.data", FileMode.CreateNew))
                    {
                        Tools.saveTo(filestream);
                    }
                }
                catch(IOException e)
                {

                }
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp =(StackPanel)sender;
            sp.Background = new SolidColorBrush(COLORS[curColor]);
        }
    }
}
