using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class LinhaValidator : AbstractValidator<LinhaDto> {
    public LinhaValidator() {
      RuleFor(l => l.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(l => l.Prefixo)
          .NotNull().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.LinhaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.LinhaDto}")
          .MaximumLength(16);

      RuleFor(l => l.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.LinhaDto}")
          .NotNull().WithMessage(x => $"{Resources.PrefixoRequired} {Resources.LinhaDto}")
          .MaximumLength(128);

      RuleFor(l => l.Uteis).NotNull().WithMessage(x => Resources.UteisRequired);
      RuleFor(l => l.Sabados).NotNull().WithMessage(x => Resources.SabadosRequired);
      RuleFor(l => l.Domingos).NotNull().WithMessage(x => Resources.DomingosRequired);
      RuleFor(l => l.DominioId).NotNull()
          .WithMessage(x => $"{Resources.DominioIdRequired} {Resources.LinhaDto}");

      RuleFor(l => l.OperacaoId).NotNull()
          .WithMessage(x => $"{Resources.OperacaoIdRequired} {Resources.LinhaDto}");

      RuleFor(l => l.Classificacao).NotNull()
          .WithMessage(x => $"{Resources.ClassificacaoIdRequired} {Resources.LinhaDto}");

      RuleFor(l => l.Captacao).NotNull().WithMessage(x => Resources.CaptacaoRequired);
      RuleFor(l => l.Transporte).NotNull().WithMessage(x => Resources.TransporteRequired);
      RuleFor(l => l.Distribuicao).NotNull().WithMessage(x => Resources.DistribuicaoRequired);
      RuleFor(l => l.ExtensaoAB).ScalePrecision(18, 3);
      RuleFor(l => l.ExtensaoBA).ScalePrecision(18, 3);
    }
  }
}
