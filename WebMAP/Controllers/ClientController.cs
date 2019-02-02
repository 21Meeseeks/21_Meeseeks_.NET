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
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/client").Result;
            //localhost:18080/21meeseeks-web/rest/client
            ViewBag.result2 = response.Content.ReadAsAsync<IEnumerable<Client>>().Result;
            
          /*  clientservice cs = new clientservice();
            List<Client> e = new List<Client>();
            IEnumerable<client> c = cs.GetMany();
            foreach (var item in c)
            {
                e.Add(new Client()
                {
                    idUser = item.idUser,
                    address = item.address,
                    clientName=item.clientName,
                     logo=item.logo,
                     phoneNumber=item.phoneNumber,
                     clientType=item.clientType,
                     email=item.email
                });
            }
            ViewBag.result2 = e;*/
            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            clientservice us = new  clientservice();
            ProjectService ps = new ProjectService();
            client c = us.GetById(id);
            Client cl = new Client();
            cl.idUser = id;
            cl.logo = c.logo;
            cl.phoneNumber = c.phoneNumber;
            cl.address = c.address;
            cl.email = c.email;
            cl.clientName = c.clientName;
            ViewBag.client = cl;
            var total = ps.GetMany(a => a.client_idUser == cl.idUser).Count();
            ViewBag.totalproject = total;
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
        public async System.Threading.Tasks.Task<ActionResult> Create(Client cc, HttpPostedFileBase file)
        {
            //file upload

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/ProfilePictures/"),
                                               Path.GetFileName(file.FileName));
                    //file.SaveAs(path);
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

                await client.PostAsJsonAsync<Client>("client", cc).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());


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
            clientservice cs = new clientservice();
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
