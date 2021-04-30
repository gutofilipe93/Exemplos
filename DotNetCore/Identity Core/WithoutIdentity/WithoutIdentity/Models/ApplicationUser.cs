using System;
using Microsoft.AspNetCore.Identity;

namespace WithoutIdentity.Models
{
    // Classe IdentityUser contei tudo que é necessario para autenticar o usuário
    public class ApplicationUser : IdentityUser<Guid>
    {
        
    }
}