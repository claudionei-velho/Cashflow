using System;

namespace Domain.Models {
  public class Produto {
    public int Id { get; private set; }
    public string Gtin { get; private set; }
    public string Descricao { get; private set; }
    public int NcmId { get; private set; }
    public string UnidadeId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Ncm Ncm { get; private set; }
    public UComercial UComercial { get; private set; }
  }
}
