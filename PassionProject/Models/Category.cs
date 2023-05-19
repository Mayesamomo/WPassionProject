using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Guitar> Guitars { get; set; }
    }

    // Category DTO
public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}