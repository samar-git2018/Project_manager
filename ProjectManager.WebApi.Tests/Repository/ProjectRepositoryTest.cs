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

    public class ProjectRepositoryTests

    {
        IProjectRepository _repository;
        List<Project> expectedProjects;
        List<Task> expectedTasks;
        Mock<ProjectManagerEntities> contextMock = new Mock<ProjectManagerEntities>();
        Mock<DbSet<Project>> dbSetMockPoject = new Mock<DbSet<Project>>();
        Mock<DbSet<Task>> dbSetMockTask = new Mock<DbSet<Task>>();

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]

        public void SetUp()

        {
            contextMock.Setup(x => x.Set<Project>()).Returns(dbSetMockPoject.Object);
            _repository = new ProjectRepository(contextMock.Object);
            expectedProjects = DataInitializer.GetAllProjects();
            expectedTasks = DataInitializer.GetAllTasks();
        }


        /// <summary>
        /// Projects the crud.
        /// </summary>
        [Test]
        public void ProjectCrud()
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
            var Project = expectedProjects.First();

            // Act
            dbSetMockPoject.Setup(x => x.Add(It.IsAny<Project>())).Returns(Project);

            // Act
            _repository.Post(Project);

            //Assert
            contextMock.Verify(x => x.Set<Project>());
            dbSetMockPoject.Verify(x => x.Add(It.Is<Project>(y => y == Project)));
        }



        /// <summary>
        /// Updates the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        private void Put()
        {
            // Arrange
            var Project = expectedProjects.First();

            // Act
            dbSetMockPoject.Setup(x => x.Add(It.IsAny<Project>())).Returns(Project);
            Project.ProjectName = "UVIS";
            // Act
            _repository.Put(Project.Project_ID, Project);

            //Assert
            contextMock.Verify(x => x.Set<Project>());
            dbSetMockPoject.Verify(x => x.Add(It.Is<Project>(y => y == Project)));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        private void GetAll()
        {
            var project = new sp_GetProjectData_Result
            {
                Project_ID = 1,
                ProjectName = "UVIS",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(1),
                Priority = 10
            };
            // Arrange
            var ProjectList = new List<sp_GetProjectData_Result>() { project };
            var mockResultSet = new Mock<ObjectResult<sp_GetProjectData_Result>>();
            mockResultSet.As<IQueryable<sp_GetProjectData_Result>>().Setup(x => x.Provider).Returns
                                                 (ProjectList.AsQueryable().Provider);
            mockResultSet.As<IQueryable<sp_GetProjectData_Result>>().Setup(x => x.Expression).
                                                 Returns(ProjectList.AsQueryable().Expression);
            mockResultSet.As<IQueryable<sp_GetProjectData_Result>>().Setup(x => x.ElementType).Returns
                                                 (ProjectList.AsQueryable().ElementType);
            mockResultSet.As<IQueryable<sp_GetProjectData_Result>>().Setup(x => x.GetEnumerator()).Returns
                                                 (ProjectList.AsQueryable().GetEnumerator());
            contextMock.Setup(x => x.sp_GetProjectData()).Returns(mockResultSet.Object);
            // Act
            var result = _repository.Get();
            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id of the Project.</param>
        private void GetByID()
        {
            // Arrange
            Project Project = expectedProjects.First();
            dbSetMockPoject.Setup(x => x.Find(It.IsAny<int>())).Returns(Project);
            // Act
            _repository.GetByID(1);
            // Assert
            contextMock.Verify(x => x.Set<Project>());
            dbSetMockPoject.Verify(x => x.Find(It.IsAny<int>()));
        }

        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        private void Delete()
        {
            // Arrange
            var Project = expectedProjects.First();
            dbSetMockPoject.Setup(x => x.Remove(It.IsAny<Project>())).Returns(Project);
            dbSetMockTask.As<IQueryable<Task>>().Setup(x => x.Provider).Returns
                                                 (expectedTasks.AsQueryable().Provider);
            dbSetMockTask.As<IQueryable<Task>>().Setup(x => x.Expression).
                                                 Returns(expectedTasks.AsQueryable().Expression);
            dbSetMockTask.As<IQueryable<Task>>().Setup(x => x.ElementType).Returns
                                                 (expectedTasks.AsQueryable().ElementType);
            dbSetMockTask.As<IQueryable<Task>>().Setup(x => x.GetEnumerator()).Returns
                                                 (expectedTasks.AsQueryable().GetEnumerator());
            contextMock.Setup(x => x.Tasks).Returns(dbSetMockTask.Object);
            // Act
            _repository.Delete(Project.Project_ID);

            //Assert
            contextMock.Verify(x => x.Set<Project>());
            dbSetMockPoject.Verify(x => x.Remove(It.Is<Project>(y => y == Project)));

        }
    }
}