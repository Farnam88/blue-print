using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAssignment.Utilities.Exceptions;
using TestAssignment.WebApi.Controllers.Bases;

namespace TestAssignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        public TestController(ILogger<TestController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new InvalidRequestException(info: new Dictionary<string, string>
            {
                {"Message1", "This is a test"},
                {"Message2", "Number 1"},
            });
        }

        [HttpPost]
        public IActionResult ValidationTest(ValidationTestDto input)
        {
            Logger.LogInformation("Ok Result");
            return Ok(input);
        }
    }
}