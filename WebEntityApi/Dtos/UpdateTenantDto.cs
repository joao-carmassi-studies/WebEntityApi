using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record UpdateTenantDto
{
    [MinLength(3)]
    public string? Name { get; init; }
}
