using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for LeadForm.xaml
    /// </summary>
    public partial class LeadForm : UserControl
    {
        public static int LeadId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public LeadForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = new Lead();
                var territory = new Territory();
                var salesStage = context.SalesStages.OrderBy(c => c.RankNo).Select(c => c.SalesStageName).ToList();
                var territories = context.Territories.Select(c => c.TerritoryName).ToList();

                cbTerritory.ItemsSource = null;
                if (territories != null) { cbTerritory.ItemsSource = territories; }
                else
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "Territories are empty.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                cbStatus.ItemsSource = null;
                if (salesStage != null) { cbStatus.ItemsSource = salesStage; }
                else
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "Sales Stage Status are empty.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                if (LeadId > 0)
                {
                    lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);
                    territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);

                    if (lead != null)
                    {
                        lblLeadId.Visibility = Visibility.Visible;
                        txtLeadId.Visibility = Visibility.Visible;
                        txtLeadId.Text = Convert.ToString(lead.LeadID);
                        txtCompanyAddress.Text = lead.CompanyAddress;
                        txtCompanyName.Text = lead.CompanyName;
                        cbTerritory.SelectedItem = territory.TerritoryName;
                        cbStatus.Text = lead.Status;
                    }
                }
                else
                {
                    lblLeadId.Visibility = Visibility.Hidden;
                    txtLeadId.Visibility = Visibility.Hidden;
                    txtCompanyAddress.Text = "";
                    txtCompanyName.Text = "";
                    cbStatus.SelectedItem = "";
                    cbTerritory.SelectedItem = null;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = new Lead();
                var territory = new Territory();

                if(txtCompanyAddress.Text != "" && txtCompanyName.Text != "" 
                        && (cbStatus.Text != "" || cbStatus.Text != null) 
                        && (cbTerritory.Text != "" || cbTerritory.Text != null))
                {
                    if (LeadId > 0)
                    {
                        territory = context.Territories.FirstOrDefault
                            (c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                        lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                        if (lead != null)
                        {
                            var leadName = context.Leads.FirstOrDefault
                                (c => c.CompanyName.ToLower() == txtCompanyName.Text.ToLower());

                            if (leadName != null)
                            {
                                if (lead.CompanyName.ToLower() == leadName.CompanyName.ToLower()
                                    && lead.CompanyAddress.ToLower() == leadName.CompanyAddress.ToLower())
                                {
                                    lead.CompanyAddress = txtCompanyAddress.Text;
                                    lead.CompanyName = txtCompanyName.Text;
                                    lead.Status = cbStatus.Text;
                                    lead.TerritoryID = territory.TerritoryID;

                                    context.SaveChanges();
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Lead successfully updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                                else
                                {
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Similar Lead detected";
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
                        territory = context.Territories.FirstOrDefault
                            (c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                        lead = context.Leads.FirstOrDefault
                            (c => c.CompanyName.ToLower() == txtCompanyName.Text.ToLower()
                            && (c.CompanyAddress.ToLower() == txtCompanyAddress.Text.ToLower()
                            || c.TerritoryID == territory.TerritoryID));

                        if (lead == null)
                        {
                            lead = new Lead();

                            lead.CompanyAddress = txtCompanyAddress.Text;
                            lead.CompanyName = txtCompanyName.Text;
                            lead.Status = cbStatus.Text;
                            lead.TerritoryID = territory.TerritoryID;

                            context.Leads.Add(lead);
                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Lead successfully created";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        else
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Lead already exist";
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
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "PLEASE PROVIDE ALL ASSOCIATED WITH ASTERISKS(*)";
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
