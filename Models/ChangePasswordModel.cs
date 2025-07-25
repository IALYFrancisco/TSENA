using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class ChangePasswordModel {
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}