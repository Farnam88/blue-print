using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestAssignment.WebApi.Controllers.Bases
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }
    }
}