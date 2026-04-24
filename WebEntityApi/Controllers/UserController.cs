using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Service;

namespace WebEntityApi.Controllers;

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
        var userDto = await UserService.Create(createUserDto);
        return CreatedAtAction(nameof(GetUser), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id}")]
    async public Task<IActionResult> GetUser(int id)
    {
        var userDto = await UserService.Get(id);
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    async public Task<IActionResult> Delete(int id)
    {
        await UserService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    async public Task<IActionResult> Put(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        await UserService.Update(id, updateUserDto);
        return NoContent();
    }
}
