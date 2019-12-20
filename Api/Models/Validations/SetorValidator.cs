using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class SetorValidator : AbstractValidator<SetorDto> {
    public SetorValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Codigo)
          .NotNull().WithMessage(x => $"{Resources.CodigoRequired} {Resources.SetorDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CodigoRequired} {Resources.SetorDto}")
          .MaximumLength(8);

      RuleFor(c => c.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.SetorDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.SetorDto}")
          .MaximumLength(64);
    }
  }
}
