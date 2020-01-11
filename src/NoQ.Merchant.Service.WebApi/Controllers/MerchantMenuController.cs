using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoQ.Framework.Extensions;

namespace NoQ.Merchant.Service.WebApi.Controllers
{
    public class MerchantMenuController : ControllerBase
    {
        private readonly ILogger<MerchantMenuController> _logger;

        public MerchantMenuController(ILogger<MerchantMenuController> logger)
        {
            _logger = logger.VerifyNotNull(nameof(logger));
        }
    }
}
