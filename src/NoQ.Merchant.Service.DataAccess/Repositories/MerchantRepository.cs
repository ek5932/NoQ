using System.Threading.Tasks;
using NoQ.Merchant.Service.DataAccess.Contexts;
using NoQ.Merchant.Service.DataAccess.Entities;
using NoQ.Merchant.Service.DataAccess.Framework;
using NoQ.Merchant.Service.Domain.Entities;
using NoQ.Merchant.Service.Domain.Repositorties;

namespace NoQ.Merchant.Service.DataAccess.Repositories
{
    public class MerchantRepository : RepositoryBase<MerchantDbContext, MerchantDetails>, IMerchantRepository
    {
        public MerchantRepository(MerchantDbContext merchantDbContext) : base (merchantDbContext, (x) => x.Merchants)
        {
        }

        public new async Task<IMerchantDetails[]> GetAll() => await base.GetAll();

        public new async Task<IMerchantDetails> GetById(int id) => await base.GetById(id);
    }
}
