using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public string? ImagePath { get; set; }
        public string? Description { get; set; }
        public int IsActive { get; set; } 
        public DateTime CreatedDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        //public List<Order> Orders { get; set; }
        [JsonIgnore]
        public List<Menu> Menus { get; set; }
        
    }
}
