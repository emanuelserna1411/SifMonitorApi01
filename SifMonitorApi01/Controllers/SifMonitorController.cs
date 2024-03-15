using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sif;
using Sif.Rest.Api;
using SifMonitorApi01.Entities;
using SifMonitorApi01.Helpers;
using SifMonitorApi01.Services;
using System.Diagnostics;
using System.Net.Mime;


namespace SifMonitorApi01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SifMonitorController : SifControllerBase
    {
        /// <summary>
        /// Endpoint que se invoca desde SifBranchMonitor para registar la actividad de un usuario en un día.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        [HttpPost("UserActivityCreate")]
        public IActionResult UserActivityCreate(UserActivity activity)
        {
            DataDict dictionary = new DataDict();
            dictionary.Security.UserId = Convert.ToInt32(activity.UserId);
            dictionary.Sif.LoggedBranch = Convert.ToString(activity.BranchId);
            this.Dictionary = dictionary;
            this.StartService(new CreateActivity(this.Dictionary, activity));
            return Ok();
        }
        /// <summary>
        /// Endpoint para obtener el top n de los usuarios con mayor inactividad.
        /// </summary>
        /// <param name="topUsers"></param>
        /// <returns></returns>
        [HttpPost("UserInactivityTop")]

        public ActionResult<String> UserInactivityTop(TopUsersInactivity topUsers)
        {
            //TODO:INVOCAR EL SERVICIO QUE TARE EL TOP 10 DE LOS USUARIOS CON MAYOR INACTIVIDAD DE UN DIA ESPECIFICO
            this.StartService(new TopInactivity(this.Dictionary, topUsers));
            return TopInactivity.Response;
        }

        [HttpPost("Events")]

        public ActionResult<String> UserEvents()
        {
            return Ok();
        }

        [HttpPost("NumTransacrions")]

        public ActionResult<String> NumTransactions()
        {
            //TOD: Invocar el servicio que trae el top de los usuarios con mayor numero de transacciones realizadas
            return Ok();
        }
    }
}
