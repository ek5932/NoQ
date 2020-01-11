using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoQ.Merchant.Service.DataAccess.Contexts;
using NoQ.Merchant.Service.DataAccess.Entities;

namespace NoQ.Merchant.Service
{
    public class MockDataSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MerchantDbContext(serviceProvider.GetRequiredService<DbContextOptions<MerchantDbContext>>()))
            {
                context.Merchants.Add(new MerchantDetails { Id = 1 });
                context.Merchants.Add(new MerchantDetails { Id = 2 });
                context.Merchants.Add(new MerchantDetails { Id = 3 });

                context.SaveChanges();
            }
        }
    }
}