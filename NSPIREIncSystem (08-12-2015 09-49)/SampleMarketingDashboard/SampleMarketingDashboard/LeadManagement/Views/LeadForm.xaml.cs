using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

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
                var marketingStrategies = context.MarketingStrategies.Select(c => c.Description).ToList();

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
                    Shared.Windows.NoticeWindow.message = "Sales Stage Status is empty.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }

                cbMarketingStrategy.ItemsSource = null;
                if (marketingStrategies != null) { cbMarketingStrategy.ItemsSource = marketingStrategies; }
                else
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "Marketing Strategy is empty.";
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
                    var marketingStrategy = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == lead.MarketingStrategyId);

                    if (lead != null)
                    {
                        lblLeadId.Visibility = Visibility.Visible;
                        txtLeadId.Visibility = Visibility.Visible;
                        txtLeadId.Text = Convert.ToString(lead.LeadID);
                        txtCompanyAddress.Text = lead.CompanyAddress;
                        txtCompanyName.Text = lead.CompanyName;
                        cbTerritory.SelectedItem = territory.TerritoryName;
                        cbStatus.Text = lead.Status;
                        lblActiveCheck.Visibility = Visibility.Visible;
                        tsActiveCheck.Visibility = Visibility.Visible;
                        tsActiveCheck.IsChecked = lead.IsActive;
                        if (marketingStrategy != null) { cbMarketingStrategy.SelectedItem = marketingStrategy.Description; }
                        else { cbMarketingStrategy.SelectedItem = null; }
                    }
                }
                else
                {
                    lblLeadId.Visibility = Visibility.Hidden;
                    txtLeadId.Visibility = Visibility.Hidden;
                    txtCompanyAddress.Text = "";
                    txtCompanyName.Text = "";
                    cbStatus.SelectedItem = null;
                    cbTerritory.SelectedItem = null;
                    lblActiveCheck.Visibility = Visibility.Hidden;
                    tsActiveCheck.Visibility = Visibility.Hidden;
                    cbMarketingStrategy.SelectedItem = null;
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
                        && (cbTerritory.Text != "" || cbTerritory.Text != null)
                        && (cbMarketingStrategy.Text!="" || cbMarketingStrategy.Text != null))
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
                                    var marketingStrategy = context.MarketingStrategies.FirstOrDefault(c => c.Description.ToLower() == cbMarketingStrategy.Text.ToLower());

                                    lead.CompanyAddress = txtCompanyAddress.Text;
                                    lead.CompanyName = txtCompanyName.Text;
                                    lead.Status = cbStatus.Text;
                                    lead.TerritoryID = territory.TerritoryID;
                                    lead.IsActive = tsActiveCheck.IsChecked.Value;
                                    if (marketingStrategy != null) { lead.MarketingStrategyId = marketingStrategy.MarketingStrategyId; }
                                    else { lead.MarketingStrategyId = 0; }

                                    var log = new Log();
                                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                    log.Description = NotificationWindow.username + " modified "
                                        + lead.CompanyName + ".";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);

                                    context.SaveChanges();
                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Lead successfully updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                                else
                                {
                                    var windows = new NoticeWindow();
                                    NoticeWindow.message = "Similar Lead detected";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();

                                    var log = new Log();
                                    log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                    log.Description = NotificationWindow.username + " failed to modify "
                                        + lead.CompanyName + " due to a similar lead is detected.";
                                    log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                    context.Logs.Add(log);
                                    context.SaveChanges();
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

                            var marketingStrategy = context.MarketingStrategies.FirstOrDefault(c => c.Description.ToLower() == cbMarketingStrategy.Text.ToLower());

                            lead.CompanyAddress = txtCompanyAddress.Text;
                            lead.CompanyName = txtCompanyName.Text;
                            lead.Status = cbStatus.Text;
                            lead.TerritoryID = territory.TerritoryID;
                            lead.IsActive = true;
                            lead.DateAdded = DateTime.Now.ToString("MM/dd/yyyy");
                            if (marketingStrategy != null) { lead.MarketingStrategyId = marketingStrategy.MarketingStrategyId; }
                            else { lead.MarketingStrategyId = 0; }
                            context.Leads.Add(lead);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " created a lead. ("
                                + txtCompanyName.Text + ")";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);

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
                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " failed to create a lead due to the lead is already existing.";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);
                            context.SaveChanges();

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
                    Shared.Windows.NoticeWindow.message = "Please provide all fields associated with an asterisk(*).";
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
