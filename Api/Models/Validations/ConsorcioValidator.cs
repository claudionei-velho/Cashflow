using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ConsorcioValidator : AbstractValidator<ConsorcioDto> {
    public ConsorcioValidator() {
      RuleFor(c => c.Razao)
          .NotNull().WithMessage(x => $"{Resources.RazaoRequired} {Resources.ConsorcioDto}")
          .NotEmpty().WithMessage(x => $"{Resources.RazaoRequired} {Resources.ConsorcioDto}")
          .MaximumLength(64);

      RuleFor(c => c.Fantasia)
          .NotNull().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.ConsorcioDto}")
          .NotEmpty().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.ConsorcioDto}")
          .MaximumLength(64);

      RuleFor(c => c.Cnpj)
          .NotNull().WithMessage(x => $"{Resources.CnpjRequired} {Resources.ConsorcioDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CnpjRequired} {Resources.ConsorcioDto}")
          .Matches(@"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)");

      RuleFor(c => c.IEstadual).MaximumLength(16);
      RuleFor(c => c.IMunicipal).MaximumLength(16);
      RuleFor(c => c.Endereco)
          .NotNull().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.ConsorcioDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.ConsorcioDto}")
          .MaximumLength(128);

      RuleFor(c => c.EnderecoNo).MaximumLength(8);
      RuleFor(c => c.Complemento).MaximumLength(64);
      RuleFor(c => c.Bairro).MaximumLength(32);
      RuleFor(c => c.MunicipioId).NotNull().WithMessage(x => Resources.MunicipioIdRequired);

      RuleFor(c => c.UfId)
          .NotNull().WithMessage(x => Resources.UfIdRequired).Length(2)
          .NotEmpty().WithMessage(x => Resources.UfIdRequired).Length(2);

      RuleFor(c => c.PaisId)
          .NotNull().WithMessage(x => Resources.PaisIdRequired).MaximumLength(8)
          .NotEmpty().WithMessage(x => Resources.PaisIdRequired).MaximumLength(8);

      RuleFor(c => c.Telefone).MaximumLength(32);
      RuleFor(c => c.Email).EmailAddress().MaximumLength(256);
    }
  }
}
