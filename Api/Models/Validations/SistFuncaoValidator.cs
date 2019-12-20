using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class SistFuncaoValidator : AbstractValidator<SistFuncaoDto> {
    public SistFuncaoValidator() {
      RuleFor(f => f.SistemaId).NotNull().WithMessage(x => Resources.SistemaIdRequired);
      RuleFor(f => f.Item).NotNull().WithMessage(x => Resources.ItemRequired);
      RuleFor(f => f.FuncaoId).NotNull().WithMessage(x => Resources.FuncaoIdRequired);
      RuleFor(f => f.Quantidade)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .ScalePrecision(24, 6).GreaterThan(0);

      RuleFor(f => f.SalBase)
          .NotNull().WithMessage(x => Resources.QuantidadeRequired)
          .ScalePrecision(24, 4);
    }
  }
}
