using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperBodega.API.Models
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int Cantidad { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
        
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
        
        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }
    }
}