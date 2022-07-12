using BluePrint.Application.Contexts.TestTodoServices.Commands;
using BluePrint.Application.Contexts.TestTodoServices.Dtos;
using BluePrint.Domain.Common.Data;
using BluePrint.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BluePrint.WebApi.IntegrationTests
{
    public class TestAssignmentEndpointIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public TestAssignmentEndpointIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task CreateTestAssignment_ShouldReturnOk_OnSuccess()
        {
            //Arrange

            string title = "Test";
            var expectedResult = ResultModel<TestTodoDto>.Success("", new TestTodoDto { Id = 1, Title = title });
            CreateTestTodoCommand request = new CreateTestTodoCommand()
            {
                Title = title
            };

            var requestBody = JsonContent.Create(request);

            //Act

            var responseMessage = await _httpClient.PostAsync(EndpointsBaseRouteNames.TestTodo, requestBody);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultModel<TestTodoDto>>(resultStr);

            //Assert
            Assert.Equal(expectedResult.IsSucceeded, result.IsSucceeded);
            Assert.Equal(expectedResult.ErrorCode, result.ErrorCode);
            Assert.Equal(expectedResult.Result!.Id, result.Result!.Id);
            Assert.Equal(expectedResult.Result.Title, result.Result.Title);
        }


        [Fact]
        public async Task GetTestAssignments_ShouldReturnListOfDto_OnSuccess()
        {
            //Arrange

            var expectedResult = ResultModel<IList<TestTodoDto>>.Success("", new List<TestTodoDto>());


            //Act

            var responseMessage = await _httpClient.GetAsync(EndpointsBaseRouteNames.TestTodo);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultModel<IList<TestTodoDto>>>(resultStr);

            //Assert
            Assert.Equal(expectedResult.IsSucceeded, result.IsSucceeded);
            Assert.Equal(expectedResult.ErrorCode, result.ErrorCode);
            Assert.Equal(expectedResult.Result, result.Result);
        }
    }
}