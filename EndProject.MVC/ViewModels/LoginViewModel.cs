using System.ComponentModel.DataAnnotations;

namespace EndProject.MVC.ViewModels;

public class LoginViewModel
{
    [DataType(DataType.Text)]
    [StringLength(50)]
    public string UserName { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
