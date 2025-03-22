using System.Linq.Expressions;
using UisCompanyTask.Models;

namespace UisCompanyTask.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public static explicit operator Product(ProductViewModel productViewModel)
        {
            return new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Quantity = productViewModel.Quantity,
                Unit = productViewModel.Unit
            };
        }
        public static Expression<Func<Product, ProductViewModel>> ProductViewModelProjection
        {
            get
            {
                return a => new ProductViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Unit = a.Unit,
                    Price = a.Price,
                    Quantity=a.Quantity,
                };
            }
        }
    }
}
