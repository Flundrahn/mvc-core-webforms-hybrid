using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace Data.Repositories
{
    public abstract class BaseRepository<T>
    {
        private readonly ISession _session;

        protected BaseRepository(ISession session)
        {
            // set the private field
            _session = session;
        }

        public void Save(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);

                transaction.Commit();
            }
        }

        public void Delete(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);

                transaction.Commit();
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var transaction = _session.BeginTransaction())
            {
                var entities = _session.Query<T>().ToList();

                transaction.Commit();

                return entities;
            }
        }

        public T Get(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var entity = _session.Get<T>(id);

                transaction.Commit();

                return entity;
            }
        }
    }
}
