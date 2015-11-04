using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for ProductCategoryForm.xaml
    /// </summary>
    public partial class ProductCategoryForm : UserControl
    {
        public static int ProdCategoryId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public ProductCategoryForm()
        {
            InitializeComponent();
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var Prodcat = new ProductCategory();

                if (ProdCategoryId > 0)
                {
                    var prod = context.ProductCategories.FirstOrDefault
                        (c => c.CategoryID == ProdCategoryId);

                    if (prod != null)
                    {
                        if (lblCategoryId.Visibility == Visibility.Hidden) { lblCategoryId.Visibility = Visibility.Visible; }
                        if (txtCategoryId.Visibility == Visibility.Hidden) { txtCategoryId.Visibility = Visibility.Visible; }
                        Grid.SetRow(lblCategoryName, 1);
                        Grid.SetRow(txtCategoryName, 1); Grid.SetColumn(txtCategoryName, 1);

                        txtCategoryId.Text = Convert.ToString(prod.CategoryID);
                        txtCategoryName.Text = prod.CategoryName;
                    }
                }
                else
                {
                    if (lblCategoryId.Visibility != Visibility.Hidden) { lblCategoryId.Visibility = Visibility.Hidden; }
                    if (txtCategoryId.Visibility != Visibility.Hidden) { txtCategoryId.Visibility = Visibility.Hidden; }
                    Grid.SetRow(lblCategoryName, 0);
                    Grid.SetRow(txtCategoryName, 0); Grid.SetColumn(txtCategoryName, 1);

                    txtCategoryName.Text = "";
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var prodcat = new ProductCategory();

                if (txtCategoryName.Text != "" && txtCategoryName.Text != null)
                {
                    if (ProdCategoryId > 0)
                    {
                        var category = context.ProductCategories.FirstOrDefault(c => c.CategoryID == ProdCategoryId);

                        if (category != null)
                        {
                            var duplicateCategory = context.ProductCategories.FirstOrDefault
                                (c => c.CategoryID == category.CategoryID &&
                                    c.CategoryName.ToLower() == txtCategoryName.Text.ToLower());
                            var existingCategory = context.ProductCategories.FirstOrDefault
                                (c => c.CategoryName.ToLower() == txtCategoryName.Text.ToLower());

                            if (duplicateCategory != null || existingCategory == null)
                            {
                                category.CategoryID = Convert.ToInt32(txtCategoryId.Text);
                                category.CategoryName = txtCategoryName.Text;

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Description = NotificationWindow.username + " modifies "
                                    + category.CategoryName + "'s product category details.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);

                                context.SaveChanges();
                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Product category successfully updated";
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
                                log.Description = NotificationWindow.username + " fails to modify "
                                    + category.CategoryName
                                    + "'s details due to a similar product category is already existing.";
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                context.Logs.Add(log);
                                context.SaveChanges();

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Similar product category detected";
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
                        var category = context.ProductCategories.FirstOrDefault
                            (c => c.CategoryName.ToLower() == txtCategoryName.Text.ToLower());

                        if (category == null)
                        {
                            category = new ProductCategory();
                            category.CategoryName = txtCategoryName.Text;
                            context.ProductCategories.Add(category);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Description = NotificationWindow.username + " creates a new product category. ("
                                + txtCategoryName.Text + ")";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);

                            context.SaveChanges();
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Product category successfully created";
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
                            log.Description = NotificationWindow.username
                                + " fails to create due to a similar product category is already existing.";
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            context.Logs.Add(log);
                            context.SaveChanges();

                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Product category already exists";
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
    }
}