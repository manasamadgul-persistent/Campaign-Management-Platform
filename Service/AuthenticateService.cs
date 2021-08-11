using CMPApiMicroservice.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMPApiMicroservice.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        // Added UserLogin for authentication
        private List<UserLogin> userLogin = new List<UserLogin>()
        {
            new UserLogin{
                UserId = 1,
                UserName = "Test",
                Password = "Test123"
            }
        };

        //Create token for authentication 
        public UserLogin Authenticate(string userName, string Password)
        {
            var data = userLogin.SingleOrDefault(au => au.UserName == userName && au.Password == Password);
            if(data == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name,data.UserId.ToString()),
                        new Claim(ClaimTypes.Role,"Admin"),
                    }),
                    Expires = DateTime.UtcNow.AddDays(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                data.Token = tokenHandler.WriteToken(token);
                data.Password = null;
                return data;
             }
        }
    }
}
