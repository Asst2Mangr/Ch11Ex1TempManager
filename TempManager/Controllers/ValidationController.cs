using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TempManager.Models;

namespace TempManager.Controllers
{
    public class ValidationController : Controller
    {
        private TempManagerContext context;
        public ValidationController(TempManagerContext cxt) => context = cxt;

        public JsonResult CheckDate(string date)
        {
            DateTime convertedDate = DateTime.Parse(date);
            string errorMsg = "Date is already in the database";

            var temp = context.Temps.FirstOrDefault(x => x.Date == convertedDate);

            if (temp != null)
            {
                TempData["okDate"] = true;
                return Json(errorMsg);
            }
            else
            {
                return Json(true);
            }
        }
        public static string DateExists(TempManagerContext context, string date)
        {
            string msg = "";
            DateTime convertedDate = DateTime.Parse(date);
            if (!string.IsNullOrEmpty(date))
            {
                var temp = context.Temps.FirstOrDefault(x => x.Date == convertedDate);
                if(temp != null)
                {
                    msg = "Duplicate Date";
                }
            }
            return msg;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
