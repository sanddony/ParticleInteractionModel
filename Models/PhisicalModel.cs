using System;
using System.Collections.Generic;

namespace ParticleInteractionModel.Models
{
    public delegate void CollisionDelegate(IPhisicObject obj_1, IPhisicObject obj_2);

    
    public abstract class PhysicalModel
    {
        // использование ивента, для того, чтобы иметь возможность подписаться из других классов и как-то дополнительно обработать коллизию
        public abstract event CollisionDelegate? CollisionHappened; 

        //TO-DO: Need func, which would upcast objects
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
            else
            {
                throw new NotSupportedException();
            }
        }
        public abstract void CollisionHandler(Ball obj_1, Rect obj_2);
        public abstract void CollisionHandler(Rect obj_1, Ball obj_2);
        public abstract void CollisionHandler(Ball obj_1, Ball obj_2);
        public abstract void CollisionHandler(Rect obj_1, Rect obj_2);
        public abstract void CheckCollision(List<IPhisicObject> objects);
    }
}