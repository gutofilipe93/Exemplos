using System.ComponentModel.DataAnnotations;

namespace WithoutIdentity.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name="Número de telefone")]
        public string PhoneNumber { get; set; }
        public string StatusMessage { get; set; }

    }
}