using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace PracticeAlpha_WPF_Edition.EntitiesController
{
    internal class Enemy
    {
        public Image EnemyImage { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }

        public Enemy(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            EnemyImage = new Image
            {
                Width = width,
                Height = height,
                Source = new BitmapImage(new Uri("/PracticeAlpha-WPF_Edition;component/Resources/Entities/enemy.png", UriKind.Relative))
            };
        }
    }
}
