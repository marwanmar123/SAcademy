using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Formation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Certificate { get; set; }
        public string? Presentation { get; set; }
        public string? Skills { get; set; }
        public string? ModeId { get; set; }
        public Mode? Mode { get; set; }
        public string? VilleId { get; set; }
        public Ville? Ville { get; set; }
        public string? TypeId { get; set; }
        public FType? Type { get; set; }
        public ICollection<Registration>? Registration { get; set; }

        //Offres
        public string? OffreFColor { get; set; }
        public int? OffreFSize { get; set; }
        public string? OffreFBgColor { get; set; }
        public string? OffreFBgColorButton { get; set; }
    }
}
