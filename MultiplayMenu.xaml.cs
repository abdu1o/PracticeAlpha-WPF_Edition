using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PracticeAlpha_WPF_Edition
{
    public partial class MultiplayMenu : Window
    {
        public MultiplayMenu()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, MouseButtonEventArgs e)
        {
            //buttonSound.PlayAsync();
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;

            this.Close();
            mainWindow.Show();
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ToolTip = null;
        }
    }
}
