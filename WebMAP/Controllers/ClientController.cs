using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Domain.Entity;
using Service;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class ClientController : Controller
    {   
        // GET: Client/all
        public ActionResult Index()
        {
            /*HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/client").Result;
            //localhost:18080/21meeseeks-web/rest/client
            ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Client>>().Result;
            */
            ClientService cs = new ClientService();

            ViewBag.result2 = cs.GetMany();
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            ClientService us = new  ClientService();
            client c = us.GetById(id);
            Client cl = new Client();
            cl.idUser = id;
            cl.logo = c.logo;
            cl.phoneNumber = c.phoneNumber;
            cl.address = c.address;
            cl.email = c.email;
            cl.clientName = c.clientName;
            ViewBag.client = cl;

            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("client/category").Result;

            ViewBag.result =  response.Content.ReadAsAsync<IEnumerable<ClientCategory>>().Result;
                
            return View("Create");
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client cc, HttpPostedFileBase file )
        {
            //file upload

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/ProfilePictures/"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    cc.logo = file.FileName;

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    return View();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
                return View();
            }
            

            //client category affectation
            ClientCategory category = new ClientCategory
            {
                name = Request.Form["category"]

            };
            cc.clientCategory = category;

            try
            {
                HttpClient client = new HttpClient();
                //  var token=   Session["token"].ToString();

                // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                cc.clientType = "NEW_CLIENT";
                client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/rest/");

                client.PostAsJsonAsync<Client>("client", cc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
                System.Threading.Thread.Sleep(3000);


                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        // GET: Client/Create
        public ActionResult Archive(int id)
        {//localhost:18080/21meeseeks-web/rest/client/
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response2 = client.GetAsync("rest/client/"+id).Result;
            System.Threading.Thread.Sleep(3000);    
            return RedirectToAction("Index", "Client", new { area = "" });

        }


        // GET: Client/ajax
        public string Search(string query)
        {
            IEnumerable<client> StuList;
            ClientService cs = new ClientService();
            StuList = cs.GetMany(a => a.clientName.StartsWith(query));
            var result = "<div class=\"m-list-search__results\">";
            if (StuList.Count() == 0)
            {
                result = result +
      "	<span class=\"m-list-search__result-message\">" +
      "  No record found" +
      " </ span >";

            }
            if(StuList.Count()!=0)
            {
                result = result + "  <span class=\"m-list-search__result-category m-list-search__result-category--first\">	Client </span>";


                foreach (var item in StuList)
                {
                    result = result + "<a href=\"   "+ Url.Content("~/") + "Client/Details/"+item.idUser+" \" class=\"m-list-search__result-item\">	<span class=\"m-list-search__result-item-pic\">";


                        if (item.logo==null)
                    {
                        result = result + "<img class=\"m--img-rounded\" src=\" "+ Url.Content("~/") + "Content/assets/app/media/img/users/user4.jpg\" />";
                    }
                        else
                    {
                        result = result + "<img class=\"m--img-rounded\" src=\" " + Url.Content("~/") + "Content/ProfilePictures/" + item.logo +"\")\" />";

                    }
                    result =result+       "</span>		<span class=\"m-list-search__result-item-text\">" + item.clientName + "</span></a>";
                }
     
    }


            result = result + "</div>";
                return (result);
            
        }






        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
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

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
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
