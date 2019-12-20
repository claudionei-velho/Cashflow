using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class SistDespesaValidator : AbstractValidator<SistDespesaDto> {
    public SistDespesaValidator() {
      RuleFor(d => d.SistemaId).NotNull().WithMessage(x => Resources.SistemaIdRequired);
      RuleFor(d => d.Item).NotNull().WithMessage(x => Resources.ItemRequired);
      RuleFor(d => d.ContaId).NotNull().WithMessage(x => Resources.ContaIdRequired);
      RuleFor(d => d.Quantidade)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .ScalePrecision(24, 6).GreaterThan(0);

      RuleFor(d => d.ValorBase)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .ScalePrecision(24, 4);
    }
  }
}
