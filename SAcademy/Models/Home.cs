using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Home
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? BgNav { get; set; }
        public byte[]? Logo { get; set; }
    }
}
