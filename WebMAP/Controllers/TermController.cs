using Domain.Entity;
using Service;
using Service.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebMAP.Controllers
{
    public class TermController : Controller
    {
        // GET: test
        public ActionResult ResourcesCompetencesAvailabilities(int idCompetence = -1, bool Available = false, bool Unavailable = false, bool AvailableSoon = false, bool UnavailableSoon = false)
        {

            if (idCompetence == -1 && !(Available || Unavailable || UnavailableSoon || AvailableSoon))
            {
                var comptences = ResourceTermService.FindCompetence();
                ViewBag.Competences = comptences;
                var listResource = new List<resource>();
                return View(listResource);
            }
            else
            {
                var searchValue = new resource();
                searchValue.levels = new List<level>();
                searchValue.levels.Add(new level() { idCompetence = idCompetence });

                searchValue.availability = string.Join(";", Available ? "Available" : "", Unavailable ? "Unavailable" : "", AvailableSoon ? "AvailableSoon" : "", UnavailableSoon ? "UnavailableSoon" : "");
                ResourceTermService RC = new ResourceTermService();
                var listResource = RC.GetMany(searchValue);
                var comptences = ResourceTermService.FindCompetence();
                ViewBag.Competences = comptences;
                return View(listResource);
            }


        }
        public ActionResult List()
        {
            TermService ts = new TermService();
            var model = ts.GetMany().ToList();

            return View(model);
        }

        public ActionResult Archive()
        {
            Model1 model = new Model1();
            var archiveTerms = model.termarchives.ToList();

            return View(archiveTerms);
        }
        public ActionResult archived(int idProject, int idResource)
        {

            term t = new term();
            t.idProject = idProject;
            t.idResource = idResource;
            return View(t);
        }
        public ActionResult Terminate(int idProject, int idResource, string mail)
        {
            TermService ts = new TermService();
            var term = ts.Get(t => t.idProject == idProject && t.idResource == idResource);
            term.dateEnd = DateTime.Now;
            ts.Update(term);
            ts.Commit();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Bonjour,");
            sb.AppendLine();
            sb.AppendLine("On a terminé votre mandat pour des raisons bien déterminées.\nVeuillez contacter l'administration pour plus d'information.");
            NewMail(mail, "Mandat terminé", sb.ToString());
            return RedirectToAction("List");

        }

        public ActionResult AddTermToResource(int id, string firstname, string lastname, double? salary, string mail)
        {
            term t = new term();
            t.idResource = id;
            t.project = new project();
            t.resource = new resource() { firstName = firstname, lastName = lastname, idUser = id, email = mail };
            var projects = ResourceTermService.FindProjects();
            ViewBag.Projects = projects;

            return View(t);
        }
        [HttpPost]
        public ActionResult AddTermToResource(term model)
        {

            //var newTerm = new
            //{
            //    pkTerm = new { idProject = model.project.idProject, idResource = model.idResource },
            //    dateEnd = model.dateEnd.Value.ToString(""),
            //    dateStart = model.dateStart,
            //    numberofDaysTerm = model.numberofDaysTerm
            //};
            ApiFactory.AddTerm(model.project.idProject, model.idResource, model.dateStart.Value, model.dateEnd.Value, model.numberofDaysTerm, model.description);
            return RedirectToAction("List");
        }


        public ActionResult GetEndDate(int idUser, string dateStart, int numberofDaysTerm)
        {

            DateTime result;
            try
            {
                DateTime start;
                if (DateTime.TryParse(dateStart, out start))
                {
                    ApiFactory ap = new ApiFactory();
                    result = ap.CalculateEndTerm(idUser, numberofDaysTerm, start);
                    return Json(result != null ? result.ToString("yyyy-MM-dd") : "", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                return Json("No result", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SendMail(string mail)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Bonjour,");
                sb.AppendLine();
                sb.AppendLine("Veuillez contacter l'administration pour trouver une solution concernant la date fin mandat qui dépasse la date fin du projet.");
                NewMail(mail, "Date Fin Mandat", sb.ToString());
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        private static void NewMail(string mail, string subject, string body)
        {
            var fromAddress = new MailAddress("meeseeks.esprit@gmail.com", "MEESEEKS ESPRIT");
            var toAddress = new MailAddress(mail);
            string fromPassword = "esprit21";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public ActionResult GetFrais(double? salary, int numberofDaysTerm)
        {
            try
            {
                var frais = ApiFactory.Frais(numberofDaysTerm, salary ?? 0);
                return Json(frais, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}