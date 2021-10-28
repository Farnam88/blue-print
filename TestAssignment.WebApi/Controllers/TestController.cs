using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAssignment.Core;
using TestAssignment.Models;
using TestAssignment.Utilities.Common.Data;
using TestAssignment.WebApi.Controllers.Bases;

namespace TestAssignment.WebApi.Controllers
{
    [Route("api/tests")]
    public class TestController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public TestController(ILogger<TestController> logger, IUnitOfWork uow) : base(logger)
        {
            _uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<TestAssignmentEntity>), 200)]
        public async Task<IActionResult> Get()
        {
            var testAssignment = await _uow.TestAssignmentRepository.FindAsync(1);
            return Ok(testAssignment);
        }

        [HttpPost]
        public IActionResult ValidationTest(ValidationTestDto input)
        {
            Logger.LogInformation("Ok Result");
            return Ok(input);
        }
    }
}