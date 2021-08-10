using CMPApiMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Service
{
    public interface IAuthenticateService
    {
        UserLogin Authenticate(string userName, string Password);
    }
}
