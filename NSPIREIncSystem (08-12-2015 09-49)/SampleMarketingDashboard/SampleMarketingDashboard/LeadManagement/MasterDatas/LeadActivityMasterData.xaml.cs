using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Reports;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for LeadActivityMasterData.xaml
    /// </summary>
    public partial class LeadActivityMasterData : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        bool _isExpanded = false;
        public List<ActivityView> activityList = new List<ActivityView>();
        public static int LeadId;
        public static int contactId;

        public LeadActivityMasterData()
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

        #region Load details
        private Task<string> QueryLoadLeadActivity()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    activityList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        if (LeadId > 0)
                        {
                            var activity = context.LeadActivities.ToList();

                            if (activity != null)
                            {
                                foreach (var item in activity)
                                {
                                    var lead = context.Leads.FirstOrDefault(c => c.LeadID == item.LeadID);

                                    if (lead != null)
                                    {
                                        var contact = context.Contacts.FirstOrDefault(c => c.LeadId == lead.LeadID);

                                        activityList.Add(new ActivityView()
                                        {
                                            ActivityDate = item.ActivityDate,
                                            ActivityId = item.ActivityID,
                                            ActivityTime = item.ActivityTime,
                                            ClientResponse = item.ClientResponse,
                                            Cost = item.Cost,
                                            Description = item.Description,
                                            IsFinalized = item.IsFinalized,
                                            MarketingVoucher = item.MarketingVoucherNo,
                                            NextStep = item.NextStep,
                                            NextStepDueDate = item.DueDateOfNextStep,
                                            SalesRep = item.SalesRep,
                                            CompanyName = lead.CompanyName,
                                            ContactPerson = contact.ContactPersonName,
                                            TransactionDetails = item.DetailsOfTransaction

                                        });
                                    }
                                }
                            }
                        }
                    }
                    return null;
                }

                catch (Exception ex)
                {
                    return "Error Message : " + ex.Message;
                }
            });
        }

        private async void RefreshTable(string str)
        {
            using (var context = new DatabaseContext())
            {
                string message = "";
                busyIndicator.IsBusy = true;
                message = await QueryLoadLeadActivity();

                if (message != null)
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = message;
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                var leads = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                if (leads != null)
                {
                    dcLeadActivitiesList.ItemsSource = activityList.Where
                        (c => (c.Description.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.NextStep.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.SalesRep.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.TransactionDetails.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.MarketingVoucher.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.ActivityDate.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.ActivityTime.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.NextStepDueDate.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            c.ContactPerson.ToLower().Contains(txtSearch.Text.ToLower())) &&
                            c.CompanyName.ToLower() == leads.CompanyName.ToLower()).
                            OrderByDescending(c => Convert.ToDateTime(c.NextStepDueDate)).ToList();

                    viewLeadActivity.BestFitColumns();
                }

                if (activityList.Count == 0)
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "List has no activity.";
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

        private void LoadMethod(string text)
        {
            using (var context = new DatabaseContext())
            {
                if (LeadId > 0)
                {
                    var activities = context.LeadActivities.ToList();

                    if (activities != null)
                    {
                        activityList.Clear();
                        foreach (var item in activities)
                        {
                            var lead = context.Leads.FirstOrDefault(c => c.LeadID == item.LeadID);
                            var contact = context.Contacts.FirstOrDefault(c => c.LeadId == lead.LeadID);

                            activityList.Add(new ActivityView
                            {
                                ActivityDate = item.ActivityDate,
                                ActivityId = item.ActivityID,
                                ActivityTime = item.ActivityTime,
                                ClientResponse = item.ClientResponse,
                                CompanyName = lead.CompanyName,
                                Cost = item.Cost,
                                Description = item.Description,
                                MarketingVoucher = item.MarketingVoucherNo,
                                NextStep = item.NextStep,
                                NextStepDueDate = item.DueDateOfNextStep,
                                SalesRep = item.SalesRep,
                                TransactionDetails = item.DetailsOfTransaction,
                                ContactPerson = contact.ContactPersonName,
                                IsFinalized = item.IsFinalized
                            });
                        }

                        var leads = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                        if (leads != null)
                        {
                            dcLeadActivitiesList.ItemsSource = activityList.Where
                                (c => (c.Description.ToLower().Contains(text.ToLower()) ||
                                    c.NextStep.ToLower().Contains(text.ToLower()) ||
                                    c.SalesRep.ToLower().Contains(text.ToLower()) ||
                                    c.TransactionDetails.ToLower().Contains(text.ToLower()) ||
                                    c.MarketingVoucher.ToLower().Contains(text.ToLower()) ||
                                    c.ActivityDate.ToLower().Contains(text.ToLower()) ||
                                    c.ActivityTime.ToLower().Contains(text.ToLower()) ||
                                    c.NextStepDueDate.ToLower().Contains(text.ToLower()) ||
                                    c.ContactPerson.ToLower().Contains(text.ToLower())) &&
                                    c.CompanyName.ToLower() == leads.CompanyName.ToLower()).
                                    OrderByDescending(c => Convert.ToDateTime(c.NextStepDueDate)).ToList();

                            viewLeadActivity.BestFitColumns();
                        }
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadActivity();
            canvasLeadActivityMasterData.Width = GetCanvasMinWidth(canvasLeadActivityMasterData);
            canvasLeadActivityMasterData.Height = GetCanvasMinHeight(canvasLeadActivityMasterData);
            canvasLeadActivityMasterData.Visibility = Visibility.Collapsed;
            canvasLeadActivityMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasLeadActivityMasterData);

            using (var context = new DatabaseContext())
            {
                var lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                lblLeadActivity.Text = lead.CompanyName.ToUpper() + " LEAD ACTIVITIES";
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //LoadMethod(txtSearch.Text);
            RefreshTable(txtSearch.Text);
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedActivity = dcLeadActivitiesList.SelectedItem as ActivityView;

            Storyboard sb;
            if (_isExpanded != true && selectedActivity != null)
            {
                LeadActivityDetails.ActivityId = selectedActivity.ActivityId;

                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                var page = new LeadActivityDetails();
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
            LeadActivityForm.ActivityId = 0;
            LeadActivityForm.LeadId = LeadId;

            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new LeadActivityForm();
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

            var selectedActivity = dcLeadActivitiesList.SelectedItem as ActivityView;

            if (selectedActivity != null)
            {
                LeadActivityForm.ActivityId = selectedActivity.ActivityId;
                LeadActivityForm.LeadId = LeadId;

                var page = new LeadActivityForm();
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
            Shared.Windows.NoticeWindow.message = "An activity cannot be deleted.";
            windows.Height = 0;
            windows.Top = screenTopEdge + 8;
            windows.Left = (screenWidth / 2) - (windows.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            windows.ShowDialog();

            //using (var context = new DatabaseContext())
            //{
            //    var selectedActivity = dcLeadActivitiesList.SelectedItem as ActivityView;

            //    if (selectedActivity != null)
            //    {
            //        var activity = context.LeadActivities.FirstOrDefault(c => c.ActivityID == selectedActivity.ActivityId);

            //        if (activity != null)
            //        {
            //            var window = new MessageBoxWindow("Are you sure you want to delete this record?");
            //            window.Height = 0;
            //            window.Top = screenTopEdge + 8;
            //            window.Left = (screenWidth / 2) - (window.Width / 2);
            //            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
            //            window.ShowDialog();

            //            if (Variables.yesClicked == true)
            //            {
            //                if (activity.ActivityDate != null && activity.ActivityTime != null)
            //                {
            //                    context.LeadActivities.Remove(activity);

            //                    var windows = new Shared.Windows.NoticeWindow();
            //                    Shared.Windows.NoticeWindow.message = "Activity successfully deleted";
            //                    windows.Height = 0;
            //                    windows.Top = screenTopEdge + 8;
            //                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
            //                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            //                    windows.ShowDialog();

            //                    context.SaveChanges();
            //                }
            //                else
            //                {
            //                    var windows = new Shared.Windows.NoticeWindow();
            //                    Shared.Windows.NoticeWindow.message = "Activity is still not done.";
            //                    windows.Height = 0;
            //                    windows.Top = screenTopEdge + 8;
            //                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
            //                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            //                    windows.ShowDialog();
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        NullMessage();
            //    }
            //    LoadMethod(txtSearch.Text);
            //}
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var activities = activityList.ToList();
                int itemNo = 0;
                var lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);
                if (activities.Count() > 0)
                {
                    List<LeadActivityReportData> dataList = new List<LeadActivityReportData>();
                    List<LeadActivityReportDetail> detailsList = new List<LeadActivityReportDetail>();

                    foreach (var leadActivity in activities)
                    {
                        var contact = context.Contacts.FirstOrDefault(c => c.ContactPersonName == leadActivity.ContactPerson);

                        if (leadActivity.CompanyName == lead.CompanyName)
                        {
                            itemNo++;
                            var detail = new LeadActivityReportDetail();

                            detail.ActivityDate = leadActivity.ActivityDate;
                            detail.ActivityNo = itemNo;
                            detail.ActivityTime = leadActivity.ActivityTime;
                            detail.ClientResponse = leadActivity.ClientResponse;
                            detail.CompanyName = leadActivity.CompanyName;
                            detail.Cost = leadActivity.Cost;
                            detail.Description = leadActivity.Description;
                            detail.DetailsOfTransaction = leadActivity.TransactionDetails;
                            detail.DueDateOfNextStep = leadActivity.NextStepDueDate;
                            detail.MarketingVoucherNo = leadActivity.MarketingVoucher;
                            detail.NextStep = leadActivity.NextStep;
                            detail.SalesRep = leadActivity.SalesRep;
                            if (contact != null) { detail.ContactPerson = contact.ContactPersonName; }
                            else { detail.ContactPerson = ""; }
                            detailsList.Add(detail);
                        }
                    }

                    dataList.Add(new LeadActivityReportData()
                    {
                        ReportHeader = "LEAD ACTIVITIES",
                        ReportTitle = "LEAD ACTIVITIES of " + lead.CompanyName.ToUpper()
                        + " as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalActivities = detailsList.Count(),
                        details = detailsList
                    });

                    var report = new LeadActivityReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "LEAD ACTIVITIES of " + lead.CompanyName.ToUpper()
                        + " as of " + DateTime.Now.ToString("MMMM dd, yyyy")
                    };

                    using (ReportPrintTool printTool = new ReportPrintTool(report))
                    {
                        printTool.ShowRibbonPreviewDialog();
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "No data to print.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void NullMessage()
        {
            var windows = new NoticeWindow();
            NoticeWindow.message = "Please select a record.";
            windows.Height = 0;
            windows.Top = screenTopEdge + 8;
            windows.Left = (screenWidth / 2) - (windows.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            windows.ShowDialog();
        }
    }
}
