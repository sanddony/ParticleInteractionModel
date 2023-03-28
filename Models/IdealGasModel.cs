using System;
using System.Collections.Generic;
using System.Numerics;

namespace ParticleInteractionModel.Models
{
    public class IdealGasModel : PhysicalModel
    {
        public override event CollisionDelegate? CollisionHappened;

        public IdealGasModel()
        {
            CollisionHappened += CollisionHandler;
        }

        public override void CollisionHandler(Ball obj_1, Rect obj_2)
        {
            // Вычисление на сколько край шара сместился за границы сосуда по оси X (delta_x)
            double d = Vector.FindDistances(obj_1.Position, obj_2.Position); // 

            // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
            Vector.FindSinCos(obj_1.Position, obj_2.Position, out double sin, out double cos);
            //

            // Вычисление проекций векторов скорости на ось, проходящей через центры шаров
            double Vn1 = obj_2.Velocity.X * sin + obj_2.Velocity.Y * cos;
            double Vn2 = obj_1.Velocity.X * sin + obj_1.Velocity.Y * cos;
            //

            // Обработать смещение шаров за границы прямоугольников
            // double dt = (obj_2.Diameter / 2 + obj_1.Diameter / 2 - d) / (Vn1 - Vn2); // 
            // if (dt > 0.6) dt = 0.6;
            // if (dt < -0.6) dt = -0.6;
            // obj_1.Position -= obj_1.Velocity * dt;
            // obj_2.Position -= obj_2.Velocity * dt;
            //

            // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
            Vector.FindSinCos(obj_1.Position, obj_2.Position, out sin, out cos);
            //

            // Вычисление проекций векторов скорости на координатную плоскость "соудорения" (после смещения)
            Vn1 = obj_2.Velocity.X * sin + obj_2.Velocity.Y * cos;
            Vn2 = obj_1.Velocity.X * sin + obj_1.Velocity.Y * cos;

            double Vt1 = -obj_2.Velocity.X * cos + obj_2.Velocity.Y * sin;
            double Vt2 = -obj_1.Velocity.X * cos + obj_1.Velocity.Y * sin;
            //

            double Vn2_i = Vn2;
            double Vn1_i = Vn1;

            // Вычилсение скорости шаров, в зависимости от их массы, согласно ЗСЭ и ЗСИ
            Vn1 = ((obj_2.Mass - obj_1.Mass) * Vn1_i + 2 * obj_1.Mass * Vn2_i) /
                (obj_1.Mass + obj_2.Mass);
            Vn2 = ((obj_1.Mass - obj_2.Mass) * Vn2_i + 2 * obj_2.Mass * Vn1_i) /
                (obj_1.Mass + obj_2.Mass);
            //

            // Изменение скоростей шаров
            obj_1.Velocity.X = Vn2 * sin - Vt2 * cos;
            obj_1.Velocity.Y = Vn2 * cos + Vt2 * sin;

            obj_2.Velocity.X = Vn1 * sin - Vt1 * cos;
            obj_2.Velocity.Y = Vn1 * cos + Vt1 * sin;
        }
        public override void CollisionHandler(Rect obj_1, Ball obj_2) => CollisionHandler(obj_2, obj_1);

        public override void CollisionHandler(Ball obj_1, Ball obj_2)
        {
            double d = Vector.FindDistances(obj_1.Position, obj_2.Position); // 

            // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
            Vector.FindSinCos(obj_1.Position, obj_2.Position, out double sin, out double cos);
            //

            // Вычисление проекций векторов скорости на ось, проходящей через центры шаров
            double Vn1 = obj_2.Velocity.X * sin + obj_2.Velocity.Y * cos;
            double Vn2 = obj_1.Velocity.X * sin + obj_1.Velocity.Y * cos;
            //

            // Смещение шаров, если их границы перекрывают друг друга
            double dt = (obj_2.Diameter / 2 + obj_1.Diameter / 2 - d) / (Vn1 - Vn2); // 
            if (dt > 0.6) dt = 0.6;
            if (dt < -0.6) dt = -0.6;
            //

            obj_1.Position -= obj_1.Velocity * dt;
            obj_2.Position -= obj_2.Velocity * dt;

            // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
            Vector.FindSinCos(obj_1.Position, obj_2.Position, out sin, out cos);
            //

            // Вычисление проекций векторов скорости на координатную плоскость "соудорения" (после смещения)
            Vn1 = obj_2.Velocity.X * sin + obj_2.Velocity.Y * cos;
            Vn2 = obj_1.Velocity.X * sin + obj_1.Velocity.Y * cos;

            double Vt1 = -obj_2.Velocity.X * cos + obj_2.Velocity.Y * sin;
            double Vt2 = -obj_1.Velocity.X * cos + obj_1.Velocity.Y * sin;
            //

            double Vn2_i = Vn2;
            double Vn1_i = Vn1;

            // Вычилсение скорости шаров, в зависимости от их массы, согласно ЗСЭ и ЗСИ
            Vn1 = ((obj_2.Mass - obj_1.Mass) * Vn1_i + 2 * obj_1.Mass * Vn2_i) /
                (obj_1.Mass + obj_2.Mass);
            Vn2 = ((obj_1.Mass - obj_2.Mass) * Vn2_i + 2 * obj_2.Mass * Vn1_i) /
                (obj_1.Mass + obj_2.Mass);
            //

            // Изменение скоростей шаров
            obj_1.Velocity.X = Vn2 * sin - Vt2 * cos;
            obj_1.Velocity.Y = Vn2 * cos + Vt2 * sin;

            obj_2.Velocity.X = Vn1 * sin - Vt1 * cos;
            obj_2.Velocity.Y = Vn1 * cos + Vt1 * sin;
        }

        public override void CollisionHandler(Rect obj_1, Rect obj_2)
        {
            throw new NotImplementedException();
        }

        public override void CheckCollision(List<IPhisicObject> objects)
        {
            bool collision = false; //Проверка на коллизии
            if (collision) CollisionHappened?.Invoke(objects[0], objects[1]);
        }
    }
}
