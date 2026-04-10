using WebEntityApi.Models;

namespace WebEntityApi.Dtos;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreationTime = user.CreationTime
        };
    }

    public static User ToEntity(this CreateUserDto createUserDto)
    {
        return new User
        {
            Email = createUserDto.Email,
            Name = createUserDto.Name,
            PassWord = createUserDto.PassWord
        };
    }

    public static void UpdateFromTdo(this User user, UpdateUserDto updateUserTdo)
    {
        user.Email = updateUserTdo.Email ?? user.Email;
        user.Name = updateUserTdo.Name ?? user.Name;
        user.PassWord = updateUserTdo.PassWord ?? user.PassWord;
    }
}
