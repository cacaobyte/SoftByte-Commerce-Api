using System;

namespace CommerceCore.Security
{
    public class UserInfo
    {
        public string? Username { get; set; }
        public string? Seller { get; set; }
        public string? Store { get; set; }
        public string? Email { get; set; }
        public string? CompleteName { get; set; }
        public int? ApplicationId { get; set; }  // Cambio de string? a int?
        public bool RequiresPasswordChange { get; set; } // Indica si el usuario debe cambiar la contraseña
        public string? LastPasswordChangeDate { get; set; } // Última fecha de cambio de clave
        public string? UserType { get; set; } // Tipo de usuario (Administrador, Usuario, etc.)
        public bool IsActive { get; set; } // Si el usuario está activo
        public string? Plan {  get; set; }
    }
}
