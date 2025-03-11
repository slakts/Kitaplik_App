using Microsoft.AspNetCore.Identity;

namespace Kitaplik_Mvc.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int Ogrencino { get; set; }

        public string? Adres { get; set; }
        public string? Fakulte { get; set; }
        public string? Bolum { get; set; }
    }
}
