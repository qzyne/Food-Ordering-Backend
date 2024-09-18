using System.ComponentModel.DataAnnotations;

namespace OrderingFoodFinalTerm.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int IsActive { get; set; }
        public int CategoryId { get; set; }
        public IFormFile file {  get; set; }     
    }
}
