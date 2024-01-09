using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISupplierRepository Supplier { get; }
        ILabSupplyRepository LabSupply { get; }

        void Save();
    }
}
