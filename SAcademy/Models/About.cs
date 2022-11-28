﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SAcademy.Models
{
    public class About
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Video { get; set; }
        public byte[]? image { get; set; }
    }
}
