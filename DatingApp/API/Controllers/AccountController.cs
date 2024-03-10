using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController: BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")] // POST: api/account/register?username=dave&password=pwd
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
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
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        //Find function will find with the primary key, but username is not the primary key
        //We have option 1: use FirstOrDefaultAsync => return the 1st element of a sequence or defaul value (Null) if we cannot find the user
        //We have option 2: use SingleOrDefault => return only element of a sequence or a default value if the sequence is empty. If more
        //than 1 user have the same name, it will throw an exception
        var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username);
        if (user == null) return Unauthorized("Invalid username");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        // for(int i = 0; i < computedHash.Length; i++)
        // {
        //     if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        // }
        var check = computedHash.SequenceEqual(user.PasswordHash); 
        if(!check) return Unauthorized("Invalid Password");

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.UserName.ToLower() == username.ToLower());
    }
}