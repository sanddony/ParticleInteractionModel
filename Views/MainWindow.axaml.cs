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

namespace ParticleInteractionModel.Views
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer graphTimer = new DispatcherTimer();
        MainModel mainModel = new MainModel();

        Dictionary<Ball, Ellipse> particles1 = new Dictionary<Ball, Ellipse>();
        Dictionary<Ball, Ellipse> particles2 = new Dictionary<Ball, Ellipse>();
        Dictionary<Ball, Ellipse> particles3 = new Dictionary<Ball, Ellipse>();

        Window InfoGraph;

        bool merged;// костыль


        int windowHeight = 600;
        int windowWigth = 1000;

        public MainWindow()
        {
            InitializeComponent();

            MainField.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(1);
            gameTimer.Start();

            graphTimer.Tick += GraphTimerEvent;
            graphTimer.Interval = TimeSpan.FromSeconds(1);
            graphTimer.Start();

            GenerateContainerWithParticles(particles1, 1, 5, 100, 1, 3, Brushes.Green, 0, windowWigth/2, 0, windowHeight);
            GenerateContainerWithParticles(particles2, 300, 10, 150, 3, 4, Brushes.Black, windowWigth / 2, windowWigth, 0, windowHeight);

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
            // RenderParticles(particles1, 0, 1600/2, 900, 0);
            if(merged) BuildGraph(particles3);
            else BuildGraph(particles2);


        }

        public void RenderParticles(Dictionary<Ball, Ellipse> particles,
                                    int LeftBorder,
                                    int RightBorder,
                                    int DownBorder,
                                    int UpBorder)
        {
            foreach (var item1 in particles)
            {
                foreach (var item2 in particles)
                {
                    if (item1.Key == item2.Key) continue;
                    else Ball.BouncingOfBalls(item1.Key, item2.Key);
                    item1.Key.BouncingOfWalls(LeftBorder, RightBorder,
                                              DownBorder, UpBorder);
                }
            }
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
                                                    int yTo)
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

        private void BuildGraph(Dictionary<Ball, Ellipse> particles)
        {
            AvaPlot avaPlot1 = InfoGraph.Find<AvaPlot>("AvaPlot1");
            if (avaPlot1 == null) return;
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
                for (int k = 0; k < dataX.Length; k++)
                {
                    if (dataX[i] == dataX[k]) count++;
                }
                dataY[i] = count;
            }
            
            avaPlot1.Plot.AddScatter(dataX, dataY).OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

            avaPlot1.Refresh();
        }

    }


}
