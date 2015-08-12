using System;
using System.Windows;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.Shared.Windows
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;

        public MessageBoxWindow(string message)
        {
            InitializeComponent();
            this.tblkMessage.Text = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation Loading
            double screenHeight = Application.Current.MainWindow.Height;
            if (screenTopEdge > 0 || screenTopEdge < -8)
            {
                screenHeight += screenTopEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, 105, (Duration)TimeSpan.FromSeconds(0.3));
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
            var anim = new DoubleAnimation(105, 0, (Duration)TimeSpan.FromSeconds(0.3));
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
