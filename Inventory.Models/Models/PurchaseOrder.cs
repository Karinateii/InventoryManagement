using Inventory.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int QuantityOrdered { get; set; }
        public string OrderStatus { get; set; }

        public int SupplyID { get; set; }
        // Navigation property
        [ForeignKey("SupplyID")]
        public LabSupply LabSupply { get; set; }

    }
}
