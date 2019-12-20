using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Setor {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Codigo { get; private set; }
    public string Denominacao { get; private set; }
    public string Descricao { get; private set; }
    public int? ResponsavelId { get; private set; }
    public int? VinculoId { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Cargo Cargo { get; private set; }
    public Empresa Empresa { get; private set; }
    public Setor Vinculo { get; private set; }

    public ICollection<Departamento> Departamentos { get; private set; }
    public ICollection<Setor> Setores { get; private set; }
  }
}
