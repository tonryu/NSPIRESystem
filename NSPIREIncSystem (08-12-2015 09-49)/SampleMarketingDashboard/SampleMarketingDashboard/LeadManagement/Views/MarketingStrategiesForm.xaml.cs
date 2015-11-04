using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for MarketingStrategiesForm.xaml
    /// </summary>
    public partial class MarketingStrategiesForm : UserControl
    {
        public static int MarketingStrategiesId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public MarketingStrategiesForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var markstrat = new MarketingStrategy();

                if (MarketingStrategiesId > 0)
                {
                    var markstarte = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == MarketingStrategiesId);
                    if (markstarte != null)
                    {
                        lblMarketingStrategyId.Visibility = Visibility.Visible;
                        txtMarketingStrategyId.Visibility = Visibility.Visible;

                        Grid.SetRow(lblMarketingStrategyName, 1);
                        Grid.SetRow(txtMarketingStrategyName, 1); Grid.SetColumn(txtMarketingStrategyName, 1);

                        txtMarketingStrategyId.Text = Convert.ToString(markstarte.MarketingStrategyId);
                        txtMarketingStrategyName.Text = markstarte.Description;
                    }
                }
                else
                {
                    lblMarketingStrategyId.Visibility = Visibility.Hidden;
                    txtMarketingStrategyId.Visibility = Visibility.Hidden;

                    Grid.SetRow(lblMarketingStrategyName, 0);
                    Grid.SetRow(txtMarketingStrategyName, 0); Grid.SetColumn(txtMarketingStrategyName, 1);

                    txtMarketingStrategyName.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var mark = new MarketingStrategy();

                if (txtMarketingStrategyName.Text != "" && txtMarketingStrategyName.Text != null)
                {
                    if (MarketingStrategiesId > 0)
                    {
                        var starts = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == MarketingStrategiesId);

                        if (starts != null)
                        {
                            var startsname = context.MarketingStrategies.FirstOrDefault(c => c.Description == txtMarketingStrategyName.Text);

                            if (startsname != null)
                            {
                                if (starts.Description.ToLower() == startsname.Description.ToLower())
                                {
                                    starts.Description = txtMarketingStrategyName.Text;

                                    var log = new Log();
                                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                    log.Description = NotificationWindow.username + " modifies "
                                        + starts.Description + "'s details.";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);

                                    context.SaveChanges();
                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Marketing strategy successfully updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                                else
                                {
                                    var log = new Log();
                                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                    log.Description = NotificationWindow.username + " fails to modify "
                                        + starts.Description
                                        + "'s details due to a similar strategy is already existing.";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);
                                    context.SaveChanges();

                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Similar marketing strategy detected";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                            }
                            else
                            {
                                starts.Description = txtMarketingStrategyName.Text;

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " modifies "
                                    + starts.Description + "'s details.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);

                                context.SaveChanges();
                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Marketing strategy successfully updated";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        var strats = context.MarketingStrategies.FirstOrDefault
                            (c => c.Description.ToLower() == txtMarketingStrategyName.Text.ToLower());

                        if (strats == null)
                        {
                            strats = new MarketingStrategy();
                            strats.Description = txtMarketingStrategyName.Text;

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " creates a new marketing strategy. ("
                                + txtMarketingStrategyName.Text + ")";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);

                            context.MarketingStrategies.Add(strats);
                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Marketing strategy successfully created";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        else
                        {
                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username
                                + " failed to create due to a similar strategy is already existing.";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);
                            context.SaveChanges();

                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Marketing strategy already exists";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Please provide all boxes labeled with an asterisk(*).";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
            }
        }
    }
}

