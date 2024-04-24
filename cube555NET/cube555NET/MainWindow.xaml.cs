using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

namespace cube555NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel[][] facelet = new StackPanel[6][];
        Color[] COLORS = { Colors.White, Colors.Red, Colors.Green, Colors.Yellow, Colors.Orange, Colors.Blue };
        int curColor;

        private int maxDepth = 21, maxTime = 5;
        bool useSeparator = true;
        bool showString = false;
        bool inverse = true;
        bool showLength = true;
        cs.cube555.Search search = new cs.cube555.Search();
        cs.min2phase.Search search333 = new cs.min2phase.Search();
        public MainWindow()
        {
            InitializeComponent();

            this.Background = Brushes.AntiqueWhite;

            for (int i = 0; i < 6; i++)
            {
                facelet[i] = new StackPanel[25];
            }
            Grid[] faceletGrids = { gridU, gridR, gridF, gridD, gridL, gridB };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    facelet[i][j] = new StackPanel();
                    facelet[i][j].Background = new SolidColorBrush(Colors.Gray);
                    facelet[i][j].Margin = new Thickness(1);
                    faceletGrids[i].Children.Add(facelet[i][j]);
                    Grid.SetRow(facelet[i][j], j / 5);
                    Grid.SetColumn(facelet[i][j], j % 5);
                    facelet[i][j].MouseUp += MainWindow_MouseUp;

                }
            }

            checkBoxShowLen.Click += checkBox_Click;
            checkBoxShowStr.Click += checkBox_Click;
            checkBoxUseSep.Click += checkBox_Click;

            Stopwatch timer = Stopwatch.StartNew();
            cs.cube555.Search.init();
            timer.Stop();
            //MessageBox.Show(timer.ElapsedMilliseconds.ToString());
            Debug.WriteLine($"isInited { timer.ElapsedMilliseconds}ms");

        }


        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            sp.Background = new SolidColorBrush(COLORS[curColor]);
        }


        private void cx_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            curColor = Convert.ToInt32(b.Name.Substring(1));
        }


        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            useSeparator = (bool)checkBoxUseSep.IsChecked;
            showString = (bool)checkBoxShowStr.IsChecked;
            showLength = (bool)checkBoxShowLen.IsChecked;
        }


        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            string r = cs.cube555.Tools.randomCube();
            txtInfo.Text = r;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    switch (r[25 * i + j])
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

        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder s = new StringBuilder(150);

            for (int i = 0; i < 150; i++)
                s.Insert(i, '-');// default initialization

            for (int i = 0; i < 6; i++)
                // read the 54 facelets
                for (int j = 0; j < 25; j++)
                {
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[0])
                        s[25 * i + j] = 'U';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[1])
                        s[25 * i + j] = 'R';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[2])
                        s[25 * i + j] = 'F';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[3])
                        s[25 * i + j] = 'D';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[4])
                        s[25 * i + j] = 'L';
                    if (((SolidColorBrush)facelet[i][j].Background).Color == COLORS[5])
                        s[25 * i + j] = 'B';
                }

            String cubestring = s.ToString();

            cubestring = "LFBBRLRRLUFUULDUFBFBUFDLFUDRRFLRFLRDLRBULDDUDBDLBUFUFURRFFDFLRFDLUUBBDDRFLURFUFRUFDBRRRDFBDRRUULRLBBDDRLLDBLDDRULFURULLBDFURBDLDUBBRBLFBDBUBBBUDFLLFBF";

            Debug.WriteLine("Cube Definition String: " + cubestring);
            if (showString)
            {
                MessageBox.Show("Cube Definiton String: " + cubestring);
            }
            int mask = 0;
            mask |= useSeparator ? cs.cube555.Search.USE_SEPARATOR : 0;
            //mask |= inverse ? Search.INVERSE_SOLUTION : 0;
            //mask |= showLength ? Search.APPEND_LENGTH : 0;
            Stopwatch timer = Stopwatch.StartNew();
            long t;
            String[] ret = search.solveReduction(cubestring, mask);
            String result = ret[0];
            if (!result.Contains("Error"))
            {
                String solution333 = search333.getsolution(ret[1], 21, int.MaxValue, 500, mask);
                result += solution333;
                if (showLength)
                {
                    int length = 0;
                    for (int i = 0; i < result.Length; i++)
                    {
                        if ("URFDLBurfdlb".IndexOf(result[i]) != -1)
                        {
                            length++;
                        }
                    }
                    result += $"({length}f)";
                }
            }
            timer.Stop();
            t = timer.ElapsedMilliseconds;
            // +++++++++++++++++++ Replace the error messages with more meaningful ones in your language ++++++++++++++++++++++
            if (result.Contains("Error"))
            {
                switch (result[result.Length - 1])
                {
                    case '1':
                        result = "There are not exactly 25 facelets of each color!";
                        break;
                    case '2':
                        result = "There are not exactly 4 Tcenters of each color!";
                        break;
                    case '3':
                        result = "There are not exactly 4 Xcenters of each color!";
                        break;
                    case '4':
                        result = "Not all 12 edges exist exactly once!";
                        break;
                    case '5':
                        result = "Not all 24 wedges exist exactly once!";
                        break;
                    case '6':
                        result = "Not all 8 corners exist exactly once!";
                        break;
                    case '7':
                        result = "Flip error: One edge has to be flipped!";
                        break;
                    case '8':
                        result = "Twist error: One corner has to be twisted!";
                        break;
                    case '9':
                        result = "Parity error: Two corners or two edges have to be exchanged!";
                        break;
                }
            }
            else
            {
                txtInfo.Text = $"{result}\n" + txtInfo.Text;
                txtInfo.Select(0, result.Length);
            }
            Debug.WriteLine("Result: " + result);
            MessageBox.Show(result, $"{t} ms");

        }

    }
}
