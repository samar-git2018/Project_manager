#region Using Namespaces...
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Persistence;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web.Http;
using ProjectManager.WebApi.Controllers;
using System.Data.Entity.Core.Objects;
#endregion

namespace ProjectManager.WebApi.Repository
{
    /// <summary>
    /// Task Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="Task"></typeparam>
    public class TaskRepository : ITaskRepository
    {
        #region Private member variables...
        internal DbSet<Task> DbSet;
        internal ProjectManagerEntities Context;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public TaskRepository(ProjectManagerEntities context)
        {
            this.DbSet = context.Set<Task>();
            this.Context = context;
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// Task Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual List<Task> Get()
        {
            var data = Context.sp_GetTaskData();
            List<Task> tasks = new List<Task>();
            foreach (sp_GetTaskData_Result item in data)
            {
                tasks.Add(new Task
                {
                    Task_ID = item.Task_ID,
                    TaskName = item.TaskName,
                    ParentTaskName = item.ParentTaskName,
                    Project_ID = item.Project_ID,
                    Priority = item.Priority,
                    Parent_ID = item.Parent_ID,
                    Start_Date = item.Start_Date,
                    End_Date = item.End_Date,
                    Status = item.Status
                });
            }
            //var data = Context.Database.SqlQuery<Task>("sp_GetTaskData").ToList();
            return tasks;
        }

        /// <summary>
        /// Task get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task GetByID(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Task Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Post(Task entity)
        {
            var data = Context.sp_InsertTaskUpdateUser(entity.Parent_ID, entity.TaskName, entity.Start_Date, entity.End_Date, entity.Priority, entity.Status, entity.User_ID, entity.Project_ID);
            //DbSet.Add(entity);
            return data;
        }

        /// <summary>
        /// Task Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual int Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            return Context.SaveChanges();
        }

        /// <summary>
        /// Task update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual int Put(int id, Task entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            return Context.SaveChanges();
            //DbSet.Attach(entityToUpdate);
            //Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        #endregion
    }
}