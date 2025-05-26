using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "5")]
    public class FeedbackController : Controller
    {
        IFeedbackRepository feedbackrepo; 
        public FeedbackController(IFeedbackRepository _feedbackrepo)
        {
            feedbackrepo = _feedbackrepo;
        }
        [Authorize(Roles = "1, 2, 3, 4")]
        public IActionResult All()
        {
            List<Feedback> feedBackList = feedbackrepo.GetAll();
            return View("All", feedBackList);
        }
        public IActionResult Random()
        {
            var allFeedbacks = feedbackrepo.GetAll("Customer");
            var random = new Random();
            var randomFeedbacks = allFeedbacks.OrderBy(x => random.Next()).Take(5).ToList();
            return View(randomFeedbacks);
        }

        [HttpGet]
        public IActionResult CreateFeedback(int orderId)
        {
            var feedbackorderVM = new FeedbackViewModel
            {
                OrderId = orderId,
            };
            return View(feedbackorderVM);
        }
        
        [HttpPost]
        public IActionResult SubmitFeedback(FeedbackViewModel feedbackVM)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    Comment = feedbackVM.Comment,
                    Rate = feedbackVM.OrderRate,
                    OrderId = feedbackVM.OrderId,
                    Customer_ID = int.Parse(User.FindFirst("CustomerID")?.Value)
                };

                feedbackrepo.Add(feedback);
                feedbackrepo.Save();
                return RedirectToAction("FeedbackSuccess"); 
            }
            return View(feedbackVM); 
        }
        public IActionResult FeedbackSuccess()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
