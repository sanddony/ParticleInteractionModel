using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public interface IPhisicObject
    {
        public Vector Position { get; }

        public Vector Velocity { get; }

        public double Mass { get; }

        public Vector Acceleration { get; }

        public void Move();


    }
}
