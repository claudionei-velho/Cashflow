using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class VCatalogoValidator : AbstractValidator<VCatalogoDto> {
    public VCatalogoValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(c => c.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(c => c.ClasseId).NotNull().WithMessage(x => Resources.ClasseIdRequired);
      RuleFor(c => c.FornecedorId).NotNull().WithMessage(x => Resources.FornecedorIdRequired);
    }
  }
}
