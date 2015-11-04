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
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for ProductsMasterData.xaml
    /// </summary>
    public partial class ProductsMasterData : UserControl
    {
        public List<LeadsView> leadsList = new List<LeadsView>();
        public List<ProductView> productsList = new List<ProductView>();
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        bool _isExpanded = false;

        public ProductsMasterData()
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

        #region Load Lead Details
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
                                var strategy = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == item.MarketingStrategyId);

                                leadsList.Add(new LeadsView
                                {
                                    CompanyAddress = item.CompanyAddress,
                                    CompanyName = item.CompanyName,
                                    LeadId = item.LeadID,
                                    SalesStageStatus = item.Status,
                                    TerritoryName = territory.TerritoryName,
                                    DateAdded = item.DateAdded,
                                    IsActive = item.IsActive,
                                    MarketingStrategy = strategy.Description
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

        private async void RefreshLeadTables(string str)
        {
            try
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
                    using (var context = new DatabaseContext())
                    {
                        var selectedProduct = dcProductsList.SelectedItem as ProductView;
                        var product = context.Products.FirstOrDefault(c => c.ProductID == selectedProduct.ProductID);

                        dcLeadsList.ItemsSource = leadsList.Where
                            (c => c.CompanyName.ToLower().Contains(str.ToLower())).
                            OrderBy(c => c.LeadId).ToList();

                        viewLead.BestFitColumns();
                    }
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
            catch (Exception ex)
            {
                var windows = new NoticeWindow();
                NoticeWindow.message = "Error : " + ex.InnerException;
                windows.Height = 0;
                windows.Top = screenTopEdge + 8;
                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                windows.ShowDialog();
            }
        }

        private void LoadLeads()
        {
            RefreshLeadTables(txtSearch2.Text);
        }
        #endregion

        #region Load Product Details
        private Task<string> QueryLoadProducts()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    productsList.Clear();
                    using (var context = new DatabaseContext())
                    {
                        var products = context.Products.ToList();

                        if (products != null)
                        {
                            foreach (var product in products)
                            {
                                var category = context.ProductCategories.FirstOrDefault
                                    (c => c.CategoryID == product.CategoryID);

                                if (category != null)
                                {
                                    productsList.Add(new ProductView
                                    {
                                        Category = category.CategoryName,
                                        Cost = product.Cost,
                                        ProductID = product.ProductID,
                                        ProductName = product.ProductName
                                    });
                                }
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

        private async void RefreshProductTables(string str)
        {
            try
            {
                string message = "";
                busyIndicator.IsBusy = true;
                message = await QueryLoadProducts();

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

                if (productsList.Count() > 0)
                {
                    dcProductsList.ItemsSource = productsList.Where
                        (c => c.ProductName.ToLower().Contains(str.ToLower()) && (c.ProductName != "" && c.ProductName != null)).
                        OrderBy(c => c.ProductID).ToList();

                    viewProducts.BestFitColumns();
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "List has no products.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                busyIndicator.IsBusy = false;
            }
            catch (Exception ex)
            {
                var windows = new NoticeWindow();
                NoticeWindow.message = "Error : " + ex.InnerException;
                windows.Height = 0;
                windows.Top = screenTopEdge + 8;
                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                windows.ShowDialog();
            }
        }

        private void LoadProducts()
        {
            RefreshProductTables(txtSearch.Text);
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            LoadProducts();

            canvasProductMasterData.Width = GetCanvasMinWidth(canvasProductMasterData);
            canvasProductMasterData.Height = GetCanvasMinHeight(canvasProductMasterData);
            canvasProductMasterData.Visibility = Visibility.Collapsed;
            canvasProductMasterData.Opacity = 0;
            FoldInnerCanvasSideward(canvasProductMasterData);
            busyIndicator.IsBusy = false;
        }

        private void dcProductsList_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    var selectedProduct = dcProductsList.SelectedItem as ProductView;
                    var leads = context.Leads.ToList();

                    lblLeads.Text = selectedProduct.ProductName;

                    leadsList.Clear();
                    if (selectedProduct != null && leads != null)
                    {
                        foreach (var lead in leads)
                        {
                            var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);
                            var strategy = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == lead.MarketingStrategyId);

                            if (territory != null && strategy != null)
                            {
                                leadsList.Add(new LeadsView
                                {
                                    LeadId = lead.LeadID,
                                    CompanyAddress = lead.CompanyAddress,
                                    CompanyName = lead.CompanyName,
                                    SalesStageStatus = lead.Status,
                                    TerritoryName = territory.TerritoryName,
                                    DateAdded = lead.DateAdded,
                                    IsActive = lead.IsActive,
                                    MarketingStrategy = strategy.Description
                                });
                            }
                            else
                            {
                                leadsList.Add(new LeadsView
                                {
                                    LeadId = lead.LeadID,
                                    CompanyAddress = lead.CompanyAddress,
                                    CompanyName = lead.CompanyName,
                                    SalesStageStatus = lead.Status,
                                    TerritoryName = territory.TerritoryName,
                                    DateAdded = lead.DateAdded,
                                    IsActive = lead.IsActive,
                                    MarketingStrategy = strategy.Description
                                });
                            }
                        }
                    }

                    dcLeadsList.ItemsSource = leadsList.ToList();
                    viewLead.BestFitColumns();
                }
                catch (Exception ex)
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Error : " + ex.Message;
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            LoadLeads();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = dcProductsList.SelectedItem as ProductView;

            Storyboard sb;
            if (selectedProduct != null)
            {
                if (selectedProduct.ProductID != ProductsView.ProductId)
                {
                    if (_isExpanded != true)
                    {
                        sb = this.FindResource("gridin") as Storyboard;
                        sb.Begin(this);
                        _isExpanded = !_isExpanded;
                    }

                    ProductsView.ProductId = selectedProduct.ProductID;
                    ProductsView.isView = true;

                    var page = new ProductsView();
                    frame.Navigate(page);
                }
                else
                {
                    sb = this.FindResource("gridout") as Storyboard;
                    sb.Begin(this);
                    _isExpanded = !_isExpanded;

                    frame.BackNavigationMode = BackNavigationMode.Root;
                    frame.GoBack();
                    ProductsView.isView = false;

                    LoadProducts();
                }
            }
            else
            {
                NullMessage();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductsView.ProductId = 0;
            ProductsView.isView = false;

            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new ProductsView();
            frame.Navigate(page);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = dcProductsList.SelectedItem as ProductView;

            using (var context = new DatabaseContext())
            {
                if (selectedProduct != null)
                {
                    Storyboard sb;
                    if (_isExpanded != true)
                    {
                        sb = this.FindResource("gridin") as Storyboard;
                        sb.Begin(this);
                        _isExpanded = !_isExpanded;
                    }

                    ProductsView.ProductId = selectedProduct.ProductID;
                    ProductsView.isView = false;

                    var page = new ProductsView();
                    frame.Navigate(page);
                }
                else
                {
                    NullMessage();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = dcProductsList.SelectedItem as ProductView;

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                int productNo = 0;
                if (productsList.Count() > 0)
                {
                    List<ProductsReportData> dataList = new List<ProductsReportData>();
                    List<ProductsReportDetails> detailsList = new List<ProductsReportDetails>();
                    List<LeadsPerProducts> leadsList = new List<LeadsPerProducts>();
                    var leads = context.Leads.ToList();

                    foreach (var product in productsList)
                    {
                        if (product != null)
                        {
                            productNo++;
                            var detail = new ProductsReportDetails();

                            detail.Category = product.Category;
                            detail.Cost = product.Cost;
                            detail.ProductId = productNo;
                            detail.ProductName = product.ProductName;
                            detailsList.Add(detail);
                        }

                        if (leads != null)
                        {
                            foreach (var lead in leads)
                            {
                                var toReport = new LeadsPerProducts();

                                toReport.LeadName = lead.CompanyName;
                                leadsList.Add(toReport);
                            }
                        }
                    }

                    dataList.Add(new ProductsReportData()
                    {
                        ReportHeader = "PRODUCTS",
                        ReportTitle = "PRODUCTS OF as of " + DateTime.Now.ToString("MMMM dd, yyyy"),
                        TotalProducts = detailsList.Count(),
                        details = detailsList,
                        leads = leadsList
                    });

                    var report = new ProductsReportDesign
                    {
                        DataSource = dataList.Distinct(),
                        Name = "LEAD ACTIVITIES OF as of " + DateTime.Now.ToString("MMMM dd, yyyy")
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
            var navigate = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            navigate.BackNavigationMode = BackNavigationMode.PreviousScreen;
            navigate.GoBack();
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
