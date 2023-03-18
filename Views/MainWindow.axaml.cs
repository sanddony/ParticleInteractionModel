using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Linq;
using Avalonia.Interactivity;
using System.Collections.Generic;
using ParticleInteractionModel.Models;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;
using ScottPlot.Avalonia;
using ScottPlot.Drawing.Colormaps;
using Avalonia.Input;

namespace ParticleInteractionModel.Views
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer graphTimer = new DispatcherTimer();
        MainModel mainModel = new MainModel();

        private Dictionary<Ball, Ellipse> particles1 = new Dictionary<Ball, Ellipse>(); // изменить
        private Dictionary<Ball, Ellipse> particles2 = new Dictionary<Ball, Ellipse>(); // к примеру, сделать список словарей
        private Dictionary<Ball, Ellipse> particles3 = new Dictionary<Ball, Ellipse>(); // потому что их количество может меняться

        Window InfoGraph;

        bool merged;// костыль


        int windowHeight = 900;
        int windowWigth = 1400;

        public MainWindow()
        {
            InitializeComponent();

            MainField.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            gameTimer.Start();

            /*graphTimer.Tick += GraphTimerEvent;
            graphTimer.Interval = TimeSpan.FromSeconds(5);
            graphTimer.Start();*/

            GenerateContainerWithParticles(particles1, 100, 10, 100, 1, 5, Brushes.Green, 0, windowWigth, 0, windowHeight);
            GenerateContainerWithParticles(particles2, 300, 20, 150, 3, 4, Brushes.Black, windowWigth / 2, windowWigth, 0, windowHeight);

            merged = false; // костыль
        }


        private void GameTimerEvent(object sender, EventArgs e)
        {   
            
            if(merged)RenderParticles(particles3, 0, windowWigth, windowHeight, 0);
            else
            {
                RenderParticles(particles1, 0, windowWigth / 2, windowHeight, 0);
                RenderParticles(particles2, windowWigth / 2, windowWigth, windowHeight, 0);
            }

        }        
        
        private void GraphTimerEvent(object sender, EventArgs e)
        {   
            if(merged) BuildGraph(particles3);
            else BuildGraph(particles1);
        }

        public void RenderParticles(Dictionary<Ball, Ellipse> particles,
                                    int LeftBorder,
                                    int RightBorder,
                                    int DownBorder,
                                    int UpBorder)
        {
            //
            foreach (var item1 in particles)
            {
                foreach (var item2 in particles)
                {
                    if (item1.Key == item2.Key) continue;
                    else Ball.BouncingOfBalls(item1.Key, item2.Key);
                    item1.Key.BouncingOfWalls(LeftBorder, RightBorder,
                                              DownBorder, UpBorder);
                }
            } // вынести в Model
            
            foreach (var item in particles)
            {
                item.Key.Position += item.Key.Velocity;
                Canvas.SetLeft(item.Value, item.Key.Position.X);
                Canvas.SetTop(item.Value, item.Key.Position.Y);
            }

        }

        private void GenerateContainerWithParticles(Dictionary<Ball, Ellipse> particles,
                                                    int Count,
                                                    int diameter,
                                                    int mass,
                                                    int speedFrom,
                                                    int speedTo,
                                                    IBrush color,
                                                    int xFrom,
                                                    int xTo,
                                                    int yFrom,
                                                    int yTo) // Вынести в Model или хотя бы в VM
        {
            Random random = new Random();
            for (int i = 0; i < Count; i++)
            {
                int x = random.Next(2 * diameter + xFrom + 10, xTo - diameter);
                int y = random.Next(2 * diameter + yFrom + 10, yTo - diameter);
                Vector velocity = new Vector(random.Next(speedFrom, speedTo), random.Next(speedFrom, speedTo));
                Ball ball = new Ball(new Vector(x, y),
                                     velocity,
                                     (0, 0, 0), diameter, mass);
                Ellipse el = new Ellipse();
                el.Width = ball.Diameter;
                el.Height = ball.Diameter;
                el.Fill = color;
                Canvas.SetLeft(el, ball.Position.X);
                Canvas.SetTop(el, ball.Position.Y);
                MainField.Children.Add(el);
                particles.Add(ball, el);
            }
        }

        private void MergeContainersClick(object sender, RoutedEventArgs e)
        {
            if (!merged)// костыль
            {
                particles3 = particles1.Concat(particles2).ToDictionary(x => x.Key, x => x.Value);
                merged = true;
            }
            particles1 = new Dictionary<Ball, Ellipse>();
            particles2 = new Dictionary<Ball, Ellipse>();
        }

        private void ShowInfoGraphicsClick(object sender, RoutedEventArgs e)
        {
            InfoGraph = new InfoGraphicss();
            InfoGraph.Show();
        }

        private void AddRemoveEllipses(object sender, PointerPressedEventArgs e)
        {
            if(e.Pointer is Ellipse)
            {
                Ellipse rect = (Ellipse)e.Pointer;
                MainField.Children.Add(rect);
            }
            this.Find<TextBlock>("Menu").Text= e.ToString(); // REMOVE
        }


        private void BuildGraph(Dictionary<Ball, Ellipse> particles)
        {
            AvaPlot avaPlot1;
            try
            {
                avaPlot1 = InfoGraph.Find<AvaPlot>("AvaPlot1");
            }
            catch (Exception ArgumentNullException)
            {
                Console.WriteLine(ArgumentNullException);
                 return;
            }
            avaPlot1.Plot.Clear();
            avaPlot1.Refresh();
            double[] dataX = new double[particles.Count];
            double[] dataY = new double[particles.Count];
            double e = 0;
            for (int i = 0; i < particles.Count; i++)
            {
                dataX[i] = Math.Round(particles.ElementAt(i).Key.Velocity.Length(),1);
                e += ( particles.ElementAt(i).Key.Mass * Math.Pow(particles.ElementAt(i).Key.Velocity.Length(), 2) ) /2;
            }
            double e_k = Math.Round(e / particles.Count, 2);
            double n = (particles.Count / (windowHeight * windowWigth));
            double k = 1.38;
            double t = (1 / k) * (2 / 3) * e_k;
            double p = n * k * t;
            double v = Math.Sqrt((3*k*t)/150);

            InfoGraph.Find<TextBlock>("Energy").Text = e_k.ToString();
            InfoGraph.Find<TextBlock>("Pressure").Text = p.ToString();
            InfoGraph.Find<TextBlock>("Temperature").Text = t.ToString();
            InfoGraph.Find<TextBlock>("Velocity").Text = v.ToString();

            double temp;
            for (int i = 0; i < dataX.Length; i++)
            {
                for (int j = i + 1; j < dataX.Length; j++)
                {
                    if (dataX[i] > dataX[j])
                    {
                        temp = dataX[i];
                        dataX[i] = dataX[j];
                        dataX[j] = temp;
                    }
                }
            }
            for (int i = 0; i < dataX.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < dataX.Length; j++)
                {
                    if (dataX[i] == dataX[j]) count++;
                }
                dataY[i] = count;
            }
            
            avaPlot1.Plot.AddScatter(dataX, dataY).OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

            avaPlot1.Refresh();
        }

    }


}
