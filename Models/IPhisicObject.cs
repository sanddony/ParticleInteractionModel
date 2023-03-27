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

        public Vector Velocity { get; }

        public double Mass { get; }

        public void Move();

        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object);
    }
}
