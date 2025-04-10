using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class User {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    [DataType(DataType.Date)]
    public DateTime SignInDate { get; set; }
}