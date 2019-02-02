using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class SeniorityController : Controller
    {
        // GET: Seniority
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/seniority").Result;
            var result = response.Content.ReadAsAsync<IEnumerable<SeniorityVM>>().Result;

            return View(result);
        }

        // GET: Seniority/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Seniority/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seniority/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Seniority/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Seniority/Edit/5
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

        // GET: Seniority/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Seniority/Delete/5
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
