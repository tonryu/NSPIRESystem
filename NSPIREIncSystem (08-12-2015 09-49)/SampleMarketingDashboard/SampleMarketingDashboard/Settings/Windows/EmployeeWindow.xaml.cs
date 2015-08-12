using System;
using System.Linq;
using System.Windows;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.Settings.Windows
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public static int EmployeeId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EmployeeId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var employee = context.Employees.FirstOrDefault(c => c.EmployeeId == EmployeeId);

                    if (employee != null)
                    {
                        lblEmployee.Text = "EDIT EMPLOYEE";
                        txtEmployeeId.IsEnabled = false;
                        txtEmployeeId.Text = Convert.ToString(EmployeeId);
                        txtFirstName.Text = employee.FirstName;
                        txtMiddleName.Text = employee.MiddleName;
                        txtLastName.Text = employee.LastName;
                        txtAddress.Text = employee.Address;
                        cbTerritory.Text = employee.Territory;
                        txtEmailAddress.Text = employee.EmailAddress;
                        txtPhoneNo.Text = employee.PhoneNo;
                        txtFaxNo.Text = employee.FaxNo;
                        txtPosition.Text = employee.Position;
                    }
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    var employee = context.Employees.Max(c => c.EmployeeId);
                    employee++;
                    
                    lblEmployee.Text = "ADD EMPLOYEE";
                    txtEmployeeId.IsEnabled = false;
                    txtEmployeeId.Text = Convert.ToString(employee);
                    txtFirstName.Text = "";
                    txtMiddleName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    cbTerritory.Text = "";
                    txtEmailAddress.Text = "";
                    txtPhoneNo.Text = "";
                    txtFaxNo.Text = "";
                    txtPosition.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var employee = context.Employees.FirstOrDefault(c => c.EmployeeId == EmployeeId);

                    if (employee != null)
                    {
                        employee.Address = txtAddress.Text;
                        employee.EmailAddress = txtEmailAddress.Text;
                        employee.EmployeeId = Convert.ToInt32(txtEmployeeId.Text);
                        employee.FaxNo = txtFaxNo.Text;
                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.MiddleName = txtMiddleName.Text;
                        employee.PhoneNo = txtPhoneNo.Text;
                        employee.Position = txtPosition.Text;
                        employee.Territory = cbTerritory.Text;
                            
                        var employeeName = context.Employees.FirstOrDefault
                            (c => c.FirstName.ToLower() == txtFirstName.Text.ToLower() && 
                            c.MiddleName.ToLower() == txtMiddleName.Text.ToLower() &&
                            c.LastName.ToLower() == txtLastName.Text.ToLower());

                        if (employeeName == null)
                        {
                            context.SaveChanges();

                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Successfully updated";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();

                            this.Close();
                        }
                    }
                }
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    var employeeName = context.Employees.FirstOrDefault
                                (c => c.FirstName.ToLower() == txtFirstName.Text.ToLower() && 
                                c.MiddleName.ToLower() == txtMiddleName.Text.ToLower() &&
                                c.LastName.ToLower() == txtLastName.Text.ToLower());

                    if (employeeName == null)
                    {
                        var employee = new Employee();
                        employee.Address = txtAddress.Text;
                        employee.EmailAddress = txtEmailAddress.Text;
                        employee.EmployeeId = Convert.ToInt32(txtEmployeeId.Text);
                        employee.FaxNo = txtFaxNo.Text;
                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.MiddleName = txtMiddleName.Text;
                        employee.PhoneNo = txtPhoneNo.Text;
                        employee.Position = txtPosition.Text;
                        employee.Territory = cbTerritory.Text;

                        context.Employees.Add(employee);
                        context.SaveChanges();

                        var windows = new Shared.Windows.NoticeWindow();
                        Shared.Windows.NoticeWindow.message = "Employee succesfully created";
                        windows.Height = 0;
                        windows.Top = screenTopEdge + 8;
                        windows.Left = (screenWidth / 2) - (windows.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                        windows.ShowDialog();

                        this.Close();
                    }
                    else
                    {
                        var windows = new Shared.Windows.NoticeWindow();
                        Shared.Windows.NoticeWindow.message = "A name similar to this employee is already existing.";
                        windows.Height = 0;
                        windows.Top = screenTopEdge + 8;
                        windows.Left = (screenWidth / 2) - (windows.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                        windows.ShowDialog();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
