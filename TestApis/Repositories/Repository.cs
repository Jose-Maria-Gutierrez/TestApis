using TestApis.Datos;
using TestApis.Models;

namespace TestApis.Repositories
{
    public class Repository<T> : IProductoRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public int count()
        {
            return this.context.Set<T>().Count();
        }

        public void delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public void deleteAll()
        {
            int id = 0;
            while (this.context.Set<T>().Any())
            {
                T? entity = this.context.Set<T>().Find(id);
                if (entity != null)
                {
                    this.context.Set<T>().Remove(entity);
                }
                id++;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return this.context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void update(T entity)
        {
            this.context.Set<T>().Update(entity);
        }
    }
}
