using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Supplier
        //Get All Suppliers
        IEnumerable<T> GetAll(string? includeProperties = null);

        //Similar to  Supplier? supplierFromDb1 = _db.Suppliers.FirstOrDefault(u=>u.SupplierID==id);
        //Get Particular Supplier
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        //Add Supplier
        void Add(T entity);

        //Remove Supplier
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
