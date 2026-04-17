using Microsoft.AspNetCore.Mvc;
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
    async public Task<IEnumerable<UserDto>> Get()
    {
        var users = await Users.ListAsync();
        return users.Select(u => u.ToDto());
    }

    [HttpPost]
    async public Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
    {
        var user = createUserDto.ToEntity();
        await Users.AddAsync(user);
        var userDto = user.ToDto();
        return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetUser(int id)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        if (user == null) return NotFound();

        var userDto = user.ToDto();
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        if (user == null) return NotFound();

        await Users.Remove(user);
        return NoContent();
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        if (user == null) return NotFound();

        user.UpdateFromTdo(updateUserDto);
        await Users.Update(user);
        return NoContent();
    }
}
