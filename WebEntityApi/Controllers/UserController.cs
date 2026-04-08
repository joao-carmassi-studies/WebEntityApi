using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Models;
using WebEntityApi.Repository;

namespace WebEntityApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private Dal<User> Users;
    private IMapper mapper;

    public UserController(Dal<User> users, IMapper mapper)
    {
        this.mapper = mapper;
        Users = users;
    }

    [HttpGet]
    public IEnumerable<UserDto> Get()
    {
        var users = Users.List();
        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        Users.Add(user);
        var userDto = mapper.Map<UserDto>(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = Users.Find(user => user.Id == id);
        if (user == null) return NotFound();

        var userDto = mapper.Map<UserDto>(user);
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
    public IActionResult Put(int id, [FromBody] UserDto userDto)
    {
        var user = Users.Find(user => user.Id == id);
        if (user == null) return NotFound();

        mapper.Map(userDto, user);
        Users.Update(user);
        return NoContent();
    }
}
