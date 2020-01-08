using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class PremissaValidator : AbstractValidator<PremissaDto> {
    public PremissaValidator() {
      RuleFor(p => p.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(p => p.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(p => p.Mes).NotNull().WithMessage(x => Resources.MesRequired);
      RuleFor(p => p.KmProdutivo).NotNull().WithMessage(x => Resources.KmProdutivoRequired);
      RuleFor(p => p.FrotaOperacao).NotNull().WithMessage(x => Resources.FrotaOperacaoRequired);
      RuleFor(p => p.IdadeFrota)
          .NotNull().WithMessage(x => Resources.IdadeFrotaRequired)
          .ScalePrecision(4, 1);

      RuleFor(p => p.Demanda).NotNull().WithMessage(x => Resources.DemandaRequired);
      RuleFor(p => p.Equivalente).NotNull().WithMessage(x => Resources.EquivalenteRequired);
    }
  }
}
