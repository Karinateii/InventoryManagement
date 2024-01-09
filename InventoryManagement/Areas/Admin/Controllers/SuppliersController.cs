using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository.IRepository;
using Inventory.Models.Models;
using Inventory.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class SuppliersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SuppliersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Suppliers
        public IActionResult Index()
        {
            List<Supplier> suppliersList = _unitOfWork.Supplier.GetAll().ToList();
            return View(suppliersList);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        public IActionResult Create(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Supplier Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Suppliers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Supplier? supplierFromDb = _unitOfWork.Supplier.Get(u => u.SupplierID == id);
            // Supplier? supplierFromDb1 = _db.Suppliers.FirstOrDefault(u=>u.SupplierID==id);
            // Supplier? supplierFromDb2 = _db.Suppliers.Where(u=>u.SupplierID==id).FirstOrDefault();

            if (supplierFromDb == null)
            {
                return NotFound();
            }
            return View(supplierFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Supplier Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Supplier? supplierFromDb = _unitOfWork.Supplier.Get(u => u.SupplierID == id);

            if (supplierFromDb == null)
            {
                return NotFound();
            }
            return View(supplierFromDb);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Supplier? obj = _unitOfWork.Supplier.Get(u => u.SupplierID == id);
            {
                if (obj == null)
                {
                    return NotFound();
                }

                _unitOfWork.Supplier.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Supplier Deleted Successfully";
                return RedirectToAction("Index");
            }

        }

    }
}
