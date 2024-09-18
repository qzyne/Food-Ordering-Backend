using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
