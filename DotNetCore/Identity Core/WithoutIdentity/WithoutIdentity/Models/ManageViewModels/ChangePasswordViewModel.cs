using System.ComponentModel.DataAnnotations;

namespace WithoutIdentity.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [StringLength(100, ErrorMessage = "O campo {0} deve ter no minimo {2} e no máximo {1} caracteres")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage = "As senha devem ser iguais")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}