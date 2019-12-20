using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class SalarioValidator : AbstractValidator<SalarioDto> {
    public SalarioValidator() {
      RuleFor(c => c.FuncaoId).NotNull().WithMessage(x => Resources.FuncaoIdRequired);
      RuleFor(c => c.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(c => c.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(c => c.SalBase).NotNull().WithMessage(x => Resources.SalBaseRequired);
    }
  }
}
