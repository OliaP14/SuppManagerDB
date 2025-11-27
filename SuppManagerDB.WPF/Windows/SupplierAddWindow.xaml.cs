using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class SupplierAddWindow : Window
    {
        private readonly ISupplierManager _supplierManager;

        public SupplierAddWindow(ISupplierManager manager)
        {
            InitializeComponent();
            _supplierManager = manager;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var supplier = new Supplier
            {
                Name = txtName.Text.Trim(),
                Info = txtInfo.Text.Trim(),
                Location = txtLocation.Text.Trim(),
                Status = true // створюємо лише активних
            };

            _supplierManager.Create(supplier);
            MessageBox.Show("Постачальник успішно створений!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
