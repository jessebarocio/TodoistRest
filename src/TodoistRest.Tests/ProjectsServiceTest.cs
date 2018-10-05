using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace TodoistRest.Tests
{
    public class ProjectsServiceTest
    {
        [Fact]
        public async Task GetAllProjectsAsync_MakesCorrectHttpCall()
        {
            // Arrange
            var projects = new Project[] { };
            var mockHttp = new Mock<ITodoistHttpHandler>();
            mockHttp.Setup(m => m.GetAsync<IEnumerable<Project>>(It.IsAny<string>()))
                .ReturnsAsync(projects);
            var projectsService = new ProjectsService(mockHttp.Object);

            // Act
            var result = await projectsService.GetAllProjectsAsync();

            // Assert
            Assert.Same(projects, result);
        }
    }
}
