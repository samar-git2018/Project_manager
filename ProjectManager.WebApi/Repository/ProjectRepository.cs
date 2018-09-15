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

#endregion

namespace ProjectManager.WebApi.Repository
{
    /// <summary>
    /// Project Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="Project"></typeparam>
    public class ProjectRepository:  IProjectRepository
    {
        #region Private member variables...
        internal DbSet<Project> DbSet;
        internal ProjectManagerEntities Context;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public ProjectRepository(ProjectManagerEntities context)
        {
            this.DbSet = context.Set<Project>();
            this.Context = context;
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// Project Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual List<Project> Get()
        {
            var data = Context.sp_GetProjectData();
            List<Project> projects = new List<Project>();
            foreach (sp_GetProjectData_Result item in data)
            {
                projects.Add(new Project
                {
                    ProjectName = item.ProjectName,
                    Project_ID = item.Project_ID,
                    Manager_Id = item.Manager_Id,
                    Start_Date = item.Start_Date,
                    Priority = item.Priority,
                    End_Date = item.End_Date,
                    TaskCount = item.TaskCount,
                    CompletedTaskCount = item.CompletedTaskCount
                });
            }
            return projects;
        }

        /// <summary>
        /// Project get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Project GetByID(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Project Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Post(Project entity)
        {
            DbSet.Add(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// Project Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual int Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            foreach (Task task in Context.Tasks)
            {
                if (task.Project_ID == id)
                {
                    Context.Tasks.Remove(task);
                }
            }
            return Context.SaveChanges();
        }
        
        /// <summary>
        /// Project update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual int Put(int id, Project entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            return Context.SaveChanges();
            //DbSet.Attach(entityToUpdate);
            //Context.Entry(entityToUpdate).State = EntityState.Modified;
        }       
        #endregion
    }
}