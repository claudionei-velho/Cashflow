using System;

using Domain.Lists;

namespace Api.Models {
  public class NFiscalDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public int FornecedorId { get; set; }
    public string ChaveNfe { get; set; }    
    public string Natureza { get; set; }
    public int FPagamentoId { get; set; }

    public string FPagamentoCap => new FPagamento().Items[FPagamentoId];

    public int Modelo { get; set; }
    public int Serie { get; set; }
    public int Numero { get; set; }
    public DateTime Emissao { get; set; }
    public DateTime? DataHora { get; set; }
    public int Operacao { get; set; }
    public int Digito { get; set; }
    public int Finalidade { get; set; }

    public string FinalidadeCap => new NfProposito().Items[Finalidade];

    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public FornecedorDto Fornecedor { get; private set; }
  }
}
