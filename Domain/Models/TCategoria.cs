using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class TCategoria {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Denominacao { get; private set; }
    public bool Gratuidade { get; private set; }
    public decimal? Rateio { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }

    public ICollection<Producao> Producoes { get; private set; }
    public ICollection<ProducaoMedia> ProducoesMensais { get; private set; }
  }
}
