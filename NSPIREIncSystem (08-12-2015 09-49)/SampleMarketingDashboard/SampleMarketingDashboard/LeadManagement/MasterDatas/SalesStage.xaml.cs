using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.Views;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Settings.Views;
using NSPIREIncSystem.Shared.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NSPIREIncSystem.LeadManagement.MasterDatas
{
    /// <summary>
    /// Interaction logic for SalesStage.xaml
    /// </summary>
    public partial class SalesStage : UserControl
    {
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        double screenHeight = Application.Current.MainWindow.Height;
        public List<SalesStagesView> salessatageList = new List<SalesStagesView>();
        bool _isExpanded = false;
        public static int SalesStageId;

        public SalesStage()
        {
            InitializeComponent();
        }

        #region Measure canvas and child objects
        private double GetCanvasMinWidth(Canvas canvas)
        {
            double canvasWidth = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                double childLeftMargin = canvasChildren.FirstOrDefault().Margin.Left;
                double childWidth = canvasChildren.FirstOrDefault().ActualWidth;
                double childRightMargin = canvasChildren.FirstOrDefault().Margin.Right;

                double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                canvasWidth = totalChildWidth;
            }

            return canvasWidth;
        }

        private double GetCanvasMaxWidth(Canvas canvas)
        {
            double canvasWidth = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                foreach (var child in canvasChildren)
                {
                    double childLeftMargin = child.Margin.Left;
                    double childWidth = child.ActualWidth;
                    double childRightMargin = child.Margin.Right;

                    double totalChildWidth = childLeftMargin + childWidth + childRightMargin;
                    canvasWidth += totalChildWidth;
                }
            }

            return canvasWidth;
        }

        private double GetCanvasMinHeight(Canvas canvas)
        {
            double canvasHeight = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                double childTopMargin = canvasChildren.FirstOrDefault().Margin.Top;
                double childWidth = canvasChildren.FirstOrDefault().ActualHeight;
                double childBottomMargin = canvasChildren.FirstOrDefault().Margin.Bottom;

                double totalChildHeight = childTopMargin + childWidth + childBottomMargin;
                canvasHeight = totalChildHeight;
            }

            return canvasHeight;
        }

        private double GetCanvasMaxHeight(Canvas canvas)
        {
            double canvasHeight = 0;

            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvasChildren.Count() > 0)
            {
                foreach (var child in canvasChildren)
                {
                    double childTopMargin = child.Margin.Top;
                    double childHeight = child.ActualHeight;
                    double childBottomMargin = child.Margin.Bottom;

                    double totalChildHeight = childTopMargin + childHeight + childBottomMargin;
                    canvasHeight += totalChildHeight;
                }
            }

            return canvasHeight;
        }
        #endregion

        #region Activate effects
        private void FoldCanvasSideward(Canvas canvas)
        {
            double minCanvasWidth = GetCanvasMinWidth(canvas);
            double maxCanvasWidth = GetCanvasMaxWidth(canvas);
            if (canvas.Width == minCanvasWidth)
            {
                var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
                if (canvasChildren.Count() > 0)
                {
                    int numberOfButtonsMinusOne = canvasChildren.Count() - 1;//number of buttons - 1
                    double initialCenterWidth = minCanvasWidth - canvasChildren.FirstOrDefault().ActualWidth - canvasChildren.FirstOrDefault().Margin.Left - canvasChildren.FirstOrDefault().Margin.Right;//width to be centered
                    double finalCenterWidth = maxCanvasWidth - canvasChildren.FirstOrDefault().ActualWidth - canvasChildren.FirstOrDefault().Margin.Left - canvasChildren.FirstOrDefault().Margin.Right;
                    double initialUnitWidth = Math.Round(initialCenterWidth / numberOfButtonsMinusOne);
                    double finalUnitWidth = Math.Round(finalCenterWidth / numberOfButtonsMinusOne);

                    StackPanel firstStackPanel = canvasChildren.FirstOrDefault();
                    int index = 0;
                    foreach (StackPanel child in canvasChildren.Where(c => c != firstStackPanel).ToList())//all buttons except first one
                    {
                        index++;
                        if (child != canvasChildren.Last())//until before the last button
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = initialUnitWidth * index, To = finalUnitWidth * index, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                        else//last button
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = 0, To = maxCanvasWidth - minCanvasWidth, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                    }
                    DoubleAnimation canvasAnimation = new DoubleAnimation() { From = GetCanvasMinWidth(canvas), To = GetCanvasMaxWidth(canvas), Duration = TimeSpan.Parse("0:0:0.35") };
                    canvas.BeginAnimation(Canvas.WidthProperty, canvasAnimation);
                }
            }
            else
            {
                var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
                if (canvasChildren.Count() > 0)
                {
                    StackPanel firstStackPanel = canvasChildren.FirstOrDefault();
                    foreach (StackPanel child in canvasChildren)//all buttons except first one
                    {
                        if (child != firstStackPanel)
                        {
                            DoubleAnimation childAnimation = new DoubleAnimation() { From = Canvas.GetLeft(child), To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                            child.BeginAnimation(Canvas.LeftProperty, childAnimation);
                        }
                    }
                    DoubleAnimation canvasAnimation = new DoubleAnimation() { From = GetCanvasMaxWidth(canvas), To = GetCanvasMinWidth(canvas), Duration = TimeSpan.Parse("0:0:0.35") };
                    canvas.BeginAnimation(Canvas.WidthProperty, canvasAnimation);
                }
            }
        }

        private void FoldInnerCanvasSideward(Canvas canvas)
        {
            var canvasChildren = canvas.Children.OfType<StackPanel>().ToList();
            if (canvas.Visibility == Visibility.Collapsed)
            {
                canvas.Visibility = Visibility.Visible;
                DoubleAnimation canvasAnimation = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.Parse("0:0:0.35") };
                if (canvasChildren.Count() > 0)
                {
                    double canvasMinWidth = GetCanvasMinWidth(canvas);
                    FoldCanvasSideward(canvas);
                }
                canvas.BeginAnimation(Canvas.OpacityProperty, canvasAnimation);
            }
            else
            {
                DoubleAnimation canvasAnimation = new DoubleAnimation() { From = 1, To = 0, Duration = TimeSpan.Parse("0:0:0.35") };
                canvasAnimation.Completed += (s, e) => canvas.Visibility = Visibility.Collapsed;
                canvas.BeginAnimation(Canvas.OpacityProperty, canvasAnimation);
                FoldCanvasSideward(canvas);
            }
        }
        #endregion

        #region Load Details

        private Task <string> QueryLoadSalesStages()
        {
            return Task.Factory.StartNew(() =>
                {
                    try
                    {
                        salessatageList.Clear();
                        using(var context = new DatabaseContext())
                        {
                                var salesstage = context.SalesStages.ToList();
                                
                                
                                    foreach (var item in salesstage)
                                    {
                                        var stage = context.SalesStages.FirstOrDefault(c => c.SalesStageID == item.SalesStageID);

                                        salessatageList.Add(new SalesStagesView()
                                        {
                                            SalesStageID = item.SalesStageID,
                                            SalesStageName = item.SalesStageName,
                                            RankNo = item.RankNo                                           
                                           

                                        });


                                    }
                                }

                        return null;
                    }

                    catch (Exception ex)
                    {
                        return "Error Message" + ex.Message;
                    }
                });
        }

         private async void RefreshTable(string str)
        {
            using (var context = new DatabaseContext())
            {
                string message = "";
                busyIndicator.IsBusy = true;
                message = await QueryLoadSalesStages();

                 if (message != null)
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = message;
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                

                    dcSalesStagesList.ItemsSource = salessatageList.Where
                        (c => (c.SalesStageName.ToLower().Contains(txtSearch.Text.ToLower()))).
                        OrderBy(c => c.RankNo).ToList();
                           
                   viewSalesStages.BestFitColumns();


                if (salessatageList.Count == 0)
                {
                    var windows = new Shared.Windows.NoticeWindow();
                    NoticeWindow.message = "List has no Sales Stages.";
                    windows.Height = 0;
                    windows.Top = screenTopEdge + 8;
                    windows.Left = (screenWidth / 2) - (windows.Width / 2);
                    if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                    windows.ShowDialog();
                }
                busyIndicator.IsBusy = false;
            }
        }

         private void LoadActivity()
         {
             RefreshTable("");
         }

         #endregion

         private void UserControl_Loaded(object sender, RoutedEventArgs e)
         {
            
             LoadActivity();
             canvasSalesStagesMasterData.Width = GetCanvasMinWidth(canvasSalesStagesMasterData);
             canvasSalesStagesMasterData.Height = GetCanvasMinHeight(canvasSalesStagesMasterData);
             canvasSalesStagesMasterData.Visibility = Visibility.Collapsed;
             canvasSalesStagesMasterData.Opacity = 0;
             FoldInnerCanvasSideward(canvasSalesStagesMasterData);
             
         }

         private void btnView_Click(object sender, RoutedEventArgs e)
         {
             var selectedstage = dcSalesStagesList.SelectedItem as SalesStagesView;

             Storyboard sb;
             if (_isExpanded != true && selectedstage != null)
             {
                 SalesStageDetails.SalesStageId = selectedstage.SalesStageID;

                 sb = this.FindResource("gridin") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;

                 var page = new SalesStageDetails();
                 navigation.Navigate(page);
             }
             else
             {
                 sb = this.FindResource("gridout") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;

                 LoadActivity();
             }
         }

         private void btnAdd_Click(object sender, RoutedEventArgs e)
         {
              SalesStageDetails.SalesStageId = 0;
              
            Storyboard sb;
            if (_isExpanded != true)
            {
                sb = this.FindResource("gridin") as Storyboard;
                sb.Begin(this);
                _isExpanded = !_isExpanded;
            }

            var page = new SalesStageForm();
            navigation.Navigate(page);
            
         }

         private void btnEdit_Click(object sender, RoutedEventArgs e)
         {
             Storyboard sb;
             if (_isExpanded != true)
             {
                 sb = this.FindResource("gridin") as Storyboard;
                 sb.Begin(this);
                 _isExpanded = !_isExpanded;
             }

             var selectedsales = dcSalesStagesList.SelectedItem as SalesStagesView;

             if (selectedsales != null)
             {
                 SalesStageForm.SalesStageId = selectedsales.SalesStageID;
                 

                 var page = new SalesStageForm();
                 navigation.Navigate(page);
             }
             else
             {
                 NullMessage();
             }

           
         }

         private void btnDelete_Click(object sender, RoutedEventArgs e)
         {
             using (var context= new DatabaseContext())
             {
                 var selectedSalestage = dcSalesStagesList.SelectedItem as SalesStagesView;

                 if (selectedSalestage != null)
                 {
                     var salestage = context.SalesStages.First(c => c.SalesStageID == selectedSalestage.SalesStageID);

                     if (salestage != null)
                     {
                         var window = new MessageBoxWindow("Are you sure you want to delete this record?");
                         window.Height = 0;
                         window.Top = screenTopEdge + 8;
                         window.Left = (screenWidth / 2) - (window.Width / 2);
                         if (screenLeftEdge > 0 || screenLeftEdge < -8) { window.Left += screenLeftEdge; }
                         window.ShowDialog();

                         if (Variables.yesClicked == true)
                         {
                             context.SalesStages.Remove(salestage);
                             var windows = new Shared.Windows.NoticeWindow();
                             Shared.Windows.NoticeWindow.message = "SALE STAGE SUCCESSFULLY DELETED";
                             windows.Height = 0;
                             windows.Top = screenTopEdge + 8;
                             windows.Left = (screenWidth / 2) - (windows.Width / 2);
                             if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                             windows.ShowDialog();

                             context.SaveChanges();

                         }
                     }
                     else
                     {
                         NullMessage();
                     }
                 }

                 LoadActivity();
             }
            
         }

         private void btnPrint_Click(object sender, RoutedEventArgs e)
         {

         }

         private void btnBack_Click(object sender, RoutedEventArgs e)
         {
             var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
             frame.BackNavigationMode = BackNavigationMode.PreviousScreen;
             frame.GoBack();
         }

         private void NullMessage()
         {
             var windows = new Shared.Windows.NoticeWindow();
             NoticeWindow.message = "Please select a record.";
             windows.Height = 0;
             windows.Top = screenTopEdge + 8;
             windows.Left = (screenWidth / 2) - (windows.Width / 2);
             if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
             windows.ShowDialog();
         }

         private void btnSearch_Click(object sender, RoutedEventArgs e)
         {
             LoadActivity();
         }
<<<<<<< HEAD

=======
>>>>>>> origin/master
    }
}
