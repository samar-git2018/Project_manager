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
    /// Parent_Task Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="Parent_Task"></typeparam>
    public class ParentTaskRepository: IParentTaskRepository
    {
        #region Private member variables...
        internal DbSet<Parent_Task> DbSet;
        internal ProjectManagerEntities Context;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public ParentTaskRepository(ProjectManagerEntities context)
        {
            this.DbSet = context.Set<Parent_Task>();
            this.Context = context;
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// Parent_Task Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Parent_Task> Get()
        {
            return DbSet.AsEnumerable();
        }

        /// <summary>
        /// Parent_Task get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Parent_Task GetByID(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Parent_Task Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Post(Parent_Task entity)
        {
            DbSet.Add(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// <summary>
        /// Parent task Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual int Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            return Context.SaveChanges();
        }

        /// <summary>
        /// Parent_Task update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual int Put(int id, Parent_Task entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            return Context.SaveChanges();
            //DbSet.Attach(entityToUpdate);
            //Context.Entry(entityToUpdate).State = EntityState.Modified;
        }       
        #endregion
    }
}