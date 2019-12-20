using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class PCoeficienteValidator : AbstractValidator<PCoeficienteDto> {
    public PCoeficienteValidator() {
      RuleFor(p => p.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(p => p.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(p => p.Mes).NotNull().WithMessage(x => Resources.MesRequired);
    }
  }
}
