using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WPF.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SuppManagerDB.WPF.ViewModels
{
    public class SuppliersListViewModel : INotifyPropertyChanged
    {
        private readonly ISupplierManager _supplierManager;
        private readonly IProductManager _productManager;   //+


        public ObservableCollection<Supplier> Suppliers { get; set; }

        public string SearchText { get; set; } = string.Empty;

        // Команди 

        public ICommand SearchSupplierCommand { get; set; }
        public ICommand BlockSupplierCommand { get; set; }
        public ICommand UnblockSupplierCommand { get; set; }
        public ICommand OpenDetailsCommand { get; set; }
        public ICommand AddSupplierCommand { get; set; }
        public ICommand AddProductCommand { get; set; }   //+

        public SuppliersListViewModel(ISupplierManager supplierManager, IProductManager productManager)
        {
            _supplierManager = supplierManager;
            _productManager = productManager;   //+

            // 1. Завантажуємо список постачальників
            Suppliers = new ObservableCollection<Supplier>(_supplierManager.GetAll());

            // 2. підключаємо команди:
            SearchSupplierCommand = new SearchSupplierCommand(this, _supplierManager);
            BlockSupplierCommand = new BlockSupplierCommand(this, _supplierManager);
            UnblockSupplierCommand = new UnblockSupplierCommand(this, _supplierManager);
            OpenDetailsCommand = new OpenSupplierDetailsCommand(this, _supplierManager);
            AddSupplierCommand = new AddSupplierCommand(this, _supplierManager);
            AddProductCommand = new AddProductCommand(this, _supplierManager, _productManager);   //+


        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void RefreshSuppliers()
        {
            Suppliers.Clear();
            var newList = _supplierManager.GetAll();
            foreach (var s in newList)
                Suppliers.Add(s);
        }

        public void Search()
        {
            Suppliers.Clear();
            var filtered = _supplierManager.Search(SearchText);
            foreach (var s in filtered)
                Suppliers.Add(s);
        }


    }
}
