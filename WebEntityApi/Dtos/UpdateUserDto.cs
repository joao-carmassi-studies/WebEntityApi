using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public class UpdateUserDto
{
    [MinLength(3)]
    public string? Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(6)]
    public string? PassWord { get; set; }
}
