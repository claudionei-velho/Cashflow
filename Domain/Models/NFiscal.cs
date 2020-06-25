using System;
using System.Collections.Generic;

using Domain.Lists;

namespace Domain.Models {
  public class NFiscal {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int FornecedorId { get; private set; }
    public string ChaveNfe { get; private set; }
    public string Natureza { get; private set; }
    public int FPagamentoId { get; private set; }
    public string FPagamentoCap {
      get {
        return FPagamento.Items[FPagamentoId];
      }
    }

    public int Modelo { get; private set; }
    public int Serie { get; private set; }
    public int Numero { get; private set; }
    public DateTime Emissao { get; private set; }
    public DateTime? DataHora { get; private set; }
    public int Operacao { get; private set; }
    public int Digito { get; private set; }
    public int Finalidade { get; private set; }
    public string FinalidadeCap {
      get {
        return NfProposito.Items[Finalidade];
      }
    }

    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }
    public Fornecedor Fornecedor { get; private set; }

    public ICollection<NfCombustivel> NfCOmbustiveis { get; private set; }
    public ICollection<NfEntrega> NfEntregas { get; private set; }
    public ICollection<NfReferencia> NfReferencias { get; private set; }
    public ICollection<NfVeiculo> NfVeiculos { get; private set; }
  }
}
