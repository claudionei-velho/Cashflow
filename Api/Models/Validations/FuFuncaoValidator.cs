using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class FuFuncaoValidator : AbstractValidator<FuFuncaoDto> {
    public FuFuncaoValidator() {
      RuleFor(f => f.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(f => f.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(f => f.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(f => f.FuncaoId).NotNull().WithMessage(x => Resources.FuncaoIdRequired);
      RuleFor(f => f.Titular).NotNull().WithMessage(x => Resources.TitularRequired);
    }
  }
}
