using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_JoseOrtega.Models;

    public class Componente
    {
        [Key]
        public int ComponenteId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Existencia { get; set; }



        [InverseProperty("componentes")]
         public virtual ICollection<PedidosDetalle> pedidosDetalle { get; set; } = new List<PedidosDetalle>();
}
