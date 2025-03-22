using Microsoft.EntityFrameworkCore;

namespace UisCompanyTask.Models
{
    public class UisProductContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<ProductTransaction> productTransactions { get; set; }
        public UisProductContext(DbContextOptions<UisProductContext> options):base(options)
        {
        }
    }
}
