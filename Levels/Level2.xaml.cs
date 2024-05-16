using PracticeAlpha_WPF_Edition.EntitiesController.Calculator;
using PracticeAlpha_WPF_Edition.EntitiesController;
using PracticeAlpha_WPF_Edition.PlayerDataSave;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Media3D;

namespace PracticeAlpha_WPF_Edition.Levels
{

    public partial class Level2 : Window
    {
        //Stats
        private double playerSpeed = 3;
        private double bulletSpeed = 20;
        private double shootSpeed = 350;
        private bool isMovingUp, isMovingDown, isMovingLeft, isMovingRight;

        private double enemySpeed = 0;
        private int spawnTime = 4;

        private DispatcherTimer timer, shootingTimer, levelTimer, spawnTimer;

        private TimeSpan elapsedTime;

        private List<Bullet> bullets = new List<Bullet>();
        private List<Enemy> enemies = new List<Enemy>();

        private Spawn spawn;
        private Player player;
        private Calculator calculator = new Calculator();
        private Rectangle overlay;

        private int countOfLocation = 8;

        private int scoreValue = 0, pointsPerKill = 50;

        public Level2()
        {
            InitializeComponent();
            GameInitialized();

            Music.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\level1.mp3", 0.1);
        }

        public Level2(string customMusic)
        {
            InitializeComponent();
            GameInitialized();

            Music.Play(customMusic, 0.1);
        }

        public void GameInitialized()
        {
            Uri cursorUri = new Uri("pack://application:,,,/PracticeAlpha-WPF_Edition;component/Resources/Icons/cursor.cur");
            StreamResourceInfo streamInfo = Application.GetResourceStream(cursorUri);

            //Custom cursor
            Cursor customCursor = new Cursor(streamInfo.Stream);
            this.Cursor = customCursor;

            levelTimer = new DispatcherTimer();
            levelTimer.Interval = TimeSpan.FromSeconds(1);
            levelTimer.Tick += LevelTimer_Tick;
            levelTimer.Start();

            shootingTimer = new DispatcherTimer();
            shootingTimer.Interval = TimeSpan.FromMilliseconds(shootSpeed);
            shootingTimer.Tick += ShootingTimer_Tick;

            spawnTimer = new DispatcherTimer();
            spawnTimer.Interval = TimeSpan.FromSeconds(spawnTime);
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();

            //--====Player initialization====--
            player = new Player(925, 500, 48, 36, "/PracticeAlpha-WPF_Edition;component/Resources/Entities/player2.png");
            mainCanvas.Children.Add(player.PlayerImage);
            Canvas.SetZIndex(player.PlayerImage, 100);


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

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            SetEnemySpawn();

            enemySpeed += 0.4;

            if (enemySpeed > 12)
            {
                enemySpeed = 12;
            }
        }

