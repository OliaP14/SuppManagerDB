using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using SuppManagerDB.WPF.Windows;
using System;
using System.Windows.Input;

namespace SuppManagerDB.WPF.Commands
{
    public class OpenSupplierDetailsCommand : ICommand
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly ISupplierManager _supplierManager;

        public event EventHandler? CanExecuteChanged;

        public OpenSupplierDetailsCommand(SuppliersListViewModel vm, ISupplierManager manager)
        {
            _viewModel = vm;
            _supplierManager = manager;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter != null; // 💡 потрібен SupplierID
        }

        public void Execute(object? parameter)
        {
            int supplierId = (int)parameter;
            var detailsWindow = new SupplierDetails(supplierId, _supplierManager);

            detailsWindow.ShowDialog(); // 📌 модальне як у викладача
            _viewModel.RefreshSuppliers();
        }
    }
}
