using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class About
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? TitleColor { get; set; }
        public int? TitleSize { get; set; }
        public string? FontFamily { get; set; }
        public string? Content { get; set; }
        public string? Video { get; set; }
        public byte[]? image { get; set; }
        public int? VideoWidth { get; set; }
        public int? VideoHeight { get; set; }
        public Boolean? Visible { get; set; } = true;
    }
}
