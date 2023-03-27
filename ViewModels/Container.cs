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
        public PhysicalModel MyProperty => throw new NotImplementedException();

        public double Size => throw new NotImplementedException();

        public List<IPhisicObject> Walls => throw new NotImplementedException();

        public List<IPhisicObject> Objects => throw new NotImplementedException();

        public Timer Timer => throw new NotImplementedException();

        public Canvas MainField => throw new NotImplementedException();

        public void Add(List<IPhisicObject> phisicObjects)
        {
            throw new NotImplementedException();
        }

        public void Add(IPhisicObject phisicObject)
        {
            throw new NotImplementedException();
        }

        public void ModelCalculations()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Remove(List<IPhisicObject> phisicObjects)
        {
            throw new NotImplementedException();
        }

        public void Remove(IPhisicObject phisicObject)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}