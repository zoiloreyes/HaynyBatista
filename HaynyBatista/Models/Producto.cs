using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }
        [Required]
        public int Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        public decimal Precio { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Valor debe ser mayor a {0}")]
        public int Cantidad { get; set; }

        public int IdImagen { get; set; }
        public virtual Imagen Imagen { get; set; }
        
        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}