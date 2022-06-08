using TestApis.DTO;
using TestApis.Models;

namespace TestApis.Repositories
{
    public interface IProductoRepository : IProductoRepository<Producto>
    {
        IEnumerable<ProductoDTO> ObtenerProductos();
        void AgregarProducto(ProductoDTO productoDTO);
    }
}
