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
        public string? Description { get; set; }
        public string? Certification { get; set; }
        public string? Presentation { get; set; }
        public string? Competences { get; set; }
        public string? Programme { get; set; }
        public string? Prerequis { get; set; }
        public string? Price { get; set; }
        public string? TypeId { get; set; }
        public FType? Type { get; set; }
        public ICollection<Formation>? Formations { get; set; }
    }
}
