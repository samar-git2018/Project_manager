using Moq;
using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class UserRepositoryTests

    {

        IUserRepository _repository;
        List<User> expectedUsers;
        Mock<ProjectManagerEntities> contextMock = new Mock<ProjectManagerEntities>();
        Mock<DbSet<User>> dbSetMock = new Mock<DbSet<User>>();

        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            contextMock.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            _repository = new UserRepository(contextMock.Object);
            expectedUsers = DataInitializer.GetAllUsers();

        }


        /// <summary>

        /// Users the crud.

        /// </summary>

        [Test]

        public void UserCrud()

        {

            Post();
            GetByID();
            GetAll();
            Put();
            Delete();

        }

        /// <summary>

        /// Creates this instance.

        /// </summary>

        /// <returns>The id of the new record.</returns>

        private void Post()

        {

            // Arrange
            var user = expectedUsers.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<User>())).Returns(user);

            // Act
            _repository.Post(user);

            //Assert
            contextMock.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.Is<User>(y => y == user)));
        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put()

        {
            // Arrange
            var user = expectedUsers.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<User>())).Returns(user);
            user.First_Name = "Souvik";
            // Act
            _repository.Put(user.User_ID, user);

            //Assert
            contextMock.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.Is<User>(y => y == user)));
        }



        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Arrange
            var user = expectedUsers.First();
            var userList = new List<User>() { user };

            dbSetMock.As<IQueryable<User>>().Setup(x => x.Provider).Returns
                                                 (userList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.Expression).
                                                 Returns(userList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.ElementType).Returns
                                                 (userList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns
                                                 (userList.AsQueryable().GetEnumerator());
            // Act
            var result = _repository.Get();
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>

        /// Gets the by ID.

        /// </summary>

        /// <param name="id">The id of the User.</param>

        private void GetByID()

        {
            // Arrange
            User user = expectedUsers.First();

            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(user);
            // Act
            _repository.GetByID(1);
            // Assert
            contextMock.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));

        }

        /// <summary>
        /// Deletes the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Delete()

        {
            // Arrange
            var user = expectedUsers.First();
            dbSetMock.Setup(x => x.Remove(It.IsAny<User>())).Returns(user);

            // Act
            _repository.Delete(user.User_ID);

            //Assert
            contextMock.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Remove(It.Is<User>(y => y == user)));

        }
    }
}