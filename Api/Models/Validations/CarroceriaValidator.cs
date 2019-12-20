using System;

using FluentValidation;
using Api.Properties;

namespace Api.Models.Validations {
  public class CarroceriaValidator : AbstractValidator<CarroceriaDto> {
    public CarroceriaValidator() {
      RuleFor(c => c.VeiculoId).NotNull().WithMessage(x => Resources.VeiculoIdRequired);
      RuleFor(c => c.Fabricante).MaximumLength(64);
      RuleFor(c => c.Modelo).MaximumLength(64);
      RuleFor(c => c.Referencia).MaximumLength(32);
      RuleFor(c => c.Aquisicao).LessThanOrEqualTo(DateTime.Now);
      RuleFor(c => c.Fornecedor).MaximumLength(64);
      RuleFor(c => c.NotaFiscal).MaximumLength(16);
      RuleFor(c => c.ChaveNfe).MaximumLength(64);
      RuleFor(c => c.QuemEncarroca).MaximumLength(64);
      RuleFor(c => c.NotaEncarroca).MaximumLength(16);
      RuleFor(c => c.Portas)
          .NotNull().WithMessage(x => $"{Resources.PortasRequired} {Resources.VeiculoDto}")
          .GreaterThan((byte)0);

      RuleFor(c => c.Piso).MaximumLength(32);
      RuleFor(c => c.EscapeV).NotNull().WithMessage(x => Resources.EscapeVRequired);
      RuleFor(c => c.EscapeH).NotNull().WithMessage(x => Resources.EscapeHRequired);
      RuleFor(c => c.PortaIn)
          .NotNull().WithMessage(x => Resources.PortaInRequired)
          .GreaterThan(0);

      RuleFor(c => c.SaidaFrente).NotNull().WithMessage(x => Resources.SaidaFrenteRequired);
      RuleFor(c => c.SaidaMeio).NotNull().WithMessage(x => Resources.SaidaMeioRequired);
      RuleFor(c => c.SaidaTras).NotNull().WithMessage(x => Resources.SaidaTrasRequired);
    }
  }
}
