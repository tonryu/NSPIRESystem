using System;
using System.Windows;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region animation Closing
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.5));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
            #endregion

            using (var context = new DatabaseContext())
            {
                if (Variables.yesClicked != true && NotificationWindow.username != null)
                {
                    var log = new Log();
                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                    log.Description = NotificationWindow.username + " logs out on "
                        + DateTime.Now.ToString("MMMM d, yyyy") + " at " + DateTime.Now.ToString("HH:mm") + ".";
                    context.Logs.Add(log);
                    context.SaveChanges();
                }
            }
        }
    }
}
