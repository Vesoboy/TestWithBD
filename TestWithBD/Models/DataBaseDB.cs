using System;
using System.ComponentModel.DataAnnotations;

namespace TestWithBD.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
