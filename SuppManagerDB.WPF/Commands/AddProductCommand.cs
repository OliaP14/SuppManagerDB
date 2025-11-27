using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SuppManagerDB.WPF.Commands
{
    public class AddProductCommand : ICommand
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly IProductManager _productManager;
        private readonly ISupplierManager _supplierManager;

        public event EventHandler? CanExecuteChanged;

        public AddProductCommand(
            SuppliersListViewModel vm,
            ISupplierManager supplierManager,
            IProductManager productManager)
        {
            _viewModel = vm;
            _supplierManager = supplierManager;
            _productManager = productManager;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            MessageBox.Show("Щоб додати товар, спочатку відкрийте деталі постачальника, а потім виберіть 'Товари постачальника'.",
                "Підказка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
