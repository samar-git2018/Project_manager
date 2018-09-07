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
    /// Contains NUnit test cases for ParentTaskController
    /// </summary>
    [TestFixture]
    public class ParentTaskServiceTest
    {
       // private Counter _counter;
        ParentTaskController ParentTaskController;
        List<Parent_Task> expectedParentTasks;
        List<Task> expectedTasks;
        Mock<IParentTaskRepository> mockRepository;
        bool isCreateOrUpdateInvokedInRepository = false;

        [SetUp]
        //[PerfSetup]
        public void Setup()
        {
            expectedParentTasks = DataInitializer.GetAllParentTasks();
            expectedTasks = DataInitializer.GetAllTasks();
            mockRepository = new Mock<IParentTaskRepository>();
        }

         [Test, Order(1)]
       // [PerfBenchmark(
       //NumberOfIterations = 3, RunMode = RunMode.Throughput,
       //RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
       // //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
       // //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
       // [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetAllParentTask()
        {
            
            mockRepository.Setup(x => x.Get()).Returns(() => expectedParentTasks);
            ParentTaskController = new ParentTaskController(mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ParentTaskController.Get();

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Parent_Task>>(jsonString.Result);
            Assert.AreEqual(expectedParentTasks.Count, actual.Count);
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
        [Test, Order(2)]
        public void PostParentTask()
        {
            //ARRANGE
            var user = expectedParentTasks.First();

            mockRepository.Setup(x => x.Post(It.IsAny<Parent_Task>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            ParentTaskController = new ParentTaskController(
                mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            //ACT
            var response = ParentTaskController.Post(user);

            //ASSERT
            Assert.IsTrue(isCreateOrUpdateInvokedInRepository,
                "Post method in Repository not invoked");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(3)]
        public void GetParentTaskByParentTaskId()
        {
            var expectedParentTask = expectedParentTasks.First();
            mockRepository.Setup(x => x.GetByID(1)).Returns(() => expectedParentTask);
            ParentTaskController = new ParentTaskController(mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ParentTaskController.Get(1);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Parent_Task>(jsonString.Result);
            Assert.AreEqual(1, actual.Parent_ID, "Wrong parent id");
        }
        [Test, Order(3)]
        public void GetParentTaskByParentTaskIdNotFound()
        {
            mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
            ParentTaskController = new ParentTaskController(mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ParentTaskController.Get(6);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(4)]
        public void PutParentTask()
        {
            // Arrange   
            mockRepository.Setup(x => x.Post(It.IsAny<Parent_Task>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            ParentTaskController = new ParentTaskController(
              mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Act
            Task updatedParentTask = expectedTasks.First();
            updatedParentTask.TaskName = "Support and maintenance";
            var response = ParentTaskController.Put(updatedParentTask.Parent_ID, updatedParentTask);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(5)]
        public void DeleteParentTask()
        {
            mockRepository.Setup(x => x.Delete(1)).
               Callback(() => isCreateOrUpdateInvokedInRepository = true);
            ParentTaskController = new ParentTaskController(
               mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Remove last Product
              var actualResult = ParentTaskController.Delete(1);
            Assert.AreEqual(actualResult.StatusCode, HttpStatusCode.OK);
        }
    }
}
