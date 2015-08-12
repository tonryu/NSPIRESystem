using System;
using System.Windows;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.Shared.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxLogout.xaml
    /// </summary>
    public partial class MessageBoxLogout : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;

        public MessageBoxLogout()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation Loading
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

            #region animation Closing
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(85, 0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.HeightProperty, anim);
            #endregion
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Variables.yesClicked = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Variables.yesClicked = false;
            this.Close();
        }
    }
}
