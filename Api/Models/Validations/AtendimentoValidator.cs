using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class AtendimentoValidator : AbstractValidator<AtendimentoDto> {
    public AtendimentoValidator() {
      RuleFor(a => a.LinhaId).NotNull()
          .WithMessage(x => $"{Resources.LinhaIdRequired} {Resources.AtendimentoDto}");

      RuleFor(a => a.Prefixo)
          .NotNull().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.AtendimentoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.AtendimentoDto}")
          .MaximumLength(16);

      RuleFor(a => a.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.AtendimentoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.AtendimentoDto}")
          .MaximumLength(128);

      RuleFor(a => a.Uteis).NotNull().WithMessage(x => Resources.UteisRequired);
      RuleFor(a => a.Sabados).NotNull().WithMessage(x => Resources.SabadosRequired);
      RuleFor(a => a.Domingos).NotNull().WithMessage(x => Resources.DomingosRequired);
      RuleFor(a => a.ExtensaoAB).ScalePrecision(18, 3);
      RuleFor(a => a.ExtensaoBA).ScalePrecision(18, 3);
    }
  }
}
