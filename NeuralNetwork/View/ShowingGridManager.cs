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
    public class ShowingGridManager
    {
        public Grid ManagedGrid { get; }
        protected List<List<Rectangle>> pixels = new List<List<Rectangle>>();

        protected Color defaultPixelColor = Colors.Gray;
        protected Color paintedPixelColor = Colors.Green;

        public ShowingGridManager(Grid grid)
        {
            ManagedGrid = grid;

            List<Rectangle> rects = new List<Rectangle>();
            for (int i = 0; i < 225; i++)
            {
                int x = i % 15;
                int y = (int)Math.Floor((decimal)i / 15);

                if (x % 15 == 0 && y != 0)
                {
                    pixels.Add(rects);
                    rects = new List<Rectangle>();
                }

                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(defaultPixelColor);
                rect.Margin = new Thickness(x * 10, y * 10, 0, 0);

                rects.Add(rect);
                ManagedGrid.Children.Add(rect);
            }
            pixels.Add(rects);
        }

        public void Show(List<bool> data)
        {
            for (int i = 0; i < 225; i++)
            {
                int x = i % 15;
                int y = (int)Math.Floor((decimal)i / 15);

                Rectangle pixel = pixels[x][y];
                pixel.Fill = new SolidColorBrush(defaultPixelColor);
                if (data[i])
                    pixel.Fill = new SolidColorBrush(paintedPixelColor);
            }
        }

        public List<bool> GetData()
        {
            List<bool> result = new List<bool>();

            for (int i = 0; i < 225; i++)
            {
                int x = i % 15;
                int y = (int)Math.Floor((decimal)i / 15);

                Rectangle pixel = pixels[x][y];

                bool sign = true;
                if (((SolidColorBrush)pixel.Fill).Color == Colors.Gray)
                    sign = false;
                result.Add(sign);
            }
            return result;
        }
    }
}

