using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Thematic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? ColorTitle { get; set; }
        public string? Background { get; set; }
        public string? TypeId { get; set; }
        public FType? Type { get; set; }
    }
}
