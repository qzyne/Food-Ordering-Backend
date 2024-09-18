using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderingFoodFinalTerm
{
    public class User
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string? Firstname { get; set; }
        [MaxLength(50)]
        public string? Lastname { get; set; }
        [MaxLength(12)]
        public string? Phonenumber { get; set; }
        [MaxLength(50)]
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }

        public Cart Cart { get; set; }  
        [JsonIgnore]

        public List<Order> Orders { get; set; }

    }
}
