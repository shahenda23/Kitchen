using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [EmailAddress (ErrorMessage = "Invalid Email Address Fromat\U0001fae0\r\n!")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\+20|0)(10|11|12|15)\d{8}$", ErrorMessage = "Please enter a valid phone number 😡\r\n ")]
        public string PhoneNumber { get; set; }
        
        [ForeignKey("CustomerID")]
        public int? CustomerID { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("ChefID")]
        public int? ChefID { get; set; }
        public Chef Chef { get; set; }

        [ForeignKey("ManagerID")]
        public int? ManagerID { get; set; }
        public Manager Manager { get; set; }


        public List<AccountRole>? AccountRoles { get; set; }


    }
}
