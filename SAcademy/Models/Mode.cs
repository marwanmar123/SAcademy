using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Mode
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Formation>? Formations { get; set; }

    }
}
