using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class ResourceController : Controller
    {
        IResourceService rs;
        public ResourceController()
        {
            rs = new ResourceService();
        }
        // GET: Resource
        public ActionResult Index(string searchString)
        {
            var list = rs.GetMany();
            
            var content = ResourceService.getAll().Content;
            //string jsonrsult = content.ReadAsStringAsync().Result;
            var result = content.ReadAsAsync<IEnumerable<ResourceVM>>().Result;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(m => m.firstName==searchString).ToList();
                //list = list.Where(c => c.lastName == searchString).ToList();
                return View(result);
            }
            return View(result);
        }




        // GET: Resource/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Resource/Edit
        public ActionResult Edit(int id = 0)
        {
            var resume = ResourceService.getResumes().Content.ReadAsAsync<IEnumerable<ResumeVM>>().Result;
            var seniority = ResourceService.getSeniority().Content.ReadAsAsync<IEnumerable<SeniorityVM>>().Result;
            SelectList slResume = new SelectList(resume, "idResume", "description");
            SelectList slSeniority = new SelectList(seniority, "idSeniority", "name");
            ViewBag.resume = slResume;
            ViewBag.seniority = slSeniority;
            ResourceVM model = new ResourceVM();
            model.resume = new ResumeVM();
            model.seniority = new SeniorityVM();
            if (id > 0)
            {
                model = ResourceService.getByID(id).Content.ReadAsAsync<ResourceVM>().Result;
            }
            return View(model);
        }

        // POST: Resource/Edit
        [HttpPost]
        public ActionResult Edit(ResourceVM resourceVM, HttpPostedFileBase File)
        {

            try
            {

                resourceVM.photo = File.FileName;
                if (File.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/"), File.FileName);
                    File.SaveAs(path);
                }
                //string token = ResourceService.GetToken("Emma.Stone@mail.sw", "aaa");//  Response.Cookies["token"].Value;
                ResourceService.Edit("", Newtonsoft.Json.JsonConvert.SerializeObject(resourceVM), resourceVM.idUser == 0 ? "POST" : "PUT");
                var x = 0;
                MailMessage message = new MailMessage("mohamed.jeddi@esprit.tn", resourceVM.email, "your LOGIN AND PASSWORD", "Hello Mr "+resourceVM.lastName+" your login :"+resourceVM.email+"your password"+resourceVM.password );
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new  System.Net.NetworkCredential("mohamed.jeddi@esprit.tn", "mohamedjeddi07250725");
                client.Send(message);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: Resource/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Resource/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Resource/Delete/5
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/resource/" + id).Result;
            ViewBag.result = response.Content.ReadAsAsync<ResourceVM>().Result;
            return View(ViewBag.result);
        }

        // POST: Resource/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("rest/resource/"+id).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
