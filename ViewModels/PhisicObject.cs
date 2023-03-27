using System;
using ParticleInteractionModel.Models;
using Avalonia.Controls.Shapes;
using Avalonia.Media;


namespace ParticleInteractionModel.ViewModels
{
    public class PhisicObject :  Shape, IPhisicObject
    {
        public Vector Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector Velocity { get; }
        public double Mass { get; }

        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        protected override Geometry? CreateDefiningGeometry()
        {
            throw new NotImplementedException();
        }
    }
}