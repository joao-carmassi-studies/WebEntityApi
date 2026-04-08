using System.ComponentModel.DataAnnotations;

namespace WebEntityApi.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreationTime { get; set; }
}
