using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]

        public List<CartItem> CartItems { get; set; }
    }
}
