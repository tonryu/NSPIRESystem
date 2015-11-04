using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
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
                        var agent = context.Agents.FirstOrDefault(c => c.AgentId == account.AgentId);

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
                            txtAgent.Text = agent.AgentName;
                        }
                    }
                }
            }
        }
    }
}
