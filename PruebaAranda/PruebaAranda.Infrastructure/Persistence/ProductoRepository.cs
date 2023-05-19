using Microsoft.EntityFrameworkCore;
using PruebaAranda.Domain.Model;
using PruebaAranda.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Infrastructure.Persistence
{
    public class ProductoRepository : IProductoRepository
    {
        private ProductoDbContext productoDb;
        public ProductoRepository(ProductoDbContext dbContext)
        {
            productoDb = dbContext;
        }
        public async Task<Producto> Create(string nombre, string descripcion, string categoria, string imagen)
        {
            Producto producto = new();
            producto.Nombre = nombre;
            producto.Descripcion = descripcion;
            producto.Categoria = categoria;
            producto.Imagen = imagen;
            await productoDb.Productos.AddAsync(producto);
            await productoDb.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> Delete(Producto producto)
        {
            productoDb.Productos.Remove(producto);
            await productoDb.SaveChangesAsync();
            return true;
        }

        public async Task<Producto?> Get(int id)
        {
            return await productoDb.Productos.Where(producto => producto.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Producto>> GetFilters(PageFilterProduct pageFilter)
        {
            var query = productoDb.Productos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pageFilter.Nombre))
            {
                query = query.Where(p => p.Nombre.Contains(pageFilter.Nombre));
            }
            if (!string.IsNullOrWhiteSpace(pageFilter.Descripcion))
            {
                query = query.Where(p => p.Descripcion.Contains(pageFilter.Descripcion));
            }
            if (!string.IsNullOrWhiteSpace(pageFilter.Categoria))
            {
                query = query.Where(p => p.Nombre.Contains(pageFilter.Categoria));
            }

            if (pageFilter.AscNombre is not null)
            {
                if ((bool)pageFilter.AscNombre)
                {
                    query = query.OrderBy(p => p.Nombre);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Nombre);
                }
            }

            if (pageFilter.AscCategoria is not null)
            {
                if ((bool)pageFilter.AscCategoria)
                {
                    query = query.OrderBy(p => p.Categoria);
                }
                else
                {
                    query = query.OrderByDescending(p => p.Categoria);
                }
            }
            int count = query.Count();
            var items = await query.Skip((pageFilter.PageNumber - 1) * pageFilter.PageSize).Take(pageFilter.PageSize).ToListAsync();
            return new PagedList<Producto>(items, count, pageFilter.PageNumber, pageFilter.PageSize);
        }

        public async Task<Producto> Update(Producto producto, string nombre, string descripcion, string categoria, string imagen)
        {
            producto.Nombre = nombre;
            producto.Categoria = categoria;
            producto.Descripcion = descripcion;
            producto.Imagen = imagen;
            await productoDb.SaveChangesAsync();
            return producto;
        }
    }
}
