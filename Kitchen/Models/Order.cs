using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public float TotalPrice { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<OrderDetails>? OrderDetails { get; set; }
        public List<Feedback>? Feedbacks { get; set; }
    }
}
