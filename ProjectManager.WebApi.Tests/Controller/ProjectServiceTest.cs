using Moq;
using NBench;
using Newtonsoft.Json;
using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Controllers;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ProjectManager.WebApi.Tests.Controller
{
    /// <summary>
    /// Contains NUnit test cases for ProjectController
    /// </summary>
    [TestFixture]
    public class ProjectServiceTest
    {
        // private Counter _counter;
        ProjectController ProjectController;
        List<Project> expectedProjects;
        int count = 0;
        Mock<IProjectRepository> mockRepository;
        bool isCreateOrUpdateInvokedInRepository = false;

        [SetUp]
        //[PerfSetup]
        public void Setup()
        {
            expectedProjects = DataInitializer.GetAllProjects();
            mockRepository = new Mock<IProjectRepository>();
        }

        [Test, Order(1)]
        // [PerfBenchmark(
        //NumberOfIterations = 3, RunMode = RunMode.Throughput,
        //RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        // //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        // //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        // [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetAllProject()
        {

            mockRepository.Setup(x => x.Get()).Returns(() => expectedProjects);
            ProjectController = new ProjectController(mockRepository.Object);
            ProjectController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ProjectController.Get();

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Project>>(jsonString.Result);
            Assert.AreEqual(expectedProjects.Count, actual.Count);
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
        [Test, Order(2)]
        public void PostProject()
        {
            //ARRANGE
            var project = expectedProjects.First();

            mockRepository.Setup(x => x.Post(It.IsAny<Project>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            var controller = new ProjectController(
                mockRepository.Object);
            controller.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            //ACT
            var response = controller.Post(project);

            //ASSERT
            Assert.IsTrue(isCreateOrUpdateInvokedInRepository,
                "Post method in Repository not invoked");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(3)]
        public void GetProjectByProjectId()
        {
            var expectedProject = expectedProjects.First();
            mockRepository.Setup(x => x.GetByID(1)).Returns(() => expectedProject);
            ProjectController = new ProjectController(mockRepository.Object);
            ProjectController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ProjectController.Get(1);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Project>(jsonString.Result);
            Assert.AreEqual(1, actual.Project_ID, "Wrong project id");
        }
        [Test, Order(3)]
        public void GetProjectByProjectIdNotFound()
        {
            mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
            ProjectController = new ProjectController(mockRepository.Object);
            ProjectController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ProjectController.Get(6);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(4)]
        public void PutProject()
        {
            // Arrange   
            mockRepository.Setup(x => x.Post(It.IsAny<Project>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            ProjectController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Act
            Project updatedProject = expectedProjects[0];
            updatedProject.ProjectName = "NY PEIC";
            var response = ProjectController.Put(updatedProject.Project_ID, updatedProject);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(5)]
        public void DeleteProject()
        {
            mockRepository.Setup(x => x.Delete(1)).
              Callback(() => isCreateOrUpdateInvokedInRepository = true);
            ProjectController = new ProjectController(
               mockRepository.Object);
            ProjectController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Remove last Product
            var actualResult = ProjectController.Delete(1);
            Assert.AreEqual(actualResult.StatusCode, HttpStatusCode.OK);
        }
    }
}
