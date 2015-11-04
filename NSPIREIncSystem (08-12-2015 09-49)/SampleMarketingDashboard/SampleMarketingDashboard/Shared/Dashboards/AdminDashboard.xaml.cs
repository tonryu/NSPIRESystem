using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.CustomerServiceManagement.Dashboards;
using NSPIREIncSystem.LeadManagement.Dashboards;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Settings.MasterDatas;
using NSPIREIncSystem.Shared.Windows;
using NSPIREIncSystem.TaskManagement.Dashboards;

namespace NSPIREIncSystem.Shared.Dashboards
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<TextBlock> textBlockList { get; set; }

        public AdminDashboard()
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
        private Task<string> QueryLogs()
        {
            return Task.Factory.StartNew(() => 
            {
                try
                {
                    using (var context = new DatabaseContext())
                    {
                        var logs = context.Logs.ToList();

                        if (logs != null)
                        {
                            return null;
                        }
                        else
                        {
                            return "No logs";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "Error : " + ex.Message;
                }
            });
        }

        private async void RefreshLogs()
        {
            var message = await QueryLogs();

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
            else
            {
                using (var context = new DatabaseContext())
                {
                    var textBlock = new TextBlock();
                    var logs = context.Logs.OrderByDescending(c => c.LogId).ToList();
                    Thickness margin = textBlock.Margin;
                    var specificStackPanel = new StackPanel();
                    var wholeStackPanel = new StackPanel();
                    var separator = new Separator();
                    BrushConverter conveter = new BrushConverter();
                    Brush brush = conveter.ConvertFromString("#FF0069FF") as Brush;

                    textBlock.TextWrapping = TextWrapping.Wrap;
                    wholeStackPanel.Height = Double.NaN;
                    specificStackPanel.Height = Double.NaN;

                    sbLogs.Content = null;
                    wholeStackPanel.Children.Clear();
                    foreach (var log in logs)
                    {
                        //textblock for date and time
                        specificStackPanel = new StackPanel();
                        textBlock = new TextBlock();
                        textBlock.Text = Convert.ToDateTime(log.Date).ToString("MMMM d, yyyy") + " " +
                            Convert.ToDateTime(log.Time).ToString("hh:mm:ss tt");
                        margin.Top = 5;
                        margin.Bottom = 5;
                        margin.Left = 10;
                        margin.Right = 10;
                        textBlock.Margin = margin;
                        specificStackPanel.Children.Add(textBlock);

                        //textblock for log description
                        textBlock = new TextBlock();
                        textBlock.Text = log.Description;
                        margin.Top = 0;
                        margin.Bottom = 5;
                        margin.Left = 10;
                        margin.Right = 20;
                        textBlock.Margin = margin;
                        specificStackPanel.Children.Add(textBlock);
                        separator = new Separator();
                        brush = conveter.ConvertFromString("#FF0069FF") as Brush;
                        separator.BorderBrush = brush;
                        specificStackPanel.Children.Add(separator);
                        wholeStackPanel.Children.Add(specificStackPanel);
                    }

                    sbLogs.Content = wholeStackPanel;
                    lblTotalLogs.Text = "Total : " + logs.Count();
                }
            }
        }

        private void LoadLogs()
        {
            RefreshLogs();
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogs();

            canvasMainMenu.Width = GetCanvasMinWidth(canvasMainMenu);
            canvasMainMenu.Height = GetCanvasMinHeight(canvasMainMenu);
            canvasMainMenu.Visibility = Visibility.Collapsed;
            canvasMainMenu.Opacity = 0;
            FoldInnerCanvasSideward(canvasMainMenu);

            canvasSettings.Width = GetCanvasMinWidth(canvasSettings);
            canvasSettings.Height = GetCanvasMinHeight(canvasSettings);
            canvasSettings.Visibility = Visibility.Collapsed;
            canvasSettings.Opacity = 0;
        }

        private void btnLead_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            LeadDashboard page = new LeadDashboard();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasMainMenu);
        }

        private void btnSales_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            SalesDashboard page = new SalesDashboard();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasMainMenu);
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            TaskManagementDashboard page = new TaskManagementDashboard();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasMainMenu);
        }

        private void btnCustomerService_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            CustomerServiceDashboard page = new CustomerServiceDashboard();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasMainMenu);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasMainMenu);
            FoldInnerCanvasSideward(canvasSettings);
        }

        private void btnSysParams_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasSettings);
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            Employees page = new Employees();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasSettings);
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            Users page = new Users();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasSettings);
        }

        private void btnTerritories_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new Territories();
            frame.Navigate(page);
            FoldInnerCanvasSideward(canvasSettings);
        }

        private void btnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            FoldInnerCanvasSideward(canvasSettings);
            FoldInnerCanvasSideward(canvasMainMenu);
        }

        //private void btnLogs_Click(object sender, RoutedEventArgs e)
        //{
        //    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
        //    SystemLog page = new SystemLog();
        //    frame.Navigate(page);
        //    FoldInnerCanvasSideward(canvasMainMenu);
        //}
    }
}
