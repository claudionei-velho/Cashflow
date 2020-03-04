namespace Domain.Models {
  public class NfCombustivel {
    public int Id { get; private set; }
    public int NotaId { get; private set; }
    public int ItemId { get; private set; }
    public int ProdutoId { get; private set; }
    public decimal? MixGN { get; private set; }
    public long? Codif { get; private set; }
    public decimal Quantidade { get; private set; }
    public string UfConsumo { get; private set; }
    public decimal BaseCide { get; private set; }
    public decimal AliquotaCide { get; private set; }
    public decimal ValorCide {
      get {
        return BaseCide * (AliquotaCide * 0.01m);
      }
    }

    // Navigation Properties
    public NFiscal NFiscal { get; private set; }
    public AnpProduto AnpProduto { get; private set; }
  }
}
