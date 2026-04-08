using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Models;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string PassWord { get; set; }

    [Required]
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}
