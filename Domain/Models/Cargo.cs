using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Cargo {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Codigo { get; private set; }
    public string Denominacao { get; private set; }
    public string Titulo { get; private set; }
    public string Cbo { get; private set; }
    public string Descricao { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }

    public ICollection<Departamento> Departamentos { get; private set; }
    public ICollection<ESistema> ESistemas { get; private set; }
    public ICollection<Funcao> Funcoes { get; private set; }
    public ICollection<Setor> Setores { get; private set; }
  }
}
