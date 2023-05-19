using PruebaAranda.Application.Dtos;
using PruebaAranda.Application.Services.Interfaces;
using PruebaAranda.Domain.Model;
using PruebaAranda.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Application.Services
{
    public class ProductoService : IProductoService
    {
        private IProductoRepository productoRepository;
        public ProductoService(IProductoRepository _productoRepository)
        {
            productoRepository = _productoRepository;

        }
        public async Task<ProductoDto> Create(ProductoCreate producto)
        {
            string filePath = "";
            if (producto.Imagen.Length > 0)
            {
                    filePath = Path.Combine("Imagenes",
                    producto.Imagen.FileName.Trim().Replace(" ", ""));

                using (var stream = System.IO.File.Create(filePath))
                {
                    await producto.Imagen.CopyToAsync(stream);
                }
            }

            return new ProductoDto( await productoRepository.Create(producto.Nombre, producto.Descripcion, producto.Categoria, filePath));
        }

        public async Task<bool> Delete(int id)
        {
            Producto? producto = await productoRepository.Get(id);
            if (producto is not null)
            {
                return await productoRepository.Delete(producto);
            }

            return false;
        }

        public async Task<ProductoDto?> Get(int id)
        {
            Producto? producto = await productoRepository.Get(id);
            if (producto is not null)
            {
                return new ProductoDto (producto);
            }

            return null;
        }

        public async Task<PagedList<Producto>> GetFilter(PageFilterProduct pageFilter)
        {
            return await productoRepository.GetFilters(pageFilter);
        }

        public async Task<ProductoDto?> Update(int id, ProductoUpdate producto)
        {
            Producto? productoUpdate = await productoRepository.Get(id);
            if (productoUpdate is not null)
            {
                string filePath = productoUpdate.Imagen;
                if (producto.Imagen?.Length > 0)
                {
                    filePath = Path.Combine("Imagenes",
                    producto.Imagen.FileName.Trim().Replace(" ", ""));

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await producto.Imagen.CopyToAsync(stream);
                    }
                }
                return new ProductoDto(await productoRepository.Update(productoUpdate,producto.Nombre,producto.Descripcion,producto.Categoria,filePath));
            }

            return null;
        }
    }
}
