using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Forms;
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
        public static bool isSelectFinish = false;

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
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "No territories";
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
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "No sales stage statuses";
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
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "No marketing strategies";
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
                    var leadsProducts = context.LeadsProducts.Where(c => c.LeadId == lead.LeadID).ToList(); ;

                    if (lead != null)
                    {
                        lblLeadId.Visibility = Visibility.Visible;
                        txtLeadId.Visibility = Visibility.Visible;
                        Grid.SetRow(lblCompanyName, 1);
                        Grid.SetRow(txtCompanyName, 1); Grid.SetColumn(txtCompanyName, 1);
                        Grid.SetRow(lblAddress, 2);
                        Grid.SetRow(txtCompanyAddress, 2); Grid.SetColumn(txtCompanyAddress, 1);
                        Grid.SetRow(lblTerritory, 3);
                        Grid.SetRow(cbTerritory, 3); Grid.SetColumn(cbTerritory, 1);
                        Grid.SetRow(lblSalesStageStatus, 4);
                        Grid.SetRow(cbStatus, 4); Grid.SetColumn(cbStatus, 1);
                        Grid.SetRow(lblMarketingStrategy, 5);
                        Grid.SetRow(cbMarketingStrategy, 5); Grid.SetColumn(cbMarketingStrategy, 1);
                        Grid.SetRow(lblProduct, 6);
                        Grid.SetRow(gridProduct, 6); Grid.SetColumn(gridProduct, 1);
                        Grid.SetRow(lblActiveCheck, 7);
                        Grid.SetRow(tsActiveCheck, 7); Grid.SetColumn(tsActiveCheck, 1);

                        txtLeadId.Text = Convert.ToString(lead.LeadID);
                        txtCompanyAddress.Text = lead.CompanyAddress;
                        txtCompanyName.Text = lead.CompanyName;
                        cbTerritory.SelectedItem = territory.TerritoryName;
                        cbStatus.SelectedItem = lead.Status;
                        tsActiveCheck.IsChecked = lead.IsActive;
                        if (marketingStrategy != null) { cbMarketingStrategy.SelectedItem = marketingStrategy.Description; }
                        else { cbMarketingStrategy.SelectedItem = null; }
                        if (leadsProducts != null)
                        {
                            foreach (var leadsProduct in leadsProducts)
                            {
                                lbLeadsProducts.Items.Add(leadsProduct);
                            }
                        }
                        else { lbLeadsProducts.Items.Clear(); }
                    }
                }
                else
                {
                    lblLeadId.Visibility = Visibility.Hidden;
                    txtLeadId.Visibility = Visibility.Hidden;
                    Grid.SetRow(lblCompanyName, 0);
                    Grid.SetRow(txtCompanyName, 0); Grid.SetColumn(txtCompanyName, 1);
                    Grid.SetRow(lblAddress, 1);
                    Grid.SetRow(txtCompanyAddress, 1); Grid.SetColumn(txtCompanyAddress, 1);
                    Grid.SetRow(lblTerritory, 2);
                    Grid.SetRow(cbTerritory, 2); Grid.SetColumn(cbTerritory, 1);
                    Grid.SetRow(lblSalesStageStatus, 3);
                    Grid.SetRow(cbStatus, 3); Grid.SetColumn(cbStatus, 1);
                    Grid.SetRow(lblMarketingStrategy, 4);
                    Grid.SetRow(cbMarketingStrategy, 4); Grid.SetColumn(cbMarketingStrategy, 1);
                    Grid.SetRow(lblProduct, 5);
                    Grid.SetRow(gridProduct, 5); Grid.SetColumn(gridProduct, 1);
                    Grid.SetRow(lblActiveCheck, 6);
                    Grid.SetRow(tsActiveCheck, 6); Grid.SetColumn(tsActiveCheck, 1);

                    txtCompanyAddress.Text = "";
                    txtCompanyName.Text = "";
                    cbStatus.SelectedItem = null;
                    cbTerritory.SelectedItem = null;
                    tsActiveCheck.IsChecked = true;
                    cbMarketingStrategy.SelectedItem = null;
                    if (isSelectFinish != true)
                    {
                        lbLeadsProducts.Items.Clear();
                    }
                }
            }

            #region selection finish
            if (isSelectFinish != false)
            {
                if (Variables.yesClicked == true)
                {
                    using (var context = new DatabaseContext())
                    {
                        var product = ProductSelection.passList;

                        lbLeadsProducts.Items.Add(product);
                        isSelectFinish = false;
                        Variables.yesClicked = false;
                    }
                }
            }
            #endregion
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
                        #region edit
                        territory = context.Territories.FirstOrDefault
                            (c => c.TerritoryName.ToLower() == cbTerritory.Text.ToLower());
                        lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);

                        if (lead != null)
                        {
                            var duplicateName = context.Leads.FirstOrDefault
                                (c => (c.LeadID == lead.LeadID) && 
                                    (c.CompanyName.ToLower() == txtCompanyName.Text.ToLower()));
                            var existingLead = context.Leads.FirstOrDefault
                                (c => c.CompanyName.ToLower() == txtCompanyName.Text.ToLower());

                            if (duplicateName != null || existingLead == null)
                            {
                                var marketingStrategy = context.MarketingStrategies.FirstOrDefault(c => c.Description.ToLower() == cbMarketingStrategy.Text.ToLower());

                                lead.CompanyAddress = txtCompanyAddress.Text;
                                lead.CompanyName = txtCompanyName.Text;
                                lead.Status = cbStatus.Text;
                                lead.TerritoryID = territory.TerritoryID;
                                lead.IsActive = tsActiveCheck.IsChecked.Value;
                                if (marketingStrategy != null) { lead.MarketingStrategyId = marketingStrategy.MarketingStrategyId; }
                                else { lead.MarketingStrategyId = 0; }

                                if (lbLeadsProducts != null)
                                {
                                    foreach (var product in lbLeadsProducts.Items)
                                    {
                                        var productId = context.Products.FirstOrDefault(c => c.ProductName == (string) product);

                                        if (productId != null)
                                        {
                                            var products = context.LeadsProducts.FirstOrDefault(c => c.ProductId == productId.ProductID && c.LeadId == LeadId);

                                            if (products != null)
                                            {
                                                products.LeadId = LeadId;
                                                products.ProductId = productId.ProductID;
                                            }
                                        }
                                    }
                                }

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " modifies "
                                    + lead.CompanyName + "'s details.";
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
                                NoticeWindow.message = "Similar lead detected";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " fails to modify "
                                    + lead.CompanyName + " due to a similar lead is detected.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);
                                context.SaveChanges();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region add
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
                            lead.IsActive = tsActiveCheck.IsChecked.Value;
                            lead.DateAdded = DateTime.Now.ToString("MM/dd/yyyy");
                            if (marketingStrategy != null) { lead.MarketingStrategyId = marketingStrategy.MarketingStrategyId; }
                            else { lead.MarketingStrategyId = 0; }
                            if (lbLeadsProducts != null)
                            {
                                foreach (var product in lbLeadsProducts.Items)
                                {
                                    var productId = context.Products.FirstOrDefault(c => c.ProductName == (string) product);

                                    if (productId != null)
                                    {
                                        var products = context.LeadsProducts.FirstOrDefault(c => c.ProductId == productId.ProductID && c.LeadId == LeadId);

                                        if (products == null)
                                        {
                                            products = new LeadsProduct();

                                            context.LeadsProducts.Add(new LeadsProduct
                                            {
                                                ProductId = productId.ProductID,
                                                LeadId = products.LeadId
                                            });
                                        }
                                    }
                                }
                            }
                            context.Leads.Add(lead);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " creates a new lead. ("
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
                            log.Description = NotificationWindow.username + " fails to create a new lead due to the lead is already existing.";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);
                            context.SaveChanges();

                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Lead already exists";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        #endregion
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Please provide all boxes labeled with an asterisk(*).";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new ProductSelection();
            frame.Navigate(page);
        }
    }
}
