using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Centro {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Classificacao { get; private set; }
    public string Denominacao { get; private set; }
    public int? VinculoId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Centro Vinculo { get; private set; }
    public Empresa Empresa { get; private set; }

    public ICollection<Centro> Centros { get; private set; }
    public ICollection<Funcao> Funcoes { get; private set; }
  }
}
