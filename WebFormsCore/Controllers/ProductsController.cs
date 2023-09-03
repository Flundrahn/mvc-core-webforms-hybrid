using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebFormsCore.Models;

namespace WebFormsCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _repo;

        public ProductsController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _repo = new ProductRepository();
        }

        public IActionResult Index()
        {
            var entities = _repo.GetAll();
            var viewModels = entities.Select(x => new ProductsViewModel(x));

            return View(viewModels);
        }
    }
}
