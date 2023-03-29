using System;
using System.Collections.Generic;
using ParticleInteractionModel.Models;
using Avalonia.Media;
using Avalonia.Controls.Shapes;
using Avalonia.Controls;

namespace ParticleInteractionModel.ViewModels
{
    public class IdealGasContainerFactory : ConainerFactory
    {
        List<IPhisicObject> Objects { get; set; }
        PhysicalModel Engine { get; set; }
        int TimerInterval { get; set; }
        double Size { get; set; }
        public Canvas MainField { get; set; }


        public IdealGasContainerFactory()
        {

        }

        public IdealGasContainerFactory(int timerInterval,double width, double height)
        {
            SetMainField(width, height);
            SetTimer(timerInterval);
            SetPhisicEngine();
            CreateWalls(height, width);
        }

        public IdealGasContainerFactory(int timerInterval,
                                      double width, 
                                      double height,
                                      int countPhisicsObj,
                                      IBrush color,
                                      double mass,
                                      int speedFrom,
                                      int speedTo)
        {
            SetMainField(width, height);
            SetTimer(timerInterval);
            SetPhisicEngine();
            CreateWalls(height, width);
            
            AddPhisicsObjects(countPhisicsObj, color, mass, height, width, speedFrom, speedTo);
        }

        public void SetTimer(int timerInterval)
        {
            Container.Timer = new System.Timers.Timer(timerInterval);
            Container.Timer.Elapsed += Container.ModelCalculations;
            Container.Timer.Elapsed += Container.Render;
        }

        public void SetMainField(double width, double height)
        {
            MainField = new Canvas();
            MainField.Focus();
            MainField.Width = width;
            MainField.Height = height;
        }

        public void SetPhisicEngine()
        {
            Engine = new IdealGasModel();
        }

        public override void CreateWalls(double xLen, double yLen, double thickness = 1)
        {
            Rect left = new Rect();
            Rect right = new Rect();
            Rect up = new Rect();
            Rect down = new Rect();

            left.Width = right.Width = up.Height = down.Height = 1;
            up.Height = down.Height = 1;
            left.Height = right.Width = yLen;
            up.Height = down.Width = xLen;

            left.Mass = right.Mass = up.Mass = down.Mass = double.PositiveInfinity;
            left.Velocity = right.Velocity = up.Velocity = down.Velocity = new Vector(0, 0);

            left.Position = new Vector(0, 1); // чтобы не накладывалась на вернхнюю стенку
            right.Position = new Vector(xLen - 1, 0);
            up.Position = new Vector(0, 0);
            down.Position = new Vector(0, yLen - 1);

            Container.Walls.Add(left);
            Container.Walls.Add(up);
            Container.Walls.Add(right);
            Container.Walls.Add(down);
        }

        protected override IPhisicObject CreateIPhisicObject(Vector velocity,
                                                            Vector position,
                                                            double height,
                                                            double width,
                                                            double mass)
        {
            return new Ball(position, velocity, width, height, mass);
        }

        protected override Shape SetVisualObject(IBrush color,
                                                Vector position,
                                                double height,
                                                double width)
        {
            Ellipse circle = new Ellipse();
            circle.Fill = color;
            circle.Height = height;
            circle.Width = width;
            Canvas.SetLeft(circle, position.X);
            Canvas.SetTop(circle, position.Y);
            MainField.Children.Add(circle);
            return circle;
        }

        public override GraphicPhisicObject CreateObject(IBrush color,
                                                        Vector velocity,
                                                        Vector position,
                                                        double height,
                                                        double width,
                                                        double mass)
        {
            GraphicPhisicObject obj = new GraphicPhisicObject();
            obj.ObjectModel = CreateIPhisicObject(velocity, position, height, width, mass);
            obj.VisualObject = SetVisualObject(color, position, height, width);
            return obj;
        }
    
    }
}