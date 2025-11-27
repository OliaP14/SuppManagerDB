using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace SuppManagerDB.WPF.Commands
{
    public class UnblockSupplierCommand : ICommand
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly ISupplierManager _supplierManager;

        public event EventHandler? CanExecuteChanged;

        public UnblockSupplierCommand(SuppliersListViewModel vm, ISupplierManager manager)
        {
            _viewModel = vm;
            _supplierManager = manager;
        }

        public bool CanExecute(object? parameter)
        {
            return parameter != null; // ❗ потрібен SupplierID
        }

        public void Execute(object? parameter)
        {
            int supplierId = (int)parameter;
            _supplierManager.SetStatus(supplierId, true); // ✔ розблокування
            _viewModel.RefreshSuppliers();
        }
    }
}
