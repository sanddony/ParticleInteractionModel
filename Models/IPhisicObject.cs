using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public interface IPhisicObject
    {
        public Vector Position { get; set;}

        public Vector Velocity { get; set;}

        public double Mass { get; set;}
        public double Width { get; set;}
        public double Height { get; set;}
        public double Area { get; }


        public void Move();

        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object);
    }
}
