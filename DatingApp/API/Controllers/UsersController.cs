using System.Net.NetworkInformation;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users because the name of the file is UsersController, so the path will take the name before the word "Controller"
public class UserController: ControllerBase
{
    private readonly DataContext _context;
    public UserController (DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users.ToList();
        return users;
    }

    [HttpGet("{id}")] // /api/users/2
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = _context.Users.Find(id);
        return user;
    }

}