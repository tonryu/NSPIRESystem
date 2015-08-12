using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace NSPIREIncSystem.Shared.Windows
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public static string username;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        int tick = 0;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public NotificationWindow()
        {
            InitializeComponent();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 2)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            if (username != "Incorrect username/password.")
            {
                txtWelcome.Text = "Welcome " + username + "!";
            }
            else
            {
                txtWelcome.Text = username;
            }
            #region animation onLoading
            double screenHeight = Application.Current.MainWindow.Height;
            if (screenTopEdge > 0 || screenTopEdge < -8)
            {
                screenHeight += screenTopEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, 85, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.HeightProperty, animation);
            #endregion
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double screenHeight = Application.Current.MainWindow.Height;
            if (screenTopEdge > 0 || screenTopEdge < -8)
            {
                screenHeight += screenTopEdge;
            }
            #region animation onClosing
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(85, 0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.HeightProperty, anim);
            #endregion
        }
    }
}
