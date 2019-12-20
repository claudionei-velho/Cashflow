using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class CstChassiValidator : AbstractValidator<CstChassiDto> {
    public CstChassiValidator() {
      RuleFor(p => p.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(p => p.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(p => p.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(p => p.ClasseId).NotNull().WithMessage(x => Resources.ClasseIdRequired);
      RuleFor(p => p.Marca).NotNull().WithMessage(x => Resources.MarcaRequired);
      RuleFor(p => p.Modelo).NotNull().WithMessage(x => Resources.ModeloRequired);
      RuleFor(p => p.Quantidade).NotNull().WithMessage(x => Resources.QuantidadeRequired);
      RuleFor(p => p.Unitario).NotNull().WithMessage(x => Resources.UnitarioRequired);
    }
  }
}
