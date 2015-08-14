using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.MasterDatas;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Dashboards
{
    /// <summary>
    /// Interaction logic for LeadDashboard.xaml
    /// </summary>
    public partial class LeadDashboard : UserControl
    {
        public List<ActivityView> activityList = new List<ActivityView>();
        public List<LeadsListBox> leadListBox = new List<LeadsListBox>();
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;

        public LeadDashboard()
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

        #region Fill charts
        private void FillCharts()
        {
            FillLeadPerYear();
            FillLeadPerMonth();
            FillLeadPerSalesStageStatus();
            //FillLeadPerTerritory();
            FillEngagedClientsPerYear();
            FillEngagedClientsPerMonth();
            FillEngagedClientsPerTerritory();
            FillOverallLeads();
        }

        private void FillLeadPerYear()
        {
            using (var context = new DatabaseContext())
            {
                string year = DateTime.Now.ToString("yyyy");
                int year1 = 0, year2 = 0, year3 = 0;
                var lead = context.Leads.ToList();
                foreach (var item in lead)
                {
                    var date = context.Leads.FirstOrDefault(c => c.DateAdded == item.DateAdded);

                    if (date != null)
                    {
                        var dateAdded = Convert.ToDateTime(date.DateAdded).ToString("yyyy");

                        if (dateAdded == Convert.ToString(Convert.ToInt32(year) - 3))
                        {
                            year1++;
                        }
                        else if (dateAdded == Convert.ToString(Convert.ToInt32(year) - 2))
                        {
                            year2++;
                        }
                        else if (dateAdded == Convert.ToString(Convert.ToInt32(year) - 1))
                        {
                            year3++;
                        }
                    }
                }
                ccLeadPerYear.Diagram.Series[0].Points.Clear();
                ccLeadPerYear.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(Convert.ToInt32(year) - 3), year1));
                ccLeadPerYear.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(Convert.ToInt32(year) - 2), year2));
                ccLeadPerYear.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(Convert.ToInt32(year) - 1), year3));

                lblChartPerYear.Text = "Total Leads : " + Convert.ToString(year1 + year2 + year3);
            }
        }

        private void FillLeadPerMonth()
        {
            using (var context = new DatabaseContext())
            {
                string date = DateTime.Now.ToString("MM/dd/yyyy");
                string month = DateTime.Now.ToString("MM");
                string year = DateTime.Now.ToString("yyyy");
                int month1 = 0, month2 = 0, month3 = 0, month4 = 0, month5 = 0, month6 = 0, month7 = 0, month8 = 0, month9 = 0, month10 = 0, month11 = 0, month12 = 0;
                var lead = context.Leads.ToList();
                string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };

                foreach (var item in lead)
                {
                    var dateAdded = context.Leads.FirstOrDefault(c => c.DateAdded == item.DateAdded);
                    var monthAdded = Convert.ToDateTime(dateAdded.DateAdded).ToString("MM");
                    var yearAdded = Convert.ToDateTime(dateAdded.DateAdded).ToString("yyyy");

                    if (Convert.ToString(monthAdded) == "01" && yearAdded == year) { month1++; }
                    else if (Convert.ToString(monthAdded) == "02" && yearAdded == year) { month2++; }
                    else if (Convert.ToString(monthAdded) == "03" && yearAdded == year) { month3++; }
                    else if (Convert.ToString(monthAdded) == "04" && yearAdded == year) { month4++; }
                    else if (Convert.ToString(monthAdded) == "05" && yearAdded == year) { month5++; }
                    else if (Convert.ToString(monthAdded) == "06" && yearAdded == year) { month6++; }
                    else if (Convert.ToString(monthAdded) == "07" && yearAdded == year) { month7++; }
                    else if (Convert.ToString(monthAdded) == "08" && yearAdded == year) { month8++; }
                    else if (Convert.ToString(monthAdded) == "09" && yearAdded == year) { month9++; }
                    else if (Convert.ToString(monthAdded) == "10" && yearAdded == year) { month10++; }
                    else if (Convert.ToString(monthAdded) == "11" && yearAdded == year) { month11++; }
                    else if (Convert.ToString(monthAdded) == "12" && yearAdded == year) { month12++; }
                }
                ccLeadPerMonth.Diagram.Series[0].Points.Clear();
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[0]), month1));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[1]), month2));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[2]), month3));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[3]), month4));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[4]), month5));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[5]), month6));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[6]), month7));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[7]), month8));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[8]), month9));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[9]), month10));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[10]), month11));
                ccLeadPerMonth.Diagram.Series[0].Points.Add(new SeriesPoint(Convert.ToString(months[11]), month12));

                lblChartPerMonth.Text = "Total Leads : " + Convert.ToString(month1 + month2 + month3 
                    + month4 + month5 + month6 + month7 + month8 + month9 + month10 + month11 + month12);
            }
        }

        private void FillLeadPerSalesStageStatus()
        {
            using (var context = new DatabaseContext())
            {
                var leads = context.Leads.ToList();
                var salesStageStatus = context.SalesStages.ToList();
                int countLeads = 0, overAll = 0;

                ccPerSalesStageStatus.Diagram.Series[0].Points.Clear();
                foreach (var item in salesStageStatus)
                {
                    foreach (var lead in leads)
                    {
                        if (lead.Status.ToLower() == item.SalesStageName.ToLower())
                        {
                            countLeads++;
                            overAll++;
                        }
                    }
                    ccPerSalesStageStatus.Diagram.Series[0].Points.Add(new SeriesPoint(item.SalesStageName, countLeads));
                    ccPerSalesStageStatus.ToolTipEnabled = true;
                    ccPerSalesStageStatus.Legend.DataContext = item.SalesStageName;
                    countLeads = 0;
                }

                lblChartPerSalesStageStatus.Text = "Total Leads : " + overAll;
            }
        }

        //private void FillLeadPerTerritory()
        //{
        //    using (var context = new DatabaseContext())
        //    {
        //        var leads = context.Leads.ToList();
        //        var territories = context.Territories.ToList();
        //        int countLeads = 0, overAll = 0;

        //        ccLeadPerTerritory.Diagram.Series[0].Points.Clear();
        //        foreach (var item in territories)
        //        {
        //            foreach (var lead in leads)
        //            {
        //                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);

        //                if (territory != null)
        //                {
        //                    if (territory.TerritoryID == item.TerritoryID)
        //                    {
        //                        countLeads++;
        //                        overAll++;
        //                    }
        //                }
        //            }
        //            ccLeadPerTerritory.Diagram.Series[0].Points.Add(new SeriesPoint(item.TerritoryName, countLeads));
        //            ccLeadPerTerritory.ToolTipEnabled = true;
        //            countLeads = 0;
        //        }

        //        lblChartPerTerritory.Text = "Total Leads : " + overAll;
        //    }
        //}

        private void FillEngagedClientsPerYear()
        {

        }

        private void FillEngagedClientsPerMonth()
        {

        }

        private void FillEngagedClientsPerTerritory()
        {

        }

        private void FillOverallLeads()
        {
            using (var context = new DatabaseContext())
            {
                var leads = context.Leads.ToList();
                List<string> statuses = new List<string>() {"Engaged client", "Active", "Not active"};
                int countLeads = 0, overAll = leads.Count(), percent = 0;

                foreach (var status in statuses)
                {
                    foreach (var item in leads)
                    {
                        var customerAccount = context.CustomerAccounts.FirstOrDefault(c => c != null && c.LeadID == item.LeadID);

                        if (item.IsActive == true && status == "Engaged client" && customerAccount != null)
                        {
                            countLeads++;
                        }
                        else if (item.IsActive == true && status == "Active" && customerAccount == null)
                        {
                            countLeads++;
                        }
                        else if (item.IsActive == false && status == "Not active" && customerAccount == null)
                        {
                            countLeads++;
                        }
                    }

                    Compute(countLeads, overAll, out percent);
                    countLeads = 0;

                    if (status == "Engaged client")
                    {
                        lblEngaged.Text = percent + "%";
                    }
                    else if (status == "Active")
                    {
                        lblActive.Text = percent + "%";
                    }
                    else if (status == "Not active")
                    {
                        lblNotContinue.Text = percent + "%";
                    }
                }

                lbloverAllLeads.Text = "Overall Leads : " + overAll;
            }
        }

        private static int Compute(int part, int whole, out int percent)
        {
            if (part > 0 && whole > 0)
            {
                return percent = Convert.ToInt32((Convert.ToDouble((part * 100.00) / whole)));
            }
            else
            {
                return percent = 00;
            }
        }

        #endregion

        #region Load Details
        private Task<string> QueryLoadLeadDashboard()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var context = new DatabaseContext())
                    {
                        var lead = context.Leads.ToList();

                        leadListBox.Clear();
                        foreach (var item in lead)
                        {
                            leadListBox.Add(new LeadsListBox
                            {
                                CompanyName = item.CompanyName
                            });
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return "Error Message: " + ex.Message;
                }
            });
        }

        private async void RefreshTables(string str)
        {
            FillCharts();
            using (var context = new DatabaseContext())
            {
                string message = "";
                busyIndicator.IsBusy = true;
                message = await QueryLoadLeadDashboard();

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

                lbeLeads.ItemsSource = leadListBox.Select(c => c.CompanyName).ToList();

                lblTotalLead.Text = "Total Leads : " + leadListBox.Count();

                busyIndicator.IsBusy = false;
            }
        }

        private void LoadLeadDashboard()
        {
            RefreshTables("");
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            canvasLeadMenu.Width = GetCanvasMinWidth(canvasLeadMenu);
            canvasLeadMenu.Height = GetCanvasMinHeight(canvasLeadMenu);
            canvasLeadMenu.Visibility = Visibility.Collapsed;
            canvasLeadMenu.Opacity = 0;
            FoldInnerCanvasSideward(canvasLeadMenu);

            canvasSalesStages.Width = GetCanvasMinWidth(canvasSalesStages);
            canvasSalesStages.Height = GetCanvasMinHeight(canvasSalesStages);
            canvasSalesStages.Visibility = Visibility.Collapsed;
            canvasSalesStages.Opacity = 0;

            //FillCharts();
            LoadLeadDashboard();

            //using (var context = new DatabaseContext())
            //{
            //    var lead = context.Leads.ToList();

            //    leadListBox.Clear();
            //    foreach (var item in lead)
            //    {
            //        leadListBox.Add(new LeadsListBox
            //        {
            //            CompanyName = item.CompanyName
            //        });
            //    }
            //    lbeLeads.ItemsSource = leadListBox.Select(c => c.CompanyName).ToList();

            //    lblTotalLead.Text = "Total Lead : " + leadListBox.Count();
            //}
        }

        private void btnLeads_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            LeadMasterData page = new LeadMasterData();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasLeadMenu);
        }

        private void btnSalesStage_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasLeadMenu);
            FoldInnerCanvasSideward(canvasSalesStages);
        }

        private void btnPerCompany_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void btnBackToLeadMenu_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasSalesStages);
            FoldInnerCanvasSideward(canvasLeadMenu);
        }

        private void lbeLeads_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedRow = Convert.ToString(lbeLeads.SelectedItem);

                if (selectedRow != null)
                {
                    var lead = context.Leads.FirstOrDefault(c => c.CompanyName == selectedRow);

                    if (lead != null)
                    {
                        var allActivities = context.LeadActivities.ToList();

                        if (allActivities.Count > 0)
                        {
                            activityList.Clear();

                            foreach (var item in allActivities)
                            {
                                lead = context.Leads.FirstOrDefault(c => c.LeadID == item.LeadID);
                                var contact = context.Contacts.FirstOrDefault(c => c.ContactID == item.ContacId);

                                if (contact != null)
                                {
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
                                else
                                {
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
                                        ContactPerson = null,
                                        IsFinalized = item.IsFinalized
                                    }); 
                                }
                            }

                            dcActivity.ItemsSource = activityList.Where(c => c.CompanyName == selectedRow
                                && c.IsFinalized != true);

                            viewActivity.BestFitColumns();
                        }
                    }
                    lblTotalLeadActivity.Text = "Total Lead Activities : " + activityList.Where(c => c.CompanyName == selectedRow
                                && c.IsFinalized != true).Count();
                }
            }
        }

        private void btnsalesmaster_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            NSPIREIncSystem.LeadManagement.MasterDatas.SalesStage page = new MasterDatas.SalesStage();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasSalesStages);
        }
    }
}
