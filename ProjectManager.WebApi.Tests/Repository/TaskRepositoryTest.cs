using Moq;
using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class TaskRepositoryTests

    {

        ITaskRepository _repository;
        List<Task> expectedTasks;
        Mock<ProjectManagerEntities> contextMock = new Mock<ProjectManagerEntities>();
        Mock<DbSet<Task>> dbSetMock = new Mock<DbSet<Task>>();

        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            contextMock.Setup(x => x.Set<Task>()).Returns(dbSetMock.Object);
            _repository = new TaskRepository(contextMock.Object);
            expectedTasks = DataInitializer.GetAllTasks();

        }


        /// <summary>

        /// Tasks the crud.

        /// </summary>

        [Test]

        public void TaskCrud()

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
            var Task = expectedTasks.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<Task>())).Returns(Task);

            // Act
            _repository.Post(Task);

            //Assert
            contextMock.Verify(x => x.Set<Task>());
            //dbSetMock.Verify(x => x.Add(It.Is<Task>(y => y == Task)));
        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put()

        {
            // Arrange
            var Task = expectedTasks.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<Task>())).Returns(Task);
            Task.TaskName = "Estimation";
            // Act
            _repository.Put(Task.Task_ID, Task);

            //Assert
            contextMock.Verify(x => x.Set<Task>());
            //dbSetMock.Verify(x => x.Add(It.Is<Task>(y => y == Task)));
        }



        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Arrange

            var task = new sp_GetTaskData_Result {
                Task_ID = 1,
                TaskName = "Analysis",
                Project_ID = 1,
                Priority = 10,
                Parent_ID = 1,
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(1),
                Status = "I"
            };
            var TaskList = new List<sp_GetTaskData_Result>() { task };
            var mockResultSet = new Mock<ObjectResult<sp_GetTaskData_Result>>();

            //mockResultSet.Setup(x => x.GetEnumerator()).Returns(expectedTasks.GetEnumerator());
            contextMock.Setup(x => x.Set<Task>()).Returns(dbSetMock.Object);
            
            mockResultSet.As<IQueryable<sp_GetTaskData_Result>>().Setup(x => x.Provider).Returns
                                                 (TaskList.AsQueryable().Provider);
            mockResultSet.As<IQueryable<sp_GetTaskData_Result>>().Setup(x => x.Expression).
                                                 Returns(TaskList.AsQueryable().Expression);
            mockResultSet.As<IQueryable<sp_GetTaskData_Result>>().Setup(x => x.ElementType).Returns
                                                 (TaskList.AsQueryable().ElementType);
            mockResultSet.As<IQueryable<sp_GetTaskData_Result>>().Setup(x => x.GetEnumerator()).Returns
                                                 (TaskList.AsQueryable().GetEnumerator());
            contextMock.Setup(x => x.sp_GetTaskData()).Returns(mockResultSet.Object);
            // Act
            var result = _repository.Get();
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>

        /// Gets the by ID.

        /// </summary>

        /// <param name="id">The id of the Task.</param>

        private void GetByID()

        {
            // Arrange
            Task Task = expectedTasks.First();

            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(Task);
            // Act
            _repository.GetByID(1);
            // Assert
            contextMock.Verify(x => x.Set<Task>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));

        }

        /// <summary>
        /// Deletes the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Delete()

        {
            // Arrange
            var Task = expectedTasks.First();
            dbSetMock.Setup(x => x.Remove(It.IsAny<Task>())).Returns(Task);

            // Act
            _repository.Delete(Task.Task_ID);

            //Assert
            contextMock.Verify(x => x.Set<Task>());
            dbSetMock.Verify(x => x.Remove(It.Is<Task>(y => y == Task)));

        }
    }
}