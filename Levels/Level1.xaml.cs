using PracticeAlpha_WPF_Edition.EntitiesController;
using PracticeAlpha_WPF_Edition.SoundControl;
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
using System.Windows.Threading;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public partial class Level1 : Window
    {
        //Stats
        private double playerSpeed = 2;
        private double bulletSpeed = 20;
        private double shootSpeed = 350;


        private Player player;
        private bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight;

        private List<Enemy> enemies = new List<Enemy>();


        private DispatcherTimer timer;
        private DispatcherTimer shootingTimer;
        private List<Bullet> bullets = new List<Bullet>();

        private SoundController sound;
        private string shootSound = "Sounds\\shoot4.mp3";
        private string deathSound = "Sounds\\enemy_death.mp3";

        private Spawn spawn;
        private int countOfLocation = 8;
        private int timeStart = 0;

        public Level1()
        {
            InitializeComponent();

            MusicController.Initialize("Music\\level1.mp3");
            MusicController.Play();

            shootingTimer = new DispatcherTimer();
            shootingTimer.Interval = TimeSpan.FromMilliseconds(shootSpeed);
            shootingTimer.Tick += ShootingTimer_Tick;

            //--====Player initialization====--
            player = new Player(950, 100, 48, 36);
            mainCanvas.Children.Add(player.PlayerImage);

            Canvas.SetLeft(player.PlayerImage, player.X);
            Canvas.SetTop(player.PlayerImage, player.Y);

            player.PlayerImage.RenderTransformOrigin = new Point(0.5, 0.5);

            mainCanvas.MouseMove += Window_MouseMove;
            mainCanvas.MouseDown += Window_MouseDown;
            mainCanvas.MouseUp += Window_MouseUp;

            //--====Enemy initialization====--
            spawn = new Spawn();

            SetEnemySpawn();


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetSpawnTimer(555);

            foreach (Enemy enemy in enemies)
            {
                UpdateEnemyMovement(enemy);
            }

            UpdatePlayerMovement();
            UpdateBullets();
        }


        //--====Shooting====--
        private void StartShooting()
        {
            shootingTimer.Start();
        }

        private void StopShooting()
        {
            shootingTimer.Stop();
        }

        private void ShootingTimer_Tick(object sender, EventArgs e)
        {
            Shoot();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Shoot();
                StartShooting();
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                StopShooting();
            }
        }

        private void Shoot()
        {
            sound = new SoundController(shootSound);
            sound.PlayAsync();

            Point mousePosition = Mouse.GetPosition(mainCanvas);

            double angle = Math.Atan2(mousePosition.Y - (player.Y + player.Height / 2),
                                       mousePosition.X - (player.X + player.Width / 2));

            Bullet bullet = new Bullet(0, 0, 10, 10, bulletSpeed);
            bullet.Angle = angle;

            RotatePoint(player.X + player.Width / 2, player.Y + player.Height / 2, player.X + player.Width / 2, player.Y + player.Height / 2, player.Rotation, out double rotatedX, out double rotatedY);

            bullet.X = rotatedX;
            bullet.Y = rotatedY;

            bullets.Add(bullet);
            mainCanvas.Children.Add(bullet.BulletImage);

            Canvas.SetLeft(bullet.BulletImage, bullet.X);
            Canvas.SetTop(bullet.BulletImage, bullet.Y);
        }

        private void RotatePoint(double x, double y, double centerX, double centerY, double angle, out double rotatedX, out double rotatedY)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            rotatedX = cos * (x - centerX) - sin * (y - centerY) + centerX;
            rotatedY = sin * (x - centerX) + cos * (y - centerY) + centerY;
        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                bullet.X += bullet.Speed * Math.Cos(bullet.Angle);
                bullet.Y += bullet.Speed * Math.Sin(bullet.Angle);

                Canvas.SetLeft(bullet.BulletImage, bullet.X);
                Canvas.SetTop(bullet.BulletImage, bullet.Y);

                if (bullet.X > mainCanvas.ActualWidth || bullet.Y > mainCanvas.ActualHeight
                    || bullet.X < 0 || bullet.Y < 0)
                {
                    bullets.RemoveAt(i);
                    mainCanvas.Children.Remove(bullet.BulletImage);
                }
            }
        }

        //--====Movement====--
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

            Canvas.SetLeft(player.PlayerImage, player.X);
            Canvas.SetTop(player.PlayerImage, player.Y);

            player.Rotation = Math.Atan2(Mouse.GetPosition(mainCanvas).Y - (player.Y + player.Height / 2),
                                Mouse.GetPosition(mainCanvas).X - (player.X + player.Width / 2));
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(mainCanvas);
            double angle = CalculateAngle(player.X + player.Width / 2, player.Y + player.Height / 2, currentPosition.X, currentPosition.Y);

            RotateTransform rotateTransform = new RotateTransform(angle);
            player.PlayerImage.RenderTransform = rotateTransform;
        }

        private double CalculateAngle(double x1, double y1, double x2, double y2)
        {
            double angleRad = Math.Atan2(y2 - y1, x2 - x1);
            return angleRad * (180 / Math.PI);
        }


        // Enemy Methods

        private void UpdateEnemyMovement(Enemy enemy)
        {
            ChangeEnemyPosition(enemy);
            double angle = CalculateAngle(enemy.X + enemy.Width / 2, enemy.Y + enemy.Height / 2, player.X, player.Y);
            RotateTransform rotateTransform = new RotateTransform(angle);
            enemy.EnemyImage.RenderTransform = rotateTransform;

            Canvas.SetLeft(enemy.EnemyImage, enemy.X);
            Canvas.SetTop(enemy.EnemyImage, enemy.Y);

        }

        private void ChangeEnemyPosition(Enemy enemy)
        {
            if (enemy.X != player.X || enemy.Y != player.Y)
            {
                double deltaX = player.X - enemy.X;
                double deltaY = player.Y - enemy.Y;

                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if (distance > player.Width)
                {
                    double ratio = enemy.Speed / distance;

                    enemy.X += ratio * deltaX;
                    enemy.Y += ratio * deltaY;
                }
            }
        }

        private Enemy CreateEnemy(Spawn spawn, int location)
        {
            double x = 0;
            double y = 0;


            switch (location)
            {
                case 1:
                    x = spawn.LeftTop().Item1;
                    y = spawn.LeftTop().Item2;
                    break;

                case 2:
                    x = spawn.LeftMidl().Item1;
                    y = spawn.LeftMidl().Item2;
                    break;

                case 3:
                    x = spawn.LeftBottom().Item1;
                    y = spawn.LeftBottom().Item2;
                    break;

                case 4:
                    x = spawn.TopMidl().Item1;
                    y = spawn.TopMidl().Item2;
                    break;

                case 5:
                    x = spawn.BottomMidl().Item1;
                    y = spawn.BottomMidl().Item2;
                    break;

                case 6:
                    x = spawn.RightTop().Item1;
                    y = spawn.RightTop().Item2;
                    break;

                case 7:
                    x = spawn.RightMidl().Item1;
                    y = spawn.RightMidl().Item2;
                    break;

                case 8:
                    x = spawn.RightBottom().Item1;
                    y = spawn.RightBottom().Item2;
                    break;
            }

            Enemy enemy = new Enemy(x, y, 48, 36);
            enemy.Speed = 2;
            mainCanvas.Children.Add(enemy.EnemyImage);

            Canvas.SetLeft(enemy.EnemyImage, enemy.X);
            Canvas.SetTop(enemy.EnemyImage, enemy.Y);

            enemy.EnemyImage.RenderTransformOrigin = new Point(0.5, 0.5);

            return enemy;
        }

        private void SetEnemySpawn()
        {
            for (int location = 1; location <= countOfLocation; location++)
            {
                enemies.Add(CreateEnemy(spawn, location));
            }
        }

        private void SetSpawnTimer(int timer)
        {
            timeStart++;
            if (timeStart == timer)
            {
                SetEnemySpawn();
                timeStart = 0;
            }
        }
    }
}