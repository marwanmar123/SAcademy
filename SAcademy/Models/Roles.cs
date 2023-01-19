using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SAcademy.Models
{
    public class Roles
    {
        [Display(Name = "Role")]
        public string? RoleId { get; set; }
        [Display(Name = "User")]
        public string? UserId { get; set; }
    }
}
