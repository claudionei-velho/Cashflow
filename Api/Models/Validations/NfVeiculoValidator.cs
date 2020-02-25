using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class NfVeiculoValidator : AbstractValidator<NfVeiculoDto> {
    public NfVeiculoValidator() {
      RuleFor(v => v.NotaId).NotNull().WithMessage(x => Resources.NotaIdRequired);
      RuleFor(v => v.ItemId).NotNull().WithMessage(x => Resources.ItemIdRequired);
      RuleFor(v => v.ChassiNo)
          .NotNull().WithMessage(x => $"{Resources.ChassiNoRequired} {Resources.NfVeiculoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.ChassiNoRequired} {Resources.NfVeiculoDto}")
          .MaximumLength(32);

      RuleFor(v => v.CorId).MaximumLength(4);
      RuleFor(v => v.Cor).MaximumLength(32);
      RuleFor(v => v.MotorCv).MaximumLength(4);
      RuleFor(v => v.Cilindrada).MaximumLength(4);
      RuleFor(v => v.Liquido)
          .NotNull().WithMessage(x => Resources.LiquidoRequired)
          .ScalePrecision(24, 6);

      RuleFor(v => v.Bruto)
          .NotNull().WithMessage(x => Resources.BrutoRequired)
          .ScalePrecision(24, 6);

      RuleFor(v => v.Serial).MaximumLength(16);
      RuleFor(v => v.CombustivelId).NotNull().WithMessage(x => Resources.CombustivelIdRequired);
      RuleFor(v => v.MotorNo).MaximumLength(32);
      RuleFor(v => v.Tracao)
          .NotNull().WithMessage(x => Resources.TracaoRequired)
          .ScalePrecision(24, 6);

      RuleFor(v => v.EntreEixos).ScalePrecision(9, 3);
      RuleFor(v => v.Pintura).MaximumLength(16);
      RuleFor(v => v.TVeiculoId).NotNull().WithMessage(x => Resources.TVeiculoIdRequired);
      RuleFor(v => v.EVeiculoId).NotNull().WithMessage(x => Resources.EVeiculoIdRequired);
      RuleFor(v => v.CondicaoVin).NotNull().WithMessage(x => Resources.CondicaoVinRequired);
      RuleFor(v => v.CondicaoId).NotNull().WithMessage(x => Resources.CondicaoIdRequired);
      RuleFor(v => v.Modelo).NotNull().WithMessage(x => Resources.ModeloRequired);
      RuleFor(v => v.CorDenatran).NotNull().WithMessage(x => Resources.CorDenatranRequired);
      RuleFor(v => v.Lotacao).NotNull().WithMessage(x => Resources.LotacaoRequired);
      RuleFor(v => v.RestricaoId).NotNull().WithMessage(x => Resources.RestricaoIdRequired);
    }
  }
}
