using Domain.Entity;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections;
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
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            //List<establishment> myDeserializedObjList = (List<establishment>)Newtonsoft.Json.JsonConvert.DeserializeObject("", typeof(List<establishment>));
            var content = ResumeService.getAll().Content;
            ViewBag.result = content.ReadAsAsync<IEnumerable<ResumeVM>>().Result;
            return View();
        }

        // GET: Resume/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Resume/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resume/Create
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

        // GET: Resume/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var estab = ResumeService.getEastablishement().Content.ReadAsAsync<IEnumerable<EstablishementVM>>().Result;
            MultiSelectList slEstab = new MultiSelectList(estab, "idEstablishment", "nameEstablishment");
            ViewBag.estab = slEstab;
            ResumeVM model = new ResumeVM();
            model.etablissement = new List<EstablishementVM>();
            if (id > 0)
            {
                model = ResumeService.getByID(id).Content.ReadAsAsync<ResumeVM>().Result;
                
            }
            return View(model);
        }

        // POST: Resume/Edit/5
        [HttpPost]
        public ActionResult Edit(ResumeVM resumeVM)
        {
            try
            {

                
                ResumeService.Edit(Newtonsoft.Json.JsonConvert.SerializeObject(resumeVM), resumeVM.idResume == 0 ? "POST" : "PUT");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Resume/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Resume/Delete/5
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
