using System;
using System.Collections.Generic;
using ParticleInteractionModel.Models;
using Avalonia.Media;
using Avalonia.Controls.Shapes;

namespace ParticleInteractionModel.ViewModels
{   // TO-DO: сделать делегат для CreateObject и в реализации конкретных фабрик делать 
    // методы для создания конкретных объектов
    public delegate double Overflow();
    public abstract class ConainerFactory
    {
        public event Overflow? ContainerOverflow;// подписаться в графической обработке для отображения переполнения контейнера
        
        public Container Container {get; protected set;}
        

        public abstract GraphicPhisicObject CreateObject(IBrush color,
                                                Vector velocity,
                                                Vector position,
                                                double height,
                                                double width,
                                                double mass);
        public abstract void CreateWalls(double xLen, double yLen, double thickness = 1);
        
        protected abstract Shape SetVisualObject(IBrush color,
                                              Vector position,
                                                double height,
                                                double width);
        protected abstract IPhisicObject CreateIPhisicObject(Vector velocity, 
                                                Vector position,
                                                double height,
                                                double width,
                                                double mass);

        protected Vector GenerateRandomPosition(double height, double width){
            Random random = new Random();
            return new Vector(random.Next((int)(2 * width + Container.Walls[0].Position.X + 10), 
                                                        (int)(Container.Walls[2].Position.X + 10 - width)),
                                            random.Next((int)(2 * height + Container.Walls[1].Position.Y + 10), 
                                                        (int)(Container.Walls[3].Position.Y + 10 - width)));
        }
        protected Vector GenerateRandomVelocity(int speedFrom, int speedTo){
            Random random = new Random();
            return new Vector(random.Next(speedFrom, speedTo), 
                                        random.Next(speedFrom, speedTo));
        }

        public void AddPhisicsObjects(int count,
                                      IBrush color,
                                      double mass,
                                      double height,
                                      double width,
                                      int speedFrom=3,
                                      int speedTo=10)
        {
            for (int i = 0; i < count; i++)
            {
                Vector velocity = GenerateRandomVelocity(speedFrom, speedTo);
                Vector position = GenerateRandomPosition(height, width);

                Container.Objects.Add(CreateObject(color, velocity, position, height, width, mass));
            }
        }

        public void AddPhisicsObjects(int count,
                                      IBrush color,
                                      Vector velocity,
                                      double mass,
                                      double height,
                                      double width)
        {
            for (int i = 0; i < count; i++)
            {
                Vector position = GenerateRandomPosition(height, width);

                Container.Objects.Add(CreateObject(color, velocity, position, height, width, mass));
            }
        }


    }

    


}