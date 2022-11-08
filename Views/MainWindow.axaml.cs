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

namespace ParticleInteractionModel.Views
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        MainModel mainModel = new MainModel();
        Ellipse el;
        Dictionary<Ball, Ellipse> particles1 = new Dictionary<Ball, Ellipse>();
        Dictionary<Ball, Ellipse> particles2 = new Dictionary<Ball, Ellipse>();
        Dictionary<Ball, Ellipse> particles3 = new Dictionary<Ball, Ellipse>();
        bool merged;// костыль



        public MainWindow()
        {
            InitializeComponent();

            MainField.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTimer.Start();

            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int diameter = random.Next(15, 16);
                int mass = diameter * 10;
                int x = random.Next(diameter, 1600 / 2 - diameter);
                int y = random.Next(diameter, 900 - diameter);
                Vector velocity = new Vector(random.Next(1, 3), random.Next(1, 3));
                Ball ball = new Ball(new Vector(x, y),
                                     velocity,
                                     (0, 0, 0), diameter, mass);
                el = new Ellipse();
                el.Width = ball.Diameter;
                el.Height = ball.Diameter;
                el.Fill = Brushes.Black;
                el.Stroke = Brushes.Red;
                el.StrokeThickness = 1;
                Canvas.SetLeft(el, ball.Position.X);
                Canvas.SetTop(el, ball.Position.Y);
                MainField.Children.Add(el);
                particles1.Add(ball, el);
            }

            for (int i = 0; i < 50; i++)
            {
                int diameter = random.Next(25, 26);
                int mass = diameter * 10;
                int x = random.Next(diameter + 800, 1600 - diameter);
                int y = random.Next(diameter, 900 - diameter);
                Vector velocity = new Vector(random.Next(1, 3), random.Next(1, 3));
                Ball ball = new Ball(new Vector(x, y),
                                     velocity,
                                     (0, 0, 0), diameter, mass);
                el = new Ellipse();
                el.Width = ball.Diameter;
                el.Height = ball.Diameter;
                el.Fill = Brushes.Green;
                el.Stroke = Brushes.Red;
                el.StrokeThickness = 1;
                Canvas.SetLeft(el, ball.Position.X);
                Canvas.SetTop(el, ball.Position.Y);
                MainField.Children.Add(el);
                particles2.Add(ball, el);
            }
            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
            avaPlot1.Plot.AddScatter(dataX, dataY);
            avaPlot1.Refresh();

            merged = false; // костыль
        }


        private void GameTimerEvent(object sender, EventArgs e)
        {   
            // RenderParticles(particles1, 0, 1600/2, 900, 0);
            RenderParticles(particles1, 0, 1600 / 2, 900, 0);
            RenderParticles(particles2, 1600 / 2, 1600, 900, 0);
            RenderParticles(particles3, 0, 1600, 900, 0);





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
                    item1.Key.Move();
                }
            }
            foreach (var item in particles)
            {
                item.Key.Position += item.Key.Velocity;
                Canvas.SetLeft(item.Value, item.Key.Position.X);
                Canvas.SetTop(item.Value, item.Key.Position.Y);
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

    }


}
