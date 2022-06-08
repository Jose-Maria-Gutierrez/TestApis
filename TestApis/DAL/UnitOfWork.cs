using TestApis.Datos;
using TestApis.Repositories;

namespace TestApis.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IProductoRepository ProductoRepository { get; }
        public UnitOfWork(ApplicationDbContext context, IProductoRepository ProductoRepository)
        {
            this.context = context;
            this.ProductoRepository = ProductoRepository;
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
