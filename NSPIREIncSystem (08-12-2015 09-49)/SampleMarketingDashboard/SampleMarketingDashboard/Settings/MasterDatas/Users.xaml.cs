using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Settings.Windows;
using NSPIREIncSystem.Shared.Windows;
using System.Threading.Tasks;

namespace NSPIREIncSystem.Settings.MasterDatas
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public List<UsersLists> usersList = new List<UsersLists>();
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;

        public Users()
        {
            InitializeComponent();
        }

        #region Load Details
        private Task<string> QueryLoadUsers()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    usersList.Clear();
                    using(var context = new DatabaseContext())
                    {
                        var user = context.UserAccounts.ToList();

                        foreach(var item in user)
                        {
                            var empname = context.Employees.FirstOrDefault(c => c.EmployeeId == item.EmployeeId);
                            usersList.Add(new UsersLists()
                            {
                                IsAdmin= item.IsAdmin,
                                CustomerServiceManagementAccess= item.CustomerServiceAccess,
                                EmployeeName = empname.FirstName + "" + empname.LastName,
                                LeadManagementAccess= item.LeadManagementAccess,
                                TaskManagementAccess = item.TaskManagementAccess,
                                UserAccountId = item.UserAccountId
                            });

                        }
                    }
                    return null;
            }

                catch(Exception ex)
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
                message = await QueryLoadUsers();

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

                dcUsersList.ItemsSource = usersList.Where(c => c.UserAccountId.Contains(txtSearch.Text) ||
                   c.EmployeeName.Contains(txtSearch.Text)).OrderBy(c => c.UserAccountId).ToList();

                viewUser.BestFitColumns();

                if (usersList.Count == 0)
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "List has no Record.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                busyIndicator.IsBusy = false;
            }
        }

        private void LoadUsers()
        {
            RefreshTable("");
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();

            canvasUserMasterData.Width = GetCanvasMinWidth(canvasUserMasterData);
            canvasUserMasterData.Height = GetCanvasMinHeight(canvasUserMasterData);
            canvasUserMasterData.Visibility = Visibility.Collapsed;
            canvasUserMasterData.Opacity = 0;

            FoldInnerCanvasSideward(canvasUserMasterData);
        }

        //private void LoadMethod(string text)
        //{
        //    using (var context = new DatabaseContext())
        //    {
        //        var user = context.UserAccounts.ToList();

        //        usersList.Clear();
        //        foreach (var item in user)
        //        {
        //            var employee = context.Employees.FirstOrDefault(c => c.EmployeeId == item.EmployeeId);
        //            usersList.Add(new UsersLists
        //            {
        //                UserAccountId = item.UserAccountId,
        //                EmployeeName = employee.FirstName + " " + employee.LastName,
        //                LeadManagementAccess = item.LeadManagementAccess,
        //                CustomerServiceManagementAccess = item.CustomerServiceAccess,
        //                TaskManagementAccess = item.TaskManagementAccess,
        //                IsAdmin = item.IsAdmin
        //            });
        //        }
        //        dcUsersList.ItemsSource = usersList.Where(c => c.UserAccountId.Contains(text) || 
        //            c.EmployeeName.Contains(text)).OrderBy(c => c.UserAccountId).ToList();

        //        viewUser.BestFitColumns();
        //    }
        //}

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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           LoadUsers();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserAccountWindow();
            window.Top = Application.Current.MainWindow.Top + 98;
            window.Height = screenHeight - 115;
            window.Width = (screenWidth - 90) * 0.35;
            window.ShowDialog();

            LoadUsers();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedRow = dcUsersList.SelectedItem as UsersLists;

                if (selectedRow != null)
                {
                    UserAccountWindow.UserAccountId = selectedRow.UserAccountId;

                    var window = new UserAccountWindow();
                    window.Top = Application.Current.MainWindow.Top + 98;
                    window.Height = screenHeight - 115;
                    window.Width = (screenWidth - 90) * 0.35;
                    window.ShowDialog();
                }
                else
                {
                    NullMessage();
                }

                UserAccountWindow.UserAccountId = "";
               LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var selectedUser = dcUsersList.SelectedItem as UsersLists;

                if (selectedUser != null)
                {
                    var user = context.UserAccounts.FirstOrDefault
                        (c => c.UserAccountId.ToLower() == selectedUser.UserAccountId.ToLower());
                    
                    if (user != null)
                    {
                        var window = new MessageBoxWindow("Are you sure you want to delete this record?");
                        window.Height = 0;
                        window.Top = screenTopEdge + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                        window.ShowDialog();

                        if (Variables.yesClicked == true)
                        {
                            context.UserAccounts.Remove(user);

                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "LEAD SUCCESSFULLY DELETED";
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
                LoadUsers();
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
            var windows = new Shared.Windows.NoticeWindow();
            Shared.Windows.NoticeWindow.message = "No row is selected.";
            windows.Height = 0;
            windows.Top = screenTopEdge + 8;
            windows.Left = (screenWidth / 2) - (windows.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            windows.ShowDialog();
        }
    }
}
