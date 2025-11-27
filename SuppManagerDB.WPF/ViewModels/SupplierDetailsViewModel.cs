using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DTO;
using SuppManagerDB.WPF.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuppManagerDB.WPF.ViewModels
{
    public class SupplierDetailsViewModel : INotifyPropertyChanged
    {
        private readonly ISupplierManager _supplierManager;

        private Supplier _supplier;
        public Supplier Supplier
        {
            get => _supplier;
            set { _supplier = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => Supplier.Name;
            set { Supplier.Name = value; OnPropertyChanged(); }
        }

        public string Info
        {
            get => Supplier.Info;
            set { Supplier.Info = value; OnPropertyChanged(); }
        }

        public string Location
        {
            get => Supplier.Location;
            set { Supplier.Location = value; OnPropertyChanged(); }
        }

        public bool Status
        {
            get => Supplier.Status;
            set { Supplier.Status = value; OnPropertyChanged(); }
        }

        public SaveSupplierCommand SaveSupplierCommand { get; set; }

        public SupplierDetailsViewModel(int supplierId, ISupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
            Supplier = _supplierManager.GetAll().FirstOrDefault(s => s.SupplierID == supplierId);
            SaveSupplierCommand = new SaveSupplierCommand(this, _supplierManager);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
