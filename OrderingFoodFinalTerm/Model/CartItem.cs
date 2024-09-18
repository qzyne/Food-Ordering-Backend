using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public Product Product { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
