using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using SuppManagerDB.WPF.Windows;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class SuppliersList : Window
    {
        private readonly IAuthManager _auth;

        public SuppliersList()
        {
            
            InitializeComponent();

            // Отримую Auth через DI
            _auth = App.Services.GetService(typeof(IAuthManager)) as IAuthManager;


            // Перевіряю роль
            if (!_auth.HasPrivilege(App.CurrentUser.UserID, "SupplierManager"))
            {
                MessageBox.Show("Доступ заборонено!", "Помилка");
                Close();
                return;
            }

            // підключаю ViewModel
            var supplierManager = App.Services.GetService(typeof(ISupplierManager)) as ISupplierManager;
            // + отримуємо ProductManager
            var productManager = App.Services.GetService(typeof(IProductManager)) as IProductManager;
            DataContext = new SuppliersListViewModel(supplierManager, productManager);

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.CurrentUser = null;

                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ПОМИЛКА:\n" + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
