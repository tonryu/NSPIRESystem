using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for AgentsDetails.xaml
    /// </summary>
    public partial class AgentsDetails : UserControl
    {
        public static int AgentId;

        public AgentsDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var agent = context.Agents.FirstOrDefault(c => c.AgentId == AgentId);

                if (agent != null)
                {
                    txtContactNo.Text = agent.ContactNo;
                    txtAgentId.Text = Convert.ToString(agent.AgentId);
                    txtAgentName.Text = agent.AgentName;
                    if (agent.IsEmployee != false) { txtIsEmployee.Text = "YES"; }
                    else { txtIsEmployee.Text = "NO"; }
                    txtPosition.Text = agent.Position;
                    txtTerritory.Text = agent.Territory;
                }
            }
        }
    }
}
