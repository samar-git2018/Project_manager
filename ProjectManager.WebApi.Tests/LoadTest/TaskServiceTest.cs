using Moq;
using NBench;
using Newtonsoft.Json;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Controllers;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ProjectManager.WebApi.Tests.LoadTest
{
    /// <summary>
    /// Contains NUnit test cases for TaskController
    /// </summary>
    public class TaskServiceTest
    {
        // private Counter _counter;
        TaskController TaskController;
        List<Task> expectedTasks;
        Mock<ITaskRepository> mockRepository;
        private Counter _counter;
        public const int numberOfThreads = 500;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            expectedTasks = DataInitializer.GetAllTasks();
            mockRepository = new Mock<ITaskRepository>();
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetAllTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Get()).Returns(() => expectedTasks);
                TaskController = new TaskController(mockRepository.Object);
                TaskController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = TaskController.Get();
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
        public void PostTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                //ARRANGE
                var Task = expectedTasks.First();

                mockRepository.Setup(x => x.Post(It.IsAny<Task>()));
                TaskController = new TaskController(
                    mockRepository.Object);
                TaskController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };

                //ACT
                var response = TaskController.Post(Task);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetTaskByTaskId()
        {
            for (int i = 0; i < numberOfThreads; i++)
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
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetTaskByTaskIdNotFound()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
                TaskController = new TaskController(mockRepository.Object);
                TaskController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = TaskController.Get(6);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PutTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                // Arrange   
                mockRepository.Setup(x => x.Post(It.IsAny<Task>()));
                TaskController = new TaskController(mockRepository.Object);
                TaskController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Act
                Task updatedTask = expectedTasks[0];
                updatedTask.TaskName = "NY PEIC";
                var response = TaskController.Put(updatedTask.Task_ID, updatedTask);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void DeleteTask()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Delete(1));
                TaskController = new TaskController(
                   mockRepository.Object);
                TaskController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Remove last Product
                var actualResult = TaskController.Delete(1);
                _counter.Increment();
            }
        }
    }
}
