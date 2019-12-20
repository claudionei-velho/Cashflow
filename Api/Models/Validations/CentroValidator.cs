using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class CentroValidator : AbstractValidator<CentroDto> {
    public CentroValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Classificacao)
          .NotNull().WithMessage(x => $"{Resources.ClassificacaoRequired} {Resources.CentroDto}")
          .NotEmpty().WithMessage(x => $"{Resources.ClassificacaoRequired} {Resources.CentroDto}")
          .MaximumLength(16);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.CentroDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.CentroDto}")
          .MaximumLength(64);
    }
  }
}
