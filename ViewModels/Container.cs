using System;
using System.Collections.Generic;
using System.Timers;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using ParticleInteractionModel.Models;

namespace ParticleInteractionModel.Models
{
    public class Container : IContainer, IContainerUI
    {
        public PhysicalModel Engine {get; set;}

        public double Size {get => Width * Height; set => Size = value;}
        public double Width {get; set;}
        public double Height {get; set;}

        public List<IPhisicObject> Walls {get; set;}

        public List<IPhisicObject> Objects {get; set;}

        public Timer Timer {get; set;}

        public Canvas MainField {get; set;}

        public Container()
        {

        }
        public Container(List<IPhisicObject> walls, 
                         List<IPhisicObject> objects,
                         PhysicalModel engine,
                         int timerInterval,
                         double width,
                         double height)
        {
            Width = width;
            Height = height;
            Walls = walls;
            Objects = objects;
            
            Timer = new Timer(timerInterval);
            Timer.Elapsed += ModelCalculations;

            MainField = new Canvas();
            MainField.Focus();
            MainField.Width = width; 
            MainField.Height = height; 

        }

        public void ModelCalculations(object? sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Render(object? sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }


        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}