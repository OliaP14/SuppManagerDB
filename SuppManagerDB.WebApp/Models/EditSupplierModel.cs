using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuppManagerDB.WebApp.Models
{
    public class EditSupplierModel
    {
        public int SupplierId { get; set; }

        // Name
        [Required(ErrorMessage = "Supplier name is required")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "Supplier name must be between 3 and 20 characters")]
        [DisplayName("Supplier Name")]
        public string Name { get; set; } = string.Empty;

        // Info( phone / email)
        [Required(ErrorMessage = "Contact info is required")]
        [StringLength(15,
            ErrorMessage = "Info must not exceed 15 characters")]
        [DisplayName("Contact Info")]
        public string Info { get; set; } = string.Empty;

        // Location
        [Required(ErrorMessage = "Location is required")]
        [StringLength(50,
            ErrorMessage = "Location must not exceed 50 characters")]
        [DisplayName("Location")]
        public string Location { get; set; } = string.Empty;

        // Status
        [DisplayName("Active")]
        public bool Status { get; set; }
    }
}
