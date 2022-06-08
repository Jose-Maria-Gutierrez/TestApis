namespace TestApis.Repositories
{
    public interface IProductoRepository<T> where T : class
    {
        void add(T entity);
        void delete(T entity);
        void update(T entity);
        int count();
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void deleteAll();
    }
}
