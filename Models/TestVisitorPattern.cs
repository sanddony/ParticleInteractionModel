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
        Vector Position => throw new NotImplementedException();

        Vector Velocity => throw new NotImplementedException();

        double Mass => throw new NotImplementedException();

        void CalculateCollision(IPhisicsRule rule, IPhisicObject collided_object)
        {
            rule.CollisionHandler(this, collided_object);
        }

        void Move()
        {
            throw new NotImplementedException();
        }
    }

    public interface IPhisicsRule
    {
        void CollisionHandler(Ball obj1, Rect obj2);

        void CollisionHandler(Rect obj1, Rect obj2);

        void CollisionHandler(Ball obj1, Ball obj2);
    }

    
}