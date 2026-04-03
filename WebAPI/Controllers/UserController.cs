using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; // Hozircha DI qilmaymiz, test uchun yetarli
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsersFromFile();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, User user)
        {
            user.Id = id;
            var updatedUser = _userService.UpdateUser(user);
            if (updatedUser == null) return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            _userService.DeleteUser(user);
            return NoContent();
        }
    }
}