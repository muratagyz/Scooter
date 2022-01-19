using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scooter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Scooter.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [Route("GetNotification")]
        [HttpGet]
        public ActionResult GetNotification()
        {
            return new Json( NotificationService.GetNotification(), new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
