
using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository.IRepository;
using Inventory.Models.Models;
using Inventory.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Json;

namespace InventoryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LabSuppliesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LabSuppliesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: LabSupplies
        public IActionResult Index()
        {
            List<LabSupply> labSupplyList = _unitOfWork.LabSupply.GetAll(includeProperties:"Supplier").ToList();
            
            return View(labSupplyList);
        }
      

        // GET: LabSupplies/Create
        public IActionResult Upsert(int? id)
        {          
            LabSupplyVM labSupplyVM = new()
            {
                SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()

                }),
                LabSupply = new LabSupply()
            };

            if(id == null || id == 0)
            {
                //Create
                return View(labSupplyVM);
            }
            else
            {
                //Update
                labSupplyVM.LabSupply = _unitOfWork.LabSupply.Get(u => u.SupplyID==id);
                return View(labSupplyVM);
            }
            
        }

        // POST: LabSupplies/Create
        [HttpPost]
        public IActionResult Upsert(LabSupplyVM labSupplyVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\supply");

                    if(!string.IsNullOrEmpty(labSupplyVM.LabSupply.ImageURL)) 
                    {
                        //Delete old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, labSupplyVM.LabSupply.ImageURL.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //Upload New Image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    //Update ImageURL
                    labSupplyVM.LabSupply.ImageURL = @"\images\supply\" + fileName;

                }

                if(labSupplyVM.LabSupply.SupplyID == 0)
                {
                    _unitOfWork.LabSupply.Add(labSupplyVM.LabSupply);
                }
                else
                {
                    _unitOfWork.LabSupply.update(labSupplyVM.LabSupply);
                }

                
                _unitOfWork.Save();
                TempData["success"] = "LabSupply Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                labSupplyVM.SupplierList = _unitOfWork.Supplier.GetAll().Select(u => new SelectListItem
                {
                    Text = u.SupplierName,
                    Value = u.SupplierID.ToString()
                });
                return View(labSupplyVM);
            }         
        }

        //// GET: LabSupplies/Edit/5
        //public IActionResult Edit(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    LabSupply? labSupplyFromDb = _unitOfWork.LabSupply.Get(u => u.SupplyID == id);

        //    if (labSupplyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(labSupplyFromDb);
        //}

        //// POST: LabSupplies/Edit/5
        //[HttpPost]
        //public IActionResult Edit(LabSupply obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.LabSupply.update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "LabSupply Updated Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}



        ///// GET: LabSupplies/Delete/5
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    LabSupply? labSupplyFromDb = _unitOfWork.LabSupply.Get(u => u.SupplyID == id);

        //    if (labSupplyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(labSupplyFromDb);

        //}

        //// POST: LabSupplies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    LabSupply? obj = _unitOfWork.LabSupply.Get(u => u.SupplyID == id);
            
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.LabSupply.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "LabSupply Deleted Successfully";
        //    return RedirectToAction("Index");            
        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<LabSupply> labSupplyList = _unitOfWork.LabSupply.GetAll(includeProperties: "Supplier").ToList();

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Add any other serialization options if needed
            };

            var jsonData = JsonSerializer.Serialize(new { data = labSupplyList }, jsonOptions);

            return Content(jsonData, "application/json");
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var supplyToBeDeleted = _unitOfWork.LabSupply.Get(u => u.SupplyID == id);
            if (supplyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

           var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
               supplyToBeDeleted.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.LabSupply.Remove(supplyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}


