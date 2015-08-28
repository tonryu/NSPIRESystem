using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.SalesManagement.Views
{
    /// <summary>
    /// Interaction logic for CustomerAccountsForm.xaml
    /// </summary>
    public partial class CustomerAccountsForm : UserControl
    {
        public static string AccountNumber;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public CustomerAccountsForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var customers = context.Customers.ToList();
                var territories = context.Territories.ToList();
                var products = context.Products.ToList();
                var modes = new string[] { "CASH", "QUARTERLY", "MONTHLY" };

                if (customers != null)
                {
                    cbCompanyName.ItemsSource = null;
                    cbCompanyName.ItemsSource = customers.Select(c => c.CompanyName).ToList();
                }
                else { cbCompanyName.ItemsSource = null; }

                if (territories != null)
                {
                    cbTerritory.ItemsSource = null;
                    cbTerritory.ItemsSource = territories.Select(c => c.TerritoryName).ToList();
                }
                else { cbTerritory.ItemsSource = null; }

                if (products != null)
                {
                    cbProduct.ItemsSource = null;
                    cbProduct.ItemsSource = products.Select(c => c.ProductName).ToList();
                }
                else { cbProduct.ItemsSource = null; }

                if (modes != null)
                {
                    cbModeOfPayment.ItemsSource = null;
                    cbModeOfPayment.ItemsSource = modes.ToList();
                }
                else { cbModeOfPayment.ItemsSource = null; }

                if (AccountNumber != null)
                {
                    var account = context.CustomerAccounts.FirstOrDefault(c => c.AccountNumber == AccountNumber);

                    if (account != null)
                    {
                        var customer = context.Customers.FirstOrDefault(c => c.CustomerID == account.CustomerID);
                        var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == account.TerritoryID);
                        var product = context.Products.FirstOrDefault(c => c.ProductID == account.ProductID);

                        if (customer != null && territory != null && product != null)
                        {
                            lblAccountNumber.Visibility = Visibility.Visible;
                            txtAccountNumber.Visibility = Visibility.Visible;
                            Grid.SetRow(lblCompanyName, 1);
                            Grid.SetRow(cbCompanyName, 1); Grid.SetColumn(cbCompanyName, 1);
                            Grid.SetRow(lblTerritory, 2);
                            Grid.SetRow(cbTerritory, 2); Grid.SetColumn(cbTerritory, 1);
                            Grid.SetRow(lblProduct, 3);
                            Grid.SetRow(cbProduct, 3); Grid.SetColumn(cbProduct, 1);
                            Grid.SetRow(lblModeOfPayment, 4);
                            Grid.SetRow(cbModeOfPayment, 4); Grid.SetColumn(cbModeOfPayment, 1);

                            txtAccountNumber.Text = account.AccountNumber;
                            txtDiscount.Text = account.Discount;
                            txtGross.Text = account.Gross;
                            txtNetValue.Text = account.NetValue;
                            txtServiceCharge.Text = account.ServiceCharge;
                            cbCompanyName.SelectedItem = customer.CompanyName;
                            cbModeOfPayment.SelectedItem = account.ModeOfPayment;
                            cbProduct.SelectedItem = product.ProductName;
                            cbTerritory.SelectedItem = territory.TerritoryName;
                        }
                    }
                }
                else
                {
                    lblAccountNumber.Visibility = Visibility.Hidden;
                    txtAccountNumber.Visibility = Visibility.Hidden;
                    Grid.SetRow(lblCompanyName, 0);
                    Grid.SetRow(cbCompanyName, 0); Grid.SetColumn(cbCompanyName, 1);
                    Grid.SetRow(lblTerritory, 1);
                    Grid.SetRow(cbTerritory, 1); Grid.SetColumn(cbTerritory, 1);
                    Grid.SetRow(lblProduct, 2);
                    Grid.SetRow(cbProduct, 2); Grid.SetColumn(cbProduct, 1);
                    Grid.SetRow(lblModeOfPayment, 3);
                    Grid.SetRow(cbModeOfPayment, 3); Grid.SetColumn(cbModeOfPayment, 1);

                    txtDiscount.Text = "₱";
                    txtGross.Text = "₱";
                    txtNetValue.Text = "₱";
                    txtServiceCharge.Text = "₱";
                    cbCompanyName.SelectedItem = "";
                    cbModeOfPayment.SelectedItem = "";
                    cbProduct.SelectedItem = "";
                    cbTerritory.SelectedItem = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if ((txtDiscount.Text != null || txtDiscount.Text != "") &&
                    (txtGross.Text != null || txtGross.Text != "") &&
                    (txtNetValue.Text != null || txtNetValue.Text != "") &&
                    (txtServiceCharge.Text != null || txtServiceCharge.Text != "") &&
                    (cbCompanyName.Text != null || cbCompanyName.Text != "") &&
                    (cbModeOfPayment.Text != null || cbModeOfPayment.Text != "") &&
                    (cbProduct.Text != null || cbProduct.Text != "") &&
                    (cbTerritory.Text != null || cbTerritory.Text != ""))
                {
                    var territory = context.Territories.FirstOrDefault(c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                    var product = context.Products.FirstOrDefault(c => c.ProductName.ToLower() == cbProduct.Text.ToLower());
                    var customer = context.Customers.FirstOrDefault(c => c.CompanyName.ToLower() == cbCompanyName.Text.ToLower());

                    if (AccountNumber != null)
                    {
                        var account = context.CustomerAccounts.FirstOrDefault(c => c.AccountNumber == AccountNumber);

                        if ((account != null) && (territory != null) && (product != null) && (customer != null))
                        {
                            if (account.ProductID == product.ProductID)
                            {
                                account.AccountNumber = txtAccountNumber.Text;
                                account.CustomerID = customer.CustomerID;
                                account.Discount = txtDiscount.Text;
                                account.Gross = txtGross.Text;
                                account.ModeOfPayment = cbModeOfPayment.SelectedItem.ToString();
                                account.NetValue = txtNetValue.Text;
                                account.ProductID = product.ProductID;
                                account.ServiceCharge = txtServiceCharge.Text;
                                account.TerritoryID = territory.TerritoryID;

                                context.SaveChanges();

                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Customer account successfully updated";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                            else
                            {
                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Customer account cannot change a product.";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        if ((territory != null) && (product != null) && (customer != null))
                        {
                            var account = context.CustomerAccounts.FirstOrDefault(c => c.CustomerID == customer.CustomerID && c.ProductID == product.ProductID);

                            if (account == null)
                            {
                                account = new CustomerAccount();

                                var maxNumber = context.CustomerAccounts.Max(c => c.AccountNumber);
                                int maxNumber2 = Convert.ToInt32(maxNumber);
                                maxNumber2++;

                                if (maxNumber2 <= 9)
                                {
                                    account.AccountNumber = "000" + maxNumber2;
                                }
                                else if (maxNumber2 > 9 && maxNumber2 <= 99)
                                {
                                    account.AccountNumber = "00" + maxNumber2;
                                }
                                else if (maxNumber2 > 99 && maxNumber2 <= 999)
                                {
                                    account.AccountNumber = "0" + maxNumber2;
                                }
                                else if (maxNumber2 > 999 && maxNumber2 <= 9999)
                                {
                                    account.AccountNumber = Convert.ToString(maxNumber2);
                                }
                                account.CustomerID = customer.CustomerID;
                                account.Discount = txtDiscount.Text;
                                account.Gross = txtGross.Text;
                                account.ModeOfPayment = cbModeOfPayment.SelectedItem.ToString();
                                account.NetValue = txtNetValue.Text;
                                account.ProductID = product.ProductID;
                                account.ServiceCharge = txtServiceCharge.Text;
                                account.TerritoryID = territory.TerritoryID;

                                context.CustomerAccounts.Add(account);
                                context.SaveChanges();

                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Customer account successfully created";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                            else
                            {
                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Customer account is already existing.";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                    }
                }
                else
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "Please fill all boxes associated with an asterisk (*) sign.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
            }
        }
    }
}
