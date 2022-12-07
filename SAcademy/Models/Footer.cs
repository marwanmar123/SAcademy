using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Footer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? ContentInfos { get; set; }
        public byte[]? Logo { get; set; }
        public string? ContentNews { get; set; }
        public string? ContentCopyRight { get; set; }
        public string? Background { get; set; }
    }
}
