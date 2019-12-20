using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EEncargoValidator : AbstractValidator<EEncargoDto> {
    public EEncargoValidator() {
      RuleFor(e => e.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(e => e.EncargoId).NotNull().WithMessage(x => Resources.EncargoIdRequired);
      RuleFor(e => e.Formula).MaximumLength(1024);
      RuleFor(e => e.Coeficiente)
          .NotNull().WithMessage(x => $"{Resources.CoeficienteRequired} {Resources.EncargoDto}")
          .ScalePrecision(24, 12);
    }
  }
}
