using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.CustomModels
{

    public class APIResponse
    {
        public bool status { get; set; }
        public int statuscode { get; set; }
        public string message { get; set; }
        public object items { get; set; }
        public object errorlist { get; set; }
    }
    public class ErrorList
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
    public class APIUsers
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string access_token { get; set; }
        public string phone_token { get; set; }
        public DateTime expiration { get; set; }
        public object Member { get; set; }
        public string Name { get; set; }
        public string Pic { get; set; }
        public bool TokenwasSent { get; set; }
    }
}
