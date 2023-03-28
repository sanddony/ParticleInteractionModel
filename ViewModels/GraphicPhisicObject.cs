using System;
using ParticleInteractionModel.Models;
using Avalonia.Controls.Shapes;
using Avalonia.Media;


namespace ParticleInteractionModel.ViewModels
{
    public class GraphicPhisicObject : IPhisicObject
    {
        public Shape VisualObject {get; set ; }
        public IPhisicObject ObjectModel {get; set; }
        public Vector Position { get => ObjectModel.Position; set => ObjectModel.Position=value; }
        public Vector Velocity { get => ObjectModel.Velocity; set => ObjectModel.Velocity=value;  }
        public double Mass { get => ObjectModel.Mass; set => ObjectModel.Mass=value;  }
        public double Width { get => ObjectModel.Width; set => ObjectModel.Width=value;  }
        public double Height { get => ObjectModel.Height; set => ObjectModel.Height=value;  }

        public double Area { get => ObjectModel.Area;}

        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

    }
}