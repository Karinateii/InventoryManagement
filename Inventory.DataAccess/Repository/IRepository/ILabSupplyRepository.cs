using Inventory.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository.IRepository
{
    public interface ILabSupplyRepository : IRepository<LabSupply>
    {
        void update(LabSupply obj);
    }
}
