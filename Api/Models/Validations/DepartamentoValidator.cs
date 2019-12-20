using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class DepartamentoValidator : AbstractValidator<DepartamentoDto> {
    public DepartamentoValidator() {
      RuleFor(c => c.SetorId).NotNull().WithMessage(x => Resources.SetorIdRequired);
      RuleFor(c => c.Codigo)
          .NotNull().WithMessage(x => $"{Resources.CodigoRequired} {Resources.DepartamentoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CodigoRequired} {Resources.DepartamentoDto}")
          .MaximumLength(8);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.DepartamentoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.DepartamentoDto}")
          .MaximumLength(64);
    }
  }
}
