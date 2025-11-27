using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WPF.ViewModels;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class SupplierDetails : Window
    {
        public SupplierDetails(int supplierId, ISupplierManager supplierManager)
        {
            InitializeComponent();
            DataContext = new SupplierDetailsViewModel(supplierId, supplierManager);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as SupplierDetailsViewModel;  // ✔️ Отримуємо ViewModel
            if (vm == null || vm.Supplier == null)
                return;

            var window = new SupplierProductsWindow(vm.Supplier.SupplierID); // ✔️ Передаємо ID
            window.ShowDialog();
        }

    }
}
