using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace NSPIREIncSystem.Shared.Windows
{
    /// <summary>
    /// Interaction logic for TaskSchedulerWindow.xaml
    /// </summary>
    public partial class TaskSchedulerWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public TaskSchedulerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation Loading
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }

            DoubleAnimation animation = new DoubleAnimation(0, this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            DoubleAnimation animation2 = new DoubleAnimation(screenWidth, screenWidth - this.Width,
                (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.WidthProperty, animation);
            this.BeginAnimation(Window.LeftProperty, animation2);
            #endregion
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region animation Closing
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }

            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(screenWidth, (Duration)TimeSpan.FromSeconds(0.3));
            var anim2 = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.LeftProperty, anim);
            this.BeginAnimation(Window.WidthProperty, anim2);
            #endregion
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
