using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoQ.Merchant.Service.Configuration
{
    internal class AuthenticationConfig
    {
        public string ServerUri { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
    }
}
