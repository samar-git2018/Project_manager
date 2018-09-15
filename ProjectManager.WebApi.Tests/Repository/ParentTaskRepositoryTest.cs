using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class ParentTaskRepositoryTests

    {

        IParentTaskRepository _repository;

        List<Parent_Task> expectedParentTasks;
        List<Task> expectedTasks;


        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            _repository = new ParentTaskRepository(new ProjectManagerEntities());
            expectedParentTasks = DataInitializer.GetAllParentTasks();
            expectedTasks = DataInitializer.GetAllTasks();

        }


        /// <summary>

        /// ParentTasks the crud.

        /// </summary>

        [Test]

        public void ParentTaskCrud()

        {

            int parent_ID = Post();

            GetByID(parent_ID);

            GetAll();

            Put(parent_ID);

            Delete(parent_ID);

        }

        
        /// <summary>

        /// Creates this instance.

        /// </summary>

        /// <returns>The id of the new record.</returns>

        private int Post()

        {

            // Arrange



            var parentTask = expectedParentTasks.First();

            // Act

            _repository.Post(parentTask);



            // Assert

            //Assert.AreEqual(1, ParentTask.ID, "Creating new record does not return id");

            parentTask = _repository.GetByID(parentTask.Parent_ID);

            return parentTask.Parent_ID;

        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put(int id)

        {

            // Arrange

            Parent_Task task = expectedParentTasks.First();
            task.ParentTaskName = "Support and maintenance";


            // Act

            _repository.Put(id, task);



            Parent_Task parentTask = _repository.GetByID(task.Parent_ID);


            // Assert

            Assert.AreEqual("Support and maintenance", parentTask.ParentTaskName);

        }



        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Act

            IEnumerable<Parent_Task> items = _repository.Get();

            // Assert

            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");

        }



        /// <summary>

        /// Gets the by ID.

        /// </summary>

        /// <param name="id">The id of the ParentTask.</param>

        private void GetByID(int id)

        {

            // Act
            Parent_Task updatedParentTask = expectedParentTasks[0];

            Parent_Task parentTask = _repository.GetByID(id);



            // Assert

            Assert.IsNotNull(parentTask, "GetByID returned null.");

            Assert.AreEqual(id, parentTask.Parent_ID);

            Assert.AreEqual(updatedParentTask.ParentTaskName, updatedParentTask.ParentTaskName);

        }



        /// <summary>

        /// Deletes the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Delete(int id)

        {

            // Arrange

            Parent_Task parentTask = _repository.GetByID(id);

            // Act
            _repository.Delete(id);

            parentTask = _repository.GetByID(id);

            // Assert

            Assert.IsNull(parentTask, "Record is not deleted.");

        }

    }

}