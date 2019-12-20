using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class CargoValidator : AbstractValidator<CargoDto> {
    public CargoValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Codigo)
          .NotNull().WithMessage(x => $"{Resources.CodigoRequired} {Resources.CargoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CodigoRequired} {Resources.CargoDto}")
          .MaximumLength(8);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.CargoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.CargoDto}")
          .MaximumLength(64);

      RuleFor(c => c.Titulo)
          .NotNull().WithMessage(x => $"{Resources.TituloRequired} {Resources.CargoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.TituloRequired} {Resources.CargoDto}")
          .MaximumLength(64);
    }
  }
}
