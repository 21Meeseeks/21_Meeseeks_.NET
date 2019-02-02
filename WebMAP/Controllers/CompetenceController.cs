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
    public class CompetenceController : Controller
    {
        // GET: Competence
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/competence").Result;
            ViewBag.result = response.Content.ReadAsAsync<IEnumerable<CompetenceVM>>().Result;

            return View();
        }

        // GET: Competence/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Competence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competence/Create
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

        // GET: Competence/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Competence/Edit/5
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

        // GET: Competence/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Competence/Delete/5
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
