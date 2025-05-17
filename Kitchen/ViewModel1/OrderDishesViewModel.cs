using Kitchen.Models;

namespace Kitchen.ViewModel1
{
    public class OrderDishesViewModel
    {
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string customerphone { get; set; }
        //public DateTime orderdate { get; set; } = DateTime.Now;
        public float orderprice { get; set; }
        public string? orderStatus { get; set; }
        public string OrderDetailsJson { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
