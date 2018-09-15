using Moq;
using NBench;
using Newtonsoft.Json;
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

namespace ProjectManager.WebApi.Tests.LoadTest
{
    /// <summary>
    /// Contains NUnit test cases for ParentTaskController
    /// </summary>
    public class ParentTaskServiceTest
    {
        // private Counter _counter;
        ParentTaskController ParentTaskController;
        List<Parent_Task> expectedParentTasks;
        List<Task> expectedTasks;
        Mock<IParentTaskRepository> mockRepository;
        private Counter _counter;
        public const int numberOfThreads = 500;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            expectedParentTasks = DataInitializer.GetAllParentTasks();
            expectedTasks = DataInitializer.GetAllTasks();
            mockRepository = new Mock<IParentTaskRepository>();
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetAllParentTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Get()).Returns(() => expectedParentTasks);
            ParentTaskController = new ParentTaskController(mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ParentTaskController.Get();
                _counter.Increment();
            }
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PostParentTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                //ARRANGE
                var user = expectedParentTasks.First();

            mockRepository.Setup(x => x.Post(It.IsAny<Parent_Task>()));
            ParentTaskController = new ParentTaskController(
                mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            //ACT
            var response = ParentTaskController.Post(user);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetParentTaskByParentTaskId()
        {
            for (int i = 0; i < numberOfThreads; i++)
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
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetParentTaskByParentTaskIdNotFound()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
            ParentTaskController = new ParentTaskController(mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = ParentTaskController.Get(6);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PutParentTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                // Arrange   
                mockRepository.Setup(x => x.Post(It.IsAny<Parent_Task>()));
            ParentTaskController = new ParentTaskController(
              mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Act
            Parent_Task updatedParentTask = expectedParentTasks.First();
            updatedParentTask.ParentTaskName = "Support and maintenance";
            var response = ParentTaskController.Put(updatedParentTask.Parent_ID, updatedParentTask);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void DeleteParentTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Delete(1));
            ParentTaskController = new ParentTaskController(
               mockRepository.Object);
            ParentTaskController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Remove last Product
            var actualResult = ParentTaskController.Delete(1);
                _counter.Increment();
            }
        }
    }
}
