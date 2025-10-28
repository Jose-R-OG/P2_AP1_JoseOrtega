using System.ComponentModel.DataAnnotations;

namespace P2_AP1_JoseOrtega.Models
{
    public class Modelo
    {
        [Key]
        public int ModeloId { get; set; }

        [Required]
        public DateTime Fecha { get; set;} = DateTime.Now;
    }
}
