using System.ComponentModel.DataAnnotations;


namespace TSENA.Models;
public class ResetPasswordTokens
{
    public int Id {get; set;}

    public string? Email {get; set;}

    public string? Token {get; set;}

    public DateTime ExpirationDate {get;set;}

    public bool IsUsed {get;set;} = false;

    public string? NewPassword { get; set; }

    public string? ConfirmPassword { get; set; }
}