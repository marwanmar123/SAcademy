using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class ThemeInscrit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public string? ThematicName { get; set; }
        public ICollection<Thematic>? Thematic { get; set; }
    }
}
