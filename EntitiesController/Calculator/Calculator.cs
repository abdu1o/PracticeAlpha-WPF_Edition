using PracticeAlpha_WPF_Edition.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PracticeAlpha_WPF_Edition.EntitiesController.Calculator
{
    public class Calculator
    {
        public double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double angleRad = Math.Atan2(y2 - y1, x2 - x1);
            return angleRad * (180 / Math.PI);
        }

        public bool IsCollision(Bullet bullet, Enemy enemy)
        {
            Rect rect1 = new Rect(bullet.X, bullet.Y, bullet.Width, bullet.Height);
            Rect rect2 = new Rect(enemy.X, enemy.Y, enemy.Width, enemy.Height);

            return rect1.IntersectsWith(rect2);
        }

        public bool IsPlayerHit(Player player, List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                Rect playerRect = new Rect(player.X, player.Y, player.Width, player.Height);
                Rect enemyRect = new Rect(enemy.X, enemy.Y, enemy.Width, enemy.Height);
                if (playerRect.IntersectsWith(enemyRect))
                {
                    return true;
                }
            }

            return false;
        }

        public void RotatePoint(double x, double y, double centerX, double centerY, double angle, out double rotatedX, out double rotatedY)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            rotatedX = cos * (x - centerX) - sin * (y - centerY) + centerX;
            rotatedY = sin * (x - centerX) + cos * (y - centerY) + centerY;
        }

    }
}
