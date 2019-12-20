using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class EInstalacaoValidator : AbstractValidator<EInstalacaoDto> {
    public EInstalacaoValidator() {
      RuleFor(e => e.InstalacaoId).NotNull().WithMessage(x => Resources.InstalacaoIdRequired);
      RuleFor(e => e.PropositoId).NotNull().WithMessage(x => Resources.PropositoIdRequired);
      RuleFor(e => e.Efluentes).NotNull();
    }
  }
}
