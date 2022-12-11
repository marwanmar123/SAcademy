using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Offre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? TitleColor { get; set; }
        public int? TitleSize { get; set; }
        public string? Content { get; set; }
        public Boolean? Visible { get; set; } = true;
    }
}
