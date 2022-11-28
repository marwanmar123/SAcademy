﻿using System.ComponentModel.DataAnnotations;
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
        public int? HeightSection { get; set; }
        public int? BVTopSize { get; set; }
        public int? BVLeftSize { get; set; }
        public string? BVColor { get; set; }
        public int? BVSize { get; set; }
    }
}
