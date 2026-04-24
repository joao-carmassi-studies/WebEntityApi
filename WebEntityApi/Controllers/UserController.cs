using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Models;
using WebEntityApi.Repository;
using WebEntityApi.Service;

namespace WebEntityApi.Dtos;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService UserService;

    public UserController(UserService service)
    {
        UserService = service;
    }

    [HttpGet]
    async public Task<IEnumerable<UserDto>> Get()
    {
        return await UserService.ListAll();
    }

    [HttpPost]
    async public Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
    {
        var userDto = UserService.Create(createUserDto);
        return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetUser(int id)
    {
        var userDto = await UserService.Get(id);
        if (userDto == null) return NotFound();
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        var deleted = await UserService.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var updated = await UserService.Update(id, updateUserDto);
        if (!updated) return NotFound();
        return NoContent();
    }
}
