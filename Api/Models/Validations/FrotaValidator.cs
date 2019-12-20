using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class FrotaValidator : AbstractValidator<FrotaDto> {
    public FrotaValidator() {
      RuleFor(f => f.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(f => f.Ano).NotNull().WithMessage(x => Resources.AnoRequired);
      RuleFor(f => f.Mes)
          .NotNull().WithMessage(x => Resources.MesRequired)
          .InclusiveBetween(1, 12);

      RuleFor(f => f.CategoriaId)
          .NotNull().WithMessage(x => $"{Resources.CategoriaIdRequired} {Resources.FrotaDto}");

      RuleFor(f => f.EtariaId)
          .NotNull().WithMessage(x => $"{Resources.EtariaIdRequired} {Resources.FrotaDto}");

      RuleFor(f => f.ArCondicionado).NotNull().WithMessage(x => Resources.ArCondicionadoRequired);
      RuleFor(f => f.Quantidade).NotNull().WithMessage(x => Resources.QuantidadeRequired);
    }
  }
}
