using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class TCategoriaValidator : AbstractValidator<TCategoriaDto> {
    public TCategoriaValidator() {
      RuleFor(t => t.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(t => t.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.TCategoriaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.TCategoriaDto}");

      RuleFor(t => t.Gratuidade).NotNull().WithMessage(x => Resources.GratuidadeRequired);
    }
  }
}
