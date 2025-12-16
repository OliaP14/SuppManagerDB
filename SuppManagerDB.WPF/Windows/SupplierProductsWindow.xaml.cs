using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using System.Drawing.Printing;
using System.Windows;

namespace SuppManagerDB.WPF.Windows
{
    public partial class SupplierProductsWindow : Window
    {
        private readonly int _supplierId;
        private readonly IProductManager _productManager;

        public SupplierProductsWindow(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;

            _productManager = (IProductManager)App.Services.GetService(typeof(IProductManager));

            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsGrid.ItemsSource = _productManager.GetBySupplier(_supplierId);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProductAddWindow(_supplierId);
            win.ShowDialog();
            LoadProducts();
        }

        // Редагування товару
        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product product)
            {
                var win = new ProductEditWindow(product);
                win.ShowDialog();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Оберіть товар для редагування!");
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is Product product)
            {
                if (MessageBox.Show("Видалити цей товар?", "Підтвердження",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _productManager.Delete(product.ProductID);
                    LoadProducts();
                }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ProductsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
