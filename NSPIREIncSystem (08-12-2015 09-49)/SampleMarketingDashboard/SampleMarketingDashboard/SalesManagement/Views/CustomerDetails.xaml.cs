using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : UserControl
    {
        public static int CustomerId;

        public CustomerDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (CustomerId > 0)
                {
                    var customer = context.Customers.FirstOrDefault(c => c.CustomerID == CustomerId);

                    if (customer != null)
                    {
                        txtAddress.Text = customer.CompanyAddress;
                        txtCompanyName.Text = customer.CompanyName;
                        txtCustomerId.Text = Convert.ToString(customer.CustomerID);
                        txtEmail.Text = customer.Email;
                        if (customer.LeadID > 0) { txtFromLead.Text = "YES"; }
                        else { txtFromLead.Text = "NO"; }
                        txtPhoneNo.Text = customer.PhoneNo;
                        txtWebsite.Text = customer.Website;
                    }
                }
            }
        }
    }
}
