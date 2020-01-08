using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class NfReferenciaValidator : AbstractValidator<NfReferenciaDto> {
    public NfReferenciaValidator() {
      RuleFor(n => n.NotaId).NotNull().WithMessage(x => Resources.NotaIdRequired);
      RuleFor(n => n.ChaveNfeRef)
          .NotNull().WithMessage(x => $"{Resources.ChaveNfeRequired} {Resources.NfReferenciaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.ChaveNfeRequired} {Resources.NfReferenciaDto}")
          .MaximumLength(64);

      RuleFor(n => n.AnoMes)
          .NotNull().WithMessage(x => $"{Resources.AnoMesRequired} {Resources.NfReferenciaDto}");

      RuleFor(n => n.Emitente)
          .NotNull().WithMessage(x => $"{Resources.EmitenteRequired} {Resources.NfReferenciaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EmitenteRequired} {Resources.NfReferenciaDto}")
          .MaximumLength(64);

      RuleFor(n => n.Modelo)
          .NotNull().WithMessage(x => $"{Resources.ModeloRequired} {Resources.NfReferenciaDto}");

      RuleFor(n => n.Serie)
          .NotNull().WithMessage(x => $"{Resources.SerieRequired} {Resources.NfReferenciaDto}");

      RuleFor(n => n.Numero)
          .NotNull().WithMessage(x => $"{Resources.NumeroRequired} {Resources.NfReferenciaDto}");
    }
  }
}
