using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Registration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? JobRole { get; set; }
        public string? CompanyName { get; set; }
        public string? Region { get; set; }
        public string? Ville { get; set; }
        public string? FormationName { get; set; }
        public string? Statut { get; set; } = "Prospect";
        public string? Comment { get; set; }
        public string? FormationId { get; set; }
        public Formation? Formation { get; set; }
        public Boolean? UserConfirmed { get; set; } = false;
    }
}
