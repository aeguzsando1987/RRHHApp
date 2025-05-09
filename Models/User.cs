using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema; // For ForeignKey attribute
// No longer needs System.ComponentModel.DataAnnotations for Key, Required, StringLength on User's direct properties

namespace RRHH.WebApi.Models
{
    // Inherit from IdentityUser<int> because your current ID was an int.
    // IdentityUser by default uses string for its Id primary key.
    // IdentityUser<TKey> allows specifying the key type.
    public class User : IdentityUser<int>
    {
        // The following properties are inherited from IdentityUser<int>:
        // - Id (this is your primary key, int)
        // - UserName (string, replaces your old Username)
        // - NormalizedUserName (string)
        // - Email (string)
        // - NormalizedEmail (string)
        // - EmailConfirmed (bool)
        // - PasswordHash (string, replaces your old Password. Identity handles hashing)
        // - SecurityStamp (string)
        // - ConcurrencyStamp (string)
        // - PhoneNumber (string)
        // - PhoneNumberConfirmed (bool)
        // - TwoFactorEnabled (bool)
        // - LockoutEnd (DateTimeOffset?)
        // - LockoutEnabled (bool)
        // - AccessFailedCount (int)

        // Your custom properties:
        [ForeignKey(nameof(Empleado))]
        public int Id_Empleado { get; set; }

        public bool Active { get; set; }

        // Navigation property
        public virtual Empleado Empleado { get; set; } = null!;
    }
}