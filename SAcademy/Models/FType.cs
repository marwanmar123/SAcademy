using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class FType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? BgColor { get; set; }
        public ICollection<Formation>? Formations { get; set; }

    }
}
