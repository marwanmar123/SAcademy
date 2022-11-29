using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Localisation { get; set; }
        public string? Email { get; set; }
        public int? Call { get; set; }
    }
}
