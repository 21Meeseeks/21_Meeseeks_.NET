using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class MessageController : Controller
    {
        public ActionResult Chat()
        {
            ClientService CS = new ClientService();
            AdminService AS = new AdminService();
            if (Session["role"] == "Client")
            {
                string Email = Session["username"].ToString();
                ViewBag.user = CS.Get(c => c.email == Email);
            }
            else if (Session["role"] == "Admin")
            {
                string Email = Session["username"].ToString();
                ViewBag.user = AS.Get(a => a.email == Email);
            }
            else
            {
                ViewBag.user = "No User Found";
            }
            ViewBag.clients = CS.GetMany();
            ViewBag.admins = CS.GetMany();
            return View();
        }
    }
}