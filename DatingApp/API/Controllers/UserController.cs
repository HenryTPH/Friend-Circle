using System.Net.NetworkInformation;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[Authorize]
//When using BaseApiController insteads of BaseController, we can omit those line of codes.
// [ApiController]
// [Route("api/[controller]")] // /api/users because the name of the file is UsersController, so the path will take the name before the word "Controller"
public class UserController: BaseApiController
{
    // private readonly DataContext _context;
    private readonly IUserRepository _userRepository;
    // public UserController (DataContext context)
    public UserController (IUserRepository userRepository)
    {
        // _context = context;
        _userRepository = userRepository;
    }
    
    // [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        // var users = await _context.Users.ToListAsync();
        // var users = await _userRepository.GetUsersAsync();
        // return users; - get an error
        return Ok(await _userRepository.GetUsersAsync());
    }

    // [HttpGet("{id}")] // /api/users/2
    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUser(string username)
    {
        // var user = await _context.Users.FindAsync(id);
        // return user;
        return await _userRepository.GetUserByUsernameAsync(username);
    }

}