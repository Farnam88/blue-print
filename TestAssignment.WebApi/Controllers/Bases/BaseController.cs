using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAssignment.Utilities.Common.Data;

namespace TestAssignment.WebApi.Controllers.Bases
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResultModel<object>), 500)]
    [ProducesResponseType(typeof(ResultModel<object>), 400)]
    [ProducesResponseType(typeof(ResultModel<object>), 404)]
    [ProducesResponseType(typeof(ResultModel<object>), 505)]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }
    }
}