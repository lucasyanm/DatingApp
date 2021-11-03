using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Text;
using System.Security.Cryptography;
using API.DTOs;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto a_User)
        {
            using var hashmac = new HMACSHA512();

            AppUser l_User = new AppUser(){
                Id = Guid.NewGuid().ToString(),
                UserName = a_User.username,
                PasswordHash = hashmac.ComputeHash(Encoding.UTF8.GetBytes(a_User.password)),
                PasswordSalt = hashmac.Key
            };

            _context.Users.Add(l_User);
            await _context.SaveChangesAsync();
            
            return l_User;
        }
    }
}