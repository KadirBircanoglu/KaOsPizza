using KaOsPizzaEL.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace KaOsPizzaPL.Models
{
    public class ResetPasswordVM
    {
        public AppUser? User { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string NewPasswordConfirm { get; set; }
        public string UserId { get; set; }
    }
}
