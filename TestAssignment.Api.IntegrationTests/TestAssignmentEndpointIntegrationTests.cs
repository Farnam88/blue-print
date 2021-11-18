using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TestAssignment.Application.Contexts.TestAssignmentServices.Commands;
using TestAssignment.Application.Contexts.TestAssignmentServices.Dtos;
using TestAssignment.Domain.Common.Data;
using TestAssignment.Domain.Common.Enums;
using TestAssignment.WebApi;
using Xunit;

namespace TestAssignment.Api.IntegrationTests
{
    public class TestAssignmentEndpointIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public TestAssignmentEndpointIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        [Fact]
        public async Task CreateTestAssignment_ShouldReturnOk_OnSuccess()
        {
            //Arrange

            var expectedResult = ResultModel<int>.Success("", 1);
            CreateTestAssignmentCommand request = new CreateTestAssignmentCommand()
            {
                Title = "Test"
            };

            var requestBody = JsonContent.Create(request);

            //Act

            var responseMessage = await _httpClient.PostAsync("/api/test-assignment", requestBody);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultModel<int>>(resultStr);

            //Assert
            Assert.Equal(expectedResult.IsSucceeded, result.IsSucceeded);
            Assert.Equal(expectedResult.ErrorCode, result.ErrorCode);
            Assert.Equal(expectedResult.Result, result.Result);
        }


        [Fact]
        public async Task GetTestAssignments_ShouldReturnListOfDto_OnSuccess()
        {
            //Arrange

            var expectedResult = ResultModel<IList<TestAssignmentDto>>.Success("", new List<TestAssignmentDto>());


            //Act

            var responseMessage = await _httpClient.GetAsync("/api/test-assignment");

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResultModel<IList<TestAssignmentDto>>>(resultStr);

            //Assert
            Assert.Equal(expectedResult.IsSucceeded, result.IsSucceeded);
            Assert.Equal(expectedResult.ErrorCode, result.ErrorCode);
            Assert.Equal(expectedResult.Result, result.Result);
        }
    }
}