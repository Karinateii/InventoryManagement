
using Inventory.DataAccess.Data;
using Inventory.DataAccess.Repository;
using Inventory.DataAccess.Repository.IRepository;
using Inventory.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventoryManagement.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<LabSupply> supplyList = _unitOfWork.LabSupply.GetAll(includeProperties:"Supplier");
            return View(supplyList);
        }

        public IActionResult Details(int id)
        {
            LabSupply supply = _unitOfWork.LabSupply.Get(u =>u.SupplyID==id, includeProperties: "Supplier");
            return View(supply);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}