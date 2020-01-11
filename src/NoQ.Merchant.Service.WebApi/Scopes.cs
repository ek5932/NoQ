using System.Collections.Generic;
using System.ComponentModel;

namespace NoQ.Merchant.Service.WebApi
{
    public static class Scopes
    {
        public static KeyValuePair<string, string> ReadOnly = new KeyValuePair<string, string>("customAPI.read", "Read only access");
        public static KeyValuePair<string, string> Write = new KeyValuePair<string, string>("customAPI.write", "Write access");
    }
}
