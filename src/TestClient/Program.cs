using System;
using System.Net.Http;
using IdentityModel.Client;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.SetBearerToken("pLCnPbC19suZLZHFs7kzt3agfmv81q_NmtwnDk_90aA");

            var r = httpClient.GetAsync("http://localhost:5000/api/v1/GetAll").Result;

        }
    }

    public class Test : HttpMessageInvoker
    {
        public Test(HttpMessageHandler handler) : base(handler)
        {
        }
    }
}
