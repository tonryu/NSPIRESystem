using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for CustomersForm.xaml
    /// </summary>
    public partial class CustomersForm : UserControl
    {
        public static int CustomerId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public CustomersForm()
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
                    var company = context.Customers.Where(c => customer.LeadID != 0);

                    if (customer != null)
                    {
                        lblCustomerId.Visibility = Visibility.Visible;
                        txtCustomerId.Visibility = Visibility.Visible;
                        txtCompanyName.Visibility = Visibility.Visible;
                        cbCompanyName.Visibility = Visibility.Hidden;
                        Grid.SetRow(lblFromLead, 1);
                        Grid.SetRow(tsFromLeadCheck, 1); Grid.SetColumn(tsFromLeadCheck, 1);
                        Grid.SetRow(lblCompanyName, 2);
                        Grid.SetRow(txtCompanyName, 2); Grid.SetColumn(txtCompanyName, 1);
                        Grid.SetRow(lblAddress, 3);
                        Grid.SetRow(txtAddress, 3); Grid.SetColumn(txtAddress, 1);
                        Grid.SetRow(lblPhoneNo, 4);
                        Grid.SetRow(txtPhoneNo, 4); Grid.SetColumn(txtPhoneNo, 1);
                        Grid.SetRow(lblEmail, 5);
                        Grid.SetRow(txtEmail, 5); Grid.SetColumn(txtEmail, 1);
                        Grid.SetRow(lblWebsite, 6);
                        Grid.SetRow(txtWebsite, 6); Grid.SetColumn(txtWebsite, 1);
                        Grid.SetRow(lblDateSigned, 7);
                        Grid.SetRow(txtDateSigned, 7); Grid.SetColumn(txtDateSigned, 1);

                        if (customer.LeadID > 0)
                        {
                            tsFromLeadCheck.IsChecked = true;
                        }
                        else
                        {
                            tsFromLeadCheck.IsChecked = false;
                        }
                        txtCustomerId.Text = Convert.ToString(customer.CustomerID);
                        if ((customer != null) && (tsFromLeadCheck.IsChecked.Value == true)) { var lead = context.Leads.FirstOrDefault(c => c.LeadID == customer.LeadID);
                            cbCompanyName.SelectedItem = lead.CompanyName;
                            txtAddress.Text = lead.CompanyAddress; }
                        else { txtCompanyName.Text = customer.CompanyName;
                            txtAddress.Text = customer.CompanyAddress; }
                        txtPhoneNo.Text = customer.PhoneNo;
                        txtEmail.Text = customer.Email;
                        if (customer.Website != null) { txtWebsite.Text = customer.Website; }
                        else { txtWebsite.Text = ""; }
                        txtDateSigned.Text = customer.DateSigned;
                    }
                }
                else
                {
                    lblCustomerId.Visibility = Visibility.Hidden;
                    txtCustomerId.Visibility = Visibility.Hidden;
                    lblFromLead.Visibility = Visibility.Visible;
                    tsFromLeadCheck.Visibility = Visibility.Visible;
                    txtCompanyName.Visibility = Visibility.Visible;
                    cbCompanyName.Visibility = Visibility.Hidden;
                    Grid.SetRow(lblFromLead, 0);
                    Grid.SetRow(tsFromLeadCheck, 0); Grid.SetColumn(tsFromLeadCheck, 1);
                    Grid.SetRow(lblCompanyName, 1);
                    Grid.SetRow(txtCompanyName, 1); Grid.SetColumn(txtCompanyName, 1);
                    Grid.SetRow(lblAddress, 2);
                    Grid.SetRow(txtAddress, 2); Grid.SetColumn(txtAddress, 1);
                    Grid.SetRow(lblPhoneNo, 3);
                    Grid.SetRow(txtPhoneNo, 3); Grid.SetColumn(txtPhoneNo, 1);
                    Grid.SetRow(lblEmail, 4);
                    Grid.SetRow(txtEmail, 4); Grid.SetColumn(txtEmail, 1);
                    Grid.SetRow(lblWebsite, 5);
                    Grid.SetRow(txtWebsite, 5); Grid.SetColumn(txtWebsite, 1);
                    Grid.SetRow(lblDateSigned, 6);
                    Grid.SetRow(txtDateSigned, 6); Grid.SetColumn(txtDateSigned, 1);

                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtPhoneNo.Text = "";
                    txtWebsite.Text = "";
                    cbCompanyName.SelectedItem = "";
                    txtCompanyName.Text = "";
                    txtDateSigned.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if ((txtAddress.Text != null && txtAddress.Text != "") &&
                    (txtEmail.Text != null && txtEmail.Text != "") &&
                    (txtPhoneNo.Text != null && txtPhoneNo.Text != "") &&
                    ((cbCompanyName.Text != null && cbCompanyName.Text != "") ||
                    (txtCompanyName.Text != null && txtCompanyName.Text != "")) &&
                    (txtDateSigned.Text != null && txtDateSigned.Text != ""))
                {
                    #region important variables (customer, duplicateLead)
                    var customer = new Customer();
                    var duplicateLead = context.Leads.FirstOrDefault
                        (c => c.CompanyName.ToLower().Contains(txtCompanyName.Text.ToLower()));
                    #endregion

                    if (CustomerId > 0)
                    {
                        #region edit
                        customer = context.Customers.FirstOrDefault(c => c.CustomerID == CustomerId);
                        var duplicateName = context.Customers.FirstOrDefault
                            (c => c.CompanyName.ToLower() == txtCompanyName.Text.ToLower()
                            && c.CustomerID == customer.CustomerID);

                        if (customer != null)
                        {
                            if ((duplicateName != null) && (duplicateLead == null))
                            {
                                if (tsFromLeadCheck.IsChecked.Value != true)
                                {
                                    customer.CompanyAddress = txtAddress.Text;
                                    customer.CompanyName = txtCompanyName.Text;
                                    customer.LeadID = 0;
                                }
                                else
                                {
                                    var lead = context.Leads.FirstOrDefault(c => c.LeadID == customer.LeadID);

                                    customer.CompanyAddress = lead.CompanyAddress;
                                    customer.CompanyName = lead.CompanyName;
                                    customer.LeadID = lead.LeadID;
                                }
                                customer.Email = txtEmail.Text;
                                customer.PhoneNo = txtPhoneNo.Text;
                                if (txtWebsite.Text != null && txtWebsite.Text != "") { customer.Website = txtWebsite.Text; }
                                else { customer.Website = ""; }
                                customer.DateSigned = Convert.ToDateTime(txtDateSigned.Text).ToString("MM/dd/yyyy");

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " modifies " +
                                    customer.CompanyName + "'s details.";
                                context.Logs.Add(log);

                                context.SaveChanges();

                                var window = new NoticeWindow();
                                NoticeWindow.message = "Customer details successfully updated";
                                window.Height = 0;
                                window.Top = screenTopEdge + 8;
                                window.Left = (screenWidth / 2) - (window.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                                window.ShowDialog();
                            }
                            else
                            {
                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " fails to modify " +
                                    customer.CompanyName + "'s details because an existing name is detected.";
                                context.Logs.Add(log);

                                context.SaveChanges();

                                var window = new NoticeWindow();
                                NoticeWindow.message = "A similar name already exists";
                                window.Height = 0;
                                window.Top = screenTopEdge + 8;
                                window.Left = (screenWidth / 2) - (window.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                                window.ShowDialog();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region add
                        if (duplicateLead == null)
                        {
                            if (tsFromLeadCheck.IsChecked.Value != false)
                            {
                                var lead = context.Leads.FirstOrDefault
                                    (c => c.CompanyName.ToLower() == cbCompanyName.Text.ToLower());
                                customer.CompanyAddress = lead.CompanyAddress;
                                customer.CompanyName = lead.CompanyName;
                                customer.LeadID = lead.LeadID;
                            }
                            else
                            {
                                customer.CompanyAddress = txtAddress.Text;
                                customer.CompanyName = txtCompanyName.Text;
                                customer.LeadID = 0;
                            }
                            customer.DateSigned = Convert.ToDateTime(txtDateSigned.Text).ToString("MM/dd/yyyy");
                            customer.Email = txtEmail.Text;
                            customer.PhoneNo = txtPhoneNo.Text;
                            if (txtWebsite.Text != null && txtWebsite.Text != "") { customer.Website = txtWebsite.Text; }
                            else { customer.Website = ""; }
                            context.Customers.Add(customer);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " creates a new customer. (" +
                                customer.CompanyName + ")";
                            context.Logs.Add(log);

                            var window = new NoticeWindow();
                            NoticeWindow.message = "Customer successfully created";
                            window.Height = 0;
                            window.Top = screenTopEdge + 8;
                            window.Left = (screenWidth / 2) - (window.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                            window.ShowDialog();

                            context.SaveChanges();
                        }
                        else
                        {
                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " fails to create a new customer "
                            + " because an existing name has been detected.";
                            context.Logs.Add(log);

                            context.SaveChanges();

                            var window = new NoticeWindow();
                            NoticeWindow.message = "The name already exists";
                            window.Height = 0;
                            window.Top = screenTopEdge + 8;
                            window.Left = (screenWidth / 2) - (window.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                            window.ShowDialog();
                        }
                        #endregion
                    }
                }
                else
                {
                    var window = new NoticeWindow();
                    NoticeWindow.message = "Please fill all boxes labeled with an asterisk(*) symbol.";
                    window.Height = 0;
                    window.Top = screenTopEdge + 8;
                    window.Left = (screenWidth / 2) - (window.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                    window.ShowDialog();
                }
            }
        }

        private void tsFromLeadCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (CustomerId > 0)
            {
                using (var context = new DatabaseContext())
                {
                    var leads = context.Leads.ToList();

                    if (leads != null)
                    {
                        cbCompanyName.ItemsSource = null;
                        cbCompanyName.ItemsSource = leads.Where
                            (c => c.Status == "New Customer").Select(c => c.CompanyName).ToList();
                    }
                    else { cbCompanyName.ItemsSource = null; }
                }
                if (txtCompanyName.Visibility != Visibility.Hidden) { 
                    txtCompanyName.Visibility = Visibility.Hidden; }
                cbCompanyName.Visibility = Visibility.Visible;

                Grid.SetRow(lblFromLead, 1);
                Grid.SetRow(tsFromLeadCheck, 1); Grid.SetColumn(tsFromLeadCheck, 1);
                Grid.SetRow(lblCompanyName, 2);
                Grid.SetRow(cbCompanyName, 2); Grid.SetColumn(cbCompanyName, 1);
                Grid.SetRow(lblAddress, 3);
                Grid.SetRow(txtAddress, 3); Grid.SetColumn(txtAddress, 1);
                Grid.SetRow(lblPhoneNo, 4);
                Grid.SetRow(txtPhoneNo, 4); Grid.SetColumn(txtPhoneNo, 1);
                Grid.SetRow(lblEmail, 5);
                Grid.SetRow(txtEmail, 5); Grid.SetColumn(txtEmail, 1);
                Grid.SetRow(lblWebsite, 6);
                Grid.SetRow(txtWebsite, 6); Grid.SetColumn(txtWebsite, 1);

                cbCompanyName.SelectedItem = "";
            }
            else
            {
                using (var context = new DatabaseContext())
                {
                    var leads = context.Leads.ToList();

                    if (leads != null)
                    {
                        cbCompanyName.ItemsSource = null;
                        cbCompanyName.ItemsSource = leads.Where(c => c.Status == "New Customer").
                            Select(c => c.CompanyName).ToList();
                    }
                    else { cbCompanyName.ItemsSource = null; }
                }
                if (txtCompanyName.Visibility != Visibility.Hidden) { 
                    txtCompanyName.Visibility = Visibility.Hidden; }
                cbCompanyName.Visibility = Visibility.Visible;

                Grid.SetRow(lblFromLead, 0);
                Grid.SetRow(tsFromLeadCheck, 0); Grid.SetColumn(tsFromLeadCheck, 1);
                Grid.SetRow(lblCompanyName, 1);
                Grid.SetRow(cbCompanyName, 1); Grid.SetColumn(cbCompanyName, 1);
                Grid.SetRow(lblAddress, 2);
                Grid.SetRow(txtAddress, 2); Grid.SetColumn(txtAddress, 1);
                Grid.SetRow(lblPhoneNo, 3);
                Grid.SetRow(txtPhoneNo, 3); Grid.SetColumn(txtPhoneNo, 1);
                Grid.SetRow(lblEmail, 4);
                Grid.SetRow(txtEmail, 4); Grid.SetColumn(txtEmail, 1);
                Grid.SetRow(lblWebsite, 5);
                Grid.SetRow(txtWebsite, 5); Grid.SetColumn(txtWebsite, 1);

                cbCompanyName.SelectedItem = "";
            }
        }

        private void tsFromLeadCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CustomerId > 0)
            {
                txtCompanyName.Visibility = Visibility.Visible;
                if (cbCompanyName.Visibility != Visibility.Hidden) { 
                    cbCompanyName.Visibility = Visibility.Hidden; }

                Grid.SetRow(lblFromLead, 1);
                Grid.SetRow(tsFromLeadCheck, 1); Grid.SetColumn(tsFromLeadCheck, 1);
                Grid.SetRow(lblCompanyName, 2);
                Grid.SetRow(txtCompanyName, 2); Grid.SetColumn(txtCompanyName, 1);
                Grid.SetRow(lblAddress, 3);
                Grid.SetRow(txtAddress, 3); Grid.SetColumn(txtAddress, 1);
                Grid.SetRow(lblPhoneNo, 4);
                Grid.SetRow(txtPhoneNo, 4); Grid.SetColumn(txtPhoneNo, 1);
                Grid.SetRow(lblEmail, 5);
                Grid.SetRow(txtEmail, 5); Grid.SetColumn(txtEmail, 1);
                Grid.SetRow(lblWebsite, 6);
                Grid.SetRow(txtWebsite, 6); Grid.SetColumn(txtWebsite, 1);

                txtCompanyName.Text = "";
            }
            else
            {
                txtCompanyName.Visibility = Visibility.Visible;
                if (cbCompanyName.Visibility != Visibility.Hidden) { 
                    cbCompanyName.Visibility = Visibility.Hidden; }

                Grid.SetRow(lblFromLead, 0);
                Grid.SetRow(tsFromLeadCheck, 0); Grid.SetColumn(tsFromLeadCheck, 1);
                Grid.SetRow(lblCompanyName, 1);
                Grid.SetRow(txtCompanyName, 1); Grid.SetColumn(txtCompanyName, 1);
                Grid.SetRow(lblAddress, 2);
                Grid.SetRow(txtAddress, 2); Grid.SetColumn(txtAddress, 1);
                Grid.SetRow(lblPhoneNo, 3);
                Grid.SetRow(txtPhoneNo, 3); Grid.SetColumn(txtPhoneNo, 1);
                Grid.SetRow(lblEmail, 4);
                Grid.SetRow(txtEmail, 4); Grid.SetColumn(txtEmail, 1);
                Grid.SetRow(lblWebsite, 5);
                Grid.SetRow(txtWebsite, 5); Grid.SetColumn(txtWebsite, 1);

                txtCompanyName.Text = "";
            }
        }
    }
}
