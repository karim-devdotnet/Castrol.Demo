using Castrol.Demo.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Castrol.Demo.Controllers
{
    public class HomeController : BaseController
    {
        private readonly CultureInfo cultureInfo = new CultureInfo("en");
        private const string FORM_VALIDATION_ERROR = "Es ist ein Fehler aufgetreten. Überprüfe Deine Eingaben und versuche es nochmal.";
        private const string UPLOAD_SUCCESS = "Die Daten wurden per FTP erfolgreich hochgeladen";

        [HttpGet]
        public ActionResult Index(bool editableData = false)
        {
            var model = new CastrolDataModel();
            ViewBag.EditableData = editableData;
            model.CustomerName = HttpContext.Request.QueryString["customername"];
            model.Vehicle = HttpContext.Request.QueryString["vehicle"];
            model.VehicleModel = HttpContext.Request.QueryString["vehiclemodel"];
            model.Registration = HttpContext.Request.QueryString["registration"];
            model.VIN = HttpContext.Request.QueryString["vin"];
            model.Email = HttpContext.Request.QueryString["email"];
            model.ContactPhone = HttpContext.Request.QueryString["telefon"];
            model.Fax = HttpContext.Request.QueryString["fax"];
            model.WIP = "0";
            var mileage = 0;
            int.TryParse(HttpContext.Request.QueryString["mileage"], out mileage);
            model.Mileage = mileage;

            //model.EVHCDateTimeIn = String.Format(cultureInfo,"{0:MM/dd/yyyy HH:mm}", DateTime.Now);
            //model.DateVehicleFirstRegistered = String.Format(cultureInfo,"{0:MM/dd/yyyy}", DateTime.Now);
            //model.CarDateTimeDueOut = String.Format(cultureInfo,"{0:MM/dd/yyyy HH:mm}", DateTime.Now);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UploadCastrolData(CastrolDataModel model)
        {
            TempData["ValidationError"] = null;
            if (ModelState.IsValid)
            {

                //using (var stream = new StreamWriter(Server.MapPath("~/Upload/CastrolData.csv")))
                //{
                //    using (var csv = new CsvWriter(stream))
                //    {
                //        csv.Configuration.HasHeaderRecord = false;
                //        csv.Configuration.Delimiter = ",";
                //        csv.Configuration.QuoteAllFields = true;
                //        csv.WriteRecord(model);
                //    }
                //}

                using (var dataStream = new MemoryStream())
                {
                    using (var stream = new StreamWriter(dataStream))
                    {
                        try
                        {
                            using (var csv = new CsvWriter(stream))
                            {
                                csv.Configuration.HasHeaderRecord = false;
                                csv.Configuration.Delimiter = ",";
                                csv.Configuration.QuoteAllFields = true;
                                csv.WriteRecord(model);
                            }
                        }
                        catch (Exception)
                        {
                            SetActionState(FORM_VALIDATION_ERROR);
                            return RedirectToAction("Index");
                        }
                    }

                    var data = dataStream.ToArray();
                    if(!SaveOnFtP(data))
                    {
                        SetActionState(FORM_VALIDATION_ERROR);
                        return RedirectToAction("Index");
                    }
                }

            }
            SetActionState(UPLOAD_SUCCESS, AlertType.success);
            return RedirectToAction("Index");
        }

        private bool SaveOnFtP(byte[] inData)
        {
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"{ ConfigurationManager.AppSettings["FtpServer"]}/CastrolData_{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FtpUser"], ConfigurationManager.AppSettings["FtpPassword"]);
            ftpRequest.UsePassive = true;
            ftpRequest.UseBinary = true;
            ftpRequest.KeepAlive = false;

            try
            {
                WebResponse ftpResponse = ftpRequest.GetResponse();
                using (Stream requestStream = ftpRequest.GetRequestStream())
                {
                    requestStream.Write(inData, 0, inData.Length);
                }
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}