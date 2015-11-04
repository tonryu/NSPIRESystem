using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.Shared.Forms
{
    /// <summary>
    /// Interaction logic for ProductSelection.xaml
    /// </summary>
    public partial class ProductSelection : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenHeight = Application.Current.MainWindow.Height;
        double screenWidth = Application.Current.MainWindow.Width;

        public static List<ProductView> productsList = new List<ProductView>();
        public static string passList;

        public ProductSelection()
        {
            InitializeComponent();
        }

        private void LoadProducts(string text)
        {
            using (var context = new DatabaseContext())
            {
                var products = context.Products.ToList();

                listProducts.Items.Clear();
                productsList.Clear();
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        productsList.Add(new ProductView
                        {
                            ProductName = product.ProductName
                        });
                    }
                }

                foreach (var item in productsList.Where(c => c.ProductName.ToLower().Contains(text.ToLower()) && c.ProductName != " ").OrderBy(c => c.ProductID).ToList())
                {
                    listProducts.Items.Add(item.ProductName);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (listProducts.SelectedItems != null)
                {
                    foreach (var product in listProducts.SelectedItems)
                    {
                        var convertedProduct = Convert.ToString(product);

                        var prod = context.Products.FirstOrDefault(c => c.ProductName == convertedProduct);

                        passList = prod.ProductName;
                    }
                    Variables.yesClicked = true;
                    var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                    frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
                    frame.GoBack();
                    LeadForm.isSelectFinish = true;
                }
                else
                {
                    var window = new NoticeWindow();
                    NoticeWindow.message = "Please select a row.";
                    window.Height = 0;
                    window.Top = Application.Current.MainWindow.Top + 8;
                    window.Left = (screenWidth / 2) - (window.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                    window.ShowDialog();
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Variables.yesClicked = false;
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
            frame.GoBack();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void listProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                using (var context = new DatabaseContext())
                {
                    if (listProducts.SelectedItems != null)
                    {
                        foreach (var product in listProducts.SelectedItems)
                        {
                            var convertedProduct = Convert.ToString(product);

                            var prod = context.Products.FirstOrDefault(c => c.ProductName == convertedProduct);

                            passList = prod.ProductName;
                        }
                        Variables.yesClicked = true;
                        var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
                        frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
                        frame.GoBack();
                        LeadForm.isSelectFinish = true;
                    }
                    else
                    {
                        var window = new NoticeWindow();
                        NoticeWindow.message = "Please select a row.";
                        window.Height = 0;
                        window.Top = Application.Current.MainWindow.Top + 8;
                        window.Left = (screenWidth / 2) - (window.Width / 2);
                        if (screenLeftEdge > 0 || screenLeftEdge < -8)
                        {
                            window.Left += screenLeftEdge;
                        }
                        window.ShowDialog();
                    }
                }
            }
        }
    }
}
