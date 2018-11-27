
using Domain;
using Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


namespace Service.RestApi
{


    public class ApiFactory
    {


        //public static List<resource> FindResources(int idCompetence, string[] availables)
        //{
        //    List<resource> result = new List<resource>();
        //    var requestObject = new { competence = new competence() { idCompetence = idCompetence }, availables = availables };
        //    result = PostRequest<List<resource>>(new Uri(Properties.Settings.Default.ListResourcesRequestUrl), requestObject);
        //    return result;
        //}

        public static float Frais(int numberofDaysTerm, double salary)
        {
            float result = 0;
            var requestObject = new { resources = new { salary = salary }, numberofDaysTerm = numberofDaysTerm };
            result = PostRequest<float>(new Uri(Properties.Settings.Default.CalculerFraisMandat), requestObject);
            return result;
        }

        public static void AddTerm(int idProject, int idRessource, DateTime start, DateTime end, int numberofDaysTerm)
        {
            var requestObject = new { pkTerm = new { idProject = idProject, idResource = idRessource }, dateStart = start, dateEnd = end, numberofDaysTerm = numberofDaysTerm };

            PostRequest(new Uri(Properties.Settings.Default.AddTerm), requestObject);
        }
        public static void AddArchiveTerm(DateTime dateEnd)
        {
            var requestObject = new { dateEnd = dateEnd };
            PostRequest(new Uri(Properties.Settings.Default.AddArchiveTerm), requestObject);
        }
        public static term CalculateEndTerm(int idUser, int numberOfDays, DateTime startDate)
        {
            string data = Properties.Settings.Default.EndDateRequestBady.Replace("[STARTDATE]", startDate.ToString("yyyy-MM-dd"))
                                                                        .Replace("[IDUSER]", idUser.ToString())
                                                                        .Replace("[DAYS]", numberOfDays.ToString());
            term term = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(Properties.Settings.Default.CalculateEndTermRequestUrl));

                request.Method = "POST";
                request.ContentType = "application/json";

                request.ContentLength = data.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string resultJson = responseReader.ReadToEnd().Replace("\"resources\"", "\"resource\"");
                            term = JsonConvert.DeserializeObject<term>(resultJson);
                        }
                    }
                }
                return term;
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        string resultJson = responseReader.ReadToEnd();
                        throw;
                    }
                }

            }
            finally
            {

            }
        }
        public static void PostRequest(Uri uri, object dataObject)
        {
            string data = JsonConvert.SerializeObject(dataObject);


            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        //using (StreamReader responseReader = new StreamReader(responseStream))
                        //{
                        //    string resultJson = responseReader.ReadToEnd();
                        //    result = JsonConvert.DeserializeObject<T>(resultJson);
                        //}
                    }
                }

            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        string resultJson = responseReader.ReadToEnd();
                        throw;
                    }
                }

            }
            finally
            {

            }
        }
        public static T PostRequest<T>(Uri uri, object dataObject)
        {
            string data = JsonConvert.SerializeObject(dataObject);

            T result = default(T);
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader responseReader = new StreamReader(responseStream))
                        {
                            string resultJson = responseReader.ReadToEnd();
                            result = JsonConvert.DeserializeObject<T>(resultJson);
                        }
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream))
                    {
                        string resultJson = responseReader.ReadToEnd();
                        throw;
                    }
                }

            }
            finally
            {

            }
        }
    }
}