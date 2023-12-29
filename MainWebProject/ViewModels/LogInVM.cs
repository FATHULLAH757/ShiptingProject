using System.ComponentModel.DataAnnotations;

namespace MainWebProject.ViewModels
{
    public class LogInVM
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        
        public string? ReturnUrl { get; set; }
    }
}
