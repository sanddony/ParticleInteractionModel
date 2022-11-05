using Avalonia.Controls;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Collections.Generic;
using ParticleInteractionModel.Models;

namespace ParticleInteractionModel.Views
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        MainModel mainModel = new MainModel();
        public MainWindow()
        {
            InitializeComponent();

            MainField.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            
        }
        
        
        private void GameTimerEvent(object sender, EventArgs e)
        {
            mainModel.CalculatePosition(100,0,100,0);
            foreach(Ball ball in mainModel.balls){
                Canvas.SetLeft();
            }
        }
        
    }



}
