using PracticeAlpha_WPF_Edition.EntitiesController;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;

namespace PracticeAlpha_WPF_Edition.Levels
{
    public partial class Level1_Multiplayer : Window
    {
        private UdpClient udpClient;
        private readonly int serverPort = 1234;
        private readonly string serverIp = "127.0.0.1";
        private string[] playerImages = { "/PracticeAlpha-WPF_Edition;component/Resources/Entities/player.png",
                                          "/PracticeAlpha-WPF_Edition;component/Resources/Entities/player2.png" };

        private List<Player> players = new List<Player>();

        public Level1_Multiplayer()
        {
            InitializeComponent();

            this.Loaded += Level1_Multiplayer_Loaded;
        }

        private void Level1_Multiplayer_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectToServer();
            Task.Run(() => ReceiveMessagesFromServer());
        }

        private void ConnectToServer()
        {
            try
            {
                udpClient = new UdpClient();
                udpClient.Connect(serverIp, serverPort);
                SendMessageToServer("NewClient");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        private void CreatePlayer(int playerIndex, int x, int y)
        {
            string imagePath = playerImages[playerIndex];
            Player player = new Player(x, y, 48, 36, imagePath);
            mainCanvas.Children.Add(player.PlayerImage);

            Canvas.SetZIndex(player.PlayerImage, 100);

            Canvas.SetLeft(player.PlayerImage, player.X);
            Canvas.SetTop(player.PlayerImage, player.Y);
        }

        private async void ReceiveMessagesFromServer()
        {
            try
            {
                IPEndPoint serverEndPoint;

                while (true)
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    byte[] responseData = result.Buffer;
                    string message = Encoding.UTF8.GetString(responseData);

                    serverEndPoint = result.RemoteEndPoint;

                    if (message == "Start")
                    {
                        Dispatcher.Invoke(() =>
                        {
                            CreatePlayer(0, 200, 100);
                            CreatePlayer(1, 400, 100);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving messages from server: " + ex.Message);
            }
        }

        private void SendMessageToServer(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                udpClient.Send(data, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message to server: " + ex.Message);
            }
        }


    }
}
