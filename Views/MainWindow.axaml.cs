using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Collections.Generic;
using ParticleInteractionModel.Models;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;

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
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();


            foreach (Ball ball in mainModel.balls)
            {
                el = new Ellipse();
                el.Width = ball.Diameter;
                el.Height = ball.Diameter;
                el.Fill = Brushes.Black;
                el.Stroke = Brushes.Red;
                el.StrokeThickness = 3;
                Canvas.SetLeft(el, ball.Position.X);
                Canvas.SetTop(el, ball.Position.Y);
                MainField.Children.Add(el);
                particles.Add(ball, el);
            }
        }
        
        
        private void GameTimerEvent(object sender, EventArgs e)
        {
            mainModel.CalculatePosition(650,0,450,0);
            foreach (Ball ball in mainModel.balls)
            {
                Canvas.SetLeft(particles[ball], Canvas.GetLeft(particles[ball]) + ball.Velocity.X);
                Canvas.SetTop(particles[ball], Canvas.GetTop(particles[ball]) + ball.Velocity.Y);
            }


            /*            Canvas.SetLeft(el, Canvas.GetLeft(el) + 1);
            */
        }

    }


}
