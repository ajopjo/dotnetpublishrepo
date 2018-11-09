using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [Consumes(Constants.ContentType)]
    [Produces(Constants.ContentType)]
    [Route(Constants.ResourcePath)]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //private readonly IInventoryService _inventoryService;
        //private readonly ILinkHelper _linkHelper;

        /// <summary>
        /// This API provides real time inventory data from SAP ERP.
        /// </summary>
        /// <param name="request"><see cref="InventoryRequest"/></param>
        /// <returns>An array wrapped in an object that contains a list of <see cref="string"/>, one item corresponds to one product number.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden. For the user logged in but attempting to access inventories that are not allowed.</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpGet]
        [Route("", Name = Constants.InventoryRouteName)]
        public ActionResult<string> Get([FromQuery]InventoryRequest request)
        {
            return Ok("Hello world");
        }
    }
}