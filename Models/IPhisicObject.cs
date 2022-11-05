using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public interface IPhisicObject
    {
        public Vector Position { get; set; }

        public Vector Velocity { get; set; }

        public double Mass { get; set; }

        public double Acceleration { get; set; }

        public void Move();

        public void SlowlyDown(double k);


    }
}
