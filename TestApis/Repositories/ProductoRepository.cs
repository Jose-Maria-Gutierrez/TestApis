using TestApis.Datos;
using TestApis.DTO;
using TestApis.Models;
using TestApis.Utilidades;

namespace TestApis.Repositories
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository 
    {
        public ProductoRepository(ApplicationDbContext context) : base(context) { }

        public void AgregarProducto(ProductoDTO productoDTO)
        {
            Producto nuevo = productoDTO.productoDTOtoProducto();
            this.add(nuevo);
        }

        public IEnumerable<ProductoDTO> ObtenerProductos()
        {
            return this.GetAll().Select(x => x.productoToDto());
        }
    }
}
