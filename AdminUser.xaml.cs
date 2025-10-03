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

        private void userDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (userDataGrid.SelectedItem is DataRowView user)
            {
                var usr = new
                {
                    Id = user["Id"],
                    Name = user["UserName"]

                };
                MessageBox.Show(usr.ToString());
            }

        }
    }


}
