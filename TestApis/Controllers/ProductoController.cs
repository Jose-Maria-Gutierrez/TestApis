using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        private readonly ILogger<ProductoController> logger;

        public ProductoController(IUnitOfWork unitOfWork, ILogger<ProductoController> logger)
        {
            this.uof = unitOfWork;
            this.logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<ProductoDTO> GetProductos()
        {
            logger.LogDebug("se ejecuta GetProductos");
            return this.uof.ProductoRepository.ObtenerProductos();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoDTO> GetProducto(int id)
        {
            Producto? producto = this.uof.ProductoRepository.GetById(id);
            if(producto == null)
            {
                logger.LogError($"metodo GetProducto no se encuentra el producto con id {id}");
                return NotFound();
            }
            logger.LogDebug($"se ejecuta metodo GetProducto");
            return producto.productoToDto();
        }
        
        [HttpPost]
        public ActionResult<Producto> AgregarProducto([FromBody]ProductoDTO producto)
        {
            this.uof.ProductoRepository.AgregarProducto(producto);
            this.uof.SaveChanges();
            logger.LogDebug("se ejecuta AgregarProducto");
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
                    logger.LogError($"metodo ModificarProducto no se encuentra id {id}");
                    return NotFound();
                }
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.SKU = producto.SKU;
                productoExistente.Precio = producto.Precio;
                this.uof.ProductoRepository.update(productoExistente);
                this.uof.SaveChanges();
                logger.LogDebug("se ejecuta ModificarProducto");
                return productoExistente;
            }
            logger.LogDebug("metodo ModificarProducto info no valida");
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Producto? ProductoExistente = this.uof.ProductoRepository.GetById(id);
            if (ProductoExistente == null)
            {
                logger.LogError($"metodo Delete no se encuentra id {id}");
                return NotFound();
            }
            this.uof.ProductoRepository.delete(ProductoExistente);
            this.uof.SaveChanges();
            logger.LogDebug("se ejecuta metodo Delete");
            return NoContent();
        }
    }
}
