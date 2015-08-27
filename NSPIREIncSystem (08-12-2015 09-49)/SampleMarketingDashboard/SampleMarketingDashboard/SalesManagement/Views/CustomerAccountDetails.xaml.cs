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

namespace NSPIREIncSystem.SalesManagement.Views
{
    /// <summary>
    /// Interaction logic for CustomerAccountDetails.xaml
    /// </summary>
    public partial class CustomerAccountDetails : UserControl
    {
        public static string AccountNumber;

        public CustomerAccountDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
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
                            txtAccountNumber.Text = account.AccountNumber;
                            txtDiscount.Text = account.Discount;
                            txtGross.Text = account.Gross;
                            txtNetValue.Text = account.NetValue;
                            txtServiceCharge.Text = account.ServiceCharge;
                            txtCompanyName.Text = customer.CompanyName;
                            txtModeOfPayment.Text = account.ModeOfPayment;
                            txtProduct.Text = product.ProductName;
                            txtTerritory.Text = territory.TerritoryName;
                        }
                    }
                }
            }
        }
    }
}
