using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Departamento {
    public int Id { get; private set; }
    public int SetorId { get; private set; }
    public string Codigo { get; private set; }
    public string Denominacao { get; private set; }
    public string Descricao { get; private set; }
    public int? ResponsavelId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Cargo Cargo { get; private set; }
    public Setor Setor { get; private set; }

    public ICollection<Funcao> Funcoes { get; private set; }
  }
}
