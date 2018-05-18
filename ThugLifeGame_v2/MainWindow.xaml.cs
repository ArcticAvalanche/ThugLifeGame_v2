using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ThugLifeGame_v2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void VoidMethodContainer();
        public event VoidMethodContainer MakeStepButtonPressed;
        public delegate void x2intMethodHolder(int a, int b);
        public event x2intMethodHolder CanvasClickedLeft;
        public event x2intMethodHolder CanvasClickedRight;

        Terrain terrain;

        public MainWindow()
        {
            InitializeComponent();

            terrain = new Terrain(canvas);
            terrain = new StatisticsTerrainDecorator(terrain, canvas);
            terrain = new ScannerTerrainDecorator(terrain, canvas, patternsCanvas);
            terrain = new FramedTerrainDecorator(terrain, canvas, drawLines);

            MakeStepButtonPressed += terrain.MakeStep;
            CanvasClickedLeft += terrain.PointCreate;
            CanvasClickedRight += terrain.PointRemove;
        }

        private void makeStep_Click(object sender, RoutedEventArgs e)
        {
            MakeStepButtonPressed();
            stepTime.Text = (terrain.GetTimeSpan().Milliseconds / 1000f).ToString("F3"); ;
            cellsNumber.Text = terrain.GetCellsNumber().ToString();
            changePercent.Text = terrain.GetChangePercent().ToString() + '%';
        }
        
        private void reset_Click(object sender, RoutedEventArgs e)
        {            
            terrain.ResetField();
            terrain.SetDrawLines(drawLines);
            cellsNumber.Text = "70";
            changePercent.Text = "0%";
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int X = (int)Mouse.GetPosition(canvas).X;
            int Y = (int)Mouse.GetPosition(canvas).Y;

            CanvasClickedLeft(Y, X);
        }

        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            int X = (int)Mouse.GetPosition(canvas).X;
            int Y = (int)Mouse.GetPosition(canvas).Y;

            CanvasClickedRight(Y, X);
        }

        private void showPatterns_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Visibility == Visibility.Visible)
            {
                canvas.Visibility = Visibility.Hidden;
                patternsCanvas.Visibility = Visibility.Visible;
                showPatterns.Content = "Show all";
            }
            else
            {
                patternsCanvas.Visibility = Visibility.Hidden; 
                canvas.Visibility = Visibility.Visible;
                showPatterns.Content = "Show patterns";
            }
        }

        private void drawLines_Click(object sender, RoutedEventArgs e)
        {
            terrain.SetDrawLines(drawLines);
        }
    }
}