using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperBodega.API.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [StringLength(200)]
        public string Direccion { get; set; }
        
        [StringLength(20)]
        public string Telefono { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(20)]
        public string NIT { get; set; }
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
        public bool Activo { get; set; } = true;
        
        // Relación con Productos
        public virtual ICollection<Producto> Productos { get; set; }
    }
}