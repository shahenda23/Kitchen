using Kitchen.Models;

namespace Kitchen.ViewModel1
{
    public class OrderDishesViewModel
    {
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string customerphone { get; set; }
        public DateOnly orderdate { get; set; }
        public int orderrate { get; set; }
        public string dishname {  get; set; }
        public int dishid { get; set; }
        public int orderprice { get; set; }
        //public TimeOnly ordertime { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
        public List<string> DishNames { get; set; }

    }
}
