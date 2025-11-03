using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_JoseOrtega.Models;

public class PedidosDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [ForeignKey("PedidoId")]
    [InverseProperty("pedidosDetalles")]
    public virtual Pedidos pedidos { get; set; }

    [ForeignKey("ComponenteId")]
    [InverseProperty("pedidosDetalle")]
    public virtual Componente componentes { get; set; }
}
