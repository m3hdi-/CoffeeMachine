using CoffeeMachine.EntityManagers.Interfaces;
using CoffeeMachine.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CoffeeMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeMachineController : ControllerBase
    {

        private readonly ICoffeeMachineManager _coffeeMachineManager;

        public CoffeeMachineController(ICoffeeMachineManager coffeeMachineManager)
        {
            _coffeeMachineManager = coffeeMachineManager;
        }


        [HttpGet("/GetDoses")]
        [ProducesResponseType(typeof(Dose), StatusCodes.Status200OK)]
        public ActionResult<IQueryable> GetDoses()
        {
            return Ok(_coffeeMachineManager.GetDoses());
        }


        [HttpGet("/GetLastDose")]
        [ProducesResponseType(typeof(Dose), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Dose), StatusCodes.Status200OK)]
        public ActionResult<IQueryable> GetLastDose(bool isBadge, string userName)
        {
            if (isBadge)
            {
                var lastUsedDose = _coffeeMachineManager.GetLastDose(isBadge, userName);

                return Ok(lastUsedDose.FirstOrDefault());
            }

            return StatusCode(StatusCodes.Status404NotFound, "you must use your badge to get your last used dose");

        }


        [HttpPost]
        [ProducesDefaultResponseType(typeof(Dose))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IQueryable<Dose>>> CreateCommand(string type, bool isMug, bool? isBadge, string username )
        {

            if (type.ToLowerInvariant() == "milk" || type.ToLowerInvariant() == "tea" || type.ToLowerInvariant() == "chocolat")
            {
                Dose newCmd = new Dose();
                newCmd.Type = type;
                newCmd.IsMug = isMug;
                newCmd.IsBadge = isBadge;
                newCmd.User = username;

                // set new Dose
                 var dose = await _coffeeMachineManager.CreateDose(newCmd);
                return StatusCode(StatusCodes.Status201Created, "Your Order is Ready");
            }

            return StatusCode(StatusCodes.Status404NotFound, "Please select correct choice (Milk/Tea/Chocolat)");
        }
    }
}
