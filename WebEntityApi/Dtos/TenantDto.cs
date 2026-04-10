using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public class TenantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationTime { get; set; }
    public int OwnerId { get; set; }
}
