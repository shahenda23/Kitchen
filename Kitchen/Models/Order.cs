namespace Kitchen.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? Date { get; set; }
        public decimal TotalPrice { get; set; }

        public List<Dish>? Dishes { get; set; } 
    }
}
