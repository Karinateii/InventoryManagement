//using Microsoft.AspNetCore.Mvc;
using Inventory.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models.Models
{
    public class LabSupply
    {
        [Key]
        public int SupplyID { get; set; }

        [Required]
        [Display(Name = "Supply Name")]
        public string SupplyName { get; set; }

        [Required]
        [Display(Name = "Quantity on Hand")]
        public int QuantityOnHand { get; set; }
               
        [Required]
        [Display(Name = "Reorder Point")]
        [Range(1, int.MaxValue, ErrorMessage = "Reorder Point must be a positive value")]
        public int ReorderPoint { get; set; }

        [ValidateNever]
        public string ImageURL { get; set; }

        [Display(Name = "Supplier Name")]
        public int SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        [ValidateNever]
        public Supplier? Supplier { get; set; }

        // public List<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}
