using System;
using System.Collections.Generic;
using Avalonia.Controls;


namespace ParticleInteractionModel.Models
{
    public interface IContainerUI
    {
        public Canvas MainField { get; }

        public void Render();
    }
}