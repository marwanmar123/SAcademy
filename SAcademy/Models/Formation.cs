using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime StartDay { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime EndDay { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
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
        public string? ThematicId { get; set; }
        public Thematic? Thematic { get; set; }
        public ICollection<Registration>? Registration { get; set; }

        //Offres
        public string? OffreFColor { get; set; } = "#000000";
        public int? OffreFSize { get; set; } = 2;
        public string? OffreFBgColor { get; set; } = "#FBFBFB";
        public string? OffreFBgColorButton { get; set; } = "#CE0033";
    }
}
