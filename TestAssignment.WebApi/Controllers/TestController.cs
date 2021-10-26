using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAssignment.Utilities.Exceptions;
using TestAssignment.WebApi.Helpers.Attributes;

namespace TestAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidRequestFilerAttribute))]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
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
            return Ok(input);
        }
    }
}