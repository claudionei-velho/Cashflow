using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class ECVeiculo {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public int ClasseId { get; private set; }
    public int? Minimo { get; private set; }
    public int? Maximo { get; private set; }
    public byte Passageirom2 { get; private set; }
    public byte Pneus { get; private set; }
    public int? Util { get; private set; }
    public decimal? Residual { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public CVeiculo CVeiculo { get; private set; }
    public Empresa Empresa { get; private set; }

    public ICollection<Depreciacao> Depreciacoes { get; private set; }
  }
}
