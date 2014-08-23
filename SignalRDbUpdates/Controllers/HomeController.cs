using SignalRDbUpdates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRDbUpdates.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetAllMessages());
        }
    }
}