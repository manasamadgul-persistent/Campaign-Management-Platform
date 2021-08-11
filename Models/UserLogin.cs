using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Models
{
    public class UserLogin
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
