using System.ComponentModel.DataAnnotations;


namespace TSENA.Models;
public class ResetPasswordTokens
{
    [Key]
    public int Id {get; set;}

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string? Email {get; set;}

    [Required]
    [MaxLength(256)]
    public string? Token {get; set;}

    public DateTime ExpirationDate {get;set;}

    public bool IsUsed {get;set;} = false;
}