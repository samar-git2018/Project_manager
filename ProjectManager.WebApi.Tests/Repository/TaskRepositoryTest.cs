using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class TaskRepositoryTests

    {
        ITaskRepository _repository;
        List<Task> expectedTasks;

        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            _repository = new TaskRepository(new ProjectManagerEntities());
            expectedTasks = DataInitializer.GetAllTasks();
        }

        /// <summary>
        /// Tasks the crud.
        /// </summary>

        [Test]
        public void TaskCrud()

        {
            int task_ID = Post();
            GetByID(task_ID);
            GetAll();
            Put(task_ID);
            Delete(task_ID);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The id of the new record.</returns>

        private int Post()

        {

            // Arrange
            var task = expectedTasks.First();

            // Act
            _repository.Post(task);

            // Assert
            List<Task> tasks = _repository.Get();

            return tasks.Count > 0 ? tasks.Max(dr => dr.Task_ID) : 0;

        }

        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>

        private void Put(int id)

        {

            // Arrange
            Task updatedTask = _repository.GetByID(id);

            updatedTask.TaskName = "Estimation";
            // Act

            _repository.Put(id, updatedTask);

            updatedTask = _repository.GetByID(updatedTask.Task_ID);
            // Assert
            Assert.AreEqual("Estimation", updatedTask.TaskName, "Record is not updated.");
        }



        /// <summary>
        /// Gets all.
        /// </summary>

        private void GetAll()
        {
            // Act
            IEnumerable<Task> items = _repository.Get();
            // Assert
            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
        }

        /// <summary>
        /// Gets the by ID.
        /// <param name="id">The id of the Task.</param>

        private void GetByID(int id)
        {

            // Act
            Task updatedTask = expectedTasks[0];

            Task task = _repository.GetByID(id);

            // Assert
            Assert.IsNotNull(task, "GetByID returned null.");

            Assert.AreEqual(id, task.Task_ID);

            Assert.AreEqual(updatedTask.TaskName, task.TaskName);

        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>

        private void Delete(int id)
        {
            // Arrange
            Task task = _repository.GetByID(id);
            
            // Act
            _repository.Delete(id);

            task = _repository.GetByID(id);

            // Assert
            Assert.IsNull(task, "Record is not deleted.");
        }

    }

}