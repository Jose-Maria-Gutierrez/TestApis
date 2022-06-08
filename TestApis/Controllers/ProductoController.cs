using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TestApis.DAL;
using TestApis.Datos;
using TestApis.DTO;
using TestApis.Models;
using TestApis.Utilidades;

namespace TestApis.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IUnitOfWork uof;

        public ProductoController(IUnitOfWork unitOfWork)
        {
            this.uof = unitOfWork;
        }
        
        [HttpGet]
        public IEnumerable<ProductoDTO> GetProductos()
        {
            return this.uof.ProductoRepository.ObtenerProductos();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoDTO> GetProducto(int id)
        {
            Producto? producto = this.uof.ProductoRepository.GetById(id);
            if(producto == null)
            {
                return NotFound();
            }
            return producto.productoToDto();
        }
        
        [HttpPost]
        public ActionResult<Producto> AgregarProducto([FromBody]ProductoDTO producto)
        {
            this.uof.ProductoRepository.AgregarProducto(producto);
            this.uof.SaveChanges();
            return NoContent(); //tendria que devolver el Producto
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> ModificarProducto(int id,[FromBody] ProductoDTO producto)
        {
            if (ModelState.IsValid)
            {
                Producto? productoExistente = this.uof.ProductoRepository.GetById(id);
                if (productoExistente == null)
                {
                    return NotFound();
                }
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.SKU = producto.SKU;
                productoExistente.Precio = producto.Precio;
                this.uof.ProductoRepository.update(productoExistente);
                this.uof.SaveChanges();
                return productoExistente;
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Producto? ProductoExistente = this.uof.ProductoRepository.GetById(id);
            if (ProductoExistente == null)
            {
                return NotFound();
            }
            this.uof.ProductoRepository.delete(ProductoExistente);
            this.uof.SaveChanges();
            return NoContent();
        }
    }
}
