using ProjectManager.Persistence;
using System.Net.Http;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

namespace ProjectManager.WebApi.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User GetByID(int id);
        int Post(User Entity);
        int Put(int id, User Entity);
        int Delete(int id);
    }
    public interface IProjectRepository
    {
        IEnumerable<Project> Get();
        Project GetByID(int id);
        int Post(Project Entity);
        int Put(int id, Project Entity);
        int Delete(int id);
    }
    public interface ITaskRepository
    {
        List<Task> Get();
        Task GetByID(int id);
        int Post(Task Entity);
        int Put(int id, Task Entity);
        int Delete(int id);
    }
    public interface IParentTaskRepository
    {
        IEnumerable<Parent_Task> Get();
        Parent_Task GetByID(int id);
        int Post(Parent_Task Entity);
        int Put(int id, Task Entity);
        int Delete(int id);
    }

}