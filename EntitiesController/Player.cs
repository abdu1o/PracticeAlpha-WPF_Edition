using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public class Player
    {
        public Image PlayerImage { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }

        public string modelPath;

        public Player(double x, double y, double width, double height, string path)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            modelPath = path;

            PlayerImage = new Image
            {
                Width = width,
                Height = height,
                Source = new BitmapImage(new Uri(modelPath, UriKind.Relative))
            };
        }

        public void MoveUp()
        {
            Y -= 10;
            Canvas.SetTop(PlayerImage, Y);
        }

        public void MoveDown()
        {
            Y += 10;
            Canvas.SetTop(PlayerImage, Y);
        }

        public void MoveLeft()
        {
            X -= 10;
            Canvas.SetLeft(PlayerImage, X);
        }

        public void MoveRight()
        {
            X += 10;
            Canvas.SetLeft(PlayerImage, X);
        }
    }

    public class PlayerTest
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Rectangle PlayerRectangle { get; set; }

        public PlayerTest(int x, int y)
        {
            X = x;
            Y = y;

            PlayerRectangle = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = Brushes.Red
            };
        }

        public void MoveUp()
        {
            Y -= 10;
            Canvas.SetTop(PlayerRectangle, Y);
        }

        public void MoveDown()
        {
            Y += 10;
            Canvas.SetTop(PlayerRectangle, Y);
        }

        public void MoveLeft()
        {
            X -= 10;
            Canvas.SetLeft(PlayerRectangle, X);
        }

        public void MoveRight()
        {
            X += 10;
            Canvas.SetLeft(PlayerRectangle, X);
        }
    }
}
