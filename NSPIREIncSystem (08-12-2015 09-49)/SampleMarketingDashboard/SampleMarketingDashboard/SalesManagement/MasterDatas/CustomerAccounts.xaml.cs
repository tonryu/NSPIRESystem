using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.SalesManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using NSPIREIncSystem.SalesManagement.Reports;
using DevExpress.XtraReports.UI;

namespace NSPIREIncSystem.SalesManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for CustomerAccounts.xaml
    /// </summary>
    public partial class CustomerAccounts : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<CustomerAccountsView> customerAccountsList = new List<CustomerAccountsView>();
        bool _isExpanded = false;

        public CustomerAccounts()
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
        private Task<string> QueryLoadSalesStages()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    customerAccountsList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        var customerAccounts = context.CustomerAccounts.ToList();

                        foreach (var item in customerAccounts)
                        {
                            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == item.CustomerID);
                            var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == item.TerritoryID);
                            var product = context.Products.FirstOrDefault(c => c.ProductID == item.ProductID);

                            if (customer != null)
                            {
                                if (customer.LeadID != 0)
                                {
                                    var lead = context.Leads.FirstOrDefault(c => c.LeadID == customer.LeadID);

                                    if (lead != null && territory != null && product != null)
                                    {
                                        customerAccountsList.Add(new CustomerAccountsView()
                                        {
                                            AccountNumber = item.AccountNumber,
                                            Customer = lead.CompanyName,
                                            Discount = item.Discount,
                                            Gross = item.Gross,
                                            ModeOfPayment = item.ModeOfPayment,
                                            NetValue = item.NetValue,
                                            Product = product.ProductName,
                                            ServiceCharge = item.ServiceCharge,
                                            Territory = territory.TerritoryName
                                        });
                                    }
                                }
                                else
                                {
                                    if (territory != null && product != null)
                                    {
                                        customerAccountsList.Add(new CustomerAccountsView()
                                        {
                                            AccountNumber = item.AccountNumber,
                                            Customer = customer.CompanyName,
                                            Discount = item.Discount,
                                            Gross = item.Gross,
                                            ModeOfPayment = item.ModeOfPayment,
                                            NetValue = item.NetValue,
                                            Product = product.ProductName,
                                            ServiceCharge = item.ServiceCharge,
                                            Territory = territory.TerritoryName
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


                if (customerAccountsList.Count() > 0)
                {
                    dcCustomerAccountsList.ItemsSource = customerAccountsList.Where
                        (c => c.AccountNumber.ToLower().Contains(str.ToLower())
                        || c.Customer.ToLower().Contains(str.ToLower())
                        || c.Product.ToLower().Contains(str.ToLower())
                        || c.Territory.ToLower().Contains(str.ToLower())).ToList();
                }
                else
                {
                    dcCustomerAccountsList.ItemsSource = null;

                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "No customer accounts";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                viewCustomerAccounts.BestFitColumns();


                if (customerAccountsList.Count == 0)
                {
                    var windows = new Shared.Windows.NoticeWindow();
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

        private void LoadActivity()
         {
             RefreshTable(txtSearch.Text);
         }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadActivity();
            canvasCustomerAccountsMasterData.Width = GetCanvasMinWidth(canvasCustomerAccountsMasterData);
            canvasCustomerAccountsMasterData.Height = GetCanvasMinHeight(canvasCustomerAccountsMasterData);
            canvasCustomerAccountsMasterData.Visibility = Visibility.Collapsed;
            canvasCustomerAccountsMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasCustomerAccountsMasterData);
        }
        
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = dcCustomerAccountsList.SelectedItem as CustomerAccountsView;

            Storyboard sb;
            if (_isExpanded != true && selectedAccount != null)
            {
                CustomerAccountDetails.AccountNumber = selectedAccount.AccountNumber;

                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                var page = new CustomerAccountDetails();
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
            CustomerAccountsForm.AccountNumber = null;
              
            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new CustomerAccountsForm();
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

            var selectedAccount = dcCustomerAccountsList.SelectedItem as CustomerAccountsView;

            if (selectedAccount != null)
            {
                CustomerAccountsForm.AccountNumber = selectedAccount.AccountNumber;

                var page = new CustomerAccountsForm();
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
                var selectedAccount = dcCustomerAccountsList.SelectedItem as CustomerAccountsView;

                if (selectedAccount != null)
                {
                    var account = context.CustomerAccounts.First(c => c.AccountNumber == selectedAccount.AccountNumber);

                    if (account != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to delete this record?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        if (Variables.yesClicked == true)
                        {
                            context.CustomerAccounts.Remove(account);
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Customer Account successfully deleted";
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
                LoadActivity();
            }
        }
        
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var accounts = context.CustomerAccounts.ToList();
                int accountNo = 0;
                if (accounts.Count() > 0)
                {
                    List<CustomerAccountsReportData> dataList = new List<CustomerAccountsReportData>();
                    List<CustomerAccountsReportDetail> detailsList = new List<CustomerAccountsReportDetail>();
                    foreach (var account in accounts)
                    {
                        accountNo++;
                        var detail = new CustomerAccountsReportDetail();
                        var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == account.TerritoryID);
                        var customer = context.Customers.FirstOrDefault(c => c.CustomerID == account.CustomerID);
                        var product = context.Products.FirstOrDefault(c => c.ProductID == account.ProductID);

                        detail.AccountNumber = Convert.ToString(accountNo);
                        detail.Customer = customer.CompanyName;
                        detail.Discount = account.Discount;
                        detail.Gross = account.Gross;
                        detail.ModeOfPayment = account.ModeOfPayment;
                        detail.NetValue = account.NetValue;
                        detail.Product = product.ProductName;
                        detail.ServiceCharge = account.ServiceCharge;
                        detail.Territory = territory.TerritoryName;
                        detailsList.Add(detail);
                    }
                    dataList.Add(new CustomerAccountsReportData()
                    {
                        ReportHeader = "CUSTOMER ACCOUNTS",
                        ReportTitle = "CUSTOMER ACCOUNTS as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalCustomerAccounts = detailsList.Count(),
                        details = detailsList
                    });

                    var report = new CustomerAccountsReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "CUSTOMER ACCOUNTS as of "
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
            FoldInnerCanvasSideward(canvasCustomerAccountsMasterData);
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
        
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadActivity();
        }
    }
}
