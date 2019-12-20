using System;

using FluentValidation;
using Api.Properties;

namespace Api.Models.Validations {
  public class VeiculoValidator : AbstractValidator<VeiculoDto> {
    public VeiculoValidator() {
      RuleFor(v => v.EmpresaId).NotNull()
          .WithMessage(x => $"{Resources.EmpresaIdRequired} {Resources.VeiculoDto}");

      RuleFor(v => v.Numero)
          .NotNull().WithMessage(x => $"{Resources.NumeroRequired} {Resources.VeiculoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.NumeroRequired} {Resources.VeiculoDto}")
          .MaximumLength(16);

      RuleFor(v => v.Cor)
          .NotNull().WithMessage(x => $"{Resources.CorRequired} {Resources.VeiculoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CorRequired} {Resources.VeiculoDto}")
          .MaximumLength(32);

      RuleFor(v => v.Classe).NotNull()
          .WithMessage(x => $"{Resources.ClasseIdRequired} {Resources.VeiculoDto}");

      RuleFor(v => v.Placa)
          .NotNull().WithMessage(x => $"{Resources.PlacaRequired} {Resources.VeiculoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.PlacaRequired} {Resources.VeiculoDto}")
          .MaximumLength(16);

      RuleFor(v => v.Renavam)
          .NotNull().WithMessage(x => $"{Resources.RenavamRequired} {Resources.VeiculoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.RenavamRequired} {Resources.VeiculoDto}")
          .MaximumLength(16);

      RuleFor(v => v.Antt).MaximumLength(16);
      RuleFor(v => v.Inicio).LessThanOrEqualTo(DateTime.Now);
      RuleFor(v => v.Inativo).NotNull().WithMessage(x => Resources.InativoRequired);
    }
  }
}
