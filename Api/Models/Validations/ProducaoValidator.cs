using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ProducaoValidator : AbstractValidator<ProducaoDto> {
    public ProducaoValidator() {
      RuleFor(p => p.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(p => p.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(p => p.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(p => p.TarifariaId).NotNull().WithMessage(x => Resources.TarifariaIdRequired);
      RuleFor(p => p.Passageiros).NotNull().WithMessage(x => Resources.PassageirosRequired);
    }
  }
}
