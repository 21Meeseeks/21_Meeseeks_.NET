using Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class EstablishementController : Controller
    {
        // GET: Establishement
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/establishment").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<EstablishementVM>>().Result;
            return View(result);
        }

        // GET: Establishement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Establishement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Establishement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            
            try
            {
              

                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Establishement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Establishement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Establishement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Establishement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
