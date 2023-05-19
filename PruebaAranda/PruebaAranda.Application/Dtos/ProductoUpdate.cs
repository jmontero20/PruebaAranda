using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Application.Dtos
{
    public class ProductoUpdate
    {

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Nombre debe tener al maximo 500 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Categoria es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Categoria debe tener al maximo 500 caracteres.")]
        public string Categoria { get; set; }
        public IFormFile? Imagen { get; set; }
    }
}
