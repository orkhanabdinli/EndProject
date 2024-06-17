using System.ComponentModel.DataAnnotations;

namespace EndProject.MVC.ViewModels;

public class RegisterViewModel
{
    [Required]
    [DataType(DataType.Text)]
    [StringLength(30)]
    public string FirstName { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [StringLength(30)]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Text)]
    [StringLength(30)]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [StringLength(50)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    [DataType(DataType.Text)]
    [StringLength(15)]
    public string Gender { get; set; }
    [DataType(DataType.Text)]
    [StringLength(30)]
    public string? Country { get; set; }
    [DataType(DataType.Text)]
    [StringLength(30)]
    public string? City { get; set; }
}
