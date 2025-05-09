using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? Date { get; set; }

        public decimal TotalPrice { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } 
    }
}
