using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SuppManagerDB.BL.Interfaces;

namespace SuppManagerDB.WPF.Windows
{
    public partial class LoginWindow : Window
    {
        private readonly IAuthManager _authManager;

        public LoginWindow()
        {
            InitializeComponent();
            _authManager = App.Services.GetRequiredService<IAuthManager>();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                txtError.Text = "Заповніть усі поля!";
                return;
            }

            var user = _authManager.Login(login, password);

            if (user == null)
            {
                txtError.Text = "Невірний логін або пароль.";
                return;
            }

            if (!_authManager.HasPrivilege(user.UserID, "SupplierManager"))
            {
                MessageBox.Show(
                    "У вас немає прав доступу!",
                    "Доступ заборонено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            App.CurrentUser = user;

            var suppliersWindow = new SuppliersList();
            suppliersWindow.Show();
            this.Close();
        }
    }
}





/*using Microsoft.Extensions.DependencyInjection;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.BL.Interfaces;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class LoginWindow : Window
    {
        private readonly IAuthManager _authManager;

        public LoginWindow()
        {
            InitializeComponent();
            _authManager = App.Services.GetRequiredService<IAuthManager>();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("1) Клік по кнопці відпрацював");

                string login = txtLogin.Text.Trim();
                string password = txtPassword.Password;

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    txtError.Text = "Заповніть усі поля!";
                    MessageBox.Show("2) Поля порожні");
                    return;
                }

                var user = _authManager.Login(login, password);

                if (user == null)
                {
                    txtError.Text = "Невірний логін або пароль.";
                    MessageBox.Show("3) user == null (не знайдено користувача)");
                    return;
                }

                MessageBox.Show($"3) Залогінилися як: {user.Login}, UserID = {user.UserID}");

                // Подивимось, які привілеї реально приходять
                var privs = _authManager.GetUserPrivileges(user.UserID);
                string privNames = privs.Any()
                    ? string.Join(", ", privs.Select(p => p.Name))
                    : "(немає жодної привілеї)";
                MessageBox.Show("4) Привілеї користувача: " + privNames);

                bool hasSupplierManager = _authManager.HasPrivilege(user.UserID, "SupplierManager");
                MessageBox.Show("5) HasPrivilege(SupplierManager) = " + hasSupplierManager);

                if (!hasSupplierManager)
                {
                    MessageBox.Show(
                        "У вас немає прав доступу.",
                        "Доступ заборонено!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                App.CurrentUser = user;
                MessageBox.Show("6) App.CurrentUser встановлено. Відкриваю SuppliersList...");

                var suppliersWindow = new SuppliersList();
                suppliersWindow.Show();

                MessageBox.Show("7) SuppliersList.Show() викликано, закриваю LoginWindow");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ПОМИЛКА:\n" + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
*/
