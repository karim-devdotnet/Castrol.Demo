using Castrol.Context;
using Castrol.Demo.Extensions;
using Castrol.Demo.Models;
using CsvHelper;
using log4net;
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
        private const string FORM_VALIDATION_ERROR = "Es ist ein Fehler aufgetreten. Überprüfen Sie Ihre Eingaben und versuchen Sie es nochmal.";
        private const string UPLOAD_SUCCESS = "Die Daten wurden per FTP erfolgreich hochgeladen.";
        private CastrolContext db = new CastrolContext(MvcApplication.CastrolContextConnectionString);
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));


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
            model.UserID = HttpContext.Request.QueryString["kid"];

            //model.EVHCDateTimeIn = String.Format(cultureInfo,"{0:MM/dd/yyyy HH:mm}", DateTime.Now);
            //model.DateVehicleFirstRegistered = String.Format(cultureInfo,"{0:MM/dd/yyyy}", DateTime.Now);
            //model.CarDateTimeDueOut = String.Format(cultureInfo,"{0:MM/dd/yyyy HH:mm}", DateTime.Now);

            //QueryString
            Dictionary<string, string> queryString = HttpContext.Request.Unvalidated.QueryString.ToDictionary();
            Log.Info($"QueryString: {System.Web.Helpers.Json.Encode(queryString)}");

            //Get Credential from DB
            UserData userData = db.GetUserData(model.UserID);
            if(userData != null)
            {
                model.UserName = userData.UserName;
                model.UserPassword = userData.UserPassword;
            }
            else
            {
                Log.Info($"Keine Benutzerdaten für UserId:{model.UserID} gefunden!");
                model.CreateNewFTPCredentials = true;
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UploadCastrolData(CastrolDataModel model)
        {
            TempData["ValidationError"] = null;
            if (ModelState.IsValid)
            {
                UserData userData = new UserData
                {
                    UserId = model.UserID,
                    UserName = model.UserName,
                    UserPassword = model.UserPassword
                };
                //Set Credential from DB
                if (model.CreateNewFTPCredentials)
                {
                    try
                    {
                        db.UserDataSet.Add(userData);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message, ex);
                    }
                }

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
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message, ex);
                            SetActionState(FORM_VALIDATION_ERROR);
                            return View("Index", model);
                        }
                    }

                    var data = dataStream.ToArray();
                    if(!UploadToWEB(data,userData))
                    {
                        SetActionState(FORM_VALIDATION_ERROR);
                        return View("Index", model);
                    }
                }

            }
            SetActionState(UPLOAD_SUCCESS, AlertType.success);
            return View("Index", model);
        }

        public bool UploadToWEB(byte[] inData, UserData userData)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = new NetworkCredential(userData.UserName, userData.UserPassword);
                try
                {
                    wc.UploadData($"{ MvcApplication.FtpServer}/CastrolData_{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv", inData);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                    return false;
                }
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}