using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{

    public class Ball : IPhisicObject
    {

        public Vector Position { get; set; }
        public Vector Velocity { get; set; }
        public double Mass { get; set; }
        public double Radius {  get=>Diameter/2; }
        public double Diameter { get=>Width;}
        public double Width { get; set;}
        public double Height { get; set;}
        public double Area { get => Math.PI * Math.Pow(Radius, 2);}

        public Ball(Vector position,
                    Vector velocity,
                    double width,
                    double height,
                    double mass)
        {
            Position = position;
            Velocity = velocity;
            Width = width;
            Height = height;
            Mass = mass;
        }

        public void Move()
        {
            Position += Velocity;
        }

        public void Move(double dt)
        {
            Position += Velocity * dt;
        }


        public static void BouncingOfBalls(Ball ball_1, Ball ball_2)
        {
            // Вычисление расстояния между центрами шаров
            double d = Vector.FindDistances(ball_1.Position, ball_2.Position);
            //

            // Если расстояние между центрами шаров меньше суммы их радиусов
            if (d <= ball_1.Radius + ball_2.Radius)
            {
                // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
                Vector.FindSinCos(ball_1.Position, ball_2.Position, out double sin, out double cos);
                //

                // Вычисление проекций векторов скорости на ось, проходящей через центры шаров
                double Vn1 = ball_2.Velocity.X * sin + ball_2.Velocity.Y * cos;
                double Vn2 = ball_1.Velocity.X * sin + ball_1.Velocity.Y * cos;
                //

                // Смещение шаров, если их границы перекрывают друг друга
                double dt = (ball_2.Radius + ball_1.Radius - d) / (Vn1 - Vn2);
                if (dt > 0.6) dt = 0.6;
                if (dt < -0.6) dt = -0.6;
                ball_1.Position -= ball_1.Velocity * dt;
                ball_2.Position -= ball_2.Velocity * dt;
                //



                // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
                Vector.FindSinCos(ball_1.Position, ball_2.Position, out sin, out cos);
                //

                // Вычисление проекций векторов скорости на координатную плоскость "соудорения" (после смещения)
                Vn1 = ball_2.Velocity.X * sin + ball_2.Velocity.Y * cos;
                Vn2 = ball_1.Velocity.X * sin + ball_1.Velocity.Y * cos;

                double Vt1 = -ball_2.Velocity.X * cos + ball_2.Velocity.Y * sin;
                double Vt2 = -ball_1.Velocity.X * cos + ball_1.Velocity.Y * sin;
                //

                double Vn2_i = Vn2;
                double Vn1_i = Vn1;

                // Вычилсение скорости шаров, в зависимости от их массы, согласно ЗСЭ и ЗСИ
                Vn1 = ((ball_2.Mass - ball_1.Mass) * Vn1_i + 2 * ball_1.Mass * Vn2_i) /
                    (ball_1.Mass + ball_2.Mass);
                Vn2 = ((ball_1.Mass - ball_2.Mass) * Vn2_i + 2 * ball_2.Mass * Vn1_i) /
                    (ball_1.Mass + ball_2.Mass);
                //

                // Изменение скоростей шаров
                ball_1.Velocity.X = Vn2 * sin - Vt2 * cos;
                ball_1.Velocity.Y = Vn2 * cos + Vt2 * sin;

                ball_2.Velocity.X = Vn1 * sin - Vt1 * cos;
                ball_2.Velocity.Y = Vn1 * cos + Vt1 * sin;
                //

                // Изменение позиции шара на его скорость
                ball_2.Move(dt);
                ball_1.Move(dt);
                //
            }
        }


        public void BouncingOfWalls(int LeftBorder,
                                    int RightBorder,
                                    int DownBorder,
                                    int UpBorder)
        {
            if (!((Position.X + this.Radius >= RightBorder ||
                Position.X - this.Radius <= LeftBorder) || (Position.Y + this.Radius >= DownBorder ||
                Position.Y - this.Radius <= UpBorder))) return;

            //TO-DO: Сделать влидацию данных, кидать ошибку 

            // Вычисление на сколько край шара сместился за границы сосуда по оси X (delta_x)
            double delta_x = Position.X > (RightBorder - LeftBorder) / 2 + LeftBorder ?
                                (Position.X + this.Radius) - RightBorder :
                                 LeftBorder - (Position.X - this.Radius);


            // Вычисление на сколько край шара сместился за границы сосуда по оси Y (delta_y)
            double delta_y = Position.Y > (DownBorder - UpBorder) / 2 + UpBorder ?
                            (Position.Y + this.Radius) - DownBorder :
                            UpBorder - (Position.Y - this.Radius);

            // Вычисление коэффицента смещения, который показывает за сколько итераций шар пройдёт delta с текущей скоростью
            double k_y = Math.Abs(delta_y / Velocity.Y);
            double k_x = Math.Abs(delta_x / Velocity.X);

            // Вычисление скорости по оси X при соудорении с левой или правой стенкой и смещение шара назад на delta_x
            if (Position.X + this.Radius >= RightBorder ||
                Position.X - this.Radius <= LeftBorder)
            {
                Position.X -= (k_x + 0.5f) * Velocity.X;
                Velocity.X *= -1;
            }

            // Вычисление скорости по оси Y при соудорении с нижней или верхнёй стенкой И смещение шара назад на delta_y
            if (Position.Y + this.Radius >= DownBorder ||
                Position.Y - this.Radius <= UpBorder)
            {
                Position.Y -= (k_y + 0.5f) * Velocity.Y;
                Velocity.Y *= -1;
            }

            // Изменение позиции шара на его скорость
            this.Move();

        }

        public void CalculateCollision(PhysicalModel rule, IPhisicObject collided_object)
        {
            rule.CollisionHandler(this, collided_object);
        }
    }
}