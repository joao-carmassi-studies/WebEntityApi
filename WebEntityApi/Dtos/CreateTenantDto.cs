using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public class CreateTenantDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }

    [Required]
    public int OwnerId { get; set; }
}
