using System.Net.NetworkInformation;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    // public UserController (DataContext context)
    public UserController (IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
    {
        // _context = context;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    // [AllowAnonymous]
    [HttpGet]
    // public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() Section 97
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // var users = await _context.Users.ToListAsync();
        // var users = await _userRepository.GetUsersAsync();
        // return users; - get an error
        // return Ok(await _userRepository.GetUsersAsync()); Section 97
        // var users = await _userRepository.GetUsersAsync(); Section 99
        // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users); Section 99
        // return Ok(usersToReturn); Section 99
        var users = await _userRepository.GetMembersAsync();
        return Ok(users);
    }

    // [HttpGet("{id}")] // /api/users/2
    [HttpGet("{username}")]
    // public async Task<ActionResult<AppUser>> GetUser(string username): Section 97
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        // var user = await _context.Users.FindAsync(id);
        // return user;
        // var user =  await _userRepository.GetUserByUsernameAsync(username);
        // return _mapper.Map<MemberDto>(user);
        return await _userRepository.GetMemberAsync(username);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {        
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return NotFound();
        // Map every properties in memberUpdateDto into user but has not updated to the database yer yet
        _mapper.Map(memberUpdateDto, user);
        // Return 204: everything is ok but there is nothing giving back
        if(await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if(user == null) return NotFound();
        var result = await _photoService.AddPhotoAsync(file);
        if(result.Error != null) return BadRequest(result.Error.Message);
        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        if(user.Photos.Count == 0) photo.IsMain = true;
        user.Photos.Add(photo);
        if(await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
        return BadRequest("Problem adding photo");
    } 
}