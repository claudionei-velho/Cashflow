using System;

using FluentValidation;
using Api.Properties;

namespace Api.Models.Validations {
  public class ChassiValidator : AbstractValidator<ChassiDto> {
    public ChassiValidator() {
      RuleFor(c => c.VeiculoId).NotNull().WithMessage(x => Resources.VeiculoIdRequired);
      RuleFor(c => c.Fabricante).MaximumLength(64);
      RuleFor(c => c.Modelo).MaximumLength(64);
      RuleFor(c => c.ChassiNo)
          .NotNull().WithMessage(x => Resources.ChassiNoRequired)
          .MaximumLength(16);

      RuleFor(c => c.Aquisicao).LessThanOrEqualTo(DateTime.Now);
      RuleFor(c => c.Fornecedor).MaximumLength(64);
      RuleFor(c => c.NotaFiscal).MaximumLength(16);
      RuleFor(c => c.ChaveNfe).MaximumLength(64);
      RuleFor(c => c.Potencia).MaximumLength(32);
      RuleFor(c => c.EixosFrente)
          .NotNull().WithMessage(x => Resources.EixoFrenteRequired)
          .GreaterThan((byte)0);

      RuleFor(c => c.EixosTras)
          .NotNull().WithMessage(x => Resources.EixoTrasRequired)
          .GreaterThan((byte)0);

      RuleFor(c => c.PneusFrente).MaximumLength(16);
      RuleFor(c => c.PneusTras).MaximumLength(16);
    }
  }
}
