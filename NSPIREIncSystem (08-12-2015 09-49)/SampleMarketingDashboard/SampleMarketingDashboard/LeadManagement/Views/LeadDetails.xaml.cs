using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for LeadDetails.xaml
    /// </summary>
    public partial class LeadDetails : UserControl
    {
        public static int LeadId;
        double screenLeftEdge = Application.Current.MainWindow.Left;

        public LeadDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);
                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);
                var marketingStrategy = context.MarketingStrategies.FirstOrDefault(c => c.MarketingStrategyId == lead.MarketingStrategyId);

                txtCompanyAddress.Text = lead.CompanyAddress;
                txtCompanyName.Text = lead.CompanyName;
                txtLeadId.Text = Convert.ToString(lead.LeadID);
                txtStatus.Text = lead.Status;
                txtTerritory.Text = territory.TerritoryName;
                txtMarketingStrategy.Text = marketingStrategy.Description;
                if (lead.IsActive == true)
                {
                    txtActiveCheck.Text = "YES";
                }
                else
                {
                    txtActiveCheck.Text = "NO";
                }
            }
        }
    }
}
