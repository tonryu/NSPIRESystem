using NSPIREIncSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                        txtSalesStageName.IsEnabled = IsEnabled;
                        txtRankNo.IsEnabled = IsEnabled;
                        txtSalesId.Text = Convert.ToString(stage.SalesStageID);
                        txtRankNo.Text = Convert.ToString(stage.RankNo);
                        txtSalesStageName.Text = stage.SalesStageName;

                    }

                    SalesStageId = 0;

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

                                    context.SaveChanges();
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Sale Stage successfully updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }

                                else
                                {
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Similar Stage detected";
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
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Sale Stage already exist";
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
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "PLEASE PROVIDE ALL ASSOCIATED WITH ASTERISKS(*)";
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
