using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public class UpdateTenantDto
{
    [MinLength(3)]
    public string? Name { get; set; }
}
