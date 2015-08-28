using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Dashboards;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.Shared.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        public static string Username;
        List<Hyperlink> hyperlinksList = new List<Hyperlink>();
        public static Button b;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static System.Windows.Threading.DispatcherTimer updateTimer;

        public MainView()
        {
            InitializeComponent();
        }

        #region Add or Remove Hyperlinks

        public void AddHyperLink(Button button)
        {
            //button = b;
            //Run contentRun = new Run(button.Content.ToString());
            //Hyperlink existingHyperlink = hyperlinksList.FirstOrDefault(c => (c.Inlines.FirstOrDefault() as Run).Text == contentRun.Text);
            //if (existingHyperlink != null)
            //{
            //    int index = hyperlinksList.IndexOf(existingHyperlink);
            //    for (int i = hyperlinksList.Count() - 1; i >= index; i--) { hyperlinksList.Remove(hyperlinksList[i]); }
            //}
            //else
            //{
            //    Hyperlink hlink = new Hyperlink();
            //    hlink.Inlines.Add(contentRun);
            //    hlink.Foreground = Brushes.Black;

            //    hlink.Click += (object sender, RoutedEventArgs e) =>
            //    {
            //        button_Click(button, null);
            //    };

            //    hyperlinksList.Add(hlink);
            //}

            //txtHyperlink.Inlines.Clear();
            //for (int i = 0; i < hyperlinksList.Count(); i++)
            //{
            //    if (i > 0) { txtHyperlink.Inlines.Add(" | "); }
            //    txtHyperlink.Inlines.Add(hyperlinksList[i]);
            //}
        }

        #endregion

        #region Measure StackPanel
        private double GetStackPanelHeight(StackPanel stackPanel)
        {
            double stackPanelHeight = 0;

            var buttonChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
            if (buttonChildren.Count() > 0)
            {
                foreach (var child in buttonChildren)
                {
                    double childUpperMargin = child.Margin.Top;
                    double childHeight = child.Height;
                    double childLowerMargin = child.Margin.Bottom;

                    double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                    stackPanelHeight += totalChildHeight;
                }
            }

            var stackPanelChildren = stackPanel.Children.OfType<StackPanel>().ToList();
            if (stackPanelChildren.Count() > 0)
            {
                foreach (var stackPanelChild in stackPanelChildren)
                {
                    var buttons = stackPanelChild.Children.OfType<ToggleButton>().ToList();
                    if (buttons.Count() > 0)
                    {
                        foreach (var child in buttons)
                        {
                            double childUpperMargin = child.Margin.Top;
                            double childHeight = child.Height;
                            double childLowerMargin = child.Margin.Bottom;

                            double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                            stackPanelHeight += totalChildHeight;
                        }
                    }
                }
            }

            return stackPanelHeight;
        }

        private double GetStackPanelWidth(StackPanel stackPanel)
        {
            double stackPanelWidth = 0;

            var stackPanelChildren = stackPanel.Children.OfType<ToggleButton>().ToList();
            if (stackPanelChildren.Count() > 0)
            {
                double childLeftMargin = stackPanelChildren.FirstOrDefault().Margin.Left;
                double childWidth = stackPanelChildren.FirstOrDefault().Width;
                double childRightMargin = stackPanelChildren.FirstOrDefault().Margin.Right;

                double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                stackPanelWidth = totalChildWidth;
            }

            return stackPanelWidth;
        }

        private double GetStackPanelHeightButton(StackPanel stackPanel)
        {
            double stackPanelHeight = 0;

            var buttonChildren = stackPanel.Children.OfType<Button>().ToList();
            if (buttonChildren.Count() > 0)
            {
                foreach (var child in buttonChildren)
                {
                    double childUpperMargin = child.Margin.Top;
                    double childHeight = child.Height;
                    double childLowerMargin = child.Margin.Bottom;

                    double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                    stackPanelHeight += totalChildHeight;
                }
            }

            var stackPanelChildren = stackPanel.Children.OfType<StackPanel>().ToList();
            if (stackPanelChildren.Count() > 0)
            {
                foreach (var stackPanelChild in stackPanelChildren)
                {
                    var buttons = stackPanelChild.Children.OfType<Button>().ToList();
                    if (buttons.Count() > 0)
                    {
                        foreach (var child in buttons)
                        {
                            double childUpperMargin = child.Margin.Top;
                            double childHeight = child.Height;
                            double childLowerMargin = child.Margin.Bottom;

                            double totalChildHeight = childUpperMargin + childHeight + childLowerMargin;
                            stackPanelHeight += totalChildHeight;
                        }
                    }
                }
            }

            return stackPanelHeight;
        }

        private double GetStackPanelWidthButton(StackPanel stackPanel)
        {
            double stackPanelWidth = 0;

            var stackPanelChildren = stackPanel.Children.OfType<Button>().ToList();
            if (stackPanelChildren.Count() > 0)
            {
                double childLeftMargin = stackPanelChildren.FirstOrDefault().Margin.Left;
                double childWidth = stackPanelChildren.FirstOrDefault().Width;
                double childRightMargin = stackPanelChildren.FirstOrDefault().Margin.Right;

                double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                stackPanelWidth = totalChildWidth;
            }

            return stackPanelWidth;
        }
        
        private void FoldStackPanelSideward(StackPanel stackPanel)
        {
            double stackPanelWidth = GetStackPanelWidth(stackPanel);
            if (stackPanel.Width > 0)
            {
                DoubleAnimation animation = new DoubleAnimation() { From = stackPanelWidth, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelWidth, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
            }
        }
        #endregion

        #region Animate StackPanel
        private void FoldStackPanelUpward(StackPanel stackPanel)
        {
            double stackPanelHeight = GetStackPanelHeight(stackPanel);
            if (stackPanel.Height > 0)
            {
                DoubleAnimation animation = new DoubleAnimation() { From = stackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
        }

        private void FoldStackPanelUpwardButton(StackPanel stackPanel)
        {
            double stackPanelHeight = GetStackPanelHeightButton(stackPanel);
            if (stackPanel.Height > 0)
            {
                DoubleAnimation animation = new DoubleAnimation() { From = stackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
            else
            {
                DoubleAnimation animation = new DoubleAnimation() { From = 0, To = stackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
        }

        private void FoldAfterAnotherSingleMode(StackPanel openedStackPanel, StackPanel closedStackPanel)
        {
            double openedStackPanelHeight = GetStackPanelHeight(openedStackPanel);
            double closedStackPanelHeight = GetStackPanelHeight(closedStackPanel);

            DoubleAnimation closingAnimation = new DoubleAnimation() { From = openedStackPanelHeight, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
            DoubleAnimation openingAnimation = new DoubleAnimation() { From = 0, To = closedStackPanelHeight, Duration = TimeSpan.Parse("0:0:0.35") };
            closingAnimation.Completed += (s, e) => closedStackPanel.BeginAnimation(StackPanel.HeightProperty, openingAnimation);
            openedStackPanel.BeginAnimation(StackPanel.HeightProperty, closingAnimation);
        }

        private void CloseStackPanel(StackPanel stackPanel)
        {
            if (stackPanel.Height > 0)
            {
                DoubleAnimation animation = new DoubleAnimation() { From = stackPanel.Height, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                stackPanel.BeginAnimation(StackPanel.HeightProperty, animation);
            }
        }
        #endregion

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblTimer.Content = DateTime.Now.ToString("MMMM dd, yyyy - h:mm:ss tt");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            updateTimer = new System.Windows.Threading.DispatcherTimer();
            updateTimer.Tick += new EventHandler(UpdateUserProfileEvent);
            updateTimer.Interval = TimeSpan.FromSeconds(0.1);
            updateTimer.Start();

            using (var context = new DatabaseContext())
            {
                var emp = context.UserAccounts.FirstOrDefault(c => c.UserAccountId == Username);

                if (emp.IsAdmin != true && (emp.CustomerServiceAccess != "Full" || emp.LeadManagementAccess != "Full"
                    || emp.TaskManagementAccess != "Full"))
                {
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frameNavigate);
                    UserDashboard page = new UserDashboard();
                    frame.Navigate(page);

                    tbUsertype.Text = "User";
                }
                //else if (emp.IsAdmin == true && emp.LeadManagementAccess == "Full" && emp.TaskManagementAccess != "Full"
                //    && emp.CustomerServiceAccess != "Full")
                //{
                //    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frameNavigate);
                //    LeadDashboard page = new LeadDashboard();
                //    frame.Navigate(page);

                //    tbUsertype.Text = "Lead Administrator";
                //}
                //else if (emp.IsAdmin == true && emp.TaskManagementAccess == "Full" && emp.LeadManagementAccess != "Full"
                //    && emp.CustomerServiceAccess != "Full")
                //{
                //    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frameNavigate);
                //    TaskManagementDashboard page = new TaskManagementDashboard();
                //    frame.Navigate(page);

                //    tbUsertype.Text = "Task Administrator";
                //}
                //else if (emp.IsAdmin == true && emp.CustomerServiceAccess == "Full" && emp.TaskManagementAccess != "Full" 
                //    && emp.LeadManagementAccess != "Full")
                //{
                //    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frameNavigate);
                //    CustomerServiceDashboard page = new CustomerServiceDashboard();
                //    frame.Navigate(page);

                //    tbUsertype.Text = "Customer Service Administrator";
                //}
                else if (emp.IsAdmin == true && (emp.CustomerServiceAccess == "Full" && emp.LeadManagementAccess == "Full"
                    && emp.TaskManagementAccess == "Full"))
                {
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(frameNavigate);
                    AdminDashboard page = new AdminDashboard();
                    frame.Navigate(page);

                    tbUsertype.Text = "Administrator";
                }
                else
                {

                }
            }

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            stackUserContent.Height = 0;
            tbDisplayName.Text = NotificationWindow.username;
        }

        private void UpdateUserProfileEvent(object source, EventArgs e)
        {
            //for detecting the user profile for collapsing
            if (Variables.collapseUserContent == true)
            {
                CloseStackPanel(stackUserContent);
                Variables.collapseUserContent = false;
            }
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            Variables.collapseMenu = true;
            FoldStackPanelUpwardButton(stackUserContent);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            FoldStackPanelUpwardButton(stackUserContent);

            var window = new MessageBoxLogout();
            double screenWidth = Application.Current.MainWindow.Width;
            window.Height = 0;
            window.Top = Application.Current.MainWindow.Top + 8;
            window.Left = (screenWidth / 2) - (window.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                window.Left += screenLeftEdge;
            }
            window.ShowDialog();
            if (Variables.yesClicked == true)
            {
                using (var context = new DatabaseContext())
                {
                    var log = new Log();
                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                    log.Description = NotificationWindow.username + " logs out on "
                        + DateTime.Now.ToString("MMMM d, yyyy") + " at " + log.Time + ".";
                    context.Logs.Add(log);
                    context.SaveChanges();
                }

                var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                frame.BackNavigationMode = BackNavigationMode.Root;
                frame.GoBack();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FoldStackPanelUpwardButton(stackUserContent);

            var window = new MessageBoxWindow("Are you sure you want to exit?");
            double screenWidth = Application.Current.MainWindow.Width;
            window.Height = 0;
            window.Top = Application.Current.MainWindow.Top + 8;
            window.Left = (screenWidth / 2) - (window.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                window.Left += screenLeftEdge;
            }
            window.ShowDialog();
            if (Variables.yesClicked == true)
            {
                using (var context = new DatabaseContext())
                {
                    var log = new Log();
                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                    log.Description = NotificationWindow.username + " logs out on "
                        + log.Date + " at " + log.Time + ".";
                    context.Logs.Add(log);
                    context.SaveChanges();
                }

                Application.Current.MainWindow.Close();
            }
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            #region //leave or remove hyperlink
            Button clickedButton = sender as Button;
            AddHyperLink(clickedButton);
            #endregion
        }
    }
}
