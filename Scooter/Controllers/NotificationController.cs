using Scooter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Scooter.Controllers
{
    public class NotificationController : ApiController
    {
        [System.Web.Http.Route("Api/Notification/GetNotification")]
        [System.Web.Http.HttpGet]
        public JsonResult GetNotification()
        {
            return new JsonResult()
            {
                Data = NotificationService.GetNotification(),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
