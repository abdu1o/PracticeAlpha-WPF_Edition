using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public class Player
    {
        public Rectangle Rectangle { get; private set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Player(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            Rectangle = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = Brushes.Blue 
            };
        }
    }
}
