using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ContatoValidator : AbstractValidator<ContatoDto> {
    public ContatoValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Nome)
          .NotNull().WithMessage(x => $"{Resources.NomeRequired} {Resources.ContatoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.NomeRequired} {Resources.ContatoDto}")
          .MaximumLength(64);

      RuleFor(c => c.Cargo).MaximumLength(32);
      RuleFor(c => c.Telefone)
          .NotNull().WithMessage(x => $"{Resources.TelefoneRequired} {Resources.ContatoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.TelefoneRequired} {Resources.ContatoDto}")
          .MaximumLength(32);

      RuleFor(c => c.Email).EmailAddress().MaximumLength(256);
    }
  }
}
