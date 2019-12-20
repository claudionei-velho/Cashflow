using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class CstPneuValidator : AbstractValidator<CstPneuDto> {
    public CstPneuValidator() {
      RuleFor(p => p.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(p => p.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(p => p.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(p => p.ClasseId).NotNull().WithMessage(x => Resources.ClasseIdRequired);
      RuleFor(p => p.UnitPneu).NotNull().WithMessage(x => Resources.UnitPneuRequired);
      RuleFor(p => p.UnitRecap).NotNull().WithMessage(x => Resources.UnitRecapRequired);
    }
  }
}
