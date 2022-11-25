using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Header
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public byte[]? Background { get; set; }
        public string? Content { get; set; }
        public string? Button { get; set; }
        public string? Video { get; set; }
        public int? TopSize { get; set; }
        public int? LeftSize { get; set; }
    }
}
