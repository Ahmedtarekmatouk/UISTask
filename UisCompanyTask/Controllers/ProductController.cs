using Microsoft.AspNetCore.Mvc;
using UisCompanyTask.IService;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductservice productservice;
        public ProductController(IProductservice productservice)
        {
            this.productservice = productservice;
        }
        public IActionResult Create()
        {
            return View("Create");
        }
        public async Task< IActionResult> save(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await productservice.AddProduct(model);
                TempData["SuccessMessage"] = "Product created successfully!";
                return RedirectToAction("create");
            }
            return View("Create", model);
        }
    }
}
