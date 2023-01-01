﻿using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class Slide
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? TitleColor { get; set; }
        public int? TitleSize { get; set; }
        public string? Content { get; set; }
        public ICollection<Image> Images { get; set; }
        public Boolean? Visible { get; set; } = true;
    }
}
