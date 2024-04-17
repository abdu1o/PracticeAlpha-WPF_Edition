using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        private void ConnectToServer()
        {
            try
            {
                udpClient = new UdpClient();
                udpClient.Connect(serverIp, serverPort);

                string message = "NewClient";
                SendMessageToServer(message);

                SendMessageToServer("GetConnectedClients");

                IPEndPoint serverEndPoint = (IPEndPoint)udpClient.Client.RemoteEndPoint;

                //wait server response
                byte[] responseData = udpClient.Receive(ref serverEndPoint);
                string responseMessage = Encoding.UTF8.GetString(responseData);

                int connectedClientsCount;
                if (!int.TryParse(responseMessage, out connectedClientsCount))
                {
                    MessageBox.Show("Error parsing connected clients count from server response.");
                    return;
                }

                if (connectedClientsCount == 1)
                {
                    CreatePlayer(0, 125, 500);
                }
                else if (connectedClientsCount == 2)
                {
                    CreatePlayer(1, 1125, 500);
                }
                else
                {
                    MessageBox.Show("Server returned unexpected number of connected clients: " + connectedClientsCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
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
