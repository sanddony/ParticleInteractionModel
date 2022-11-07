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
                    balls[i].Move();
                }
            }
        }
    }

}



