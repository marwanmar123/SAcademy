using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? TitleMenu { get; set; }
        public string? Link { get; set; }
        public string? Color { get; set; } = "white";
        public string? ColorFooter { get; set; } = "white";
        public string? FontFamily { get; set; }
        public int? Size { get; set; } = 18;
        public int? SizeFooter { get; set; } = 18;
    }
}
