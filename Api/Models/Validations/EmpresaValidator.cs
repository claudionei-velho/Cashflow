using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EmpresaValidator : AbstractValidator<EmpresaDto> {
    public EmpresaValidator() {
      RuleFor(e => e.Razao)
          .NotNull().WithMessage(x => $"{Resources.RazaoRequired} {Resources.EmpresaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.RazaoRequired} {Resources.EmpresaDto}")
          .MaximumLength(64);

      RuleFor(e => e.Fantasia)
          .NotNull().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.EmpresaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.EmpresaDto}")
          .MaximumLength(64);

      RuleFor(e => e.Cnpj)
          .NotNull().WithMessage(x => $"{Resources.CnpjRequired} {Resources.EmpresaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.CnpjRequired} {Resources.EmpresaDto}")
          .Matches(@"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)");

      RuleFor(e => e.IEstadual).MaximumLength(16);
      RuleFor(e => e.IMunicipal).MaximumLength(16);
      RuleFor(e => e.Endereco)
          .NotNull().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.EmpresaDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.EmpresaDto}")
          .MaximumLength(128);

      RuleFor(e => e.EnderecoNo).MaximumLength(8);
      RuleFor(e => e.Complemento).MaximumLength(64);
      RuleFor(e => e.Bairro).MaximumLength(32);
      RuleFor(e => e.Municipio)
          .NotNull().WithMessage(x => Resources.MunicipioRequired).MaximumLength(32)
          .NotEmpty().WithMessage(x => Resources.MunicipioRequired).MaximumLength(32);

      RuleFor(e => e.UfId)
          .NotNull().WithMessage(x => Resources.UfIdRequired).Length(2)
          .NotEmpty().WithMessage(x => Resources.UfIdRequired).Length(2);

      RuleFor(e => e.PaisId)
          .NotNull().WithMessage(x => Resources.PaisIdRequired).MaximumLength(8)
          .NotEmpty().WithMessage(x => Resources.PaisIdRequired).MaximumLength(8);

      RuleFor(e => e.Telefone).MaximumLength(32);
      RuleFor(e => e.Email).EmailAddress().MaximumLength(256);
    }
  }
}
