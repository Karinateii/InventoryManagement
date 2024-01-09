using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public ISupplierRepository Supplier{ get; private set; }
        public ILabSupplyRepository LabSupply { get; private set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Supplier = new SupplierRepository(_db);
            LabSupply = new LabSupplyRepository(_db);   
        }

       
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
