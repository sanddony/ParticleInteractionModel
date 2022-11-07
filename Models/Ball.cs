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
        private Vector position, velocity;
        private double diameter, mass, acceleration;
        public (int r, int g, int b) color;

        public Vector Position { get => position; set => position=value; }
        public Vector Velocity { get => velocity; set => velocity=value; }
        public double Mass { get => mass; set => mass=value; }
        public double Acceleration { get => acceleration; set => acceleration=value; }



        public Ball(Vector position,
                    Vector velocity,
                    (int r, int g, int b) color,
                    double diameter,
                    double mass)
        {
            this.position = position;
            this.velocity = velocity;
            this.color = color;
            this.diameter = diameter;
            this.mass = mass;
        }

        public void Move()
        {
            this.position += this.velocity * acceleration;
        }

        public void Move(double dt)
        {
            this.position += this.velocity * dt;
        }


        public static void BouncingOfBalls(Ball ball_1, Ball ball_2)
        {
            // Вычисление расстояния между центрами шаров
            double d = Vector.FindDistances(ball_1.Position, ball_2.Position);
            //

            if (d <= ball_1.diameter / 2 + ball_2.diameter / 2)
            {
                // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
                Vector.FindSinCos(ball_1.position, ball_2.position, out double sin, out double cos);
                //

                // Вычисление проекций векторов скорости на ось, проходящей через центры шаров
                double Vn1 = ball_2.velocity.X * sin + ball_2.velocity.Y * cos;
                double Vn2 = ball_1.velocity.X * sin + ball_1.velocity.Y * cos;
                //

                // Смещение шаров, если их границы перекрывают друг друга
                double dt = Math.Abs((ball_2.diameter / 2 + ball_1.diameter / 2 - d) / (Vn1 - Vn2 == 0 ? 0.01 : Vn1 - Vn2));
                if (dt > 0.6) dt = 0.6;
                ball_1.position -= ball_1.velocity * dt;
                ball_2.position -= ball_2.velocity * dt;
                //

                // Вычисление синуса и косинуса между отрезком расстояния двух центров шаров и проекциями их скоростей
                Vector.FindSinCos(ball_1.position, ball_2.position, out sin, out cos);
                //

                // Вычисление проекций векторов скорости на координатную плоскость "соудорения" (после смещения)
                Vn1 = ball_2.velocity.X * sin + ball_2.velocity.Y * cos; 
                Vn2 = ball_1.velocity.X * sin + ball_1.velocity.Y * cos; 

                double Vt1 = -ball_2.velocity.X * cos + ball_2.velocity.Y * sin;
                double Vt2 = -ball_1.velocity.X * cos + ball_1.velocity.Y * sin;
                //

                double Vn2_i = Vn2;
                double Vn1_i = Vn1;

                // Вычилсение скорости шаров, в зависимости от их массы, согласно ЗСЭ и ЗСИ
                Vn1 = ((ball_2.mass - ball_1.mass) * Vn1_i + 2 * ball_1.mass * Vn2_i) /
                    (ball_1.mass + ball_2.mass);
                Vn2 = ((ball_1.mass - ball_2.mass) * Vn2_i + 2 * ball_2.mass * Vn1_i) / 
                    (ball_1.mass + ball_2.mass);
                //

                // Изменение скоростей шаров
                ball_1.velocity.X = Vn2 * sin - Vt2 * cos;
                ball_1.velocity.Y = Vn2 * cos + Vt2 * sin;

                ball_2.velocity.X = Vn1 * sin - Vt1 * cos;
                ball_2.velocity.Y = Vn1 * cos + Vt1 * sin;
                //

                // Изменение позиции шара на его скорость
                ball_1.Move(dt);
                ball_2.Move(dt);
                //
            }
        }


        public void BouncingOfWalls(int RightBorder,
                                    int LeftBorder,
                                    int DownBorder,
                                    int UpBorder)
        {
            // Вычисление на сколько край шара сместился за границы сосуда по оси X
            double delta_x = this.position.X > RightBorder / 2 ?
                                (this.position.X + this.diameter / 2) - RightBorder :
                                 LeftBorder - (this.position.X - this.diameter / 2);

            // Вычисление на сколько край шара сместился за границы сосуда по оси Y
            double delta_y = this.position.Y > DownBorder / 2 ?
                            (this.position.Y + this.diameter / 2) - DownBorder :
                            UpBorder - (this.position.Y - this.diameter / 2);

            // Вычисление коэффицента смещения, который показывает за сколько итераций шар пройдёт delta с текущей скоростью
            double k_x = Math.Abs(delta_x / this.velocity.X);
            double k_y = Math.Abs(delta_y / this.velocity.Y);

            // Вычисление скорости по оси X при соудорении с левой или правой стенкой
            if (this.position.X + this.diameter / 2 >= RightBorder ||
                this.position.X - this.diameter / 2 <= LeftBorder)
            {
                this.position.X -= k_x * this.velocity.X;
                this.velocity.X *= -1;
            }

            // Вычисление скорости по оси Y при соудорении с нижней или верхнёй стенкой
            if (this.position.Y + this.diameter / 2 >= DownBorder ||
                this.position.Y - this.diameter / 2 <= UpBorder)
            {
                this.position.Y -= k_y * this.velocity.Y;
                this.velocity.Y *= -1;
            }

            // Изменение позиции шара на его скорость
            this.Move();
        }

        public void SlowlyDown(double k)
        {
            velocity *= k;
            if (this.velocity.Length() < 0.1f) this.velocity = new Vector(0, 0);
        }

    }

}
