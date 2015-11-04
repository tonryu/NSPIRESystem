using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.Shared.Forms
{
    /// <summary>
    /// Interaction logic for EmployeeSelection.xaml
    /// </summary>
    public partial class EmployeeSelection : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenHeight = Application.Current.MainWindow.Height;
        double screenWidth = Application.Current.MainWindow.Width;

        List<EmployeeView> employeesList = new List<EmployeeView>();

        public static string selectedEmployee = "";

        public EmployeeSelection()
        {
            InitializeComponent();
        }

        private void LoadEmployees(string text)
        {
            using (var context = new DatabaseContext())
            {
                var employee = context.Employees.ToList();

                listEmployees.Items.Clear();
                employeesList.Clear();
                if (employee != null)
                {
                    foreach (var item in employee)
                    {
                        employeesList.Add(new EmployeeView
                        {
                            EmployeeName = item.FirstName + " " + item.MiddleName + " " + item.LastName
                        });
                    }
                }

                foreach (var item in employeesList.Where(c => c.EmployeeName.ToLower().Contains(text.ToLower())).ToList())
                {
                    listEmployees.Items.Add(item.EmployeeName);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployees(txtSearch.Text);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (listEmployees.SelectedItem != null)
            {
                selectedEmployee = listEmployees.SelectedItem.ToString();
                Variables.yesClicked = true;
                var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
                frame.GoBack();
                AgentsForm.isSelectFinish = true;
            }
            else
            {
                var window = new NoticeWindow();
                NoticeWindow.message = "Please select a row.";
                window.Height = 0;
                window.Top = Application.Current.MainWindow.Top + 8;
                window.Left = (screenWidth / 2) - (window.Width / 2);
                if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                window.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Variables.yesClicked = false;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees(txtSearch.Text);
        }

        private void listEmployees_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (listEmployees.SelectedItem != null)
                {
                    selectedEmployee = listEmployees.SelectedItem.ToString();
                    Variables.yesClicked = true;
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                    frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
                    frame.GoBack();
                }
                else
                {
                    var window = new NoticeWindow();
                    NoticeWindow.message = "Please select a row.";
                    window.Height = 0;
                    window.Top = Application.Current.MainWindow.Top + 8;
                    window.Left = (screenWidth / 2) - (window.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8)
                    {
                        window.Left += screenLeftEdge;
                    }
                    window.ShowDialog();
                }
            }
        }
    }
}
