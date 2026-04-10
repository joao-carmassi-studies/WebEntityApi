using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Models;

public class Tenant
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string Name { get; set; }

    [Required]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    [Required]
    public int OwnerId { get; set; }

    [Required]
    public User Owner { get; set; }
}
