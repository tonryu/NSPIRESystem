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
using NSPIREIncSystem.Models;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for ProductCategoryDetails.xaml
    /// </summary>
    public partial class ProductCategoryDetails : UserControl
    {
        public static int ProdCategoryId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;


        public ProductCategoryDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var prod = context.ProductCategories.FirstOrDefault(c => c.CategoryID == ProdCategoryId);

                txtCategoryId.Text = Convert.ToString(prod.CategoryID);
                txtCategoryName.Text = prod.CategoryName;
            }
        }
    }
}
