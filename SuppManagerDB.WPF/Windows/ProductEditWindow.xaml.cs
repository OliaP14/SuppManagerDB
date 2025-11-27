using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;

namespace SuppManagerDB.WPF.Windows
{
    public partial class ProductEditWindow : Window
    {
        private readonly IProductManager _productManager;
        private readonly Product _product;

        public ProductEditWindow(Product product)
        {
            InitializeComponent();
            _productManager = (IProductManager)App.Services.GetService(typeof(IProductManager));
            _product = product;

            txtName.Text = product.Name;
            txtModel.Text = product.Model;
            txtNumber.Text = product.Number;
            txtPrice.Text = product.Price.ToString();

            cmbCategory.ItemsSource = _productManager.GetAllCategories();
            cmbManufacturer.ItemsSource = _productManager.GetAllManufacturers();

            cmbCategory.SelectedValue = product.CategoryID;
            cmbManufacturer.SelectedValue = product.ManufacturerID;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву товару!");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var price) || price <= 0)
            {
                MessageBox.Show("Введіть коректну ціну (більше 0)!");
                return;
            }

            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Оберіть категорію!");
                return;
            }

            if (cmbManufacturer.SelectedValue == null)
            {
                MessageBox.Show("Оберіть виробника!");
                return;
            }

            // Оновлення значення
            _product.Name = txtName.Text.Trim();
            _product.Model = txtModel.Text.Trim();
            _product.Number = txtNumber.Text.Trim();
            _product.Price = price;
            _product.CategoryID = (int)cmbCategory.SelectedValue;
            _product.ManufacturerID = (int)cmbManufacturer.SelectedValue;

            // Спроба оновлення у БД
            var success = _productManager.Update(_product);

            if (success)
            {
                MessageBox.Show("Товар успішно оновлено!", "Оновлено", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Помилка при оновленні товару!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
