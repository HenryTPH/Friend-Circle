using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController: BaseApiController
{
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");
        //Initialize a new instance of HMACSHA512 class with a randomly generated key used as password salt
        //By using "using" keyword, when we finish with this class, a dispose method will be called and the class will be removed from memory
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDto.Username,
            //Hash the password using hmac, ComputeHash will return a byte array
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        //Add the new user to the Users table
        _context.Users.Add(user);
        //Save the changes asynchronously.
        await _context.SaveChangesAsync();
        return user;
    }
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
    }
}