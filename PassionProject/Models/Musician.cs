using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Musician
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Guitar> Guitars { get; set; }
    }

    public class MusicianDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
    }
}