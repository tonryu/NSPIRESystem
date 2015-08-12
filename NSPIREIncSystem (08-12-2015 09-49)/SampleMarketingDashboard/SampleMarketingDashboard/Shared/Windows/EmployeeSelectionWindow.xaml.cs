using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.Shared.Windows
{
    /// <summary>
    /// Interaction logic for EmployeeSelectionWindow.xaml
    /// </summary>
    public partial class EmployeeSelectionWindow : Window
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenHeight = Application.Current.MainWindow.Height;
        double screenWidth = Application.Current.MainWindow.Width;

        List<EmployeeView> employeesList = new List<EmployeeView>();

        public static string selectedEmployee = "";

        public EmployeeSelectionWindow()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region animation onLoading
            if (screenLeftEdge > 0 || screenLeftEdge < -8)
            {
                screenWidth += screenLeftEdge;
            }
            DoubleAnimation animation = new DoubleAnimation(0, this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            DoubleAnimation animation2 = new DoubleAnimation(screenWidth, screenWidth - this.Width, (Duration)TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(Window.WidthProperty, animation);
            this.BeginAnimation(Window.LeftProperty, animation2);
            #endregion

            LoadEmployees(txtSearch.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region animation onClosing
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
            #endregion
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (listEmployees.SelectedItem != null)
            {
                selectedEmployee = listEmployees.SelectedItem.ToString();
                Variables.yesClicked = true;
                DialogResult = true;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Variables.yesClicked = false;
            DialogResult = false;
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
                    DialogResult = true;
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
