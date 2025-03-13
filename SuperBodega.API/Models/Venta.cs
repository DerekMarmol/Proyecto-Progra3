using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperBodega.API.Models
{
    public enum EstadoVenta
    {
        Pendiente,
        Procesando,
        Despachado,
        Entregado,
        Cancelado
    }
    
    public class Venta
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        
        [Required]
        public EstadoVenta Estado { get; set; } = EstadoVenta.Pendiente;
        
        [StringLength(500)]
        public string Observaciones { get; set; }
        
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        
        // Relación con DetalleVenta
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
    }
}