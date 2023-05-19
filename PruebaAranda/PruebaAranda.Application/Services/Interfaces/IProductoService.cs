using PruebaAranda.Application.Dtos;
using PruebaAranda.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Application.Services.Interfaces
{
    public interface IProductoService
    {
        Task<ProductoDto> Create(ProductoCreate producto);
        Task<ProductoDto> Update(int id,ProductoUpdate producto);
        Task<ProductoDto?> Get(int id);
        Task<bool> Delete(int id);
        Task<PagedList<Producto>> GetFilter(PageFilterProduct pageFilter);
    }
}
