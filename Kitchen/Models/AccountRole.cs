using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class AccountRole
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Account")]
        public int AccountID { get; set; }
        public Account Account { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}
