using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}