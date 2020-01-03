using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EConsorcioValidator : AbstractValidator<EConsorcioDto> {
    public EConsorcioValidator() {
      RuleFor(c => c.ConsorcioId).NotNull().WithMessage(x => Resources.ConsorcioIdRequired);
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.Ratio)
          .NotNull().WithMessage(x => $"{Resources.RatioRequired} {Resources.EConsorcioDto}")
          .ScalePrecision(9, 6);

      RuleFor(c => c.Integracao)
          .NotNull().WithMessage(x => $"{Resources.RatioRequired} {Resources.EConsorcioDto}");

      RuleFor(c => c.Documento).MaximumLength(32);
      RuleFor(c => c.Responsavel).MaximumLength(64);
      RuleFor(c => c.CpfResponsavel).MaximumLength(16);
    }
  }
}
