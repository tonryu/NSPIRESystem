using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Settings.Views;
using NSPIREIncSystem.Shared.Windows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSPIREIncSystem.Settings.MasterDatas
{
    /// <summary>
    /// Interaction logic for SalesStage.xaml
    /// </summary>
    public partial class SalesStage : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<SalesStagesView> salessatageList = new List<SalesStagesView>();
        bool _isExpanded = false;
        public static int SalesStageId;

        public SalesStage()
        {
            InitializeComponent();
        }

        #region Measure canvas and child objects
        private double GetCanvasMinWidth(Canvas canvas)
        {
            double canvasWidth = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                double childLeftMargin = canvasChildren.FirstOrDefault().Margin.Left;
                double childWidth = canvasChildren.FirstOrDefault().ActualWidth;
                double childRightMargin = canvasChildren.FirstOrDefault().Margin.Right;

                double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                canvasWidth = totalChildWidth;
            }

            return canvasWidth;
        }

        private double GetCanvasMaxWidth(Canvas canvas)
        {
            double canvasWidth = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                foreach (var child in canvasChildren)
                {
                    double childLeftMargin = child.Margin.Left;
                    double childWidth = child.ActualWidth;
                    double childRightMargin = child.Margin.Right;

                    double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                    canvasWidth += totalChildWidth;
                }
            }

            return canvasWidth;
        }

        private double GetCanvasMinHeight(Canvas canvas)
        {
            double canvasHeight = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                double childTopMargin = canvasChildren.FirstOrDefault().Margin.Top;
                double childWidth = canvasChildren.FirstOrDefault().ActualHeight;
                double childBottomMargin = canvasChildren.FirstOrDefault().Margin.Bottom;

                double totalChildHeight = childTopMargin + childWidth + childBottomMargin;
                canvasHeight = totalChildHeight;
            }

            return canvasHeight;
        }

        private double GetCanvasMaxHeight(Canvas canvas)
        {
            double canvasHeight = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                foreach (var child in canvasChildren)
                {
                    double childTopMargin = child.Margin.Top;
                    double childHeight = child.ActualHeight;
                    double childBottomMargin = child.Margin.Bottom;

                    double totalChildHeight = childTopMargin + childHeight + childBottomMargin;
                    canvasHeight += totalChildHeight;
                }
            }

            return canvasHeight;
        }
        #endregion

        #region Activate effects
        private void FoldCanvasSideward(Canvas canvas)
        {
            double minCanvasWidth = GetCanvasMinWidth(canvas);
            double maxCanvasWidth = GetCanvasMaxWidth(canvas);
            if (canvas.Width == minCanvasWidth)
            {
                var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
                if (canvasChildren.Count() > 0)
                {
                    int numberOfButtonsMinusOne = canvasChildren.Count() - 1;//number of buttons - 1
                    double initialCenterWidth = minCanvasWidth - canvasChildren.FirstOrDefault().ActualWidth - canvasChildren.FirstOrDefault().Margin.Left - canvasChildren.FirstOrDefault().Margin.Right;//width to be centered
                    double finalCenterWidth = maxCanvasWidth - canvasChildren.FirstOrDefault().ActualWidth - canvasChildren.FirstOrDefault().Margin.Left - canvasChildren.FirstOrDefault().Margin.Right;
                    double initialUnitWidth = Math.Round(initialCenterWidth / numberOfButtonsMinusOne);
                    double finalUnitWidth = Math.Round(finalCenterWidth / numberOfButtonsMinusOne);

                    StackPanel firstStackPanel = canvasChildren.FirstOrDefault();
                    int index = 0;
                    foreach (StackPanel child in canvasChildren.Where(c => c != firstStackPanel).ToList())//all buttons except first one
                    {
                        index++;
                        if (child != canvasChildren.Last())//until before the last button
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = initialUnitWidth * index, To = finalUnitWidth * index, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                        else//last button
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = 0, To = maxCanvasWidth - minCanvasWidth, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                    }
                    DoubleAnimation canvasAnimation = new DoubleAnimation() { From = GetCanvasMinWidth(canvas), To = GetCanvasMaxWidth(canvas), Duration = TimeSpan.Parse("0:0:0.35") };
                    canvas.BeginAnimation(Canvas.WidthProperty, canvasAnimation);
                }
            }
            else
            {
                var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
                if (canvasChildren.Count() > 0)
                {
                    StackPanel firstStackPanel = canvasChildren.FirstOrDefault();
                    foreach (StackPanel child in canvasChildren)//all buttons except first one
                    {
                        if (child != firstStackPanel)
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = Canvas.GetLeft(child), To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                    }
                    DoubleAnimation canvasAnimation = new DoubleAnimation() { From = GetCanvasMaxWidth(canvas), To = GetCanvasMinWidth(canvas), Duration = TimeSpan.Parse("0:0:0.35") };
                    canvas.BeginAnimation(Canvas.WidthProperty, canvasAnimation);
                }
            }
        }

        private void FoldInnerCanvasSideward(Canvas canvas)
        {
            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvas.Visibility == Visibility.Collapsed)
            {
                canvas.Visibility = Visibility.Visible;
                DoubleAnimation canvasAnimation = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.Parse("0:0:0.35") };
                if (canvasChildren.Count() > 0)
                {
                    double canvasMinWidth = GetCanvasMinWidth(canvas);
                    FoldCanvasSideward(canvas);
                }
                canvas.BeginAnimation(Canvas.OpacityProperty, canvasAnimation);
            }
            else
            {
                DoubleAnimation canvasAnimation = new DoubleAnimation() { From = 1, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                canvasAnimation.Completed += (s, e) => canvas.Visibility = Visibility.Collapsed;
                canvas.BeginAnimation(Canvas.OpacityProperty, canvasAnimation);
                FoldCanvasSideward(canvas);
            }
        }
        #endregion

        #region Load Details
        private Task <String> QueryLoadSalesStages()
        {
            return Task.Factory.StartNew(() =>
                {
                    try
                    {
                        salessatageList.Clear();
                        using(var context = new DatabaseContext())
                        {
                            if(SalesStageId >0)
                            {
                                var salesstage = context.SalesStages.ToList();
                                
                                if(salesstage != null )
                                {
                                    foreach (var item in salesstage)
                                    {
                                        var stage = context.SalesStages.FirstOrDefault(c => c.SalesStageID == item.SalesStageID);
                                        //var contact = context.Contacts.FirstOrDefault(c => c.LeadId == leads.LeadID);

                                        salessatageList.Add(new SalesStagesView()
                                        {
                                            SalesStageID = item.SalesStageID,
                                            SalesStageName = item.SalesStageName,
                                            RankNo = item.RankNo                                           
                                           

                                        });


                                    }
                                }
                            }
                        }
                        return null;
                    }

                    catch (Exception ex)
                    {
                        return "Error Message" + ex.Message;
                    }
                });
        }
         private async void RefreshTable(string str)
        {
            using (var context = new DatabaseContext())
            {
                string message = "";
                busyIndicator.IsBusy = true;
                message = await QueryLoadSalesStages();

                 if (message != null)
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = message;
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                
                var salesstg = context.SalesStages.FirstOrDefault(c => c.SalesStageID == SalesStageId);

                if (salesstg != null)
                {
                    dcSalesStagesList.ItemsSource = salessatageList.Where
                        (c => (c.SalesStageName.ToLower().Contains(txtSearch.Text.ToLower()))).
                        OrderBy(c => c.RankNo).ToList();
                           
                   viewSalesStages.BestFitColumns();
                }

                if (salessatageList.Count == 0)
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "List has no Activity.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                busyIndicator.IsBusy = false;
            }
        }

         private void LoadActivity()
         {
             RefreshTable("");
         }
         #endregion

         private void UserControl_Loaded(object sender, RoutedEventArgs e)
         {
             LoadActivity();
         }

         private void btnView_Click(object sender, RoutedEventArgs e)
         {
             var selectedstage = dcSalesStagesList.SelectedItem as SalesStagesView;

             Storyboard sb;
             if (_isExpanded != true && selectedstage != null)
             {
                 SalesStageDetails.SalesStageId = selectedstage.SalesStageID;

                 sb = this.FindResource("gridin") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;

                 var page = new SalesStageDetails();
                 navigation.Navigate(page);
             }
             else
             {
                 sb = this.FindResource("gridout") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;

                 LoadActivity();
             }
         }

         private void btnAdd_Click(object sender, RoutedEventArgs e)
         {
              SalesStageDetails.SalesStageId = 0;
              //LeadActivityForm.LeadId = LeadId;

            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new SalesStageDetails();
            navigation.Navigate(page);
         }

         private void btnEdit_Click(object sender, RoutedEventArgs e)
         {
             Storyboard sb;
             if (_isExpanded != true)
             {
                 sb = this.FindResource("gridin") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;
             }

             var selectedsales = dcSalesStagesList.SelectedItem as SalesStagesView;

             if (selectedsales != null)
             {
                 SalesStageDetails.SalesStageId = selectedsales.SalesStageID;
                 //SalesStageDetails.LeadId = LeadId;

                 var page = new SalesStageDetails();
                 navigation.Navigate(page);
             }
             else
             {
                 NullMessage();
             }
         }

         private void btnDelete_Click(object sender, RoutedEventArgs e)
         {

             var windows = new Shared.Windows.NoticeWindow();
             Shared.Windows.NoticeWindow.message = "Sales Stage cannot be deleted.";
             windows.Height = 0;
             windows.Top = screenTopEdge + 8;
             windows.Left = (screenWidth / 2) - (windows.Width / 2);
             if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
             windows.ShowDialog();
         }

         private void btnPrint_Click(object sender, RoutedEventArgs e)
         {

         }

         private void btnBack_Click(object sender, RoutedEventArgs e)
         {
             var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
             frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
             frame.GoBack();
         }

         private void NullMessage()
         {
             var windows = new Shared.Windows.NoticeWindow();
             NoticeWindow.message = "Please select a record.";
             windows.Height = 0;
             windows.Top = screenTopEdge + 8;
             windows.Left = (screenWidth / 2) - (windows.Width / 2);
             if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
             windows.ShowDialog();
         }


         //private void LoadMethod(string text)
         //{
         //    using (var context = new DatabaseContext())
         //    {
         //        if (SalesStageId > 0)
         //        {
         //            var sales = context.SalesStages.ToList();

         //            if (sales != null)
         //            {
         //                salessatageList.Clear();
         //                foreach (var item in sales)
         //                {
         //                    var lead = context.SalesStages.FirstOrDefault(c => c.SalesStageID == sales.);
         //                    var contact = context.Contacts.FirstOrDefault(c => c.LeadId == lead.LeadID);

         //                    salessatageList.Add(new SalesStagesView
         //                    {
         //                        ActivityDate = item.ActivityDate,
         //                        ActivityId = item.ActivityID,
         //                        ActivityTime = item.ActivityTime,
         //                        ClientResponse = item.ClientResponse,
         //                        CompanyName = lead.CompanyName,
         //                        Cost = item.Cost,
         //                        Description = item.Description,
         //                        MarketingVoucher = item.MarketingVoucherNo,
         //                        NextStep = item.NextStep,
         //                        NextStepDueDate = item.DueDateOfNextStep,
         //                        SalesRep = item.SalesRep,
         //                        TransactionDetails = item.DetailsOfTransaction,
         //                        ContactPerson = contact.ContactPersonName,
         //                        IsFinalized = item.IsFinalized
         //                    });
         //                }

         //                var leads = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

         //                if (leads != null)
         //                {
         //                    dcLeadActivitiesList.ItemsSource = activityList.Where
         //                        (c => (c.Description.ToLower().Contains(text.ToLower()) ||
         //                            c.NextStep.ToLower().Contains(text.ToLower()) ||
         //                            c.SalesRep.ToLower().Contains(text.ToLower()) ||
         //                            c.TransactionDetails.ToLower().Contains(text.ToLower()) ||
         //                            c.MarketingVoucher.ToLower().Contains(text.ToLower()) ||
         //                            c.ActivityDate.ToLower().Contains(text.ToLower()) ||
         //                            c.ActivityTime.ToLower().Contains(text.ToLower()) ||
         //                            c.NextStepDueDate.ToLower().Contains(text.ToLower()) ||
         //                            c.ContactPerson.ToLower().Contains(text.ToLower())) &&
         //                            c.CompanyName.ToLower() == leads.CompanyName.ToLower()).
         //                            OrderByDescending(c => Convert.ToDateTime(c.NextStepDueDate)).ToList();

         //                    viewLeadActivity.BestFitColumns();
         //                }
         //            }
         //        }
         //    }
         //}




    }

    
}
