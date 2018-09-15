using Moq;
using NBench;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Controllers;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace ProjectManager.WebApi.Tests.LoadTest
{
    /// <summary>
    /// Contains NUnit test cases for UserController
    /// </summary>
    public class UserServiceTest
    {
        // private Counter _counter;
        UserController UserController;
        private Counter _counter;
        List<User> expectedUsers;
        Mock<IUserRepository> mockRepository;
        public const int numberOfThreads = 500;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            expectedUsers = DataInitializer.GetAllUsers();
            mockRepository = new Mock<IUserRepository>();
            _counter = context.GetCounter("TestCounter");
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetAllUser()
        {
            
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Get()).Returns(() => expectedUsers);
                UserController = new UserController(mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = UserController.Get();
                _counter.Increment();
            }
        }
        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues.",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PostUser()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                //ARRANGE
                var user = expectedUsers.First();

                mockRepository.Setup(x => x.Post(It.IsAny<User>()));
                UserController = new UserController(
                    mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };

                var response = UserController.Post(user);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetUserByUserId()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                var expectedUser = expectedUsers.First();
                mockRepository.Setup(x => x.GetByID(1)).Returns(() => expectedUser);
                UserController = new UserController(mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = UserController.Get(1);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void GetUserByUserIdNotFound()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
                UserController = new UserController(mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                //ACT
                var response = UserController.Get(6);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void PutUser()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                // Arrange   
                mockRepository.Setup(x => x.Post(It.IsAny<User>()));
                UserController = new UserController(
                  mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Act
                User updatedUser = expectedUsers[0];
                updatedUser.First_Name = "Abhik";
                updatedUser.Employee_ID = "EMP896";
                var response = UserController.Put(updatedUser.User_ID, updatedUser);
                _counter.Increment();
            }
        }

        [PerfBenchmark(Description = "Test 500 threads for 10 mintues",
        NumberOfIterations = 500, RunMode = RunMode.Iterations,
        RunTimeMilliseconds = 600000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000.0d)]
        public void DeleteUser()
        {
            for (int i = 0; i < numberOfThreads; i++)
            {
                mockRepository.Setup(x => x.Delete(1));
                UserController = new UserController(
                   mockRepository.Object);
                UserController.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };
                // Remove last Product
                var actualResult = UserController.Delete(1);
                _counter.Increment();
            }
        }
    }
}
