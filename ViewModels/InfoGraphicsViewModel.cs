using Avalonia.Controls.Shapes;
using ParticleInteractionModel.Models;
using ParticleInteractionModel.Views;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot.Drawing.Colormaps;
using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;

namespace ParticleInteractionModel.ViewModels
{
    internal class InfoGraphicsViewModel : Window
    {
        
        public InfoGraphicsViewModel(Dictionary<Ball, Ellipse> particles)
        {

        }


        private void BuildGraph(Dictionary<Ball, Ellipse> particles)
        {
            AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
            if (avaPlot1 == null) return;
            avaPlot1.Plot.Clear();
            avaPlot1.Refresh();
            double[] dataX = new double[particles.Count];
            double[] dataY = new double[particles.Count];
            for (int i = 0; i < particles.Count; i++)
            {
                dataX[i] = Math.Round(particles.ElementAt(i).Key.Velocity.Length(), 2);
            }
            double temp;
            for (int i = 0; i < dataX.Length; i++)
            {
                for (int j = i + 1; j < dataX.Length; j++)
                {
                    if (dataX[i] > dataX[j])
                    {
                        temp = dataX[i];
                        dataX[i] = dataX[j];
                        dataX[j] = temp;
                    }
                }
            }
            for (int i = 0; i < dataX.Length; i++)
            {
                int count = 0;
                for (int k = 0; k < dataX.Length; k++)
                {
                    if (dataX[i] == dataX[k]) count++;
                }
                dataY[i] = count;
            }

            avaPlot1.Plot.AddScatter(dataX, dataY).OnNaN = ScottPlot.Plottable.ScatterPlot.NanBehavior.Gap;

            avaPlot1.Refresh();
        }
    }
}
