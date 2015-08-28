using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.Settings.Views
{
    /// <summary>
    /// Interaction logic for TerritoryDetails.xaml
    /// </summary>
    public partial class TerritoryDetails : UserControl
    {
        public static int TerritoryId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;

        public TerritoryDetails()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                var territory = new Territory();

                if (TerritoryId > 0)
                {
                    territory = context.Territories.FirstOrDefault(c => c.TerritoryID == TerritoryId);

                    if (territory != null)
                    {
                        lblTerritoryId.Visibility = Visibility.Visible;
                        txtTerritoryId.Visibility = Visibility.Visible;
                        txtTerritoryId.Text = Convert.ToString(territory.TerritoryID);
                        txtTerritoryName.Text = territory.TerritoryName;
                        txtAddress.Text = territory.Address;
                        txtPhoneNo.Text = territory.PhoneNo;

                        Grid.SetRow(lblTerritoryName, 1);
                        Grid.SetRow(txtTerritoryName, 1); Grid.SetColumn(txtTerritoryName, 1);
                        Grid.SetRow(lblAddress, 2);
                        Grid.SetRow(txtAddress, 2); Grid.SetColumn(txtAddress, 1);
                        Grid.SetRow(lblPhoneNo, 3);
                        Grid.SetRow(txtPhoneNo, 3); Grid.SetColumn(txtPhoneNo, 1);
                    }
                }
                else
                {
                    lblTerritoryId.Visibility = Visibility.Hidden;
                    txtTerritoryId.Visibility = Visibility.Hidden;
                    txtTerritoryName.Text = "";
                    txtAddress.Text = "";
                    txtPhoneNo.Text = "";

                    Grid.SetRow(lblTerritoryName, 0);
                    Grid.SetRow(txtTerritoryName, 0); Grid.SetColumn(txtTerritoryName, 1);
                    Grid.SetRow(lblAddress, 1);
                    Grid.SetRow(txtAddress, 1); Grid.SetColumn(txtAddress, 1);
                    Grid.SetRow(lblPhoneNo, 2);
                    Grid.SetRow(txtPhoneNo, 2); Grid.SetColumn(txtPhoneNo, 1);
                }
            }
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (txtAddress.Text != "" && txtTerritoryName.Text != "")
                {
                    if (TerritoryId > 0)
                    {
                        var territori = context.Territories.FirstOrDefault(c => c.TerritoryID == TerritoryId);

                        if (territori != null)
                        {
                            var territoryName = context.Territories.FirstOrDefault
                                (c => c.TerritoryName.ToLower() == txtTerritoryName.Text.ToLower());

                            if (territoryName != null)
                            {
                                if (territori.TerritoryName.ToLower() == territoryName.TerritoryName.ToLower()
                                    && territori.Address.ToLower() == territoryName.Address.ToLower())
                                {
                                    territori.Address = txtAddress.Text;
                                    territori.TerritoryName = txtTerritoryName.Text;
                                    territori.PhoneNo = txtPhoneNo.Text;

                                    context.SaveChanges();
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Territory successfully updated";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                                else
                                {
                                    var windows = new Shared.Windows.NoticeWindow();
                                    Shared.Windows.NoticeWindow.message = "Similar territory detected";
                                    windows.Height = 0;
                                    windows.Top = screenTopEdge + 8;
                                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                    windows.ShowDialog();
                                }
                            }
                        }
                    }
                    else
                    {
                        var territory = context.Territories.FirstOrDefault
                             (c => c.TerritoryName.ToLower() == txtTerritoryName.Text.ToLower()
                                 && c.Address.ToLower() == txtAddress.Text.ToLower() && c.PhoneNo.ToLower() == txtPhoneNo.Text.ToLower());

                        if (territory == null)
                        {
                            territory = new Territory();
                            
                            territory.TerritoryName = txtTerritoryName.Text;
                            territory.Address = txtAddress.Text;
                            territory.PhoneNo = txtPhoneNo.Text;

                            context.Territories.Add(territory);
                            context.SaveChanges();
                            
                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Territory successfully created";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        else
                        {
                            var windows = new Shared.Windows.NoticeWindow();
                            Shared.Windows.NoticeWindow.message = "Territory already exist";
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
                    var windows = new Shared.Windows.NoticeWindow();
                    Shared.Windows.NoticeWindow.message = "Please provide all boxes associated with an asterisk(*).";
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
