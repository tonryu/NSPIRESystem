using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Windows
{
    /// <summary>
    /// Interaction logic for LeadDetailsWindow.xaml
    /// </summary>
    public partial class LeadDetailsWindow : Window
    {
        public static int LeadId;
        double screenLeftEdge = Application.Current.MainWindow.Left;

        public LeadDetailsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var lead = context.Leads.FirstOrDefault(c => c.LeadID == LeadId);
                var territory = context.Territories.FirstOrDefault(c => c.TerritoryID == lead.TerritoryID);

                txtCompanyAddress.Text = lead.CompanyAddress;
                txtCompanyName.Text = lead.CompanyName;
                txtLeadId.Text = Convert.ToString(lead.LeadID);
                txtStatus.Text = lead.Status;
                txtTerritory.Text = territory.TerritoryName;
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
