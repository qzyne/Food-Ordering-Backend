using System.ComponentModel.DataAnnotations;

namespace OrderingFoodFinalTerm
{
    public class Menu
    {
        [Key]
        public int Id { get; set; } 
        public string? MenuName { get; set; }
        public string? MenuDescription { get; set; }
        public int IsActive { get; set; } // value 1:active - 0:inactive
        
        public DateTime CreatedDate { get; set; }
        public List<Product> Products { get; set; }

    }
}
