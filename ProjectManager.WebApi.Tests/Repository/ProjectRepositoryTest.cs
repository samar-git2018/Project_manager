using NUnit.Framework;
using ProjectManager.Persistence;
using ProjectManager.WebApi.Repository;
using ProjectManager.WebApi.Tests.TestsHelper;
using System.Collections.Generic;
using System.Linq;


namespace ProjectManager.WebApi.Tests.Repository

{

    [TestFixture]

    public class ProjectRepositoryTests

    {

        IProjectRepository _repository;
        ITaskRepository _taskRepository;

        List<Project> expectedProjects;
        List<Task> expectedTasks;


        /// <summary>

        /// Sets up.

        /// </summary>

        [SetUp]

        public void SetUp()

        {
            _repository = new ProjectRepository(new ProjectManagerEntities());
            _taskRepository = new TaskRepository(new ProjectManagerEntities());
            expectedProjects = DataInitializer.GetAllProjects();
            expectedTasks = DataInitializer.GetAllTasks();

        }


        /// <summary>

        /// Projects the crud.

        /// </summary>

        [Test]

        public void ProjectCrud()

        {

            int project_ID = Post();

            GetByID(project_ID);

            GetAll();

            Put(project_ID);

            Delete(project_ID);

        }

        /// <summary>

        /// Creates this instance.

        /// </summary>

        /// <returns>The id of the new record.</returns>

        private int Post()

        {

            // Arrange

            var project = expectedProjects.First();
            var task = expectedTasks.First();

            // Act

            _repository.Post(project);
            task.Project_ID = project.Project_ID;
            _taskRepository.Post(task);

            // Assert

            Assert.IsNotNull(project.Project_ID);

            project = _repository.GetByID(project.Project_ID);

            return project.Project_ID;

        }



        /// <summary>

        /// Updates the specified id.

        /// </summary>

        /// <param name="id">The id.</param>

        private void Put(int id)

        {

            // Arrange

            Project updatedProject = expectedProjects[0];

            updatedProject.ProjectName = "CA Auto";



            // Act

            _repository.Put(id, updatedProject);

            updatedProject = _repository.GetByID(updatedProject.Project_ID);

            // Assert

            Assert.AreEqual("CA Auto", updatedProject.ProjectName, "Record is not updated.");

        }

        /// <summary>

        /// Gets all.

        /// </summary>

        private void GetAll()

        {

            // Act
            IEnumerable<Project> items = _repository.Get();
            // Assert
            Assert.IsTrue(items.Count() > 0, "GetAll returned no items.");
        }



        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id of the Project.</param>

        private void GetByID(int id)

        {
            // Act
            Project updatedProject = expectedProjects[0];

            Project project = _repository.GetByID(id);

            // Assert

            Assert.IsNotNull(project, "GetByID returned null.");

            Assert.AreEqual(id, project.Project_ID);

            Assert.AreEqual(updatedProject.ProjectName, project.ProjectName);

        }



        /// <summary>
        /// Deletes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>

        private void Delete(int id)

        {
            // Arrange

            Project project = _repository.GetByID(id);

            // Act
            _repository.Delete(id);
            project = _repository.GetByID(id);

            // Assert
            Assert.IsNull(project, "Record is not deleted.");

        }

    }

}