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
    public class ParentTaskRepositoryTests

    {

        IParentTaskRepository _repository;
        List<Parent_Task> expectedParentTasks;
        Mock<ProjectManagerEntities> contextMock = new Mock<ProjectManagerEntities>();
        Mock<DbSet<Parent_Task>> dbSetMock = new Mock<DbSet<Parent_Task>>();

        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            contextMock.Setup(x => x.Set<Parent_Task>()).Returns(dbSetMock.Object);
            _repository = new ParentTaskRepository(contextMock.Object);
            expectedParentTasks = DataInitializer.GetAllParentTasks();

        }


        /// <summary>

        /// ParentTasks the crud.

        /// </summary>

        [Test]

        public void ParentTaskCrud()

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
            var ParentTask = expectedParentTasks.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<Parent_Task>())).Returns(ParentTask);

            // Act
            _repository.Post(ParentTask);

            //Assert
            contextMock.Verify(x => x.Set<Parent_Task>());
            dbSetMock.Verify(x => x.Add(It.Is<Parent_Task>(y => y == ParentTask)));
        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put()

        {
            // Arrange
            var ParentTask = expectedParentTasks.First();

            // Act
            dbSetMock.Setup(x => x.Add(It.IsAny<Parent_Task>())).Returns(ParentTask);
            ParentTask.ParentTaskName = "Review";
            // Act
            _repository.Put(ParentTask.Parent_ID, ParentTask);

            //Assert
            contextMock.Verify(x => x.Set<Parent_Task>());
            dbSetMock.Verify(x => x.Add(It.Is<Parent_Task>(y => y == ParentTask)));
        }



        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Arrange
            var ParentTask = expectedParentTasks.First();
            var ParentTaskList = new List<Parent_Task>() { ParentTask };

            dbSetMock.As<IQueryable<Parent_Task>>().Setup(x => x.Provider).Returns
                                                 (ParentTaskList.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Parent_Task>>().Setup(x => x.Expression).
                                                 Returns(ParentTaskList.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Parent_Task>>().Setup(x => x.ElementType).Returns
                                                 (ParentTaskList.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Parent_Task>>().Setup(x => x.GetEnumerator()).Returns
                                                 (ParentTaskList.AsQueryable().GetEnumerator());
            // Act
            var result = _repository.Get();
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id of the ParentTask.</param>

        private void GetByID()

        {
            // Arrange
            Parent_Task ParentTask = expectedParentTasks.First();

            dbSetMock.Setup(x => x.Find(It.IsAny<int>())).Returns(ParentTask);
            // Act
            _repository.GetByID(1);
            // Assert
            contextMock.Verify(x => x.Set<Parent_Task>());
            dbSetMock.Verify(x => x.Find(It.IsAny<int>()));

        }

        /// <summary>
        /// Deletes the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Delete()
        {
            // Arrange
            var ParentTask = expectedParentTasks.First();
            dbSetMock.Setup(x => x.Remove(It.IsAny<Parent_Task>())).Returns(ParentTask);

            // Act
            _repository.Delete(ParentTask.Parent_ID);

            //Assert
            contextMock.Verify(x => x.Set<Parent_Task>());
            dbSetMock.Verify(x => x.Remove(It.Is<Parent_Task>(y => y == ParentTask)));
        }
    }
}