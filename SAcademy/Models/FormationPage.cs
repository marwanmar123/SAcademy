using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class FormationPage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Content { get; set; }
        public string? ContentBgColor { get; set; }
        public int? ContentHeight { get; set; }
        public string? DescriptionFilter { get; set; }
    }
}
