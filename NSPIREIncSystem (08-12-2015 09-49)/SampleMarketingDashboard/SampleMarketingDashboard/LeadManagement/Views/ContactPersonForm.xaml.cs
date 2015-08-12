using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for ContactPersonForm.xaml
    /// </summary>
    public partial class ContactPersonForm : UserControl
    {
        public static int ContactId;
        public static int LeadId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public ContactPersonForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using(var context = new DatabaseContext())
            {
                var contact = new Contact();

                if (ContactId > 0)
                {
                    contact = context.Contacts.FirstOrDefault(c => c.ContactID == ContactId);

                    if (contact != null)
                    {
                        lblContactId.Visibility = Visibility.Visible;
                        txtContactId.Visibility = Visibility.Visible;
                        Grid.SetRow(lblContactName, 1); Grid.SetColumn(lblContactName, 0);
                        Grid.SetRow(txtContactName, 1); Grid.SetColumn(txtContactName, 1);
                        Grid.SetRow(lblPosition, 2); Grid.SetColumn(lblPosition, 0);
                        Grid.SetRow(txtPosition, 2); Grid.SetColumn(txtPosition, 1);
                        Grid.SetRow(lblPhoneNo, 3); Grid.SetColumn(lblPhoneNo, 0);
                        Grid.SetRow(txtPhoneNo, 3); Grid.SetColumn(txtPhoneNo, 1);
                        txtContactId.Text = Convert.ToString(contact.ContactID);
                        txtContactName.Text = contact.ContactPersonName;
                        txtPosition.Text = contact.Position;
                        txtPhoneNo.Text = contact.PhoneNo;
                    }
                }
                else
                {
                    lblContactId.Visibility = Visibility.Hidden;
                    txtContactId.Visibility = Visibility.Hidden;
                    Grid.SetRow(lblContactName, 0); Grid.SetColumn(lblContactName, 0);
                    Grid.SetRow(txtContactName, 0); Grid.SetColumn(txtContactName, 1);
                    Grid.SetRow(lblPosition, 1); Grid.SetColumn(lblPosition, 0);
                    Grid.SetRow(txtPosition, 1); Grid.SetColumn(txtPosition, 1);
                    Grid.SetRow(lblPhoneNo, 2); Grid.SetColumn(lblPhoneNo, 0);
                    Grid.SetRow(txtPhoneNo, 2); Grid.SetColumn(txtPhoneNo, 1);
                    txtContactName.Text = "";
                    txtPosition.Text = "";
                    txtPhoneNo.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var contact = new Contact();

                if (ContactId > 0)
                {
                    contact = context.Contacts.FirstOrDefault(c => c.ContactID == ContactId);

                    if (contact != null)
                    {
                        var checkName = context.Contacts.FirstOrDefault
                            (c => c.ContactPersonName.ToLower() == txtContactName.Text.ToLower());

                        if (checkName == null 
                            || checkName.ContactID == contact.ContactID)
                        {
                            if ((txtContactName.Text != null || txtContactName.Text != "")
                                && (txtPhoneNo.Text != null || txtPhoneNo.Text != "")
                                && (txtPosition.Text != null || txtPosition.Text != ""))
                            {
                                contact.ContactPersonName = txtContactName.Text;
                                contact.PhoneNo = txtPhoneNo.Text;
                                contact.Position = txtPosition.Text;

                                context.SaveChanges();
                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Contact person successfully updated";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                            else
                            {
                                var windows = new Shared.Windows.NoticeWindow();
                                NoticeWindow.message = "Please provide all associated with an asterisk.";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                        else
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            NoticeWindow.message = "A similar name already exist.";
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
                    var checkName = context.Contacts.FirstOrDefault
                        (c => c.ContactPersonName.ToLower() == txtContactName.Text.ToLower());

                    if (checkName == null)
                    {
                        if ((txtContactName.Text != null || txtContactName.Text != "")
                            && (txtPhoneNo.Text != null || txtPhoneNo.Text != "")
                            && (txtPosition.Text != null || txtPosition.Text != ""))
                        {
                            contact.ContactPersonName = txtContactName.Text;
                            contact.LeadId = LeadId;
                            contact.PhoneNo = txtPhoneNo.Text;
                            contact.Position = txtPosition.Text;

                            context.Contacts.Add(contact);
                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            NoticeWindow.message = "Contact person successfully created";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        else
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            NoticeWindow.message = "Please provide all associated with an asterisk.";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                    }
                    else
                    {
                        var windows = new Shared.Windows.NoticeWindow();
                        NoticeWindow.message = "The contact person already exist.";
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
}
