using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? TitleColor { get; set; } 
        public int? TitleSize { get; set; }
        public string? FontFamily { get; set; }
        public string? Content { get; set; }
        public string? Localisation { get; set; }
        public string? LocalColor { get; set; }
        public string? Email { get; set; }
        public string? EmailColor { get; set; }
        public string? Call { get; set; }
        public string? CallColor { get; set; }
        public string? Maps { get; set; }
        public int? MapWidth { get; set; }
        public int? MapHeight { get; set; }
        public string? ButtonBgColor { get; set; }
        public Boolean? Visible { get; set; } = true;
    }
}
