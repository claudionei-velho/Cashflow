using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class FrotaHoraValidator : AbstractValidator<FrotaHoraDto> {
    public FrotaHoraValidator() {
      RuleFor(f => f.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(f => f.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(f => f.Mes)
          .NotNull().WithMessage(x => Resources.MesRequired)
          .InclusiveBetween(1, 12);

      RuleFor(f => f.HorarioId)
          .NotNull().WithMessage(x => Resources.HorarioIdRequired)
          .InclusiveBetween(0, 23);

      RuleFor(f => f.Frota)
          .NotNull().WithMessage(x => Resources.FrotaRequired)
          .GreaterThan(0);
    }
  }
}
