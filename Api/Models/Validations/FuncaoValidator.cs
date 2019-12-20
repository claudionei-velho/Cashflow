using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class FuncaoValidator : AbstractValidator<FuncaoDto> {
    public FuncaoValidator() {
      RuleFor(c => c.CargoId).NotNull().WithMessage(x => Resources.CargoIdRequired);
      RuleFor(c => c.DepartamentoId).NotNull().WithMessage(x => Resources.DepartamentoIdRequired);
      RuleFor(c => c.Titulo)
          .NotNull().WithMessage(x => $"{Resources.TituloRequired} {Resources.FuncaoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.TituloRequired} {Resources.FuncaoDto}")
          .MaximumLength(64);
    }
  }
}
