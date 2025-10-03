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
            MessageBox.Show("Töröl");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Módosít");
        }
    }


}
