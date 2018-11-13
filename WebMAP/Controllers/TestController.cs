using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebMAP.Models;

namespace WebMAP.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
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
            return View();
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