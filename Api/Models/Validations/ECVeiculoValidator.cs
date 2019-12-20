using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class ECVeiculoValidator : AbstractValidator<ECVeiculoDto> {
    public ECVeiculoValidator() {
      RuleFor(c => c.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(c => c.ClasseId).NotNull()
          .WithMessage(x => $"{Resources.CategoriaIdRequired} {Resources.ECVeiculoDto}");

      RuleFor(c => c.Passageirom2).NotNull()
          .WithMessage(x => $"{Resources.Passageirom2Required} {Resources.ECVeiculoDto}");
    }
  }
}
