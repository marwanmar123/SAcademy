using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class StaticNum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public int? Number { get; set; }
        public string? Description { get; set; }
    }
}
