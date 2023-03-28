using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    
    public class Rect : IPhisicObject
    {
        public Vector Position { get; set; }

        public Vector Velocity {get; set ;  }
        
        public double Mass {get ; set;  }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area {get => Width * Height; }
        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object)
        {
            rule.CollisionHandler(this, collided_object);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }


    
}