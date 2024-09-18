using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity {  get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; } // 0: pending, 1: confirmed, 2: is delivering, 3: delivered, 4: cancled
        [JsonIgnore]
        public User User { get; set; }
   
    }
}
