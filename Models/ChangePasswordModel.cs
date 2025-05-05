using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class ChangePassword {
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}