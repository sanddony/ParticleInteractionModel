using System;
using System.Timers;
using System.Collections.Generic;


namespace ParticleInteractionModel.Models
{
    public interface IContainer
    {
        public PhysicalModel Engine { get; set;}
        public double Size {get; set;}
        public double Width {get; set;}
        public double Height {get; set;}
        public List<IPhisicObject> Walls {get; set;}
        public List<IPhisicObject> Objects {get; set;}
        public Timer Timer {get; set;}
        public void ModelCalculations(object? sender, ElapsedEventArgs e);
        public void Pause();
        public void Start();

    }
}