using PruebaAranda.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Application.Dtos
{
    public class ProductoDto
    {
        public ProductoDto(Producto producto)
        {
            Id = producto.Id;
            Nombre = producto.Nombre;
            Descripcion  = producto.Descripcion;
            Categoria = producto.Categoria;
            Imagen = producto.Imagen;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }


    }
}
