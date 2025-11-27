using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace SuppManagerDB.WPF.Commands
{
    public class SaveSupplierCommand : ICommand
    {
        private readonly SupplierDetailsViewModel _vm;
        private readonly ISupplierManager _manager;

        public event EventHandler? CanExecuteChanged;

        public SaveSupplierCommand(SupplierDetailsViewModel vm, ISupplierManager manager)
        {
            _vm = vm;
            _manager = manager;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _manager.Update(_vm.Supplier);
            MessageBox.Show("Зміни збережено успішно!", "Успішно", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
