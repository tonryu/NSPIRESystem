using NSPIREIncSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSPIREIncSystem.Settings.Views
{
    /// <summary>
    /// Interaction logic for SalesStageDetails.xaml
    /// </summary>
    public partial class SalesStageDetails : UserControl
    {
        public static int SalesStageId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public SalesStageDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var salesstage = context.SalesStages.FirstOrDefault(c => c.SalesStageID == SalesStageId);
               
                txtSalesStageName.Text = salesstage.SalesStageName;
                txtRankNo.Text = Convert.ToString(salesstage.RankNo);
                txtSalesId.Text = Convert.ToString(salesstage.SalesStageID);                                
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
