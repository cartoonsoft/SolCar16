using System.Web.Mvc;

namespace AdmExemploCompleto.Controllers
{
    public class MailboxController : Controller
    {
        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Compose()
        {
            return View();
        }

        public ActionResult Read()
        {
            return View();
        }
    }
}