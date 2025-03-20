using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class User {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    [DataType(DataType.Date)]
    public DateTime SignInDate { get; set; }
}