using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.Settings.Windows
{
    /// <summary>
    /// Interaction logic for UserAccountWindow.xaml
    /// </summary>
    public partial class UserAccountWindow : Window
    {
        public static string UserAccountId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;

        public UserAccountWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.UserAccounts.FirstOrDefault(c => c.UserAccountId == UserAccountId);

                if(user != null)
                {
                    var employee = context.Employees.FirstOrDefault(c => c.EmployeeId == user.EmployeeId);

                    if (UserAccountId != null || UserAccountId != "")
                    {
                        txtEmployeeName.Text = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName;
                        txtEmployeeName.IsEnabled = false;
                        txtUsername.Text = user.UserAccountId;
                        txtUsername.IsEnabled = false;
                        lblOldPassword.Text = "OLD PASSWORD *";
                        txtOldPassword.NullText = "";
                        lblNewPassword.Visibility = Visibility.Visible; Grid.SetRow(lblNewPassword, 3);
                        txtNewPassword.Visibility = Visibility.Visible; Grid.SetRow(txtNewPassword, 3); Grid.SetColumn(txtNewPassword, 1); Grid.SetColumnSpan(txtNewPassword, 2);
                        txtNewPassword.NullText = "Must be a combination of uppercase letters and numbers";
                        lblConfirmPassword.Visibility = Visibility.Visible; Grid.SetRow(lblConfirmPassword, 4);
                        txtConfirmPassword.Visibility = Visibility.Visible; Grid.SetRow(txtConfirmPassword, 4); Grid.SetColumn(txtConfirmPassword, 1); Grid.SetColumnSpan(txtConfirmPassword, 2);
                        txtConfirmPassword.NullText = "Must match your new password";
                        cbLeadAccess.Text = user.LeadManagementAccess;
                        cbTaskAccess.Text = user.TaskManagementAccess;
                        cbCustomerServiceAccess.Text = user.CustomerServiceAccess;
                        if (user.IsAdmin != true)
                        {
                            tsAdminCheck.IsChecked = false;
                        }
                        else
                        {
                            tsAdminCheck.IsChecked = true;
                        }
                    }
                }
                else
                {
                    txtEmployeeName.Text = "";
                    txtEmployeeName.IsEnabled = false;
                    txtUsername.Text = "";
                    txtUsername.IsEnabled = true;
                    lblOldPassword.Text = "PASSWORD *";
                    txtOldPassword.NullText = "Must be a combination of uppercase letters and numbers";
                    lblNewPassword.Visibility = Visibility.Hidden;
                    txtNewPassword.Visibility = Visibility.Hidden;
                    lblConfirmPassword.Visibility = Visibility.Visible; Grid.SetRow(lblConfirmPassword, 3);
                    txtConfirmPassword.Visibility = Visibility.Visible; Grid.SetRow(txtConfirmPassword, 3); Grid.SetColumn(txtConfirmPassword, 1); Grid.SetColumnSpan(txtConfirmPassword, 2);
                    txtConfirmPassword.NullText = "Must match your password";
                    Grid.SetRow(lblLead, 4);
                    cbLeadAccess.Text = "Restricted"; Grid.SetRow(cbLeadAccess, 4); Grid.SetColumn(cbLeadAccess, 1); Grid.SetColumnSpan(cbLeadAccess, 2);
                    Grid.SetRow(lblTask, 5);
                    cbTaskAccess.Text = "Restricted"; Grid.SetRow(cbTaskAccess, 5); Grid.SetColumn(cbTaskAccess, 1); Grid.SetColumnSpan(cbTaskAccess, 2);
                    Grid.SetRow(stackCustomerService, 6);
                    cbCustomerServiceAccess.Text = "Restricted"; Grid.SetRow(cbCustomerServiceAccess, 6); Grid.SetColumn(cbCustomerServiceAccess, 1); Grid.SetColumnSpan(cbCustomerServiceAccess, 2);
                    tsAdminCheck.IsChecked = false;
                }
            }

            #region animation onLoading
            double screenWidth = Application.Current.MainWindow.Width;
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            DoubleAnimation animation2 = new DoubleAnimation(screenWidth, screenWidth - this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.WidthProperty, animation);
            this.BeginAnimation(Window.LeftProperty, animation2);
            #endregion
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var window = new Shared.Windows.EmployeeSelectionWindow();
            window.Top = Application.Current.MainWindow.Top + 98;
            window.Height = screenHeight - 115;
            window.Width = (screenWidth - 90) * 0.35;
            window.ShowDialog();
            if (Variables.yesClicked == true)
            {
                using (var context = new DatabaseContext())
                {
                    string empname = EmployeeSelectionWindow.selectedEmployee;
                    var emp = context.Employees.FirstOrDefault(c => c.FirstName + " " 
                        + c.MiddleName + " " + c.LastName == empname);
                    var duplicate = context.UserAccounts.Where(c => c.EmployeeId == emp.EmployeeId).ToList();
                    List<EmployeeView> employee = new List<EmployeeView>();

                    if (duplicate.Count() != 0)
                    {
                        var window2 = new NoticeWindow();
                        NoticeWindow.message = "Employee already has a user account.";
                        double screenWidth2 = Application.Current.MainWindow.Width;
                        window2.Height = 0;
                        window2.Top = Application.Current.MainWindow.Top + 8;
                        window2.Left = (screenWidth2 / 2) - (window2.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8)
                        {
                            window2.Left += screenLeftEdge;
                        }
                        window2.ShowDialog();
                    }
                    else
                    {
                        txtEmployeeName.Text = empname;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                byte[] data = System.Text.Encoding.Unicode.GetBytes(txtOldPassword.Text);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                String hash = System.Text.Encoding.ASCII.GetString(data);
                
                if (UserAccountId != "" && UserAccountId != null)
                {
                    var user = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == UserAccountId.ToLower());
                    
                    if (txtOldPassword.Text == "")
                    {
                        var employee = context.Employees.FirstOrDefault
                            (c => c.FirstName.ToLower() + " " + c.MiddleName.ToLower() + " " + c.LastName.ToLower()
                             == txtEmployeeName.Text.ToLower());
                        var checkUser = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == txtUsername.Text.ToLower());

                        if (checkUser != null)
                        {
                            if (txtUsername.Text.ToLower() == checkUser.UserAccountId.ToLower()
                                && checkUser.EmployeeId == user.EmployeeId)
                            {
                                user.EmployeeId = employee.EmployeeId;
                                user.UserAccountId = txtUsername.Text;
                                user.CustomerServiceAccess = cbCustomerServiceAccess.Text;
                                user.LeadManagementAccess = cbLeadAccess.Text;
                                user.TaskManagementAccess = cbTaskAccess.Text;
                                user.IsAdmin = tsAdminCheck.IsChecked.Value;

                                context.SaveChanges();
                                var windows = new Shared.Windows.NoticeWindow();
                                Shared.Windows.NoticeWindow.message = "User updated";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                                DialogResult = false;
                            }
                            else
                            {
                                var windows = new Shared.Windows.NoticeWindow();
                                Shared.Windows.NoticeWindow.message = "This username is already used.";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                        else
                        {
                            user.EmployeeId = employee.EmployeeId;
                            user.UserAccountId = txtUsername.Text;
                            user.CustomerServiceAccess = cbCustomerServiceAccess.Text;
                            user.LeadManagementAccess = cbLeadAccess.Text;
                            user.TaskManagementAccess = cbTaskAccess.Text;
                            user.IsAdmin = tsAdminCheck.IsChecked.Value;

                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "User updated";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                            DialogResult = false;
                        }
                    }
                    else if (txtOldPassword.Text != "" && txtNewPassword.Text == txtConfirmPassword.Text)
                    {
                        user = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == UserAccountId.ToLower());

                        if (hash != user.Password)
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Old password is incorrect.";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        else
                        {
                            data = System.Text.Encoding.Unicode.GetBytes(txtNewPassword.Text);
                            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                            hash = System.Text.Encoding.ASCII.GetString(data);
                            var employee = context.Employees.FirstOrDefault
                                (c => c.FirstName.ToLower() + " " + c.MiddleName.ToLower() + " " + c.LastName.ToLower()
                                 == txtEmployeeName.Text.ToLower());
                            var checkUser = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == txtUsername.Text.ToLower());

                            if (checkUser != null)
                            {
                                if (txtUsername.Text.ToLower() == checkUser.UserAccountId.ToLower() 
                                    && checkUser.EmployeeId == user.EmployeeId)
                                {
                                    user.EmployeeId = employee.EmployeeId;
                                    user.UserAccountId = txtUsername.Text;
                                    user.CustomerServiceAccess = cbCustomerServiceAccess.Text;
                                    user.LeadManagementAccess = cbLeadAccess.Text;
                                    user.TaskManagementAccess = cbTaskAccess.Text;
                                    user.IsAdmin = tsAdminCheck.IsChecked.Value;
                                    user.Password = hash;

                                    context.SaveChanges();
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "User updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                    DialogResult = false;
                                }
                                else
                                {
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "This username is already used.";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                            }
                            else
                            {
                                user.EmployeeId = employee.EmployeeId;
                                user.UserAccountId = txtUsername.Text;
                                user.CustomerServiceAccess = cbCustomerServiceAccess.Text;
                                user.LeadManagementAccess = cbLeadAccess.Text;
                                user.TaskManagementAccess = cbTaskAccess.Text;
                                user.IsAdmin = tsAdminCheck.IsChecked.Value;
                                user.Password = hash;

                                context.SaveChanges();
                                var windows = new Shared.Windows.NoticeWindow();
                                Shared.Windows.NoticeWindow.message = "User updated";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                                DialogResult = false;
                            }
                        }
                    }
                    else
                    {
                        IncorrectPasswordMessage();
                    }
                }
                else
                {
                    if (txtOldPassword.Text != "" && txtOldPassword.Text == txtConfirmPassword.Text)
                    {
                        var employee = context.Employees.FirstOrDefault
                                   (c => c.FirstName.ToLower() + " " + c.MiddleName.ToLower() + " " + c.LastName.ToLower()
                                    == txtEmployeeName.Text.ToLower());
                        var checkUser = context.UserAccounts.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);

                        if (checkUser == null)
                        {
                            checkUser = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == txtUsername.Text.ToLower());
                            
                            if (checkUser != null)
                            {
                                //var user = context.UserAccounts.FirstOrDefault(c => c.UserAccountId.ToLower() == checkUser.UserAccountId.ToLower());
                                
                                if (txtUsername.Text.ToLower() == checkUser.UserAccountId.ToLower() && checkUser.EmployeeId == employee.EmployeeId)
                                {
                                    context.UserAccounts.Add(
                                    new UserAccount
                                    {
                                        EmployeeId = employee.EmployeeId,
                                        UserAccountId = txtUsername.Text,
                                        CustomerServiceAccess = cbCustomerServiceAccess.Text,
                                        LeadManagementAccess = cbLeadAccess.Text,
                                        TaskManagementAccess = cbTaskAccess.Text,
                                        IsAdmin = tsAdminCheck.IsChecked.Value,
                                        Password = hash
                                    });
                                    context.SaveChanges();
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "New user account created";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                    DialogResult = false;
                                }
                                else
                                {
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "This username is already used.";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                            }
                            else
                            {
                                context.UserAccounts.Add(
                                    new UserAccount
                                    {
                                        EmployeeId = employee.EmployeeId,
                                        UserAccountId = txtUsername.Text,
                                        CustomerServiceAccess = cbCustomerServiceAccess.Text,
                                        LeadManagementAccess = cbLeadAccess.Text,
                                        TaskManagementAccess = cbTaskAccess.Text,
                                        IsAdmin = tsAdminCheck.IsChecked.Value,
                                        Password = hash
                                    });
                                context.SaveChanges();
                                var windows = new Shared.Windows.NoticeWindow();
                                Shared.Windows.NoticeWindow.message = "New user account created";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                                DialogResult = false;
                            }
                        }
                        else
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "This user already have an account";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                    }
                    else
                    {
                        IncorrectPasswordMessage();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void IncorrectPasswordMessage()
        {
            var windows = new Shared.Windows.NoticeWindow();
            Shared.Windows.NoticeWindow.message = "The passwords did not match";
            windows.Height = 0;
            windows.Top = screenTopEdge + 8;
            windows.Left = (screenWidth / 2) - (windows.Width / 2);
            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
            windows.ShowDialog();

            txtOldPassword.Focus();
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            double screenWidth = Application.Current.MainWindow.Width;
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }

            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(screenWidth, (Duration)TimeSpan.FromSeconds(0.3));
            var anim2 = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(Window.LeftProperty, anim);
            this.BeginAnimation(Window.WidthProperty, anim2);
        }

        private void tsAdminCheck_Checked(object sender, RoutedEventArgs e)
        {
            cbCustomerServiceAccess.SelectedItem = "Full";
            cbLeadAccess.SelectedItem = "Full";
            cbTaskAccess.SelectedItem = "Full";
        }

        private void tsAdminCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            cbCustomerServiceAccess.SelectedItem = "Restricted";
            cbLeadAccess.SelectedItem = "Restricted";
            cbTaskAccess.SelectedItem = "Restricted";
        }
    }
}
