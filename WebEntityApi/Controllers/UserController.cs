using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Models;
using WebEntityApi.Repository;

namespace WebEntityApi.Dtos;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private Dal<User> Users;

    public UserController(Dal<User> users)
    {
        Users = users;
    }

    [HttpGet]
    public IEnumerable<UserDto> Get()
    {
        var users = Users.List();
        return users.Select(u => u.ToDto());
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateUserDto createUserDto)
    {
        var user = createUserDto.ToEntity();
        Users.Add(user);
        var userDto = user.ToDto();
        return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = Users.Find(user => user.Id == id);
        if (user == null) return NotFound();

        var userDto = user.ToDto();
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = Users.Find(user => user.Id == id);
        if (user == null) return NotFound();

        Users.Remove(user);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = Users.Find(user => user.Id == id);
        if (user == null) return NotFound();

        user.UpdateFromTdo(updateUserDto);
        Users.Update(user);
        return NoContent();
    }
}
