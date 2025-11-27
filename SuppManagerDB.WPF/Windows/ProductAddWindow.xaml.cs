using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using System;
using System.Globalization;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class ProductAddWindow : Window
    {
        private readonly IProductManager _productManager;
        private readonly ISupplierManager _supplierManager;
        private readonly int _supplierId;

        public ProductAddWindow(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;

            // Отримуємо сервіси через DI
            _productManager = (IProductManager)App.Services.GetService(typeof(IProductManager));
            _supplierManager = (ISupplierManager)App.Services.GetService(typeof(ISupplierManager));

            this.Title = $"Додати товар (Постачальник ID={supplierId})";

            // Завантажуємо списки
            cmbCategory.ItemsSource = _productManager.GetAllCategories();
            cmbManufacturer.ItemsSource = _productManager.GetAllManufacturers();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву товару");
                return;
            }
            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Any,
                CultureInfo.InvariantCulture, out var price) || price <= 0)
            {
                MessageBox.Show("Некоректна ціна");
                return;
            }

            var product = new Product
            {
                Name = txtName.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Number = txtNumber.Text.Trim(),
                Price = price,
                SupplierID = _supplierId,
                CategoryID = (int?)cmbCategory.SelectedValue ?? 0,
                ManufacturerID = (int?)cmbManufacturer.SelectedValue ?? 0
            };

            _productManager.Create(product);

            MessageBox.Show("Товар успішно створений!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbManufacturer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
