using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class FornecedorValidator : AbstractValidator<FornecedorDto> {
    public FornecedorValidator() {
      RuleFor(f => f.Cnpj).Matches(@"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)");
      RuleFor(f => f.Cpf).Matches(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");
      RuleFor(f => f.Razao)
          .NotNull().WithMessage(x => $"{Resources.RazaoRequired} {Resources.FornecedorDto}")
          .NotEmpty().WithMessage(x => $"{Resources.RazaoRequired} {Resources.FornecedorDto}")
          .MaximumLength(64);

      RuleFor(f => f.Fantasia)
          .NotNull().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.FornecedorDto}")
          .NotEmpty().WithMessage(x => $"{Resources.FantasiaRequired} {Resources.FornecedorDto}")
          .MaximumLength(64);

      RuleFor(f => f.Endereco)
          .NotNull().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.FornecedorDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.FornecedorDto}")
          .MaximumLength(128);

      RuleFor(f => f.EnderecoNo).MaximumLength(8);
      RuleFor(f => f.Complemento).MaximumLength(64);
      RuleFor(f => f.Bairro).MaximumLength(32);
      RuleFor(f => f.MunicipioId).NotNull().WithMessage(x => Resources.MunicipioRequired);
      RuleFor(f => f.UfId)
          .NotNull().WithMessage(x => Resources.UfIdRequired)
          .NotEmpty().WithMessage(x => Resources.UfIdRequired)
          .Length(2);

      RuleFor(f => f.PaisId)
          .NotNull().WithMessage(x => Resources.PaisIdRequired)
          .NotEmpty().WithMessage(x => Resources.PaisIdRequired)
          .MaximumLength(8);

      RuleFor(f => f.Telefone).MaximumLength(32);
      RuleFor(f => f.IEstadual).MaximumLength(16);
      RuleFor(f => f.IESubstituto).MaximumLength(16);
      RuleFor(f => f.IMunicipal).MaximumLength(16);
      RuleFor(f => f.Cnae).MaximumLength(8);
    }
  }
}
