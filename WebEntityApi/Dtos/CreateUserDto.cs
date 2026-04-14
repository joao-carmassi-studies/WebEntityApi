using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record CreateUserDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [MinLength(6)]
    public string PassWord { get; init; }
}
