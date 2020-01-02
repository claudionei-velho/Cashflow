using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Bacia {
    public int Id { get; private set; }
    public int MunicipioId { get; private set; }
    public string Denominacao { get; private set; }
    public string Descricao { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Municipio Municipio { get; private set; }

    public ICollection<Lote> Lotes { get; private set; }
  }
}
