using System;
using System.Timers;
using System.Collections.Generic;


namespace ParticleInteractionModel.Models
{
    public interface IContainer
    {
        public PhysicalModel MyProperty { get; }
        public double Size { get; }
        public List<IPhisicObject> Walls { get; }
        public List<IPhisicObject> Objects { get; }
        public Timer Timer {get;}
        public void Add(List<IPhisicObject> phisicObjects);
        public void Add(IPhisicObject phisicObject);
        public void Remove(List<IPhisicObject> phisicObjects);
        public void Remove(IPhisicObject phisicObject);
        public void ModelCalculations();
        public void Pause();
        public void Start();

    }
}