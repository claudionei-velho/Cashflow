using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class InstalacaoValidator : AbstractValidator<InstalacaoDto> {
    public InstalacaoValidator() {
      RuleFor(i => i.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(i => i.Prefixo).MaximumLength(16);
      RuleFor(i => i.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.InstalacaoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.InstalacaoDto}")
          .MaximumLength(64);

      RuleFor(i => i.Endereco)
          .NotNull().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.InstalacaoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.EnderecoRequired} {Resources.InstalacaoDto}")
          .MaximumLength(128);

      RuleFor(i => i.EnderecoNo).MaximumLength(8);
      RuleFor(i => i.Complemento).MaximumLength(64);
      RuleFor(i => i.Bairro).MaximumLength(32);

      RuleFor(i => i.Municipio)
          .NotNull().WithMessage(x => $"{Resources.MunicipioRequired} {Resources.InstalacaoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.MunicipioRequired} {Resources.InstalacaoDto}")
          .MaximumLength(32);

      RuleFor(i => i.UfId)
          .NotNull().WithMessage(x => $"{Resources.UfIdRequired} {Resources.InstalacaoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.UfIdRequired} {Resources.InstalacaoDto}")
          .Length(2);

      RuleFor(i => i.Telefone).MaximumLength(32);
      RuleFor(i => i.Email).MaximumLength(256);
      RuleFor(i => i.Latitude).ScalePrecision(24, 12);
      RuleFor(i => i.Longitude).ScalePrecision(24, 12);
    }
  }
}
