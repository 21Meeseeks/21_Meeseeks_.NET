using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class DayoffController : Controller
    {
        // GET: Dayoff
        public ActionResult Index()
        {
            var content = DayoffService.getAll().Content;
            var result = content.ReadAsAsync<IEnumerable<DayoffVM>>().Result;
            return View(result);
        }

        public ActionResult DayOffCalendar(int id)
        {
            ResourceVM model = ResourceService.getByID(id).Content.ReadAsAsync<ResourceVM>().Result;
            List<leavetypeVM> listLeaveType = DayoffService.GetLeaveType().Content.ReadAsAsync<List<leavetypeVM>>().Result;
            //List<leavetypeVM> listLeaveType = new List<leavetypeVM>() {
            //    new leavetypeVM() { idLeaveType=1, name = "congé maternité",description=""},
            //     new leavetypeVM() { idLeaveType=2, name = "congé paternité",description=""}
            //};
            ViewBag.LeaveTypes = listLeaveType;
            return View(model);
        }

        public ActionResult GetDayOff(int id, DateTime start, DateTime end)
        {
            List<EventVM> dayOffs = DayoffService.getAll(id)
                                                 .Content
                                                 .ReadAsAsync<List<DayoffVM>>()
                                                 .Result
                                                 .Select(d => new EventVM(d)).ToList();

            return Json(dayOffs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDayOff(int idUser, int idLV, int idDayOff, DateTime start, DateTime end)
        {
           // string token = ResourceService.GetToken("Emma.Stone@mail.sw", "aaa");
            DayoffService.Edit("", Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    leaveType = new { idLeaveType = idLV },
                    startDate = start,
                    endDate = end,
                    resource = new { idUser = idUser }
                }

                ), idDayOff == 0 ? "POST" : "PUT");


            return Json("succes", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDayOff(int idDayOff)
        {
            string token = ResourceService.GetToken("Emma.Stone@mail.sw", "aaa");
            DayoffService.Delete(token, idDayOff);
            return Json("succes", JsonRequestBehavior.AllowGet);
        }

        // GET: Dayoff/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dayoff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dayoff/Create
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

        // GET: Dayoff/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dayoff/Edit/5
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

        // GET: Dayoff/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dayoff/Delete/5
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
