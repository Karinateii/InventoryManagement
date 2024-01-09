using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository.IRepository;
using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private AppDbContext _db;

        public SupplierRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Supplier obj)
        {
            _db.Suppliers.Update(obj);
        }
    }
}
