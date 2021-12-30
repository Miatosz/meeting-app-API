using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using MeetingAppAPI.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MeetingAppAPI.Data.Models.Security;
using System.Text;
using MeetingAppAPI.Data.Interfaces.Services;
using MeetingAppAPI.Data;
using MeetingAppAPI.Data.Models.DTOs.ModelsDtos;
using MeetingAppAPI.Data.Models.DTOs.HelperDtos.User;
using System.Security.Cryptography;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MeetingAppAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]/")]
    public class AccountController : ControllerBase
    {
        private ITokenService _tokenService;
        private AppDbContext _context;

        public AccountController(AppDbContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost]
        [ActionName("register")]
        public async Task<ActionResult<UserWToken>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Name)) return BadRequest("Username is taken");


            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.Name.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserWToken
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<ActionResult<UserWToken>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.UserName == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            } 

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserWToken
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost]
        [ActionName("changePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto modelDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(c => c.UserName == modelDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            } 

            if (user.Password != modelDto.OldPassword)
            {
                return Unauthorized("Invalid Password");
            } 

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(modelDto.NewPassword));

            user.Password = modelDto.NewPassword;
            user.PasswordHash = computedHash;
            

            return Ok();
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(c => c.UserName == username.ToLower());
        }







       // private ITokenService _tokenService;


        // [HttpPost]
        // [ActionName("Login")]
        // public IActionResult Login([FromBody] LoginModel User)
        // {
        //     // TODO: Authenticate Admin with Database
        //     // If not authenticate return 401 Unauthorized
        //     // Else continue with below flow

        //     var Claims = new List<Claim>
        //     {
        //         new Claim("type", "Admin"),
        //     };

        //     var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

        //     var Token = new JwtSecurityToken(
        //         "https://localhost:5001",
        //         "https://localhost:5001",
        //         Claims,
        //         expires: DateTime.Now.AddDays(30.0),
        //         signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256)
        //     );

        //     Console.WriteLine(Claims.ToList());

        //     return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(Token));
        // }

        
    }
}