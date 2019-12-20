using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class VEquipamentoValidator : AbstractValidator<VEquipamentoDto> {
    public VEquipamentoValidator() {
      RuleFor(q => q.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(q => q.Denominacao)
          .NotNull().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.VEquipamentoDto}")
          .NotEmpty().WithMessage(x => $"{Resources.DenominacaoRequired} {Resources.VEquipamentoDto}")
          .MaximumLength(64);
    }
  }
}
