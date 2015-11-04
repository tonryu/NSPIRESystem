using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.Shared.Views
{
    /// <summary>
    /// Interaction logic for SystemLog.xaml
    /// </summary>
    public partial class SystemLog : UserControl
    {
        public SystemLog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region Logs
            using (var context = new DatabaseContext())
            {
                var textBlock = new TextBlock();
                var logs = context.Logs.OrderByDescending(c => c.LogId).ToList();
                Thickness margin = textBlock.Margin;
                textBlock.TextWrapping = TextWrapping.Wrap;
                var specificStackPanel = new StackPanel();
                var wholeStackPanel = new StackPanel();
                wholeStackPanel.Height = Double.NaN;
                specificStackPanel.Height = Double.NaN;

                gridLogs.Children.Clear();
                foreach (var log in logs)
                {
                    //textblock for date and time
                    specificStackPanel = new StackPanel();
                    textBlock = new TextBlock();
                    textBlock.Text = Convert.ToDateTime(log.Date).ToString("MMMM d, yyyy") + " "
                        + Convert.ToDateTime(log.Time).ToString("hh:mm:ss tt");
                    margin.Top = 10;
                    margin.Bottom = 0;
                    margin.Left = 10;
                    margin.Right = 10;
                    textBlock.Margin = margin;
                    specificStackPanel.Children.Add(textBlock);

                    //textblock for log description
                    textBlock = new TextBlock();
                    textBlock.Text = log.Description;
                    margin.Top = 5;
                    margin.Bottom = 10;
                    margin.Left = 10;
                    margin.Right = 20;
                    textBlock.Margin = margin;
                    specificStackPanel.Children.Add(textBlock);
                    wholeStackPanel.Children.Add(specificStackPanel);
                }
                gridLogs.Children.Add(wholeStackPanel);
            }
            #endregion
        }
    }
}
