using Moq;
using TestApis.Controllers;
using TestApis.DAL;
using TestApis.DTO;
using TestApis.Models;
using TestApis.Repositories;
using TestApis.Utilidades;
using Xunit;

namespace TestApisTest
{
    public class UnitTest1
    {
        [Fact] //[TestMethod]
        public void Test1()
        {
            int id = 2;
            Producto hardcodeado = new Producto
            {
                Id = id,
                Nombre = "mesa",
                Descripcion = "de madera",
                Precio = 27.75,
                FechaAlta = System.DateTime.Now,
                SKU = "MES333"
            };
            ProductoDTO hardcodeadoDTO = hardcodeado.productoToDto();


            Mock<IProductoRepository> productosRepo = new Mock<IProductoRepository>();
            Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

            unitOfWork.Setup(a => a.ProductoRepository).Returns(productosRepo.Object);

            productosRepo.Setup(a => a.GetById(It.IsAny<int>())).Returns(hardcodeado);

            ProductoController productoController = new ProductoController(unitOfWork.Object, null);

            var respuesta = productoController.GetProducto(id);

            Assert.Equal(respuesta.Value.Nombre, hardcodeadoDTO.Nombre);
            Assert.Equal(respuesta.Value.Descripcion, hardcodeadoDTO.Descripcion);
            Assert.Equal(respuesta.Value.Precio, hardcodeadoDTO.Precio);
            Assert.Equal(respuesta.Value.SKU, hardcodeadoDTO.SKU);
        }
    }
}