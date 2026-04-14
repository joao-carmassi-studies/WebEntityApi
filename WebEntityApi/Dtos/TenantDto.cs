using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public record TenantDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime CreationTime { get; init; }
    public int OwnerId { get; init; }
}
