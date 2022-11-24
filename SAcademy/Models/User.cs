using Microsoft.AspNetCore.Identity;

namespace SAcademy.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public byte[] Logo { get; set; }
    }
}
