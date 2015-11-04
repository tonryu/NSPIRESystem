using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.WindowsUI;
using NSPIREIncSystem.LeadManagement.MasterDatas;
using NSPIREIncSystem.Models;
using NSPIREIncSystem.Shared.Forms;
using NSPIREIncSystem.Shared.Windows;

namespace NSPIREIncSystem.LeadManagement.Views
{
    /// <summary>
    /// Interaction logic for AgentsForm.xaml
    /// </summary>
    public partial class AgentsForm : UserControl
    {
        public static int AgentId;
        double screenLeftEdge = Application.Current.MainWindow.Left;
        double screenTopEdge = Application.Current.MainWindow.Top;
        double screenWidth = Application.Current.MainWindow.Width;
        public static bool isSelectFinish = false;

        public AgentsForm()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if (AgentId > 0)
                {
                    lblAgentId.Visibility = Visibility.Visible;
                    txtAgentId.Visibility = Visibility.Visible;
                    Grid.SetRow(lblAgentName, 1);
                    Grid.SetRow(gridAgentName, 1); Grid.SetColumn(gridAgentName, 1);
                    Grid.SetRow(lblPosition, 2);
                    Grid.SetRow(txtPosition, 2); Grid.SetColumn(txtPosition, 1);
                    Grid.SetRow(lblContactNo, 3);
                    Grid.SetRow(txtContactNo, 3); Grid.SetColumn(txtContactNo, 1);
                    Grid.SetRow(lblTerritory, 4);
                    Grid.SetRow(txtTerritory, 4); Grid.SetColumn(txtTerritory, 1);

                    var agent = context.Agents.FirstOrDefault(c => c.AgentId == AgentId);

                    if (agent != null)
                    {
                        txtAgentId.Text = Convert.ToString(agent.AgentId);
                        txtAgentName.Text = agent.AgentName;
                        txtPosition.Text = agent.Position;
                        txtContactNo.Text = agent.ContactNo;
                        txtTerritory.Text = agent.Territory;
                    }
                }
                else
                {
                    lblAgentId.Visibility = Visibility.Hidden;
                    txtAgentId.Visibility = Visibility.Hidden;
                    Grid.SetRow(lblAgentName, 0);
                    Grid.SetRow(gridAgentName, 0); Grid.SetColumn(gridAgentName, 1);
                    Grid.SetRow(lblPosition, 1);
                    Grid.SetRow(txtPosition, 1); Grid.SetColumn(txtPosition, 1);
                    Grid.SetRow(lblContactNo, 2);
                    Grid.SetRow(txtContactNo, 2); Grid.SetColumn(txtContactNo, 1);
                    Grid.SetRow(lblTerritory, 3);
                    Grid.SetRow(txtTerritory, 3); Grid.SetColumn(txtTerritory, 1);
                }
            }

