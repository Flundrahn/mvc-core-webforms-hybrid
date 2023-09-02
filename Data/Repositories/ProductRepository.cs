using Data.Configuration;
using Data.Entities;
using Data.Interfaces;
using NHibernate;

namespace Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ISession session) : base(session)
        {
        }

        public ProductRepository() : base(NhibernateHelper.SessionFactory.OpenSession())
        {
        }
    }
}
