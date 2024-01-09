using Inventory.Models.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [Required]
        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [Required]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        // Navigation property
        public List<LabSupply>? LabSupplies { get; set; }
        //public List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
