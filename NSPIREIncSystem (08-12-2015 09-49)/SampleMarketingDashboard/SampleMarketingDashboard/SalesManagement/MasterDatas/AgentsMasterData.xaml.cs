using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for AgentsMasterData.xaml
    /// </summary>
    public partial class AgentsMasterData : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<AgentsView> agentsList = new List<AgentsView>();
        public List<CustomerAccountsView> accountsList = new List<CustomerAccountsView>();
        bool _isExpanded = false;
        //public static bool isFinish = false;

        public AgentsMasterData()
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
        private Task<string> QueryLoadAgents()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    agentsList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        var agents = context.Agents.ToList();

                        if (agents.Count > 0)
                        {
                            foreach (var agent in agents)
                            {
                                agentsList.Add(new AgentsView()
                                {
                                    AgentId = agent.AgentId,
                                    AgentName = agent.AgentName,
                                    ContactNo = agent.ContactNo,
                                    IsEmployee = agent.IsEmployee,
                                    Position = agent.Position,
                                    Territory = agent.Territory
                                });
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
                message = await QueryLoadAgents();

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


                if (agentsList.Count() > 0)
                {
                    dcAgentsList.ItemsSource = agentsList.Where
                        (c => c.AgentName.ToLower().Contains(str.ToLower())
                        || c.ContactNo.ToLower().Contains(str.ToLower())
                        || c.Position.ToLower().Contains(str.ToLower())
                        || c.Territory.ToLower().Contains(str.ToLower())).ToList();
                }
                else
                {
                    dcAgentsList.ItemsSource = null;
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "List has no agents.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                viewAgents.BestFitColumns();
                busyIndicator.IsBusy = false;
            }
        }

        private void LoadAgents()
         {
             RefreshTable(txtSearch.Text);
         }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAgents();
            canvasAgentsMasterData.Width = GetCanvasMinWidth(canvasAgentsMasterData);
            canvasAgentsMasterData.Height = GetCanvasMinHeight(canvasAgentsMasterData);
            canvasAgentsMasterData.Visibility = Visibility.Collapsed;
            canvasAgentsMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasAgentsMasterData);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadAgents();
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedAgent = dcAgentsList.SelectedItem as AgentsView;

                if (selectedAgent != null)
                {
                    var agent = context.Agents.FirstOrDefault(c => c.AgentId == selectedAgent.AgentId);

                    if (agent != null)
                    {
                        var accounts = context.CustomerAccounts.ToList();

                        if (accounts != null)
                        {
                            accountsList.Clear();
                            foreach (var account in accounts)
                            {
                                var customer = context.Customers.FirstOrDefault(c => c.CustomerID == account.CustomerID);
                                var product = context.Products.FirstOrDefault(c => c.ProductID == account.ProductID);
                                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == account.TerritoryID);

                                if (account.AgentId == agent.AgentId)
                                {
                                    accountsList.Add(new CustomerAccountsView
                                    {
                                        AccountNumber = account.AccountNumber,
                                        Agent = agent.AgentName,
                                        Customer = customer.CompanyName,
                                        Discount = account.Discount,
                                        Gross = account.Gross,
                                        ModeOfPayment = account.ModeOfPayment,
                                        NetValue = account.NetValue,
                                        Product = product.ProductName,
                                        ServiceCharge = account.ServiceCharge,
                                        Territory = territory.TerritoryName
                                    });
                                }
                            }

                            dcCustomerAccountsList.ItemsSource = accountsList.Where
                                (c => c.Agent.ToLower().Contains(txtSearchAccount.Text.ToLower()) ||
                                c.Customer.ToLower().Contains(txtSearchAccount.Text.ToLower()) ||
                                c.Product.ToLower().Contains(txtSearchAccount.Text.ToLower()) ||
                                c.Territory.ToLower().Contains(txtSearchAccount.Text.ToLower()));
                            viewCustomerAccounts.BestFitColumns();
                        }
                    }
                }
            }
        }
        
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedAgent = dcAgentsList.SelectedItem as AgentsView;

            Storyboard sb;
            if (_isExpanded != true && selectedAgent != null)
            {
                AgentsDetails.AgentId = selectedAgent.AgentId;

                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                var page = new AgentsDetails();
                navigation.Navigate(page);
            }
            else
            {
                sb = this.FindResource("gridout") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                navigation.BackNavigationMode = BackNavigationMode.Root;
                navigation.GoBack();

                LoadAgents();
            }
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;
            if (_isExpanded != true)
            {
                AgentsForm.AgentId = 0;

                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                var page = new AgentsForm();
                navigation.Navigate(page);
            }
            else
            {
                sb = this.FindResource("gridout") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;

                LoadAgents();
            }
        }
        
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedAccount = dcAgentsList.SelectedItem as AgentsView;

            Storyboard sb;
            if (selectedAccount != null)
            {
                if (_isExpanded != true)
                {
                    AgentsForm.AgentId = selectedAccount.AgentId;

                    var page = new AgentsForm();
                    navigation.Navigate(page);

                    sb = this.FindResource("gridin") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;
                }
                else
                {
                    sb = this.FindResource("gridout") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;

                    navigation.BackNavigationMode = BackNavigationMode.Root;
                    navigation.GoBack();
                }
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
                var selectedAgents = dcAgentsList.SelectedItem as AgentsView;

                if (selectedAgents != null)
                {
                    var agent = context.Agents.FirstOrDefault(c => c.AgentId == selectedAgents.AgentId);

                    if (agent != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to permanently delete this record?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        var account = context.CustomerAccounts.FirstOrDefault(c => c.AgentId == agent.AgentId);

                        if (account == null)
                        {
                            if (Variables.yesClicked == true)
                            {
                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " deleted Agent " +
                                    agent.AgentName + ".";
                                context.Logs.Add(log);

                                context.Agents.Remove(agent);
                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Agent successfully deleted";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        NullMessage();
                    }
                }
                LoadAgents();
            }
        }
        
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
            //    var accounts = context.CustomerAccounts.ToList();
            //    int accountNo = 0;
            //    if (accounts.Count() > 0)
            //    {
            //        List<CustomerAccountsReportData> dataList = new List<CustomerAccountsReportData>();
            //        List<CustomerAccountsReportDetail> detailsList = new List<CustomerAccountsReportDetail>();
            //        foreach (var account in accounts)
            //        {
            //            accountNo++;
            //            var detail = new CustomerAccountsReportDetail();
            //            var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == account.TerritoryID);
            //            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == account.CustomerID);
            //            var product = context.Products.FirstOrDefault(c => c.ProductID == account.ProductID);

            //            detail.AccountNumber = Convert.ToString(accountNo);
            //            detail.Customer = customer.CompanyName;
            //            detail.Discount = account.Discount;
            //            detail.Gross = account.Gross;
            //            detail.ModeOfPayment = account.ModeOfPayment;
            //            detail.NetValue = account.NetValue;
            //            detail.Product = product.ProductName;
            //            detail.ServiceCharge = account.ServiceCharge;
            //            detail.Territory = territory.TerritoryName;
            //            detailsList.Add(detail);
            //        }
            //        dataList.Add(new CustomerAccountsReportData()
            //        {
            //            ReportHeader = "CUSTOMER ACCOUNTS",
            //            ReportTitle = "CUSTOMER ACCOUNTS as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
            //            TotalCustomerAccounts = detailsList.Count(),
            //            details = detailsList
            //        });

            //        var report = new CustomerAccountsReportDesign
            //        {
            //            DataSource = dataList.Distinct(),
            //            Name = "CUSTOMER ACCOUNTS as of "
            //                + DateTime.Now.ToString("MMMM dd, yyyy")
            //        };

            //        using (ReportPrintTool printTool = new ReportPrintTool(report))
            //        {
            //            printTool.ShowRibbonPreviewDialog();
            //        }
            //    }
            //    else
            //    {
            //        var windows = new Shared.Windows.NoticeWindow();
            //        Shared.Windows.NoticeWindow.message = "No data to print.";
            //        windows.Height = 0;
            //        windows.Top = screenTopEdge + 8;
            //        windows.Left = (screenWidth / 2) - (windows.Width / 2);
            //        if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            //        windows.ShowDialog();
            //    }
            }
        }

        private void dcAgentsList_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedAgent = dcAgentsList.SelectedItem as AgentsView;

                if (selectedAgent != null)
                {
                    var agent = context.Agents.FirstOrDefault(c => c.AgentId == selectedAgent.AgentId);

                    if (agent != null)
                    {
                        lblAgentName.Text = agent.AgentName + "'s Handled Accounts";

                        var accounts = context.CustomerAccounts.ToList();

                        if (accounts != null)
                        {
                            accountsList.Clear();
                            foreach (var account in accounts)
                            {
                                var customer = context.Customers.FirstOrDefault(c => c.CustomerID == account.CustomerID);
                                var product = context.Products.FirstOrDefault(c => c.ProductID == account.ProductID);
                                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == account.TerritoryID);

                                if (account.AgentId == agent.AgentId)
                                {
                                    accountsList.Add(new CustomerAccountsView
                                    {
                                        AccountNumber = account.AccountNumber,
                                        Agent = agent.AgentName,
                                        Customer = customer.CompanyName,
                                        Discount = account.Discount,
                                        Gross = account.Gross,
                                        ModeOfPayment = account.ModeOfPayment,
                                        NetValue = account.NetValue,
                                        Product = product.ProductName,
                                        ServiceCharge = account.ServiceCharge,
                                        Territory = territory.TerritoryName
                                    });
                                }
                            }

                            dcCustomerAccountsList.ItemsSource = accountsList.ToList();
                            viewCustomerAccounts.BestFitColumns();
                        }
                    }
                }
            }
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
            FoldInnerCanvasSideward(canvasAgentsMasterData);
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
