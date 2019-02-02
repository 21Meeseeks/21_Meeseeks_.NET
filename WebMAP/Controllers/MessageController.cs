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
            clientservice CS = new clientservice();
            AdminService AS = new AdminService();
            ResourceService RS = new ResourceService();
            if (Session["role"] == "Client")
            {
                string Email = Session["username"].ToString();
                ViewBag.user = CS.Get(c => c.email == Email);
                ViewBag.name = ViewBag.user.clientName;
            }
            else if (Session["role"] == "Admin")
            {
                string Email = Session["username"].ToString();
                ViewBag.user = AS.Get(a => a.email == Email);
                ViewBag.name = "Administrator";
            }
            else if (Session["role"] == "Resource")
            {
                string Email = Session["username"].ToString();
                ViewBag.user = RS.Get(r => r.email == Email);
                ViewBag.name = ViewBag.user.firstName + " " + ViewBag.user.lastName;
            }
            else
            {
                ViewBag.user = "No User Found";
            }
            ViewBag.clients = CS.GetMany();
            ViewBag.admins = AS.GetMany();
            ViewBag.resources = RS.GetMany();
            return View();
        }
    }
}