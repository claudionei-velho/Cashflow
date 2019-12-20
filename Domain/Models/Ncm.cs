using System.Collections.Generic;

namespace Domain.Models {
  public class Ncm {
    public int Id { get; private set; }
    public string Classificacao { get; private set; }
    public string Descricao { get; private set; }
    public decimal? Ipi { get; private set; }
    public bool Vigente { get; private set; }
    public int? GrupoId { get; private set; }

    // Navigation Properties
    public Ncm Agrupamento { get; private set; }

    public ICollection<Ncm> Agrupamentos { get; private set; }
    public ICollection<Produto> Produtos { get; private set; }
  }
}
