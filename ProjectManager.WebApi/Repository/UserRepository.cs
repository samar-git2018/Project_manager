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
    /// User Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="User"></typeparam>
    public class UserRepository: IUserRepository
    {
        #region Private member variables...
        internal DbSet<User> DbSet;
        internal ProjectManagerEntities Context;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="ProjectManagerDB"></param>
        public UserRepository(ProjectManagerEntities context)
        {
            this.DbSet = context.Set<User>();
            this.Context = context;
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// User Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<User> Get()
        {
            return DbSet.AsEnumerable();
        }

        /// <summary>
        /// User get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual User GetByID(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// User Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Post(User entity)
        {
            DbSet.Add(entity);
            return Context.SaveChanges();
        }

        /// <summary>
        /// User Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual int Delete(int id)
        {
            DbSet.Remove(DbSet.Find(id));
            return Context.SaveChanges();
        }
        /// <summary>
        /// User update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual int Put(int id, User entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            return Context.SaveChanges();
            //DbSet.Attach(entityToUpdate);
            //ProjectManagerDB.Entry(entityToUpdate).State = EntityState.Modified;
        }       
        #endregion
    }
}