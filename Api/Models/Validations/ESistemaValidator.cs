using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ESistemaValidator : AbstractValidator<ESistemaDto> {
    public ESistemaValidator() {
      RuleFor(e => e.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(e => e.SistemaId).NotNull().WithMessage(x => Resources.SistemaIdRequired);
      RuleFor(e => e.Codigo).MaximumLength(16);
      RuleFor(e => e.Denominacao).MaximumLength(64);
    }
  }
}
