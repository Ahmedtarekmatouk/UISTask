using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UisCompanyTask.Models
{
    public class ProductTransaction
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Unit { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        public virtual Product? Product { get; set; }
        
    }
}
