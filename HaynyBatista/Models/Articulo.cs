namespace HaynyBatista.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Articulo")]
    public partial class Articulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articulo()
        {
            EtiquetaArticulo = new HashSet<EtiquetaArticulo>();
        }

        [Key]
        public int IdArticulo { get; set; }

        public int? IdUsuario { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Contenido { get; set; }

        public DateTime FechaSubida { get; set; }
        public int Visitas { get; set; }

        public int? IdImagen { get; set; }

        public virtual Imagen Imagen { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EtiquetaArticulo> EtiquetaArticulo { get; set; }
    }
}
