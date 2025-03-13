using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperBodega.API.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        
        [StringLength(500)]
        public string Observaciones { get; set; }
        
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        
        // Relación con DetalleCompra
        public virtual ICollection<DetalleCompra> DetallesCompra { get; set; }
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
    }
}