using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository.IRepository;
using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository
{
    public class LabSupplyRepository : Repository<LabSupply>, ILabSupplyRepository
    {
        private AppDbContext _db;

        public LabSupplyRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(LabSupply obj)
        {
           var objFromDb = _db.LabSupplies.FirstOrDefault(u=>u.SupplyID == obj.SupplyID);
            if (objFromDb != null)
            {
                objFromDb.SupplyName = obj.SupplyName;
                objFromDb.QuantityOnHand = obj.QuantityOnHand;
                objFromDb.ReorderPoint = obj.ReorderPoint;
                objFromDb.ImageURL = obj.ImageURL;
                if (objFromDb.ImageURL != null)
                {
                    objFromDb.ImageURL = obj.ImageURL;   
                }
            }
        }
    }
}
