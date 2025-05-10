using System.ComponentModel.DataAnnotations;

namespace Kitchen.Models
{
    public class Manager
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Name must be less than 25 character")]
        [MinLength(3, ErrorMessage = "Name must be more than 2 character")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format")]
        public string? Email { get; set; }

        [RegularExpression(@"^(010|011|012|013|015)\d{8}$", ErrorMessage = "Invalid Egyptian phone number format")]
        public int? Phone { get; set; }
        public Account Account { get; set; }
        public List<Staff>? Chefs { get; set; }
    }
}
