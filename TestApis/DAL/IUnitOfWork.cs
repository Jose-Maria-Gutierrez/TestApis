using TestApis.Repositories;

namespace TestApis.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IProductoRepository ProductoRepository { get; }
        void SaveChanges();
    }
}
