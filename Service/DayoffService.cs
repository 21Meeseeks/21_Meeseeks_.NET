using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DayoffService
    {
        public static HttpResponseMessage getAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/dayOff").Result;
            return response;
        }

        public static HttpResponseMessage getAll(int iduser)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/dayOff/user/" + iduser.ToString()).Result;
            return response;
        }

        public static HttpResponseMessage GetLeaveType()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:18080/21meeseeks-web/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("rest/Leavetype".ToString()).Result;
            return response;
        }

        public static void Edit(string key, string dayOff, string method)
        {
            //Resource resource = null;
            string token = "Bearer " + key;// GetToken("jeddi@gmail.com", "2222");


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:18080/21meeseeks-web/rest/dayOff/");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/json";
            request.Method = method;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(dayOff);
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

        public static void Delete(string key, int id)
        {
            //Resource resource = null;
            string token = "Bearer " + key;// GetToken("jeddi@gmail.com", "2222");


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:18080/21meeseeks-web/rest/dayOff/"+id.ToString());
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/json";
            request.Method = "DELETE";

            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", token);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
            }
        }
    }
}
