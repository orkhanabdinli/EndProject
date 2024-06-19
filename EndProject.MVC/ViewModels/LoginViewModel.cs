using System.ComponentModel.DataAnnotations;

namespace EndProject.MVC.ViewModels;

public class LoginViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [StringLength(50)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
