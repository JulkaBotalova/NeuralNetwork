using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NeuronNetwork2Layers.View
{
    public class PaintingGridManager : ShowingGridManager
    {
        private bool painting;
        public PaintingGridManager(Grid grid) : base(grid)
        {
            foreach (List<Rectangle> rects in pixels)
                foreach (Rectangle pixel in rects)
                {
                    pixel.MouseLeftButtonDown += RectMouseDown;
                    pixel.MouseLeftButtonUp += RectMouseUp;
                    pixel.MouseMove += RectMouseMove;
                }

            ManagedGrid.MouseLeave += GridMouseLeave;
        }

        private void RectMouseDown(object sender, RoutedEventArgs e)
        {
            painting = true;
            (sender as Rectangle).Fill = new SolidColorBrush(Colors.Green);
        }

        private void RectMouseMove(object sender, RoutedEventArgs e)
        {
            if (painting)
                (sender as Rectangle).Fill = new SolidColorBrush(paintedPixelColor);
        }

        private void GridMouseLeave(object sender, RoutedEventArgs e)
        {
            painting = false;
        }

        private void RectMouseUp(object sender, RoutedEventArgs e)
        {
            painting = false;
        }

        public void Clear()
        {
            foreach (List<Rectangle> rects in pixels)
                foreach (Rectangle pixel in rects)
                    pixel.Fill = new SolidColorBrush(defaultPixelColor);
        }
    }
}
