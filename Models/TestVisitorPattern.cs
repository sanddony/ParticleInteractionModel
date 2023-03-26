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
        public Vector Position => throw new NotImplementedException();

        public Vector Velocity => throw new NotImplementedException();

        public double Mass => throw new NotImplementedException();

        public void CalculateCollision(IPhisicsRule rule, IPhisicObject collided_object)
        {
            rule.CollisionHandler(this, collided_object);
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }

    public interface IPhisicsRule
    {
        public void CollisionHandler<T,V>(T obj_1, V obj_2);
    }

    public class IdealGassModel : IPhisicsRule
    {
        public void CollisionHandler<T, V>(T obj_1, V obj_2)
        {
            throw new NotImplementedException();
        }
    }


}