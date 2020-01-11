using NoQ.Framework.Mapping;
using NoQ.Merchant.Service.Domain.Entities;
using NoQ.Merchant.Service.WebApi.Resources;

namespace NoQ.Merchant.Service.WebApi.Mapping
{
    public class MerchantDetailsResourceMapper : AutoObjectMapper<IMerchantDetails, MerchantDetails>, IMerchantDetailsResourceMapper
    {
    }
}
