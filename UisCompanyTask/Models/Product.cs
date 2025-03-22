using System.ComponentModel.DataAnnotations;

namespace UisCompanyTask.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }= Guid.NewGuid().ToString("N").ToUpper();
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public virtual ICollection<ProductTransaction>? ProductTransactions { get; set; }
    }
}