        private void LevelTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1));
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            timerText.Text = $"{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (calculator.IsPlayerHit(player, enemies))
            {
                GameOver();
                return;
            }

            foreach (Enemy enemy in enemies)
            {
                UpdateEnemyMovement(enemy);
            }

            UpdatePlayerMovement();
            UpdateBullets();
        }

        private void StopTimers()
        {
            timer.Stop();
            shootingTimer.Stop();
            levelTimer.Stop();
            spawnTimer.Stop();
        }

        public void ContinueTimers()
        {
            timer.Start();
            shootingTimer.Start();
            levelTimer.Start();
            spawnTimer.Start();
        }

        private void GameOver()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 4);

            switch (randomNumber)
            {
                case 1:
                    player.PlayerImage.Width = 70;
                    player.PlayerImage.Height = 60;
                    player.PlayerImage.Source = new BitmapImage(new Uri("/PracticeAlpha-WPF_Edition;component/Resources/Entities/player_dead1.png", UriKind.Relative));
                    break;

                case 2:
                    player.PlayerImage.Width = 80;
                    player.PlayerImage.Height = 50;
                    player.PlayerImage.Source = new BitmapImage(new Uri("/PracticeAlpha-WPF_Edition;component/Resources/Entities/player_dead2.png", UriKind.Relative));
                    break;
            }
            Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\deathSoundEnemy.mp3", 0.05);

            MusicController.SetVolume(0.07);
            StopTimers();
            AddOverlay();

            PlayerInfo.PushInfo(PlayerInfo.Name, scoreValue.ToString(), timerText.Text);

            YouDead youDeadWindow = new YouDead(false, typeof(Level2));
            youDeadWindow.Owner = this;
            youDeadWindow.ShowDialog();

        }

        //--====Shooting====--
        private void StartShooting()
        {
            shootingTimer.Start();
        }

        private void AddOverlay()
        {
            overlay = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)),
                Width = this.ActualWidth,
                Height = this.ActualHeight
            };

            mainCanvas.Children.Add(overlay);
            Canvas.SetZIndex(overlay, 105);
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
            Application.Current.Dispatcher.Invoke(() =>
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\shoot.mp3", 0.05);
            });

            Point mousePosition = Mouse.GetPosition(mainCanvas);

            double angle = Math.Atan2(mousePosition.Y - (player.Y + player.Height / 2),
                                       mousePosition.X - (player.X + player.Width / 2));

            Bullet bullet = new Bullet(0, 0, 10, 10, bulletSpeed);
            bullet.Angle = angle;

            calculator.RotatePoint(player.X + player.Width / 2, player.Y + player.Height / 2, player.X + player.Width / 2, player.Y + player.Height / 2, player.Rotation, out double rotatedX, out double rotatedY);

            bullet.X = rotatedX;
            bullet.Y = rotatedY;

            bullets.Add(bullet);
            mainCanvas.Children.Add(bullet.BulletImage);

            Canvas.SetLeft(bullet.BulletImage, bullet.X);
            Canvas.SetTop(bullet.BulletImage, bullet.Y);
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

                foreach (Enemy enemy in enemies)
                {
                    if (calculator.IsCollision(bullet, enemy))
                    {
                        HandleBulletCollision(bullet, enemy);
                        break;
                    }
                }

                if (bullet.X > mainCanvas.ActualWidth || bullet.Y > mainCanvas.ActualHeight
                    || bullet.X < 0 || bullet.Y < 0)
                {
                    bullets.RemoveAt(i);
                    mainCanvas.Children.Remove(bullet.BulletImage);
                }
            }
        }

        private void SetEnemyModel(Enemy enemy, int width, int height, string path)
        {
            enemy.EnemyImage.Width = width;
            enemy.EnemyImage.Height = height;
            enemy.EnemyImage.Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void HandleBulletCollision(Bullet bullet, Enemy enemy)
        {
            bullets.Remove(bullet);
            mainCanvas.Children.Remove(bullet.BulletImage);

            enemies.Remove(enemy);

            Random random = new Random();
            int randomNumber = random.Next(1, 4);

            switch (randomNumber)
            {
                case 1:
                    SetEnemyModel(enemy, 70, 60, "/PracticeAlpha-WPF_Edition;component/Resources/Entities/dead_enemy1.png");
                    break;

                case 2:
                    SetEnemyModel(enemy, 80, 50, "/PracticeAlpha-WPF_Edition;component/Resources/Entities/dead_enemy2.png");
                    break;

                case 3:
                    SetEnemyModel(enemy, 95, 35, "/PracticeAlpha-WPF_Edition;component/Resources/Entities/dead_enemy3.png");
                    break;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                Sound.Play("C:\\Users\\akapa\\source\\repos\\PracticeAlpha-WPF_Edition\\Resources\\Sounds\\deathSoundEnemy.mp3", 0.05);
            });

            scoreValue += pointsPerKill;
            scoreText.Text = scoreValue.ToString("D5");
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

                case Key.Escape:
                    StopTimers();
                    MusicController.SetVolume(0.07);

                    AddOverlay();
                    YouDead pauseWindow = new YouDead(true, typeof(Level2));
                    pauseWindow.Owner = this;
                    pauseWindow.ShowDialog();

                    mainCanvas.Children.Remove(overlay);
                    ContinueTimers();
                    break;
            }
        }

        private void UpdatePlayerMovement()
        {
            //hz sho s etim delat`
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
            if (player.X < 0)
            {
                player.X = 0;
            }
            if (player.Y < 0)
            {
                player.Y = 0;
            }
            if (player.X + player.Width > mainCanvas.ActualWidth)
            {
                player.X = mainCanvas.ActualWidth - player.Width;
            }
            if (player.Y + player.Height > mainCanvas.ActualHeight)
            {
                player.Y = mainCanvas.ActualHeight - player.Height;
            }
            //?????????????????????????????????????????????????????

            Canvas.SetLeft(player.PlayerImage, player.X);
            Canvas.SetTop(player.PlayerImage, player.Y);

            player.Rotation = Math.Atan2(Mouse.GetPosition(mainCanvas).Y - (player.Y + player.Height / 2),
                                Mouse.GetPosition(mainCanvas).X - (player.X + player.Width / 2));

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(mainCanvas);
            double angle = calculator.CalculateAngle(player.X + player.Width / 2, player.Y + player.Height / 2, currentPosition.X, currentPosition.Y);

            RotateTransform rotateTransform = new RotateTransform(angle);
            player.PlayerImage.RenderTransform = rotateTransform;
        }

        // Enemy Methods
        private void UpdateEnemyMovement(Enemy enemy)
        {
            ChangeEnemyPosition(enemy);
            double angle = calculator.CalculateAngle(enemy.X + enemy.Width / 2, enemy.Y + enemy.Height / 2, player.X, player.Y);
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
            mainCanvas.Children.Add(enemy.EnemyImage);
            Canvas.SetZIndex(enemy.EnemyImage, 99);
            enemy.Speed = 1 + enemySpeed;

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
    }
}

