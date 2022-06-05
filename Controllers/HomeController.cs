using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRIS_Sample.Models;

namespace HRIS_Sample.Controllers
{
    public class HomeController : Controller
    {
        private HRISEntities db = new HRISEntities();

        // GET: index
        public ActionResult Index()
        {
            var locations = db.locations.Include(l => l.country);
            return View(locations.ToList());
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
    }
}