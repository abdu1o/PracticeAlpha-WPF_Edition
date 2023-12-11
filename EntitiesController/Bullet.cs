using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeAlpha_WPF_Edition.EntitiesController
{
    public class Bullet
    {
        public Image BulletImage { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Speed { get; set; }

        public double Angle { get; set; }

        public Bullet(double x, double y, double width, double height, double speed)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Speed = speed;

            BulletImage = new Image
            {
                Width = width,
                Height = height,
                Source = new BitmapImage(new Uri("/PracticeAlpha-WPF_Edition;component/Resources/Entities/bullet.png", UriKind.Relative))
            };
        }
    }
}
