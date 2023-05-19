using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Domain.Model
{
    public class PageFilterProduct : PageFilter
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }

        public bool? AscNombre { get; set; }
        public bool? AscCategoria { get; set; }

    }
}
