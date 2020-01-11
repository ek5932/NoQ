using System.Threading.Tasks;
using NoQ.Merchant.Service.Domain.Entities;

namespace NoQ.Merchant.Service.Domain.Repositorties
{
    public interface IMerchantRepository
    {
        Task<IMerchantDetails[]> GetAll();
        Task<IMerchantDetails> GetById(int id);
    }
}
