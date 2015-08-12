using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for LeadActivityDetails.xaml
    /// </summary>
    public partial class LeadActivityDetails : UserControl
    {
        public static int ActivityId;

        public LeadActivityDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var activity = context.LeadActivities.FirstOrDefault(c => c.ActivityID == ActivityId);
                var contact = context.Contacts.FirstOrDefault(c => c.ContactID == activity.ContacId);

                txtActivityDate.Text = activity.ActivityDate;
                txtActivityId.Text = Convert.ToString(activity.ActivityID);
                txtActivityTime.Text = activity.ActivityTime;
                txtClientResponse.Text = activity.ClientResponse;
                txtCost.Text = Convert.ToString(activity.Cost);
                txtDescription.Text = activity.Description;
                txtMarketingVoucher.Text = activity.MarketingVoucherNo;
                txtNextStep.Text = activity.NextStep;
                txtNextStepDue.Text = activity.DueDateOfNextStep;
                txtSalesRep.Text = activity.SalesRep;
                txtTransactionDetails.Text = activity.DetailsOfTransaction;
                txtContact.Text = contact.ContactPersonName;
                if (activity.IsFinalized != false) { txtFinalizedCheck.Text = "YES"; }
                else { txtFinalizedCheck.Text = "NO"; }
            }
        }
    }
}
