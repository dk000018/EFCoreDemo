using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.DTOs;
using MyApp.Entities;
using System;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UserGetDTO>> CreateUser(UserPostDTO user)
        {

            var newUser = new UserEntity
            {
                Name = user.Name,
                Email = user.Email
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var result = new UserGetDTO
            {
                Id = newUser.Id.ToString(),
                Name = newUser.Name,
                Email = newUser.Email
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new UserGetDTO
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTO>> GetUserById(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                return BadRequest("Invalid ID");

            var user = await _context.Users.FindAsync(guid);
            if (user == null)
                return NotFound();

            return Ok(new UserGetDTO
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email
            });
        }
    }
}
