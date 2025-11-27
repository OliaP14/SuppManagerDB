using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using System;
using System.Windows.Input;

namespace SuppManagerDB.WPF.Commands
{
    public class SearchSupplierCommand : ICommand
    {
        private readonly SuppliersListViewModel _viewModel;
        private readonly ISupplierManager _supplierManager;

        public event EventHandler? CanExecuteChanged;

        public SearchSupplierCommand(SuppliersListViewModel vm, ISupplierManager manager)
        {
            _viewModel = vm;
            _supplierManager = manager;
        }

        public bool CanExecute(object? parameter)
        {
            return true; 
        }

        public void Execute(object? parameter)
        {
            _viewModel.Search();
        }
    }
}
