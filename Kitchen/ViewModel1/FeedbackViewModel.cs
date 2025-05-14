using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kitchen.ViewModel1
{
    public class FeedbackViewModel
    {
        [Display(Name = "Tell us a little more")]
        [MaxLength(100)]
        public string Comment { get; set; }
        public int orderrate { get; set; }
        public int orderId { get; set; }
        public int customerId { get; set; }
    }
}
