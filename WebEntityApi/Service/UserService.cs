using WebEntityApi.Dtos;
using WebEntityApi.Exceptions;
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
        return user.ToDto();
    }

    async public Task<UserDto> Get(int id)
    {
        var user = await Users.FindAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User with id {id} not found.");
        return user.ToDto();
    }

    async public Task Delete(int id)
    {
        var user = await Users.FindAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User with id {id} not found.");
        await Users.Remove(user);
    }

    async public Task Update(int id, UpdateUserDto updateUserDto)
    {
        var user = await Users.FindAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User with id {id} not found.");
        user.UpdateFromTdo(updateUserDto);
        await Users.Update(user);
    }
}
