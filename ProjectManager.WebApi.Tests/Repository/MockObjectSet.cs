using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WebApi.Tests.Repository
{
    public partial class MockObjectSet<T> : IObjectSet<T> where T : class
    {
        private readonly IList<T> collection = new List<T>();

        #region IObjectSet<T> Members

        public void AddObject(T entity)
        {
            collection.Add(entity);
        }

        public void Attach(T entity)
        {
            collection.Add(entity);
        }

        public void DeleteObject(T entity)
        {
            collection.Remove(entity);
        }

        public void Detach(T entity)
        {
            collection.Remove(entity);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region IQueryable<T> Members

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return collection.AsQueryable<T>().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return collection.AsQueryable<T>().Provider; }
        }

        #endregion
    }
}
