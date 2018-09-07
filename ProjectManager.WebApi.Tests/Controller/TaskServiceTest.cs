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
using System.Data.Entity.Core.Objects;

namespace ProjectManager.WebApi.Tests.Controller
{
    /// <summary>
    /// Contains NUnit test cases for TaskController
    /// </summary>
    [TestFixture]
    public class TaskServiceTest
    {
        // private Counter _counter;
        TaskController TaskController;
        List<Task> expectedTasks;
        Mock<ITaskRepository> mockRepository;
        bool isCreateOrUpdateInvokedInRepository = false;

        [SetUp]
        //[PerfSetup]
        public void Setup()
        {
            expectedTasks = DataInitializer.GetAllTasks();
            
            mockRepository = new Mock<ITaskRepository>();
        }

        [Test, Order(1)]
        // [PerfBenchmark(
        //NumberOfIterations = 3, RunMode = RunMode.Throughput,
        //RunTimeMilliseconds = 1000, TestMode = TestMode.Measurement)]
        // //[CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 10000000.0d)]
        // //[MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        // [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetAllTask()
        {

            mockRepository.Setup(x => x.Get()).Returns(() => expectedTasks);
            TaskController = new TaskController(mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = TaskController.Get();

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Task>>(jsonString.Result);
            Assert.AreEqual(expectedTasks.Count, actual.Count);
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
        [Test, Order(2)]
        public void PostTask()
        {
            //ARRANGE
            var Task = expectedTasks.First();

            mockRepository.Setup(x => x.Post(It.IsAny<Task>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            TaskController = new TaskController(
                mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            //ACT
            var response = TaskController.Post(Task);

            //ASSERT
            Assert.IsTrue(isCreateOrUpdateInvokedInRepository,
                "Post method in Repository not invoked");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(3)]
        public void GetTaskByTaskId()
        {
            var expectedTask = expectedTasks.First();
            mockRepository.Setup(x => x.GetByID(1)).Returns(() => expectedTask);
            TaskController = new TaskController(mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = TaskController.Get(1);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Task>(jsonString.Result);
            Assert.AreEqual(1, actual.Task_ID, "Wrong Task id");
        }
        [Test, Order(4)]
        public void GetTaskByTaskIdNotFound()
        {
            mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
            TaskController = new TaskController(mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = TaskController.Get(6);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(5)]
        public void PutTask()
        {
            // Arrange   
            mockRepository.Setup(x => x.Post(It.IsAny<Task>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            TaskController = new TaskController(mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Act
            Task updatedTask = expectedTasks[0];
            updatedTask.TaskName = "NY PEIC";
            var response = TaskController.Put(updatedTask.Task_ID, updatedTask);
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(6)]
        public void DeleteTask()
        {
            mockRepository.Setup(x => x.Delete(1)).
              Callback(() => isCreateOrUpdateInvokedInRepository = true);
            TaskController = new TaskController(
               mockRepository.Object);
            TaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Remove last Product
            var actualResult = TaskController.Delete(1);
            Assert.AreEqual(actualResult.StatusCode, HttpStatusCode.OK);
        }
    }
}
