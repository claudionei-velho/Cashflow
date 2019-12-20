using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class TarifaValidator : AbstractValidator<TarifaDto> {
    public TarifaValidator() {
      RuleFor(t => t.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(t => t.Referencia).NotNull().WithMessage(x => $"{Resources.ReferenciaRequired} {Resources.TarifaDto}");
      RuleFor(t => t.Valor)
          .NotNull().WithMessage(x => $"{Resources.ValorRequired} {Resources.TarifaDto}")
          .ScalePrecision(24, 6);

      RuleFor(t => t.Decreto).MaximumLength(128);
    }
  }
}
