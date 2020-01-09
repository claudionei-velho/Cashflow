using System;

namespace Api.Models {
  public class ProdutoDto {
    public int Id { get; set; }
    public string Gtin { get; set; }
    public string Descricao { get; set; }
    public int NcmId { get; set; }
    public string UnidadeId { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public NcmDto Ncm { get; private set; }
    public UComercialDto UComercial { get; private set; }
  }
}
