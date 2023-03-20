using Avalonia.Media;
using Avalonia.Controls.Shapes;
using System;
using Avalonia;
using System.Globalization;
using Avalonia.Animation.Animators;
using Avalonia.Utilities;
using ParticleInteractionModel.Models;
using Vector = ParticleInteractionModel.Models.Vector;

namespace ParticleInteractionModel
{
    public class Particle: Shape, IPhisicObject
    {
        private Ball ball;
        public Vector Position { get => ball.Position;}
        public Vector Velocity { get => ball.Velocity; }
        public double Mass { get => ball.Mass; }
        public Vector Acceleration { get => ball.Acceleration; }
        
        static Particle()
        {
            AffectsGeometry<Ellipse>(BoundsProperty, StrokeThicknessProperty);
        }

        protected override Geometry CreateDefiningGeometry()
        {
            var rect = new Rect(Bounds.Size).Deflate(StrokeThickness / 2);
            return new EllipseGeometry(rect);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(StrokeThickness, StrokeThickness);
        }
        
        public void Move()
        {
            throw new NotImplementedException();
        }

    }
}