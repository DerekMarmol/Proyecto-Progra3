using FluentValidation;
using SuperBodega.API.DTOs;

namespace SuperBodega.API.Validators
{
    public class ProductoCreacionDTOValidator : AbstractValidator<ProductoCreacionDTO>
    {
        public ProductoCreacionDTOValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(p => p.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero");

            RuleFor(p => p.Existencia)
                .GreaterThanOrEqualTo(0).WithMessage("La existencia no puede ser negativa");

            RuleFor(p => p.Categoria)
                .MaximumLength(100).WithMessage("La categoría no puede exceder los 100 caracteres");
        }
    }
}