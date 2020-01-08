using System;

using Domain.Lists;

namespace Api.Models {
  public class NFiscalDto {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int FornecedorId { get; private set; }
    public string ChaveNfe { get; private set; }    
    public string Natureza { get; private set; }
    public int FPagamentoId { get; private set; }

    public string FPagamentoCap => new FPagamento().Items[FPagamentoId];

    public int Modelo { get; private set; }
    public int Serie { get; private set; }
    public int Numero { get; private set; }
    public DateTime Emissao { get; private set; }
    public DateTime? DataHora { get; private set; }
    public int Operacao { get; private set; }
    public int Digito { get; private set; }
    public int Finalidade { get; private set; }

    public string FinalidadeCap => new NfProposito().Items[Finalidade];

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public FornecedorDto Fornecedor { get; private set; }
  }
}
