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
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto a_User)
        {
            if(await UserExists(a_User.Username)) 
            {
                return BadRequest("Username is taken");
            }

            using var hashmac = new HMACSHA512();

            AppUser l_User = new AppUser(){
                Id = Guid.NewGuid().ToString(),
                UserName = a_User.Username.ToLower(),
                PasswordHash = hashmac.ComputeHash(Encoding.UTF8.GetBytes(a_User.Password)),
                PasswordSalt = hashmac.Key
            };

            _context.Users.Add(l_User);
            await _context.SaveChangesAsync();
            
            return new UserDto
            {
                Username = l_User.UserName,
                Token = _tokenService.CreateToken(l_User)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto a_LoginDto)
        {
            AppUser l_User = await _context.Users
            //este metodo serve para, caso haja mais de um, da um trigger no erro
                .SingleOrDefaultAsync(x => x.UserName == a_LoginDto.Username);

            if(l_User == null) return Unauthorized("Invalid username");

            using var l_Hashmac = new HMACSHA512(l_User.PasswordSalt);

            byte[] l_ComputedHash = l_Hashmac.ComputeHash(
                Encoding.UTF8.GetBytes(a_LoginDto.Password)
            );

            for(int i = 0; i < l_ComputedHash.Length; i++) 
            {
                if(l_ComputedHash[i] != l_User.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = l_User.UserName,
                Token = _tokenService.CreateToken(l_User)
            };
        }

        private async Task<bool> UserExists(string a_Username)
        {
            return await _context.Users
                .AnyAsync(x => x.UserName == a_Username.ToLower());
        }
    }
}