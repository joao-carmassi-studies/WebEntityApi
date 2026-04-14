using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record UserDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public DateTime CreationTime { get; init; }
}
