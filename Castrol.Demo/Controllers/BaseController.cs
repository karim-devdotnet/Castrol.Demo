using Castrol.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Castrol.Demo.Controllers
{
    public class BaseController : Controller
    {
        protected void SetActionState(string message, AlertType alertType = AlertType.danger)
        {
            TempData["ValidationError"] = message;
            TempData["AlertType"] = alertType.ToString();
        }
    }
}