using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Dish
    {
        public int Dish_ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [ForeignKey("Chef")]
        public int Chef_ID { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public Chef Chef { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }


    }
}
