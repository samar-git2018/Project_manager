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
    /// Contains NUnit test cases for ProjectController
    /// </summary>

    public class ProjectServiceTest
    {
        // private Counter _counter;
        ProjectController ProjectController;
        List<Project> expectedProjects;
        Mock<IProjectRepository> mockRepository;
        private Counter _counter;
        public const int numberOfThreads = 500;
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            expectedProjects = DataInitializer.GetAllProjects();
            mockRepository = new Mock<IProjectRepository>();
            _counter = context.GetCounter("TestCounter");
        }


        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetAllProject()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Get()).Returns(() => expectedProjects);
                ProjectController = new ProjectController(mockRepository.Object);
                ProjectController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = ProjectController.Get();
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
        public void PostProject()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                //ARRANGE
                var project = expectedProjects.First();

                mockRepository.Setup(x => x.Post(It.IsAny<Project>()));
                var controller = new ProjectController(
                    mockRepository.Object);
                controller.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };

                //ACT
                var response = controller.Post(project);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetProjectByProjectId()
        {
            for (int i = 0; i < numberOfThreads; i++)
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
                _counter.Increment();
            }
        }


        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetProjectByProjectIdNotFound()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
                ProjectController = new ProjectController(mockRepository.Object);
                ProjectController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = ProjectController.Get(6);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PutProject()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                // Arrange   
                mockRepository.Setup(x => x.Post(It.IsAny<Project>()));
                ProjectController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Act
                Project updatedProject = expectedProjects[0];
                updatedProject.ProjectName = "NY PEIC";
                var response = ProjectController.Put(updatedProject.Project_ID, updatedProject);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void DeleteProject()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Delete(1));
                ProjectController = new ProjectController(
                   mockRepository.Object);
                ProjectController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Remove last Product
                var actualResult = ProjectController.Delete(1);
                _counter.Increment();
            }
        }
    }
}
