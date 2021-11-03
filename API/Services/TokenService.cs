using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        
        public TokenService(IConfiguration a_Config)
        {
            _key = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(a_Config["TokenKey"])
            );
        }

        public string CreateToken(AppUser a_User)
        {
            var l_Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, a_User.UserName)
            };

            var l_Credentials = new SigningCredentials
            (
                _key, 
                SecurityAlgorithms.HmacSha512Signature
            );

            var l_TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(l_Claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = l_Credentials
            };

            var l_TokenHandler = new JwtSecurityTokenHandler();

            var l_Token = l_TokenHandler.CreateToken(l_TokenDescriptor);

            return l_TokenHandler.WriteToken(l_Token);
        }
    }
}