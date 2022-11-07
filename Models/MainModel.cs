using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleInteractionModel.Models
{
    public class MainModel
    {
        public List<Ball> balls = new List<Ball>();

        public MainModel()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int diameter = random.Next(50, 80);
                int mass = diameter * 10;
                int x = random.Next(diameter, 650 - diameter);
                int y = random.Next(diameter, 450 - diameter);
                Vector velocity = new Vector(random.Next(1, 10), random.Next(1, 10));
                Ball ball = new Ball(new Vector(x, y),
                                     velocity,
                                     (0,0,0), diameter, mass);
                balls.Add(ball);
            }

        }

        public void CalculatePosition(int RightBorder,
                                    int LeftBorder,
                                    int DownBorder,
                                    int UpBorder){

            for (int i = 0; i < balls.Count; i++)
            {
                for (int k = 0; k < balls.Count; k++)
                {
                    if (balls[i] == balls[k]) continue;

                    Ball.BouncingOfBalls(balls[i], balls[k]);
                    balls[i].BouncingOfWalls(RightBorder, LeftBorder,
                                            DownBorder, UpBorder);
                }
            }
        }
    }

}



