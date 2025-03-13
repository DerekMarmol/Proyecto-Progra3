using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperBodega.API.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [StringLength(500)]
        public string Descripcion { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        
        [Required]
        public int Existencia { get; set; }
        
        [StringLength(100)]
        public string Categoria { get; set; }
        
        [StringLength(200)]
        public string ImagenUrl { get; set; }
        
        public int? ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
        public bool Activo { get; set; } = true;
    }
}