using System.ComponentModel.DataAnnotations;

namespace OrderingFoodFinalTerm
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Phonenumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int RoleId { get; set; } = 2;

    }
}
