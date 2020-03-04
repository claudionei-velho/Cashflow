using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class EInstalacao {
    public int Id { get; private set; }
    public int InstalacaoId { get; private set; }
    public int PropositoId { get; private set; }
    public decimal? AreaCoberta { get; private set; }
    public decimal? AreaTotal { get; private set; }
    public int? Empregados { get; private set; }
    public int? ContaId { get; private set; }
    public TimeSpan? Inicio { get; private set; }
    public TimeSpan? Termino { get; private set; }
    public bool Efluentes { get; private set; }
    public DateTime Cadastro { get; private set; }

    // Navigation Properties
    public Conta Conta { get; private set; }
    public FInstalacao FInstalacao { get; private set; }
    public Instalacao Instalacao { get; private set; }

    public ICollection<AAbastece> Abastecimentos { get; private set; }
    public ICollection<AAdmin> Administracoes { get; private set; }
    public ICollection<AAlmox> Almoxarifados { get; private set; }
    public ICollection<AEstaciona> Estacionamentos { get; private set; }
    public ICollection<AFunilaria> Funilarias { get; private set; }
    public ICollection<AGaragem> Garagens { get; private set; }
    public ICollection<AInspecao> Inspecoes { get; private set; }
    public ICollection<ALavacao> Lavacoes { get; private set; }
    public ICollection<ALubrifica> Lubrificacoes { get; private set; }
    public ICollection<AMantem> Manutencoes { get; private set; }
    public ICollection<ATrafego> Trafegos { get; private set; }
  }
}
