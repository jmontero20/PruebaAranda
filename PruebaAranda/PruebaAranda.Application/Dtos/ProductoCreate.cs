using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PruebaAranda.Application.Dtos
{
    public class ProductoCreate
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Nombre debe tener al maximo 500 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo Categoria es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Categoria debe tener al maximo 500 caracteres.")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "El campo Imagen es obligatorio.")]
        public IFormFile Imagen { get; set; }
    }
}
