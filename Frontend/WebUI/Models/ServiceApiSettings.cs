using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ServiceApiSettings
    {
        public string  IdentityBaseUrl { get; set; }
        public string  GetwayBaseUrl { get; set; }
        public string  PhotoStockUrl { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi PhotoStock { get; set; }
    }


    public class ServiceApi
    {
        public string  Path { get; set; }
    }
}
