using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using DevExpress.XtraReports.UI;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.LeadManagement.Reports;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for CustomersMasterData.xaml
    /// </summary>
    public partial class CustomersMasterData : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<CustomersView> customersList = new List<CustomersView>();
        bool _isExpanded = false;

        public CustomersMasterData()
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
        private Task<string> QueryLoadCustomers()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    customersList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        var customers = context.Customers.ToList();

                        foreach (var customer in customers)
                        {
                            var specificCustomer = context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);

                            if (specificCustomer != null)
                            {
                                var lead = context.Leads.FirstOrDefault(c => c.LeadID == specificCustomer.LeadID);

                                if (lead != null)
                                {
                                    customersList.Add(new CustomersView()
                                    {
                                        CompanyAddress = customer.CompanyAddress,
                                        CustomerID = customer.CustomerID,
                                        DateSigned = customer.DateSigned,
                                        Email = customer.Email,
                                        CompanyName = lead.CompanyName,
                                        PhoneNo = customer.PhoneNo,
                                        Website = customer.Website
                                    });
                                }
                                else
                                {
                                    customersList.Add(new CustomersView()
                                    {
                                        CompanyAddress = customer.CompanyAddress,
                                        CustomerID = customer.CustomerID,
                                        DateSigned = customer.DateSigned,
                                        Email = customer.Email,
                                        CompanyName = customer.CompanyName,
                                        PhoneNo = customer.PhoneNo,
                                        Website = customer.Website
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
                message = await QueryLoadCustomers();

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


                if (customersList.Count() > 0)
                {
                    dcCustomersList.ItemsSource = customersList.Where
                        (c => c.CompanyAddress.ToLower().Contains(str.ToLower())
                        || c.CompanyName.ToLower().Contains(str.ToLower())
                        || c.PhoneNo.ToLower().Contains(str.ToLower())
                        || c.Website.ToLower().Contains(str.ToLower())
                        || c.DateSigned.ToLower().Contains(str.ToLower()))
                        .OrderBy(c => c.CustomerID).ToList();
                }
                else
                {
                    dcCustomersList.ItemsSource = null;

                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "No customer accounts";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                viewCustomers.BestFitColumns();


                if (customersList.Count == 0)
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "List has no customer accounts.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                busyIndicator.IsBusy = false;
            }
        }

        private void LoadCustomers()
         {
             RefreshTable(txtSearch.Text);
         }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomers();
            canvasCustomersMasterData.Width = GetCanvasMinWidth(canvasCustomersMasterData);
            canvasCustomersMasterData.Height = GetCanvasMinHeight(canvasCustomersMasterData);
            canvasCustomersMasterData.Visibility = Visibility.Collapsed;
            canvasCustomersMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasCustomersMasterData);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomers();
        }
        
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = dcCustomersList.SelectedItem as CustomersView;

            Storyboard sb;
            if ((selectedCustomer != null) 
                && (selectedCustomer.CustomerID != CustomerDetails.CustomerId))
            {
                CustomerDetails.CustomerId = selectedCustomer.CustomerID;

                if (_isExpanded != true)
                {
                    sb = this.FindResource("gridin") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;
                }

                var page = new CustomerDetails();
                navigation.Navigate(page);
            }
            else
            {
                sb = this.FindResource("gridout") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                LoadCustomers();
            }
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomersForm.CustomerId = 0;
              
            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new CustomersForm();
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

            var selectedCustomer = dcCustomersList.SelectedItem as CustomersView;

            if (selectedCustomer != null)
            {
                CustomersForm.CustomerId = selectedCustomer.CustomerID;

                var page = new CustomersForm();
                navigation.Navigate(page);
            }
            else
            {
                NullMessage();
            }
        }
        
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context= new DatabaseContext())
            {
                var selectedCustomer = dcCustomersList.SelectedItem as CustomersView;

                if (selectedCustomer != null)
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == selectedCustomer.CustomerID);

                    if (customer != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to delete this record?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        if (Variables.yesClicked == true)
                        {
                            context.Customers.Remove(customer);
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Customer successfully deleted";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();

                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        NullMessage();
                    }
                }
                LoadCustomers();
            }
        }
        
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                int customerNo = 0;
                if (customersList.Count() > 0)
                {
                    List<CustomersReportData> dataList = new List<CustomersReportData>();
                    List<CustomersReportDetail> detailsList = new List<CustomersReportDetail>();
                    foreach (var customer in customersList.OrderBy(c => c.CustomerID))
                    {
                        customerNo++;
                        var detail = new CustomersReportDetail();
                        var lead = context.Leads.FirstOrDefault
                            (c => c.CompanyName.ToLower() == customer.CompanyName.ToLower());

                        detail.CompanyAddress = customer.CompanyAddress;
                        detail.CompanyName = customer.CompanyName;
                        detail.CustomerID = customerNo;
                        detail.DateSigned = customer.DateSigned;
                        detail.Email = customer.Email;
                        if (lead != null) { detail.FromLead = "FROM LEAD"; } else { detail.FromLead = ""; }
                        detail.PhoneNo = customer.PhoneNo;
                        detail.Website = customer.Website;
                        detailsList.Add(detail);
                    }
                    dataList.Add(new CustomersReportData()
                    {
                        ReportHeader = "CUSTOMERS",
                        ReportTitle = "CUSTOMERS as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalCustomers = detailsList.Count(),
                        details = detailsList
                    });

                    var report = new CustomersReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "CUSTOMERS as of "
                            + DateTime.Now.ToString("MMMM dd, yyyy")
                    };

                    using (ReportPrintTool printTool = new ReportPrintTool(report))
                    {
                        printTool.ShowRibbonPreviewDialog();
                    }
                }
                else
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "No data to print.";
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
            FoldInnerCanvasSideward(canvasCustomersMasterData);
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
