using Microsoft.AspNetCore.Mvc;
using WebEntityApi.Dtos;
using WebEntityApi.Models;
using WebEntityApi.Repository;

namespace WebEntityApi.Service;

public class UserService
{
    private Dal<User> Users;

    public UserService(Dal<User> users)
    {
        Users = users;
    }

    async public Task<IEnumerable<UserDto>> ListAll()
    {
        var users = await Users.ListAsync();
        return users.Select(u => u.ToDto());
    }

    async public Task<UserDto> Create(CreateUserDto createUserDto)
    {
        var user = createUserDto.ToEntity();
        await Users.AddAsync(user);
        var userDto = user.ToDto();
        return userDto;
    }

    async public Task<UserDto?> Get(int id)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        return user?.ToDto();
    }

    async public Task<bool> Delete(int id)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        if (user == null) return false;

        await Users.Remove(user);
        return true;
    }

    async public Task<bool> Update(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await Users.FindAsync(user => user.Id == id);
        if (user == null) return false;

        user.UpdateFromTdo(updateUserDto);
        await Users.Update(user);
        return true;
    }
}
