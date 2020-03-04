namespace Api.Models {
  public class NfCombustivelDto {
    public int Id { get; set; }
    public int NotaId { get; set; }
    public int ItemId { get; set; }
    public int ProdutoId { get; set; }
    public decimal? MixGN { get; set; }
    public long? Codif { get; set; }
    public decimal Quantidade { get; set; }
    public string UfConsumo { get; set; }
    public decimal BaseCide { get; set; }
    public decimal AliquotaCide { get; set; }
    public decimal ValorCide {
      get {
        return BaseCide * (AliquotaCide * 0.01m);
      }
    }

    // Navigation Properties
    public NFiscalDto NFiscal { get; private set; }
    public AnpProdutoDto AnpProduto { get; private set; }
  }
}
