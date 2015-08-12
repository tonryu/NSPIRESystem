using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Windows
{
    /// <summary>
    /// Interaction logic for LeadActivityWindow.xaml
    /// </summary>
    public partial class LeadActivityWindow : Window
    {
        public static int ActivityId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public LeadActivityWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = new Lead();

                cbLead.ItemsSource = null;
                cbLead.ItemsSource = context.Leads.Select(c => c.CompanyName).ToList();

                //if (LeadId > 0)
                //{
                //    lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);
                //    territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);

                //    if (lead != null)
                //    {
                //        lblLeadId.Visibility = Visibility.Visible;
                //        txtLeadId.Visibility = Visibility.Visible;
                //        txtLeadId.Text = Convert.ToString(lead.LeadID);
                //        txtCompanyAddress.Text = lead.CompanyAddress;
                //        txtCompanyName.Text = lead.CompanyName;
                //        txtName.Text = lead.ContactPersonName;
                //        txtPhoneNo.Text = lead.PhoneNo;
                //        txtPosition.Text = lead.Position;
                //        cbTerritory.SelectedItem = territory.TerritoryName;
                //        cbStatus.Text = lead.Status;
                //    }
                //}
                //else
                //{
                //    lblLeadId.Visibility = Visibility.Hidden;
                //    txtLeadId.Visibility = Visibility.Hidden;
                //    txtCompanyAddress.Text = "";
                //    txtCompanyName.Text = "";
                //    txtName.Text = "";
                //    txtPhoneNo.Text = "";
                //    txtPosition.Text = "";
                //    cbStatus.SelectedItem = "";
                //    cbTerritory.SelectedItem = null;
                //}
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = new Lead();
                var territory = new Territory();

                //if (txtCompanyAddress.Text != "" && txtCompanyName.Text != ""
                //        && txtName.Text != "" && txtPhoneNo.Text != "" && txtPosition.Text != ""
                //        && (cbStatus.Text != "" || cbStatus.Text != null)
                //        && (cbTerritory.Text != "" || cbTerritory.Text != null))
                //{
                //    if (LeadId > 0)
                //    {
                //        territory = context.Territories.FirstOrDefault
                //            (c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                //        lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                //        if (lead != null)
                //        {
                //            var leadName = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                //            if (leadName != null)
                //            {
                //                if (lead.CompanyName == leadName.CompanyName
                //                    && lead.CompanyAddress == leadName.CompanyAddress
                //                    && lead.ContactPersonName == leadName.ContactPersonName)
                //                {
                //                    lead.CompanyAddress = txtCompanyAddress.Text;
                //                    lead.CompanyName = txtCompanyName.Text;
                //                    lead.ContactPersonName = txtName.Text;
                //                    lead.PhoneNo = txtPhoneNo.Text;
                //                    lead.Position = txtPosition.Text;
                //                    lead.Status = cbStatus.Text;
                //                    lead.TerritoryID = territory.TerritoryID;

                //                    context.SaveChanges();
                //                    var windows = new Shared.Windows.NoticeWindow();
                //                    Shared.Windows.NoticeWindow.message = "LEAD SUCCESSFULLY UPDATED";
                //                    windows.Height = 0;
                //                    windows.Top = screenTopEdge + 8;
                //                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                //                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                //                    windows.ShowDialog();
                //                    DialogResult = false;
                //                }
                //                else
                //                {
                //                    var windows = new Shared.Windows.NoticeWindow();
                //                    Shared.Windows.NoticeWindow.message = "SIMILAR LEAD DETECTED";
                //                    windows.Height = 0;
                //                    windows.Top = screenTopEdge + 8;
                //                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                //                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                //                    windows.ShowDialog();
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        territory = context.Territories.FirstOrDefault
                //            (c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                //        lead = context.Leads.FirstOrDefault
                //            (c => c.CompanyName.ToLower() == txtCompanyName.Text.ToLower()
                //            && c.CompanyAddress.ToLower() == txtCompanyAddress.Text.ToLower()
                //            && c.TerritoryID == territory.TerritoryID);

                //        if (lead == null)
                //        {
                //            lead = new Lead();

                //            lead.CompanyAddress = txtCompanyAddress.Text;
                //            lead.CompanyName = txtCompanyName.Text;
                //            lead.ContactPersonName = txtName.Text;
                //            lead.PhoneNo = txtPhoneNo.Text;
                //            lead.Position = txtPosition.Text;
                //            lead.Status = cbStatus.Text;
                //            lead.TerritoryID = territory.TerritoryID;

                //            context.Leads.Add(lead);
                //            context.SaveChanges();
                //            var windows = new Shared.Windows.NoticeWindow();
                //            Shared.Windows.NoticeWindow.message = "LEAD SUCCESSFULLY CREATED";
                //            windows.Height = 0;
                //            windows.Top = screenTopEdge + 8;
                //            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                //            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                //            windows.ShowDialog();
                //            DialogResult = false;
                //        }
                //        else
                //        {
                //            var windows = new Shared.Windows.NoticeWindow();
                //            Shared.Windows.NoticeWindow.message = "LEAD ALREADY EXIST";
                //            windows.Height = 0;
                //            windows.Top = screenTopEdge + 8;
                //            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                //            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                //            windows.ShowDialog();
                //        }
                //    }
                //}
                //else
                //{
                //    var windows = new Shared.Windows.NoticeWindow();
                //    Shared.Windows.NoticeWindow.message = "PLEASE PROVIDE ALL ASSOCIATED WITH ASTERISKS(*)";
                //    windows.Height = 0;
                //    windows.Top = screenTopEdge + 8;
                //    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                //    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                //    windows.ShowDialog();
                //}
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
    }
}
