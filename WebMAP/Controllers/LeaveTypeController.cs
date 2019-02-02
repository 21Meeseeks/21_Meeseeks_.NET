using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class LeaveTypeController : Controller
    {
        // GET: LeaveType
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LeaveType LT)
        {
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<LeaveType>("Leavetype", LT).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Settings", "Home", new { area = "" });
        }

        public ActionResult Delete(int ID)
        {
            LeaveType LT = new LeaveType
            {
                idLeaveType = ID
            };
            HttpClient HTTPC = new HttpClient();
            HTTPC.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            HTTPC.PostAsJsonAsync<LeaveType>("Leavetype/remove", LT).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Settings", "Home", new { area = "" });
        }
    }
}