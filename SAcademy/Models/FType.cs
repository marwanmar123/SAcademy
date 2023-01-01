using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class FType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; } = "white";
        public string? BgColor { get; set; } = "black";
        public string? Content { get; set; }
        public string? DetailType { get; set; }
        public string? BgCard { get; set; }
        public string? SizeCard { get; set; }
        public ICollection<Formation>? Formations { get; set; }
        public ICollection<Thematic>? Thematics { get; set; }

    }
}
