using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for AdminUser.xaml
    /// </summary>
    public partial class AdminUser : Page
    {
        private MainWindow _mainWindow;
        private SqlStatements _sqlStatements = new SqlStatements();
        public AdminUser(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            userDataGrid.ItemsSource = _sqlStatements.GetAllUsers();
        }

        private void userDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (userDataGrid.SelectedItem is DataRowView user)
            {
                var usr = new
                {
                    Id = user["Id"],
                    Name = user["UserName"],
                    Password = user["Password"],
                    FullName = user["FullName"],
                    Email = user["Email"],
                    Date = user["RegDate"]

                };
                MessageBox.Show(usr.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is DataRowView user)
            {
                var usr = new
                {
                    Id = user["Id"]
                };

                _sqlStatements.DeleteUser(user["Id"]);
            }
            userDataGrid.ItemsSource = _sqlStatements.GetAllUsers();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is DataRowView user)
            {
                var usr = new
                {
                    Id = user["Id"],
                    Name = user["UserName"],
                    Password = user["Password"],
                    FullName = user["FullName"],
                    Email = user["Email"],
                    Date = user["RegDate"]

                };
                _sqlStatements.UpdateUser(usr);

            }
            userDataGrid.ItemsSource = _sqlStatements.GetAllUsers();
        }

        private void userDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                // Késleltetve hívjuk az UpdateUser-t
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (e.Row.Item is DataRowView rowView)
                    {
                        var user = new
                        {
                            Id = Convert.ToInt32(rowView["Id"]),
                            UserName = rowView["UserName"].ToString(),
                            Password = rowView["Password"].ToString(),
                            FullName = rowView["FullName"].ToString(),
                            Email = rowView["Email"].ToString(),
                            RegDate = Convert.ToDateTime(rowView["RegDate"])
                        };

                        SqlStatements sql = new SqlStatements();
                        _sqlStatements.UpdateUser(user);
                    }
                }), System.Windows.Threading.DispatcherPriority.Background);
            }
        }


    }
}
