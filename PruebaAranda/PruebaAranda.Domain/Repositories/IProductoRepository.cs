using PruebaAranda.Domain.Model;
namespace PruebaAranda.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<Producto> Create(string nombre, string descripcion, string categoria, string imagen);
        Task<Producto> Update(Producto producto,string nombre, string descripcion, string categoria, string imagen);
        Task<Producto?> Get(int id);
        Task<bool> Delete(Producto producto);
        Task<PagedList<Producto>> GetFilters(PageFilterProduct pageFilter);
    }
}
