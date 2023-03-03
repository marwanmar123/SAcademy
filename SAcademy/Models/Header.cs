using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Header
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public byte[]? Background { get; set; }
        public byte[]? BackgroundTwo { get; set; }
        public string? Content { get; set; }
        public string? ContentTwo { get; set; }
        public string? ContentThree { get; set; }
        public string? BgContent { get; set; }
        public string? BgContentTwo { get; set; }
        public string? Button { get; set; }
        public string? ButtonLink { get; set; }
        public string? ButtonTwo { get; set; }
        public string? ButtonTwoLink { get; set; }
        public string? ButtonThree { get; set; }
        public string? ButtonThreeLink { get; set; }
        public string? Video { get; set; }
        public int? HeightSection { get; set; }
        public int? BVTopSize { get; set; }
        public int? BVLeftSize { get; set; }
        public string? BVColor { get; set; }
        public int? BVSize { get; set; }
        public string? ButtonBgColor { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
