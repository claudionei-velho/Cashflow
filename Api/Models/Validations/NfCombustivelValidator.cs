using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class NfCombustivelValidator : AbstractValidator<NfCombustivelDto> {
    public NfCombustivelValidator() {
      RuleFor(v => v.NotaId).NotNull().WithMessage(x => Resources.NotaIdRequired);
      RuleFor(v => v.ItemId).NotNull().WithMessage(x => Resources.ItemIdRequired);
      RuleFor(v => v.ProdutoId)
          .NotNull().WithMessage(x => $"{Resources.ProdutoIdRequired} {Resources.NfCombustivelDto}");

      RuleFor(v => v.Quantidade)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .ScalePrecision(24, 6);

      RuleFor(v => v.UfConsumo)
          .NotNull().WithMessage(x => $"{Resources.UfConsumoRequired} {Resources.NfCombustivelDto}")
          .NotEmpty().WithMessage(x => $"{Resources.UfConsumoRequired} {Resources.NfCombustivelDto}")
          .Length(2);

      RuleFor(v => v.BaseCide)
          .NotNull().WithMessage(x => Resources.BaseCideRequired)
          .ScalePrecision(24, 6);

      RuleFor(v => v.AliquotaCide)
          .NotNull().WithMessage(x => Resources.AliquotaCideRequired)
          .ScalePrecision(12, 4);
    }
  }
}
