using Microsoft.EntityFrameworkCore;
using NoQ.Merchant.Service.DataAccess.Entities;

namespace NoQ.Merchant.Service.DataAccess.Contexts
{
    public class MerchantDbContext : DbContext
    {
        public MerchantDbContext(DbContextOptions<MerchantDbContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<MerchantDetails> Merchants { get; set; }
    }
}
