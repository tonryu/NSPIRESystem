using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for SalesStageForm.xaml
    /// </summary>
    public partial class SalesStageForm : UserControl
    {
        public static int SalesStageId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public SalesStageForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var SaleStage = new SalesStage();

                if (SalesStageId > 0)
                {
                    var stage = context.SalesStages.FirstOrDefault(c => c.SalesStageID == SalesStageId);
                    if (stage != null)
                    {
                        lblsaleId.Visibility = Visibility.Visible;
                        txtSalesId.Visibility = Visibility.Visible;
                        txtSalesId.IsReadOnly = true;
                        txtSalesStageName.IsEnabled = IsEnabled;
                        txtRankNo.IsEnabled = IsEnabled;
                        txtSalesId.Text = Convert.ToString(stage.SalesStageID);
                        txtRankNo.Text = Convert.ToString(stage.RankNo);
                        txtSalesStageName.Text = stage.SalesStageName;

                    }
                }
                else
                {
                    lblsaleId.Visibility = Visibility.Hidden;
                    txtSalesStageName.IsEnabled = IsEnabled;
                    txtRankNo.IsEnabled = IsEnabled;
                    txtSalesId.Visibility = Visibility.Hidden;
                    txtSalesStageName.Text = "";
                    txtRankNo.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var sale = new SalesStage();

                if (txtRankNo.Text != "" && txtSalesStageName.Text != "")
                {
                    if (SalesStageId > 0)
                    {
                        var sales = context.SalesStages.FirstOrDefault(c => c.SalesStageID == SalesStageId);

                        if (sales != null)
                        {
                            var stagename = context.SalesStages.FirstOrDefault(c => c.SalesStageName == txtSalesStageName.Text);

                            if (stagename != null)
                            {
                                if (sales.SalesStageName.ToLower() == stagename.SalesStageName.ToLower()
                                    && sales.RankNo == stagename.RankNo)
                                {
                                    sales.RankNo = Convert.ToInt32(txtRankNo.Text);
                                    sales.SalesStageName = txtSalesStageName.Text;

                                    var log = new Log();
                                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                    log.Description = NotificationWindow.username + " modified "
                                        + sales.SalesStageName + "'s details.";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);

                                    context.SaveChanges();
                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Sales Stage successfully updated";
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
                                    log.Description = NotificationWindow.username + " failed to modify "
                                        + sales.SalesStageName
                                        + "'s details due to a similar stage is already existing.";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);
                                    context.SaveChanges();

                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Similar Stage detected";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                            }
                        }
                    }

                    else
                    {
                        var Stages = context.SalesStages.FirstOrDefault
                            (c => c.SalesStageName.ToLower() == txtSalesStageName.Text.ToLower());

                        if (Stages == null)
                        {
                            Stages = new SalesStage();
                            Stages.SalesStageName = txtSalesStageName.Text;
                            Stages.RankNo =Convert.ToInt32 (txtRankNo.Text);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " created a sales stage. ("
                                + txtSalesStageName.Text + ")";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);

                            context.SalesStages.Add(Stages);
                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Sale Stage successfully created";
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
                                + " failed to create due to a similar stage is already existing.";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);
                            context.SaveChanges();

                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Sale Stage already exists";
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
                    NoticeWindow.message = "Please provide all fields associated with an asterisk(*).";
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
