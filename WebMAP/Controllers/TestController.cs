using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMAP.Models;

namespace WebMAP.Controllers
{

    //Fucking CSharp And Screw you Microsoft , I hate you so much 
    //I have to resolve this fucking reference dumb ass shit 
    //And start to do the regular things such Displaying the statistics
    //There's the idea of making a dynamic tracking for some stuffs 
    //Am gonna look for it later , but first i really have to boost up 
    public class TestController : Controller
    {
        static string nbResource = " ";
        static ITestService test;
        IEnumerable r ;
        public TestController()
        {

            test = new TestService();

            r = test.GetMany().AsEnumerable();
        }
        // GET: Test
        public ActionResult Show()
        {
           
            ViewBag.NumberOfResource = test.countResource();
            ThreadStart childref = new ThreadStart(isChanged);
            Thread thread = new Thread(childref);
            thread.Start();
            return View();
        }
        [NonAction]
        public void isChanged()
        {
            string isChanged;
            while (true) {

                Console.WriteLine("Child thread starts");
                if (test.isChanged(ref r))
                {
                    isChanged = "Something changed^^'";
                    Console.WriteLine("Changes Done");
                }
                // the thread is paused for 5000 milliseconds
                int sleepfor = 5000;

                Console.WriteLine("Child Thread Paused for {0} seconds", sleepfor / 1000);
                Thread.Sleep(sleepfor);
                Console.WriteLine("Child thread resumes");
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/project");
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            ViewBag.result = JSserializer.Deserialize<IEnumerable<Project>>(data);
            response = await Client.GetAsync("rest/resources/resource/mostTerm");
            data = await response.Content.ReadAsStringAsync();
            JSserializer = new JavaScriptSerializer();
            ViewBag.Resources = ListToDictionary(JSserializer.Deserialize<List<List<Object>>>(data));
            /*IEnumerable resources = new List<Object>();
            foreach (IEnumerable x in (IEnumerable)obj)
            {

            }*/
            //ViewBag.Resources = obj;
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Thread thread = new Thread(childref);
            thread.Start();
            return View();
        }
        public static async void CallToChildThread()
        {
            while (true) { 
                Console.WriteLine("Child thread starts");
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await Client.GetAsync("rest/resources");
                string data = await response.Content.ReadAsStringAsync();
                if (!data.Equals(nbResource)) { 
                    nbResource = data;
                    Console.WriteLine("Changes Done");
                }
                // the thread is paused for 5000 milliseconds
                int sleepfor = 5000;

                Console.WriteLine("Child Thread Paused for {0} seconds", sleepfor / 1000);
                Thread.Sleep(sleepfor);
                Console.WriteLine("Child thread resumes");
            }
        }

        [Route("Adress/{country}")]
        public async System.Threading.Tasks.Task<ActionResult> mostTerms(string country) {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await Client.GetAsync("rest/project");
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            ViewBag.result = JSserializer.Deserialize<IEnumerable<Project>>(data);
            response = await Client.GetAsync("rest/resources/resource/adress/"+country);
            data = await response.Content.ReadAsStringAsync();
            JSserializer = new JavaScriptSerializer();
            ViewBag.Resources = ListToDictionary(JSserializer.Deserialize<List<List<Object>>>(data));
            return View("Index");
        }
        private List<KeyValuePair<string , Resource>> ListToDictionary(List<List<Object>> list)
        {
            KeyValuePair<string , Resource> dictionary = new KeyValuePair<string, Resource>();
            List<KeyValuePair<string, Resource>> listFinal = new List<KeyValuePair<string, Resource>>();
            foreach (var x in list) {
                string rousource = x.ElementAt<Object>(1).ToString();
                Resource r = ObjectExtensions.ToObject<Resource>((IDictionary<string,Object>)x.ElementAt<Object>(1));
                int c = r.idUser;/*
                dictionary.Key = Convert.ToString(x.ElementAt<Object>(0);
                dictionary.Value =r;*/
                listFinal.Add(new KeyValuePair<string, Resource>(Convert.ToString(x.ElementAt<Object>(0)), r));
            }

            return listFinal;
        }
    }
    public class Temp
    {
        public int Key { get; set; }
        public Resource Value { get; set; }
    }
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                try { 
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
                }
                catch
                {

                }
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
    }
}