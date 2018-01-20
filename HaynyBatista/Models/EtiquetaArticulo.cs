namespace HaynyBatista.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EtiquetaArticulo")]
    public partial class EtiquetaArticulo
    {
        [Key]
        public int IdEtiquetaArticulo { get; set; }

        public int IdEtiqueta { get; set; }

        public int IdArticulo { get; set; }

        public virtual Articulo Articulo { get; set; }

        public virtual Etiqueta Etiqueta { get; set; }
    }
}
