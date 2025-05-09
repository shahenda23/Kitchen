using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Chef
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Name must be less than 25 character")]
        [MinLength(3, ErrorMessage = "Name must be more than 2 character")]
        public string Name { get; set; }
        public string? Specialization { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format")]
        public string? Email { get; set; }
        public int? Phone { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Manager? Manager { get; set; }
        public Account Account { get; set; }
    }
}
