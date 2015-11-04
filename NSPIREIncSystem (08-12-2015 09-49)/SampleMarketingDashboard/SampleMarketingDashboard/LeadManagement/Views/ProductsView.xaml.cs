using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public static int ProductId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public static bool isView;

        public ProductsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (isView != true)
                {
                    if (ProductId > 0)
                    {
                        var product = context.Products.FirstOrDefault(c => c.ProductID == ProductId);

                        if (product != null)
                        {
                            var category = context.ProductCategories.FirstOrDefault(c => c.CategoryID == product.CategoryID);

                            if (category != null)
                            {
                                lblProductId.Visibility = Visibility.Visible;
                                txtProductId.Visibility = Visibility.Visible;
                                btnSave.Visibility = Visibility.Visible;
                                if (cbProductCategory.Visibility == Visibility.Hidden)
                                {
                                    cbProductCategory.Visibility = Visibility.Visible;
                                }
                                if (txtProductCategory.Visibility == Visibility.Visible)
                                {
                                    txtProductCategory.Visibility = Visibility.Hidden;
                                }
                                if (txtProductName.IsReadOnly == true && txtCost.IsReadOnly == true)
                                {
                                    txtProductName.IsReadOnly = false;
                                    txtCost.IsReadOnly = false;
                                }

                                Grid.SetRow(lblProductName, 1);
                                Grid.SetRow(txtProductName, 1); Grid.SetColumn(txtProductName, 1);
                                Grid.SetRow(lblProductCategory, 2);
                                Grid.SetRow(cbProductCategory, 2); Grid.SetColumn(cbProductCategory, 1);
                                Grid.SetRow(lblCost, 3);
                                Grid.SetRow(txtCost, 3); Grid.SetColumn(txtCost, 1);

                                txtProductId.Text = Convert.ToString(product.ProductID);
                                txtProductName.Text = product.ProductName;
                                txtCost.Text = Convert.ToString(product.Cost);
                                cbProductCategory.SelectedItem = category.CategoryName;
                            }
                        }
                    }
                    else
                    {
                        lblProductId.Visibility = Visibility.Hidden;
                        txtProductId.Visibility = Visibility.Hidden;
                        btnSave.Visibility = Visibility.Visible;
                        if (cbProductCategory.Visibility == Visibility.Hidden)
                        {
                            cbProductCategory.Visibility = Visibility.Visible;
                        }
                        if (txtProductCategory.Visibility == Visibility.Visible)
                        {
                            txtProductCategory.Visibility = Visibility.Hidden;
                        }
                        if (txtProductName.IsReadOnly == true && txtCost.IsReadOnly == true)
                        {
                            txtProductName.IsReadOnly = false;
                            txtCost.IsReadOnly = false;
                        }

                        Grid.SetRow(lblProductName, 0);
                        Grid.SetRow(txtProductName, 0); Grid.SetColumn(txtProductName, 1);
                        Grid.SetRow(lblProductCategory, 1);
                        Grid.SetRow(cbProductCategory, 1); Grid.SetColumn(cbProductCategory, 1);
                        Grid.SetRow(lblCost, 2);
                        Grid.SetRow(txtCost, 2); Grid.SetColumn(txtCost, 1);

                        txtProductName.Text = "";
                        txtCost.Text = "";
                        cbProductCategory.SelectedItem = null;
                    }
                }
                else
                {
                    var product = context.Products.FirstOrDefault(c => c.ProductID == ProductId);
                    if (product != null)
                    {
                        var category = context.ProductCategories.FirstOrDefault(c => c.CategoryID == product.CategoryID);

                        if (category != null)
                        {
                            lblProductId.Visibility = Visibility.Visible;
                            txtProductId.Visibility = Visibility.Visible;
                            btnSave.Visibility = Visibility.Hidden;
                            if (cbProductCategory.Visibility == Visibility.Visible)
                            {
                                cbProductCategory.Visibility = Visibility.Hidden;
                            }
                            if (txtProductCategory.Visibility == Visibility.Hidden)
                            {
                                txtProductCategory.Visibility = Visibility.Visible;
                            }
                            if (txtProductName.IsReadOnly != true && txtCost.IsReadOnly != true)
                            {
                                txtProductName.IsReadOnly = true;
                                txtCost.IsReadOnly = true;
                            }

                            Grid.SetRow(lblProductName, 1);
                            Grid.SetRow(txtProductName, 1); Grid.SetColumn(txtProductName, 1);
                            Grid.SetRow(lblProductCategory, 2);
                            Grid.SetRow(txtProductCategory, 2); Grid.SetColumn(txtProductCategory, 1);
                            Grid.SetRow(lblCost, 3);
                            Grid.SetRow(txtCost, 3); Grid.SetColumn(txtCost, 1);

                            txtProductId.Text = Convert.ToString(product.ProductID);
                            txtProductName.Text = product.ProductName;
                            txtCost.Text = Convert.ToString(product.Cost);
                            txtProductCategory.Text = category.CategoryName;
                        }
                    }
                }

                #region product category list
                var productCategory = context.ProductCategories.ToList();
                cbProductCategory.ItemsSource = null;
                if (productCategory.Count() > 0)
                {
                    cbProductCategory.ItemsSource = productCategory.OrderBy(c => c.CategoryName).
                        Select(c => c.CategoryName).ToList();
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Category list is unavailable.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                #endregion
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (((txtProductCategory.Text != null || txtProductCategory.Text != "") ||
                    (cbProductCategory.SelectedItem != null)) &&
                    (txtProductName.Text != null || txtProductName.Text != ""))
                {
                    if (ProductId > 0)
                    {
                        var product = context.Products.FirstOrDefault(c => c.ProductID == ProductId);

                        #region edit
                        if (product != null)
                        {
                            var duplicateProduct = context.Products.FirstOrDefault
                                (c => c.ProductID == product.ProductID &&
                                    c.ProductName.ToLower().Contains(txtProductName.Text.ToLower()));
                            var existingProduct = context.Products.FirstOrDefault
                                (c => c.ProductName.ToLower().Contains(txtProductName.Text.ToLower()));

                            if (duplicateProduct != null || existingProduct == null)
                            {
                                product.CategoryID = Convert.ToInt32(txtProductId.Text);
                                product.ProductName = txtProductName.Text;
                                if (txtCost.Text != null || txtCost.Text != "") { product.Cost = Convert.ToDouble(txtCost.Text); }
                                else { product.Cost = 0.00; }
                                if (txtProductCategory.Visibility == Visibility.Visible)
                                {
                                    var category = context.ProductCategories.FirstOrDefault
                                        (c => c.CategoryName.ToLower() == 
                                            txtProductCategory.Text.ToLower());
                                    if (category != null)
                                    {
                                        product.CategoryID = category.CategoryID;
                                    }
                                }
                                else if (cbProductCategory.Visibility == Visibility.Visible)
                                {
                                    var category = context.ProductCategories.FirstOrDefault
                                        (c => c.CategoryName.ToLower() ==
                                            cbProductCategory.Text.ToLower());
                                    if (category != null)
                                    {
                                        product.CategoryID = category.CategoryID;
                                    }
                                }

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username +
                                    " modifies " + txtProductName.Text + "'s details.";
                                context.Logs.Add(log);

                                var window = new NoticeWindow();
                                NoticeWindow.message = "Product successfully modified";
                                window.Height = 0;
                                window.Top = screenTopEdge + 8;
                                window.Left = (screenWidth / 2) - (window.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                                window.ShowDialog();

                                context.SaveChanges();
                            }
                            else
                            {
                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " tries to modify " +
                                    product.ProductName + "'s details but fails due to a similar name as the product already exists.";
                                context.Logs.Add(log);
                                context.SaveChanges();

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Similar product name detected";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region add
                        var product = context.Products.FirstOrDefault
                                (c => c.ProductName.ToLower().Contains(txtProductName.Text.ToLower()));
                        var category = context.ProductCategories.FirstOrDefault
                                (c => c.CategoryName.ToLower() == cbProductCategory.Text.ToLower());

                        if ((product == null) && (category != null))
                        {
                            product = new Product();
                            product.CategoryID = category.CategoryID;
                            if ((txtCost.Text != null) && (txtCost.Text != ""))
                            { product.Cost = Convert.ToDouble(txtCost.Text); }
                            else { product.Cost = 0.00; }
                            product.ProductName = txtProductName.Text;
                            context.Products.Add(product);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " creates a new customer. ("
                                + product.ProductName + ")";
                            context.Logs.Add(log);

                            var window = new NoticeWindow();
                            NoticeWindow.message = "Product successfully created";
                            window.Height = 0;
                            window.Top = screenTopEdge + 8;
                            window.Left = (screenWidth / 2) - (window.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) 
                            { window.Left += screenLeftEdge; }
                            window.ShowDialog();

                            context.SaveChanges();
                        }
                        else
                        {
                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " tries to create a new"+
                                " customer but fails due to the product already exists.";
                            context.Logs.Add(log);

                            var window = new NoticeWindow();
                            NoticeWindow.message = "Product already exists";
                            window.Height = 0;
                            window.Top = screenTopEdge + 8;
                            window.Left = (screenWidth / 2) - (window.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                            window.ShowDialog();

                            context.SaveChanges();
                        }
                        #endregion
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Please fill all the boxes labeled with an asterisk(*).";
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
