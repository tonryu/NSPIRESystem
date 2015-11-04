using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using NSPIREIncSystem.LeadManagement.Reports;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Reports;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for LeadMasterData.xaml
    /// </summary>
    public partial class LeadMasterData : UserControl
    {
        public List<LeadsView> leadsList = new List<LeadsView>();
        public List<ContactView> contactsList = new List<ContactView>();
        public List<ProductsByLeadsView> leadsProductsList = new List<ProductsByLeadsView>();
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height; 
        bool _isExpanded = false;

        public LeadMasterData()
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
        private Task<string> QueryLoadLeads()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    leadsList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        var lead = context.Leads.ToList();

                        if (lead != null)
                        {
                            foreach (var item in lead)
                            {
                                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == item.TerritoryID);

                                leadsList.Add(new LeadsView
                                {
                                    CompanyAddress = item.CompanyAddress,
                                    CompanyName = item.CompanyName,
                                    LeadId = item.LeadID,
                                    SalesStageStatus = item.Status,
                                    TerritoryName = territory.TerritoryName
                                });
                            }
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
            string message = "";
            busyIndicator.IsBusy = true;
            message = await QueryLoadLeads();

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

            if (leadsList.Count() > 0)
            {
                dcLeadsList.ItemsSource = leadsList.Where(c => c.CompanyAddress.ToLower().Contains(txtSearch.Text.ToLower())
                    || c.CompanyName.ToLower().Contains(txtSearch.Text.ToLower()) || c.SalesStageStatus.ToLower().Contains(txtSearch.Text.ToLower())
                    || c.TerritoryName.ToLower().Contains(txtSearch.Text.ToLower())).OrderBy(c => c.LeadId).ToList();

                viewLead.BestFitColumns();
                viewContact.BestFitColumns();
            }
            else
            {
                var windows = new NoticeWindow();
                NoticeWindow.message = "List has no leads.";
                windows.Height = 0;
                windows.Top = screenTopEdge + 8;
                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                windows.ShowDialog();
            }
            busyIndicator.IsBusy = false;
        }

        private void LoadLeads()
        {
            RefreshTables("");
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            LoadLeads();

            canvasLeadMasterData.Width = GetCanvasMinWidth(canvasLeadMasterData);
            canvasLeadMasterData.Height = GetCanvasMinHeight(canvasLeadMasterData);
            canvasLeadMasterData.Visibility = Visibility.Collapsed;
            canvasLeadMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasLeadMasterData);

            canvasPrint.Width = GetCanvasMinWidth(canvasPrint);
            canvasPrint.Height = GetCanvasMinHeight(canvasPrint);
            canvasPrint.Visibility = Visibility.Collapsed;
            canvasPrint.Opacity = 0;

            canvasAdd.Width = GetCanvasMinWidth(canvasAdd);
            canvasAdd.Height = GetCanvasMinHeight(canvasAdd);
            canvasAdd.Visibility = Visibility.Collapsed;
            canvasAdd.Opacity = 0;

            canvasEdit.Width = GetCanvasMinWidth(canvasEdit);
            canvasEdit.Height = GetCanvasMinHeight(canvasEdit);
            canvasEdit.Visibility = Visibility.Collapsed;
            canvasEdit.Opacity = 0;

            canvasDelete.Width = GetCanvasMinWidth(canvasDelete);
            canvasDelete.Height = GetCanvasMinHeight(canvasDelete);
            canvasDelete.Visibility = Visibility.Collapsed;
            canvasDelete.Opacity = 0;
            busyIndicator.IsBusy = false;
        }

        //private void LoadMethod(string text)
        //{
        //    busyIndicator.IsBusy = true;
        //    using (var context = new DatabaseContext())
        //    {
        //        var lead = context.Leads.ToList();

        //        if (lead != null)
        //        {
        //            leadsList.Clear();
        //            foreach (var item in lead)
        //            {
        //                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == item.TerritoryID);

        //                leadsList.Add(new LeadsView
        //                {
        //                    CompanyAddress = item.CompanyAddress,
        //                    CompanyName = item.CompanyName,
        //                    LeadId = item.LeadID,
        //                    SalesStageStatus = item.Status,
        //                    TerritoryName = territory.TerritoryName
        //                });
        //            }
        //            dcLeadsList.ItemsSource = leadsList.Where(c => c.CompanyAddress.ToLower().Contains(text.ToLower())
        //                || c.CompanyName.ToLower().Contains(text.ToLower()) || c.SalesStageStatus.ToLower().Contains(text.ToLower())
        //                || c.TerritoryName.ToLower().Contains(text.ToLower())).OrderBy(c => c.LeadId).ToList();

        //            viewLead.BestFitColumns();
        //        }

        //        viewContact.BestFitColumns();
        //    }
        //    busyIndicator.IsBusy = false;
        //}

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadLeads();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedLead = dcLeadsList.SelectedItem as LeadsView;

            Storyboard sb;

            if (selectedLead != null)
            {
                if (_isExpanded != true)
                {
                    sb = this.FindResource("gridin") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;

                    LeadDetails.LeadId = selectedLead.LeadId;

                    var page = new LeadDetails();
                    frame.Navigate(page);
                }
                else
                {
                    sb = this.FindResource("gridout") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;

                    frame.BackNavigationMode = BackNavigationMode.Root;
                    frame.GoBack();

                    LoadLeads();
                }
            }
            else
            {
                NullMessage();
            }
        }

        private void btnLeadActivities_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dcLeadsList.SelectedItem as LeadsView;

            if (selectedRow != null)
            {
                LeadActivityMasterData.LeadId = selectedRow.LeadId;

                var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                LeadActivityMasterData page = new LeadActivityMasterData();
                frame.Navigate(page);
                FoldInnerCanvasSideward(canvasLeadMasterData);
            }
            else
            {
                NullMessage();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasLeadMasterData);
            FoldInnerCanvasSideward(canvasAdd);
        }

        private void btnAddLead_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            LeadForm.LeadId = 0;

            var navigate = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frame);
            var page = new LeadForm();
            page.Height = frame.Height;
            page.Width = frame.Width;
            navigate.Navigate(page);

            busyIndicator.IsBusy = false;
        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var selectedLead = dcLeadsList.SelectedItem as LeadsView;

            ContactPersonForm.ContactId = 0;
            ContactPersonForm.LeadId = selectedLead.LeadId;

            var navigate = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frame);
            var page = new ContactPersonForm();
            navigate.Navigate(page);

            busyIndicator.IsBusy = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasLeadMasterData);
            FoldInnerCanvasSideward(canvasEdit);
        }

        private void btEditLead_Click(object sender, RoutedEventArgs e)
        {
            var selectedLead = dcLeadsList.SelectedItem as LeadsView;

            using (var context = new DatabaseContext())
            {
                if (selectedLead != null)
                {
                    Storyboard sb;
                    if (_isExpanded != true)
                    {
                        sb = this.FindResource("gridin") as Storyboard;
                        sb.Begin(this);
                        _isExpanded = !_isExpanded;
                    }

                    LeadForm.LeadId = selectedLead.LeadId;

                    var navigate = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frame);
                    var page = new LeadForm();
                    navigate.Navigate(page);
                }
                else
                {
                    busyIndicator.IsBusy = true;
                    NullMessage();
                }
            }

            busyIndicator.IsBusy = false;
        }

        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {
            var selectedContact = dcContactsList.SelectedItem as ContactView;

            using (var context = new DatabaseContext())
            {
                if (selectedContact != null)
                {
                    Storyboard sb;
                    if (_isExpanded != true)
                    {
                        sb = this.FindResource("gridin") as Storyboard;
                        sb.Begin(this);
                        _isExpanded = !_isExpanded;
                    }

                    ContactPersonForm.ContactId = selectedContact.ContactId;

                    var navigate = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frame);
                    var page = new ContactPersonForm();
                    navigate.Navigate(page);
                }
                else
                {
                    busyIndicator.IsBusy = true;
                    NullMessage();
                }
            }
            busyIndicator.IsBusy = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasLeadMasterData);
            FoldInnerCanvasSideward(canvasDelete);
        }

        private void btnDeleteLead_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedLead = dcLeadsList.SelectedItem as LeadsView;

                if (selectedLead != null)
                {
                    var lead = context.Leads.FirstOrDefault(c => c.LeadID == selectedLead.LeadId);

                    if (lead != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to delete this lead?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        if (Variables.yesClicked == true)
                        {
                            var activity = context.LeadActivities.Where(c => ((c.ActivityDate == null || c.ActivityDate == "")
                                || (c.ActivityTime == null || c.ActivityTime == "")) && (c.IsFinalized == false)).FirstOrDefault(c => c.LeadID == lead.LeadID);

                            if (activity != null)
                            {
                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Lead still has an existing activity";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = lead.CompanyName
                                    + " is not deleted due to existing activity(ies).";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);
                                context.SaveChanges();
                            }
                            else
                            {
                                busyIndicator.IsBusy = true;
                                context.Leads.Remove(lead);

                                var contacts = context.Contacts.ToList();

                                foreach (var contact in contacts)
                                {
                                    if (contact.LeadId == lead.LeadID)
                                    {
                                        context.Contacts.Remove(contact);
                                    }
                                }

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " deleted " +
                                    lead.CompanyName + ". Note: All contacts of " +
                                    lead.CompanyName + " is deleted as well.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Lead successfully deleted";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                context.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    busyIndicator.IsBusy = true;
                    NullMessage();
                }
                LoadLeads();
                busyIndicator.IsBusy = false;
            }
        }

        private void btnDeleteContact_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedContact = dcContactsList.SelectedItem as ContactView;

                if (selectedContact != null)
                {
                    var contact = context.Contacts.FirstOrDefault(c => c.ContactID == selectedContact.ContactId);

                    if (contact != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to delete this contact person?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        if (Variables.yesClicked == true)
                        {
                            var activity = context.LeadActivities.Where(c => ((c.ActivityDate == null || c.ActivityDate == "")
                                || (c.ActivityTime == null || c.ActivityTime == "")) && (c.IsFinalized == false)).FirstOrDefault(c => c.LeadID == contact.LeadId);

                            if (activity != null)
                            {
                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Contact person still has an unaccomplished activity.";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = contact.ContactPersonName
                                    + " is not deleted due to unaccomplished activity(ies).";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);
                                context.SaveChanges();
                            }
                            else
                            {
                                busyIndicator.IsBusy = true;
                                context.Contacts.Remove(contact);

                                var lead = context.Leads.FirstOrDefault(c => c.LeadID == contact.LeadId);
                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " deleted " 
                                    + contact.ContactPersonName + " from " + lead.CompanyName 
                                    + " contacts.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Contact person successfully deleted";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                context.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    busyIndicator.IsBusy = true;
                    NullMessage();
                }
                LoadLeads();
                busyIndicator.IsBusy = false;
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasLeadMasterData);
            FoldInnerCanvasSideward(canvasPrint);
        }

        private void btnPrintLead_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                int itemNo = 0;
                if (leadsList.Count() > 0)
                {
                    List<LeadsReportData> dataList = new List<LeadsReportData>();
                    List<LeadsReportDetail> detailsList = new List<LeadsReportDetail>();
                    foreach (var lead in leadsList)
                    {
                        itemNo++;
                        var detail = new LeadsReportDetail();
                        detail.Address = lead.CompanyAddress;
                        detail.CompanyName = lead.CompanyName;
                        detail.LeadNo = itemNo;
                        detail.SalesStageStatus = lead.SalesStageStatus;
                        detail.Territory = lead.TerritoryName;
                        detailsList.Add(detail);
                    }
                    dataList.Add(new LeadsReportData()
                    {
                        ReportHeader = "LEADS",
                        ReportTitle = "LEADS as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalLeads = detailsList.Count(),
                        details = detailsList
                    });

                    var report = new LeadsReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "LEADS as of "
                            + DateTime.Now.ToString("MMMM dd, yyyy")
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

        private void btnPrintContact_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                int itemNo = 0;
                if (contactsList.Count() > 0)
                {
                    List<ContactsReportData> dataList = new List<ContactsReportData>();
                    List<ContactsReportDetail> detailsList = new List<ContactsReportDetail>();
                    foreach (var contact in contactsList)
                    {
                        itemNo++;
                        var detail = new ContactsReportDetail();

                        detail.CompanyName = contact.Company;
                        detail.ContactNo = itemNo;
                        detail.ContactPerson = contact.ContactPersonName;
                        detail.PhoneNo = contact.PhoneNo;
                        detail.Position = contact.Position;
                        detailsList.Add(detail);
                    }
                    dataList.Add(new ContactsReportData()
                    {
                        ReportHeader = "CONTACT PERSONS",
                        ReportTitle = "CONTACT PERSONS as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalContacts = detailsList.Count(),
                        details = detailsList
                    });

                    var report = new ContactsReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "CONTACT PERSONS as of "
                            + DateTime.Now.ToString("MMMM dd, yyyy")
                    };

                    using (ReportPrintTool printTool = new ReportPrintTool(report))
                    {
                        printTool.ShowRibbonPreviewDialog();
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "No details to print.";
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
            busyIndicator.IsBusy = true;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
            busyIndicator.IsBusy = false;
        }

        private void dcLeadsList_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedRow = dcLeadsList.SelectedItem as LeadsView;

                if (selectedRow != null)
                {
                    var lead = context.Leads.FirstOrDefault(c => c.LeadID == selectedRow.LeadId);

                    lblLeadContacts.Text = lead.CompanyName;

                    if (dcLeadsList.SelectedItem != null)
                    {
                        var allContacts = context.Contacts.ToList();

                        if (allContacts.Count > 0)
                        {
                            contactsList.Clear();

                            foreach (var item in allContacts)
                            {
                                lead = context.Leads.FirstOrDefault(c => c.LeadID == item.LeadId);

                                contactsList.Add(new ContactView
                                {
                                    Company = lead.CompanyName,
                                    ContactId = item.ContactID,
                                    ContactPersonName = item.ContactPersonName,
                                    PhoneNo = item.PhoneNo,
                                    Position = item.Position
                                });
                            }

                            var leads = context.Leads.FirstOrDefault(c => c.LeadID == selectedRow.LeadId);

                            dcContactsList.ItemsSource = contactsList.Where
                                (c => c.Company.ToLower() == leads.CompanyName.ToLower());

                            viewContact.BestFitColumns();
                        }

                        var allLeadsProducts = context.LeadsProducts.ToList();

                        if (allLeadsProducts.Count > 0)
                        {
                            leadsProductsList.Clear();

                            foreach (var leadsProduct in allLeadsProducts)
                            {
                                var leadProduct = context.LeadsProducts.FirstOrDefault(c => c.ListId == leadsProduct.ListId);
                                lead = context.Leads.FirstOrDefault(c => c.LeadID == leadProduct.LeadId);
                                var product = context.Products.FirstOrDefault(c => c.ProductID == leadProduct.ProductId);

                                leadsProductsList.Add(new ProductsByLeadsView
                                {
                                    Lead = lead.CompanyName,
                                    ListId = leadProduct.ListId,
                                    Product = product.ProductName
                                });
                            }

                            var selectedLead = context.Leads.FirstOrDefault(c => c.LeadID == selectedRow.LeadId);

                            dcProductsList.ItemsSource = leadsProductsList.Where(c => c.Lead.ToLower() == selectedLead.CompanyName.ToLower());

                            viewProducts.BestFitColumns();
                        }
                    }
                }
            }
        }

        private void btnBackToLeadMenu_Click(object sender, RoutedEventArgs e)
        {
            if (canvasPrint.Visibility == Visibility.Visible) { FoldInnerCanvasSideward(canvasPrint); }
            if (canvasAdd.Visibility == Visibility.Visible) { FoldInnerCanvasSideward(canvasAdd); }
            if (canvasEdit.Visibility == Visibility.Visible) { FoldInnerCanvasSideward(canvasEdit); }
            if (canvasDelete.Visibility == Visibility.Visible) { FoldInnerCanvasSideward(canvasDelete); }
            FoldInnerCanvasSideward(canvasLeadMasterData);

            if (_isExpanded != false)
            {
                Storyboard sb;
                sb = this.FindResource("gridout") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                frame.BackNavigationMode = BackNavigationMode.Root;
                frame.GoBack();

                LoadLeads();
            }
        }

        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            using (var context = new DatabaseContext())
            {
                var contact = context.Contacts.ToList();
                var selectedLead = dcLeadsList.SelectedItem as LeadsView;

                if (contact != null)
                {
                    contactsList.Clear();
                    foreach (var item in contact)
                    {
                        var lead = context.Leads.FirstOrDefault(c => c.LeadID == item.LeadId);

                        contactsList.Add(new ContactView
                        {
                            Company = lead.CompanyName,
                            ContactId = item.ContactID,
                            ContactPersonName = item.ContactPersonName,
                            PhoneNo = item.PhoneNo,
                            Position = item.Position
                        });
                    }
                    var leads = context.Leads.FirstOrDefault(c => c.LeadID == selectedLead.LeadId);

                    dcContactsList.ItemsSource = contactsList.Where(c => (c.ContactPersonName.ToLower().Contains(txtSearch2.Text.ToLower())
                        || c.Position.ToLower().Contains(txtSearch2.Text.ToLower()) || c.PhoneNo.Contains(txtSearch2.Text))
                        && (c.Company.ToLower() == leads.CompanyName.ToLower())).OrderBy(c => c.ContactId).ToList();

                    viewContact.BestFitColumns();
                }
            }
            busyIndicator.IsBusy = false;
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
