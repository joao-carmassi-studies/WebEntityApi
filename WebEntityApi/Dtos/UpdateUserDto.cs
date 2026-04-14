using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record UpdateUserDto
{
    [MinLength(3)]
    public string? Name { get; init; }

    [EmailAddress]
    public string? Email { get; init; }

    [MinLength(6)]
    public string? PassWord { get; init; }
}
