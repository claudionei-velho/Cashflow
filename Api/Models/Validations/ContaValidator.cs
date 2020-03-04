using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ContaValidator : AbstractValidator<ContaDto> {
    public ContaValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Classificacao)
          .NotNull().WithMessage(x => $"{Resources.ClassificacaoRequired} {Resources.ContaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.ClassificacaoRequired} {Resources.ContaDto}")
          .MaximumLength(16);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.ContaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.ContaDto}")
          .MaximumLength(64);
    }
  }
}
