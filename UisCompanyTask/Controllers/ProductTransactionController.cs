using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using UisCompanyTask.IService;
using UisCompanyTask.Models;
using UisCompanyTask.Services;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.Controllers
{
    public class ProductTransactionController : Controller
    {
        private readonly IProductTransactionService productTransactionService;
        private readonly IProductservice productservice;
        public ProductTransactionController(IProductTransactionService productTransactionService, IProductservice productservice)
        {
            this.productTransactionService = productTransactionService;
            this.productservice = productservice;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, bool orderByDescending = false, string? orderBy = null)
        {
            Expression<Func<ProductTransaction, bool>>? filter = null;
            var response = await productTransactionService.GetAllTransactions(pageNumber, pageSize, orderByDescending, orderBy,null);
            var currentPageCount = response.Data?.Count ?? 0;
            bool hasNextPage = currentPageCount == pageSize;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderByDescending = orderByDescending;
            ViewBag.HasNextPage = hasNextPage;
            var TransactionData = response.Data;
            return View("Index", TransactionData);
        }
        public async Task<IActionResult> Search(DateTime? TransactionDate, int pageNumber = 1, int pageSize = 10, bool orderByDescending = false, string? orderBy = null)
        {
            var response = await productTransactionService.GetAllTransactions(pageNumber, pageSize, orderByDescending, orderBy, TransactionDate);
            var currentPageCount = response.Data?.Count ?? 0;
            bool hasNextPage = currentPageCount == pageSize;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderByDescending = orderByDescending;
            ViewBag.HasNextPage = hasNextPage;
            var TransactionData = response.Data;
            return PartialView("_ajaxViewGrid", TransactionData);
        }
        public async Task<IActionResult> Create()
        {
            var products = await productservice.GetAll();
            ViewBag.Products = new SelectList(products.Data, "Id", "Name");
            ViewBag.ProductInfo = products.Data.ToDictionary(p => p.Id, p => new { price = p.Price, unit = p.Unit, available = p.Quantity });
            return View();
        }
        public async Task<IActionResult> Save(ProductTransactionViewModel model)
        {
            var product = await productservice.GetById(model.productId);

            if (product == null)
            {
                ModelState.AddModelError("productId", "Invalid product selected.");
            }
            else if (model.Quantity > product.Data.Quantity)
            {
                ModelState.AddModelError("Quantity", "Quantity exceeds available stock.");
            }
            else if (product.Data.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "This product is out of stock and cannot be sold.");
            }
            if (!ModelState.IsValid)
            {
                var products = await productservice.GetAll();
                ViewBag.Products = new SelectList(products.Data, "Id", "Name");
                ViewBag.ProductInfo = products.Data.ToDictionary(p => p.Id, p => new { p.Price, p.Unit, available = p.Quantity });
                return View("Create", model);
            }
            await productTransactionService.Add(model);
            product.Data.Quantity -= model.Quantity;
            await productservice.Update(product.Data.Id,product.Data);

            TempData["SuccessMessage"] = "Transaction created successfully!";
            return RedirectToAction("Create");
        }

    }
}
