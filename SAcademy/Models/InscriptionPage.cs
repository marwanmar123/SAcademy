using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class InscriptionPage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? ContentOne { get; set; }
        public string? ContentBgColor { get; set; }
        public int? ContentHeight { get; set; }
        public string? ContentTwo { get; set; }
    }
}
