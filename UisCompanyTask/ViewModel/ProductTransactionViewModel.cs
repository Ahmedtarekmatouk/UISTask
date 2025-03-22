using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using UisCompanyTask.Models;

namespace UisCompanyTask.ViewModel
{
    public class ProductTransactionViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Unit { get; set; }
        public int productId { get; set; }
        public string? productName {  get; set; }
        public List<SelectListItem>? ProductList { get; set; }

        public static Expression<Func<ProductTransaction, ProductTransactionViewModel>> ProductTransactionProjection
        {
            get
            {
                return a => new ProductTransactionViewModel
                {
                    Id = a.Id,
                    Quantity = a.Quantity,
                    Date = a.Date,
                    TotalPrice = a.TotalPrice,
                    Unit = a.Unit,
                    productId = a.Product.Id,
                    productName=a.Product.Name
                };
            }
        }
        public static explicit operator ProductTransaction(ProductTransactionViewModel productTransactionViewModel)
        {
            return new ProductTransaction
            {
                Quantity = productTransactionViewModel.Quantity,
                Date = productTransactionViewModel.Date,
                TotalPrice = productTransactionViewModel.TotalPrice,
                Unit = productTransactionViewModel.Unit,
                productId = productTransactionViewModel.productId,
            };
        }
    }
}
