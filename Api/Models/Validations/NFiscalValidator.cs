using FluentValidation;

using Api.Properties;

namespace Api.Models.Validations {
  public class NFiscalValidator : AbstractValidator<NFiscalDto> {
    public NFiscalValidator() {
      RuleFor(n => n.EmpresaId).NotNull().WithMessage(x => Resources.EmpresaIdRequired);
      RuleFor(n => n.FornecedorId)
          .NotNull().WithMessage(x => $"{Resources.FornecedorIdRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.ChaveNfe)
          .NotNull().WithMessage(x => $"{Resources.ChaveNfeRequired} {Resources.NFiscalDto}")
          .NotEmpty().WithMessage(x => $"{Resources.ChaveNfeRequired} {Resources.NFiscalDto}")
          .MaximumLength(64);

      RuleFor(n => n.Natureza)
          .NotNull().WithMessage(x => Resources.NaturezaRequired)
          .NotEmpty().WithMessage(x => Resources.NaturezaRequired)
          .MaximumLength(64);

      RuleFor(n => n.FPagamentoId).NotNull().WithMessage(x => Resources.FPagamentoIdRequired);
      RuleFor(n => n.Modelo)
          .NotNull().WithMessage(x => $"{Resources.ModeloRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Serie)
          .NotNull().WithMessage(x => $"{Resources.SerieRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Numero)
          .NotNull().WithMessage(x => $"{Resources.NumeroRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Emissao)
          .NotNull().WithMessage(x => $"{Resources.EmissaoRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Operacao)
          .NotNull().WithMessage(x => $"{Resources.OperacaoRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Digito)
          .NotNull().WithMessage(x => $"{Resources.DigitoRequired} {Resources.NFiscalDto}");

      RuleFor(n => n.Finalidade)
          .NotNull().WithMessage(x => $"{Resources.FinalidadeRequired} {Resources.NFiscalDto}");
    }
  }
}
