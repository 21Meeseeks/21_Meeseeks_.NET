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
    public class CertificatController : Controller
    {
        // GET: Certificat
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/certificate").Result;
            ViewBag.result = response.Content.ReadAsAsync<IEnumerable<CertificatVM>>().Result;
            return View();
        }

        // GET: Certificat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Certificat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Certificat/Create
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

        // GET: Certificat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Certificat/Edit/5
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

        // GET: Certificat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Certificat/Delete/5
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
