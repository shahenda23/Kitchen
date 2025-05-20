using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kitchen.Controllers
{
    public class FeedbackController : Controller
    {
        IFeedbackRepository feedbackrepo; 
        public FeedbackController(IFeedbackRepository _feedbackrepo)
        {
            feedbackrepo = _feedbackrepo;
        }
        [HttpGet]
        public IActionResult CreateFeedback(int orderId)
        {
            var feedbackorderVM = new FeedbackViewModel
            {
                orderId = orderId 
            };
            return View(feedbackorderVM);
        }
        public IActionResult FeedbackSuccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitFeedback(FeedbackViewModel feedbackVM)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    Comment = feedbackVM.Comment,
                    Rate = feedbackVM.orderrate,
                    OrderId = feedbackVM.orderId,
                    Customer_ID = feedbackVM.customerId
                };

                feedbackrepo.Add(feedback);
                return RedirectToAction("FeedbackSuccess"); 
            }

            return View(feedbackVM); 
        }
    }
}
