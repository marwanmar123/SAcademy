using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Newsletter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        public string? Region { get; set; }
        public string? Ville { get; set; }
    }
}