            if (isSelectFinish == true)
            {
                if (Variables.yesClicked == true)
                {
                    using (var context = new DatabaseContext())
                    {
                        string empname = EmployeeSelection.selectedEmployee;
                        var emp = context.Employees.FirstOrDefault(c => c.FirstName + " "
                            + c.MiddleName + " " + c.LastName == empname);
                        var duplicate = context.Agents.Where
                            (c => c.AgentName.ToLower() == emp.FirstName.ToLower() + " " +
                            emp.MiddleName.ToLower() + " " + emp.LastName.ToLower()).ToList();
                        List<EmployeeView> employee = new List<EmployeeView>();

                        if (duplicate.Count() != 0)
                        {
                            var window2 = new NoticeWindow();
                            NoticeWindow.message = "Employee already has a user account.";
                            double screenWidth2 = Application.Current.MainWindow.Width;
                            window2.Height = 0;
                            window2.Top = Application.Current.MainWindow.Top + 8;
                            window2.Left = (screenWidth2 / 2) - (window2.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { window2.Left += screenLeftEdge; }
                            window2.ShowDialog();
                            isSelectFinish = false;
                        }
                        else
                        {
                            txtAgentName.Text = empname;
                            txtPosition.Text = emp.Position;
                            txtTerritory.Text = emp.Territory;
                            txtAgentName.IsReadOnly = true;
                            txtPosition.IsReadOnly = true;
                            txtTerritory.IsReadOnly = true;
                            isSelectFinish = false;
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var frame = DevExpress.Xpf.Core.Native.LayoutHelper.FindParentObject<NavigationFrame>(this);
            var page = new EmployeeSelection();
            frame.Navigate(page);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new DatabaseContext())
            {
                if ((txtAgentName.Text != null || txtAgentName.Text != "") &&
                    (txtContactNo.Text != null || txtContactNo.Text != "") &&
                    (txtPosition.Text != null || txtPosition.Text != "") &&
                    (txtTerritory.Text != null || txtTerritory.Text != ""))
                {
                    var agent = context.Agents.FirstOrDefault(c => c.AgentId == AgentId);

                    if (AgentId > 0)
                    {
                        #region edit
                        if (agent != null)
                        {
                            var checkExistingAgent = context.Agents.FirstOrDefault
                                (c => c.AgentId == agent.AgentId &&
                                    c.AgentName.ToLower().Contains(txtAgentName.Text.ToLower()));
                            var duplicateAgent = context.Agents.FirstOrDefault
                                (c => c.AgentName.ToLower().Contains(txtAgentName.Text.ToLower()));

                            if ((checkExistingAgent != null) || (duplicateAgent == null))
                            {
                                agent.AgentName = txtAgentName.Text;
                                agent.ContactNo = txtContactNo.Text;
                                agent.Position = txtPosition.Text;
                                agent.Territory = txtTerritory.Text;

                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " modifies Agent " +
                                    agent.AgentName + "'s details.";
                                context.Logs.Add(log);

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Agent successfully modified";
                                windows.Height = 0;
                                windows.Top = screenTopEdge + 8;
                                windows.Left = (screenWidth / 2) - (windows.Width / 2);
                                if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                                windows.ShowDialog();

                                context.SaveChanges();

                                //AgentsMasterData.isFinish = true;
                            }
                            else
                            {
                                var log = new Log();
                                log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                                log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                                log.Description = NotificationWindow.username + " fails to modify " +
                                    agent.AgentName + "'s details because an agent with a similar name is detected.";
                                context.Logs.Add(log);
                                context.SaveChanges();

                                var windows = new NoticeWindow();
                                NoticeWindow.message = "Similar agent name detected.";
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
                        var checkExistingAgent = context.Agents.FirstOrDefault
                                (c => c.AgentName.ToLower().Contains(txtAgentName.Text.ToLower()));

                        if (checkExistingAgent == null)
                        {
                            agent = new Agent();
                            agent.AgentName = txtAgentName.Text;
                            agent.ContactNo = txtContactNo.Text;
                            agent.Position = txtPosition.Text;
                            agent.Territory = txtTerritory.Text;
                            if (txtAgentName.IsReadOnly == true && txtPosition.IsReadOnly == true
                                && txtTerritory.IsReadOnly == true) { agent.IsEmployee = true; }
                            else { agent.IsEmployee = false; }
                            context.Agents.Add(agent);

                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " creates a new agent. (" +
                                agent.AgentName + ")";
                            context.Logs.Add(log);

                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Agent successfully created";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();

                            context.SaveChanges();

                            //AgentsMasterData.isFinish = true;
                        }
                        else
                        {
                            var log = new Log();
                            log.Date = DateTime.Now.ToString("MM/dd/yyyy");
                            log.Time = DateTime.Now.ToString("hh:mm:ss tt");
                            log.Description = NotificationWindow.username + " fails to create a new agent"
                                + " because the agent already exists.";
                            context.Logs.Add(log);
                            context.SaveChanges();

                            var windows = new NoticeWindow();
                            NoticeWindow.message = "Agent already exists";
                            windows.Height = 0;
                            windows.Top = screenTopEdge + 8;
                            windows.Left = (screenWidth / 2) - (windows.Width / 2);
                            if (screenLeftEdge > 0 || screenLeftEdge < -8) { windows.Left += screenLeftEdge; }
                            windows.ShowDialog();
                        }
                        #endregion
                    }
                }
                else
                {
                    var windows = new NoticeWindow();
                    NoticeWindow.message = "Please fill all boxes labeled with an asterisk(*) symbol.";
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
