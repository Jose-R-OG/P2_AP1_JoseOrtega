using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P2_AP1_JoseOrtega.Models;

public class Pedidos
{
    [Key]
    public int PedidoId { get; set; }
    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
    public string ClienteNombre { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [InverseProperty("pedidos")]
    public virtual ICollection<PedidosDetalle> pedidosDetalles { get; set; } = new List<PedidosDetalle>();
}
