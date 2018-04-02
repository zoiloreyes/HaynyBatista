namespace HaynyBatista.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Imagen")]
    public partial class Imagen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Imagen()
        {
            Articulo = new HashSet<Articulo>();
        }

        [Key]
        public int IdImagen { get; set; }

        public int? IdUsuario { get; set; }

        [Required]
        [StringLength(5)]
        public string Formato { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public string Title { get; set; }

        public DateTime FechaSubida { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articulo> Articulo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
