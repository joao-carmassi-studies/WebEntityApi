using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record CreateTenantDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; init; }

    [Required]
    public int OwnerId { get; init; }
}
