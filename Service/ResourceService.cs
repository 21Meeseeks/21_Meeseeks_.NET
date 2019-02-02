using Data.Infrastructures;
using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Service
{
   public class ResourceService:Service<resource>, IResourceService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ResourceService() : base(utk)
        {

        }
        public static HttpResponseMessage getAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/resource").Result;
            return response;
        }

        public static HttpResponseMessage getResumes()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/resume").Result;
            return response;
        }

        public static HttpResponseMessage getByID(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/resource/"+id).Result;
            return response;
        }

        public static HttpResponseMessage getSeniority()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("rest/seniority").Result;
            return response;
        }

    
        public static void Edit( string key, string resource,string method)
        {
            //Resource resource = null;
            string token = "Bearer " + key;// GetToken("jeddi@gmail.com", "2222");


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:18080/21meeseeks-web/rest/resource/");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/json";
            request.Method = method;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(resource);
                streamWriter.Flush();
                streamWriter.Close();
            }

            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", token);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
            }
        }
        public static string GetToken(string username, string password)
        {
            string data = string.Format("username={0}&password={1}", username, password); //replace <value>
            byte[] dataStream = Encoding.UTF8.GetBytes(data);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                "http://localhost:18080/21meeseeks-web/rest/authentication");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            using (var streamWriter = httpWebRequest.GetRequestStream())
            {
                streamWriter.Write(dataStream, 0, dataStream.Length);
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }



            return response;
        }
    }
}
