using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ProdutoValidator : AbstractValidator<ProdutoDto> {
    public ProdutoValidator() {
      RuleFor(p => p.Gtin)
          .NotNull().WithMessage(x => $"{Resources.GtinRequired} {Resources.ProdutoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.GtinRequired} {Resources.ProdutoDto}")
          .MaximumLength(16);

      RuleFor(p => p.Descricao)
          .NotNull().WithMessage(x => $"{Resources.DescricaoRequired} {Resources.ProdutoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DescricaoRequired} {Resources.ProdutoDto}")
          .MaximumLength(128);

      RuleFor(p => p.NcmId)
          .NotNull().WithMessage(x => $"{Resources.NcmIdRequired} {Resources.ProdutoDto}");

      RuleFor(p => p.UnidadeId)
          .NotNull().WithMessage(x => $"{Resources.UnidadeIdRequired} {Resources.ProdutoDto}");
    }
  }
}
