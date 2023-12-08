using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public partial class Level1 : Window
    {
        private Player player;
        private bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight;
        private double playerSpeed = 20;

        public Level1()
        {
            InitializeComponent();

            player = new Player(950, 100, 30, 30);
            mainCanvas.Children.Add(player.Rectangle);

            Canvas.SetLeft(player.Rectangle, player.X);
            Canvas.SetTop(player.Rectangle, player.Y);

            player.Rectangle.RenderTransformOrigin = new Point(0.5, 0.5);

            mainCanvas.MouseMove += Window_MouseMove;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e.Key, true);
            UpdatePlayerMovement();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e.Key, false);
            UpdatePlayerMovement();
        }

        private void HandleKeyPress(Key key, bool isPressed)
        {
            switch (key)
            {
                case Key.W:
                    isMovingUp = isPressed;
                    break;

                case Key.S:
                    isMovingDown = isPressed;
                    break;

                case Key.A:
                    isMovingLeft = isPressed;
                    break;

                case Key.D:
                    isMovingRight = isPressed;
                    break;
            }
        }

        private void UpdatePlayerMovement()
        {
            if (isMovingUp)
            {
                player.Y -= playerSpeed;
            }
            if (isMovingDown)
            {
                player.Y += playerSpeed;
            }
            if (isMovingLeft)
            {
                player.X -= playerSpeed;
            }
            if (isMovingRight)
            {
                player.X += playerSpeed;
            }

            Canvas.SetLeft(player.Rectangle, player.X);
            Canvas.SetTop(player.Rectangle, player.Y);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(mainCanvas);
            double angle = CalculateAngle(player.X + player.Width / 2, player.Y + player.Height / 2, currentPosition.X, currentPosition.Y);

            RotateTransform rotateTransform = new RotateTransform(angle);
            player.Rectangle.RenderTransform = rotateTransform;
        }

        private double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double angleRad = Math.Atan2(y2 - y1, x2 - x1);
            return angleRad * (180 / Math.PI);
        }
    }
}
