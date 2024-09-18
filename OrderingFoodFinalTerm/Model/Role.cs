using System.ComponentModel.DataAnnotations;

namespace OrderingFoodFinalTerm
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? Rolename { get; set; }

        public List<User> Users { get; set; }
    }
}
