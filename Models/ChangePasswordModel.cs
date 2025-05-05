using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class ChangePassword {
    [Required]
    [DataType(DataType.Password)]
    [Display(Name="Ancien mot de passe")]
    public string? CurrentPassword { get; set; }

    [Required]
    [StringLength(100, MinimumLength=6)]
    [DataType(DataType.Password)]
    [Display(Name="Nouveau mot de passe")]
    public string? NewPassword { get; set; }

    [Required]
    [Compare("NewPassword")]
    [DataType(DataType.Password)]
    [Display(Name="Confirmer le nouveau mot de passe")]
    public string? ConfirmPassword { get; set; }
}