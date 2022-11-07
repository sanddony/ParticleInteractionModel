using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Collections.Generic;
using ParticleInteractionModel.Models;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;
using ReactiveUI;

namespace ParticleInteractionModel.Views
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        MainModel mainModel = new MainModel();
        Ellipse el;
        Dictionary<Ball, Ellipse> particles = new Dictionary<Ball, Ellipse>();

        public MainWindow()
        {
            InitializeComponent();

            MainField.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(0.1);
            gameTimer.Start();

            Random random = new Random();
            for (int i = 0; i < 130; i++)
            {
                int diameter = random.Next(20, 21);
                int mass = diameter * 10;
                int x = random.Next(diameter, 650 - diameter);
                int y = random.Next(diameter, 450 - diameter);
                Vector velocity = new Vector(random.Next(1, 5), random.Next(1, 5));
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
                particles.Add(ball, el);
            }

        }
        
        
        private void GameTimerEvent(object sender, EventArgs e)
        {

            foreach (var item1 in particles)
            {
                foreach (var item2 in particles)
                {
                    if (item1.Key == item2.Key) continue;
                    else Ball.BouncingOfBalls(item1.Key, item2.Key);
                    item1.Key.BouncingOfWalls(800, 0, 450, 0);
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

    }


}
