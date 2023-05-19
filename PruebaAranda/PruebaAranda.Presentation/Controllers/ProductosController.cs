using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAranda.Application.Dtos;
using PruebaAranda.Application.Services.Interfaces;
using PruebaAranda.Domain.Model;

namespace PruebaAranda.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService productoServices;
        public ProductosController(IProductoService service) {
            productoServices = service;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProductosFilter([FromQuery] PageFilterProduct filter)
        {
                var response = await productoServices.GetFilter(filter);
                return Ok(response);        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await productoServices.Get(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductoUpdate producto)
        {
            var response = await productoServices.Update(id,producto);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await productoServices.Delete(id);
            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> Insert([FromForm] ProductoCreate producto)
        {
            var response = await productoServices.Create(producto);
            return Ok(response);
        }
    }
}
