using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoQ.Framework.Extensions;
using NoQ.Merchant.Service.Domain.Repositorties;
using NoQ.Merchant.Service.WebApi.Framework;
using NoQ.Merchant.Service.WebApi.Mapping;
using NoQ.Merchant.Service.WebApi.Resources;

namespace NoQ.Merchant.Service.WebApi.Controllers
{
    [Route("api/v1")]
    public class MerchantDetailsController : WebApiBase<MerchantDetailsController, MerchantDetails>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ILogger<MerchantDetailsController> _logger;
        private readonly IMerchantDetailsResourceMapper _merchantDetailsResourceMapper;

        public MerchantDetailsController(ILogger<MerchantDetailsController> logger, IMerchantRepository merchantRepository, IMerchantDetailsResourceMapper merchantDetailsResourceMapper)
            : base(logger)
        {
            _logger = logger.VerifyNotNull(nameof(logger));
            _merchantRepository = merchantRepository.VerifyNotNull(nameof(merchantRepository));
            _merchantDetailsResourceMapper = merchantDetailsResourceMapper.VerifyNotNull(nameof(merchantDetailsResourceMapper));
        }

        [HttpGet("GetAll")]
        public Task<IActionResult> GetAll()
        {
            return Multiple(_merchantDetailsResourceMapper,
                 _merchantRepository.GetAll);
        }

        [Authorize(Policy = Policies.Write)]
        [HttpGet("GetById")]
        public Task<IActionResult> Get(int id)
        {
            return Single(_merchantDetailsResourceMapper,
                () => _merchantRepository.GetById(id));
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Ok");
        }
    }
}
