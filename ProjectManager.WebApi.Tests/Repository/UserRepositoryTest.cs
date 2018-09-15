using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class UserRepositoryTests

    {

        IUserRepository _repository;

        List<User> expectedUsers;

        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            _repository = new UserRepository(new ProjectManagerEntities());
            expectedUsers = DataInitializer.GetAllUsers();

        }


        /// <summary>

        /// Users the crud.

        /// </summary>

        [Test]

        public void UserCrud()

        {

            int user_ID = Post();

            GetByID(user_ID);

            GetAll();

            Put(user_ID);

            Delete(user_ID);

        }

        /// <summary>

        /// Creates this instance.

        /// </summary>

        /// <returns>The id of the new record.</returns>

        private int Post()

        {

            // Arrange
            var user = expectedUsers.First();

            // Act

            _repository.Post(user);



            // Assert

            Assert.IsNotNull(user.User_ID);

            user = _repository.GetByID(user.User_ID);

            return user.User_ID;

        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put(int id)

        {

            // Arrange

            User updatedUser = expectedUsers[0];

            updatedUser.First_Name = "Abhik";

            updatedUser.Employee_ID = "EMP896";
            // Act

            _repository.Put(id, updatedUser);



            updatedUser = _repository.GetByID(updatedUser.User_ID);


            // Assert

            Assert.AreEqual("Abhik", updatedUser.First_Name, "Record is not updated.");

        }



        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Act

            IEnumerable<User> items = _repository.Get();

            // Assert

            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");

        }



        /// <summary>

        /// Gets the by ID.

        /// </summary>

        /// <param name="id">The id of the User.</param>

        private void GetByID(int id)

        {

            // Act
            User updatedUser = expectedUsers[0];

            User user = _repository.GetByID(id);



            // Assert

            Assert.IsNotNull(user, "GetByID returned null.");

            Assert.AreEqual(id, user.User_ID);

            Assert.AreEqual(updatedUser.Employee_ID, user.Employee_ID);

        }



        /// <summary>

        /// Deletes the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Delete(int id)

        {

            // Arrange

            User user = _repository.GetByID(id);

            // Act

            _repository.Delete(id);

            user = _repository.GetByID(id);

            // Assert

            Assert.IsNull(user, "Record is not deleted.");

        }

    }

}