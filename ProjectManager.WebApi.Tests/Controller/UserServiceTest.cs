using Moq;
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
    /// Contains NUnit test cases for UserController
    /// </summary>
    [TestFixture]
    public class UserServiceTest
    {
       // private Counter _counter;
        UserController UserController;
        List<User> expectedUsers;
        Mock<IUserRepository> mockRepository;
        bool isCreateOrUpdateInvokedInRepository = false;

        [SetUp]
        public void Setup()
        {
            expectedUsers = DataInitializer.GetAllUsers();
            mockRepository = new Mock<IUserRepository>();
        }

         [Test, Order(1)]
        public void GetAllUser()
        {
            
            mockRepository.Setup(x => x.Get()).Returns(() => expectedUsers);
            UserController = new UserController(mockRepository.Object);
            UserController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = UserController.Get();

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<User>>(jsonString.Result);
            Assert.AreEqual(expectedUsers.Count, actual.Count);
        }
        [Test, Order(2)]
        public void PostUser()
        {
            //ARRANGE
            var user = expectedUsers.First();

            mockRepository.Setup(x => x.Post(It.IsAny<User>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
            UserController = new UserController(
                mockRepository.Object);
            UserController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };

            //ACT
            var response = UserController.Post(user);

            //ASSERT
            Assert.IsTrue(isCreateOrUpdateInvokedInRepository,
                "Post method in Repository not invoked");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(3)]
        public void GetUserByUserId()
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

            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<User>(jsonString.Result);
            Assert.AreEqual(1, actual.User_ID, "Wrong user id");
        }
        [Test, Order(4)]
        public void GetUserByUserIdNotFound()
        {
            mockRepository.Setup(x => x.GetByID(6)).Returns(() => null);
            UserController = new UserController(mockRepository.Object);
            UserController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            //ACT
            var response = UserController.Get(6);

            //ASSERT
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, Order(5)]
        public void PutUser()
        {
            // Arrange   
            mockRepository.Setup(x => x.Post(It.IsAny<User>())).
                Callback(() => isCreateOrUpdateInvokedInRepository = true);
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
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test, Order(6)]
        public void DeleteUser()
        {
            mockRepository.Setup(x => x.Delete(1)).
               Callback(() => isCreateOrUpdateInvokedInRepository = true);
            UserController = new UserController(
               mockRepository.Object);
            UserController.Request = new HttpRequestMessage()
            {
                Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
            // Remove last Product
              var actualResult = UserController.Delete(1);
            Assert.AreEqual(actualResult.StatusCode, HttpStatusCode.OK);
        }
    }
}
