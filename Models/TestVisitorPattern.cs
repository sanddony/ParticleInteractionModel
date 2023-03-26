using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public delegate void CollisionDelegate(IPhisicObject obj_1, IPhisicObject obj_2);
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
        public event CollisionDelegate CollisionHappened;
        public void CollisionHandler(IPhisicObject obj_1, IPhisicObject obj_2)
        {
            if (obj_1 is Ball && obj_2 is Rect)
            {
                CollisionHandler(obj_1 as Ball, obj_2 as Rect);
            } 
            else if (obj_1 is Ball && obj_2 is Ball)
            {
                CollisionHandler(obj_1 as Ball, obj_2 as Ball);
            } 
            else if (obj_1 is Rect && obj_2 is Rect)
            {
                CollisionHandler(obj_1 as Rect, obj_2 as Rect);
            } 
            else if (obj_1 is Rect && obj_2 is Ball)
            {
                CollisionHandler(obj_1 as Rect, obj_2 as Ball);
            } 
        }
        public void CollisionHandler(Ball obj_1, Rect obj_2);
        public void CollisionHandler(Rect obj_1, Ball obj_2);
        public void CollisionHandler(Ball obj_1, Ball obj_2);
        public void CollisionHandler(Rect obj_1, Rect obj_2);
        public void CheckCollision(List<IPhisicObject> objects);
    }

    public class IdealGasModel : IPhisicsRule
    {
        public event CollisionDelegate? CollisionHappened;

        public void CollisionHandler(Ball obj_1, Rect obj_2)
        {
            throw new NotImplementedException();
        }

        public void CollisionHandler(Rect obj_1, Ball obj_2)
        {
            throw new NotImplementedException();
        }

        public void CollisionHandler(Ball obj_1, Ball obj_2)
        {
            throw new NotImplementedException();
        }

        public void CollisionHandler(Rect obj_1, Rect obj_2)
        {
            throw new NotImplementedException();
        }

        public void CheckCollision(List<IPhisicObject> objects)
        {
            throw new NotImplementedException();
        }
    }


}