using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Guitar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        [Range(4, 12)]
        public int NumberOfStrings { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int MusicianId { get; set; }

        [ForeignKey("MusicianId")]
        public Musician Musician { get; set; }

        [Required]
        public string Color { get; set; }
    }

    public class GuitarDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int NumberOfStrings { get; set; }
        public int CategoryId { get; set; }
        public int MusicianId { get; set; }
        public string Color { get; set; }
    }
}