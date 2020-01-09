using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class NfEntregaValidator : AbstractValidator<NfEntregaDto> {
    public NfEntregaValidator() {
      RuleFor(n => n.NotaId).NotNull().WithMessage(x => Resources.NotaIdRequired);
      RuleFor(n => n.Endereco)
          .NotNull().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.NfEntregaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.NfEntregaDto}")
          .MaximumLength(64);

      RuleFor(n => n.MunicipioId)
          .NotNull().WithMessage(x => $"{Resources.MunicipioIdRequired} {Resources.NfEntregaDto}");
    }
  }
}
