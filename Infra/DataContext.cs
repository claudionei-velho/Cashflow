using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

using Domain.Models;

namespace Infra {
  public class DataContext : DbContext {
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<AAbastece> AAbastecimentos { get; private set; }
    public DbSet<AAdmin> AAdministracoes { get; private set; }
    public DbSet<AAlmox> AAlmoxarifados { get; private set; }
    public DbSet<AEstaciona> AEstacionamentos { get; private set; }
    public DbSet<AFunilaria> AFunilarias { get; private set; }
    public DbSet<AGaragem> AGaragens { get; private set; }
    public DbSet<AInspecao> AInspecoes { get; private set; }
    public DbSet<ALavacao> ALavacoes { get; private set; }
    public DbSet<ALubrifica> ALubrifacoes { get; private set; }
    public DbSet<AMantem> AManutencoes { get; private set; }
    public DbSet<AnpProduto> AnpProdutos { get; private set; }
    public DbSet<Atendimento> Atendimentos { get; private set; }
    public DbSet<ATrafego> ATrafegos { get; private set; }
    public DbSet<Bacia> Bacias { get; private set; }
    public DbSet<Cargo> Cargos { get; private set; }
    public DbSet<Carroceria> Carrocerias { get; private set; }
    public DbSet<Centro> Centros { get; private set; }
    public DbSet<Chassi> Chassis { get; private set; }
    public DbSet<ClassLinha> ClassLinhas { get; private set; }
    public DbSet<CLinha> CLinhas { get; private set; }
    public DbSet<Conta> Contas { get; private set; }
    public DbSet<CstCarroceria> CstCarrocerias { get; private set; }
    public DbSet<CstChassi> CstChassis { get; private set; }
    public DbSet<CstCombustivel> CstCombustiveis { get; private set; }
    public DbSet<CstPneu> CstPneus { get; private set; }
    public DbSet<CVeiculo> CVeiculos { get; private set; }
    public DbSet<Departamento> Departamentos { get; private set; }
    public DbSet<Depreciacao> Depreciacoes { get; private set; }
    public DbSet<Dominio> Dominios { get; private set; }
    public DbSet<ECVeiculo> ECVeiculos { get; private set; }
    public DbSet<EDominio> EDominios { get; private set; }
    public DbSet<EEncargo> EEncargos { get; private set; }
    public DbSet<EInstalacao> EInstalacoes { get; private set; }
    public DbSet<Embarcado> Embarcados { get; private set; }
    public DbSet<Empresa> Empresas { get; private set; }
    public DbSet<Encargo> Encargos { get; private set; }
    public DbSet<ESistema> ESistemas { get; private set; }
    public DbSet<FInstalacao> FInstalacoes { get; private set; }
    public DbSet<Fornecedor> Fornecedores { get; private set; }
    public DbSet<Frota> Frotas { get; private set; }
    public DbSet<FrotaEtaria> FrotaEtarias { get; private set; }
    public DbSet<FrotaHora> FrotaHoras { get; private set; }
    public DbSet<FuFuncao> FuFuncoes { get; private set; }
    public DbSet<Funcao> Funcoes { get; private set; }
    public DbSet<FxEtaria> FxEtarias { get; private set; }
    public DbSet<Horario> Horarios { get; private set; }
    public DbSet<Instalacao> Instalacoes { get; private set; }
    public DbSet<Linha> Linhas { get; private set; }
    public DbSet<Lote> Lotes { get; private set; }
    public DbSet<Municipio> Municipios { get; private set; }
    public DbSet<Ncm> Ncms { get; private set; }
    public DbSet<Operacao> Operacoes { get; private set; }
    public DbSet<Operacional> Operacionais { get; private set; }
    public DbSet<OpLinha> OperLinhas { get; private set; }
    public DbSet<Pais> Paises { get; private set; }
    public DbSet<PCombustivel> PCombustiveis { get; private set; }
    public DbSet<PCoeficiente> PCoeficientes { get; private set; }
    public DbSet<Plano> Planos { get; private set; }
    public DbSet<Premissa> Premissas { get; private set; }
    public DbSet<Producao> Producoes { get; private set; }
    public DbSet<ProducaoMedia> ProducoesMedias { get; private set; }
    public DbSet<Produto> Produtos { get; private set; }
    public DbSet<PSintese> PSinteses { get; private set; }
    public DbSet<RhIndice> RhIndices { get; private set; }
    public DbSet<Salario> Salarios { get; private set; }
    public DbSet<Setor> Setores { get; private set; }
    public DbSet<Sistema> Sistemas { get; private set; }
    public DbSet<SistDespesa> SistDespesas { get; private set; }
    public DbSet<SistFuncao> SistFuncoes { get; private set; }    
    public DbSet<Tarifa> Tarifas { get; private set; }
    public DbSet<TarifaMod> TarifasMod { get; private set; }
    public DbSet<TCategoria> TCategorias { get; private set; }
    public DbSet<Turno> Turnos { get; private set; }
    public DbSet<UComercial> UComerciais { get; private set; }
    public DbSet<Uf> Ufs { get; private set; }
    public DbSet<VCatalogo> VCatalogos { get; private set; }
    public DbSet<Veiculo> Veiculos { get; private set; }
    public DbSet<VEquipamento> VEquipamentos { get; private set; }
    public DbSet<Via> Vias { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

      foreach (IMutableProperty property in 
                 modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))) {
        property.SetColumnType("decimal(24, 6)");
      }

      #region NotMapped Properties
      // Atendimentos
      modelBuilder.Entity<Atendimento>().Ignore(p => p.Descricao);
      modelBuilder.Entity<Atendimento>().Ignore(p => p.DiasOperacao);
      modelBuilder.Entity<Atendimento>().Ignore(p => p.Extensao);

      // Chassis
      modelBuilder.Entity<Chassi>().Ignore(p => p.MotorCap);
      modelBuilder.Entity<Chassi>().Ignore(p => p.PosMotorCap);
      modelBuilder.Entity<Chassi>().Ignore(p => p.TransmiteCap);
      modelBuilder.Entity<Chassi>().Ignore(p => p.DirecaoCap);

      // Carrocerias
      modelBuilder.Entity<Carroceria>().Ignore(p => p.CatracaCap);
      modelBuilder.Entity<Carroceria>().Ignore(p => p.PortaInCap);

      // Custo das Carrocerias
      modelBuilder.Entity<CstCarroceria>().Ignore(p => p.ArCondicionado);
      modelBuilder.Entity<CstCarroceria>().Ignore(p => p.Elevatoria);
      modelBuilder.Entity<CstCarroceria>().Ignore(p => p.Ponderado);

      // Depreciacoes
      modelBuilder.Entity<Depreciacao>().Ignore(p => p.Coeficiente);
      modelBuilder.Entity<Depreciacao>().Ignore(p => p.Acumulado);

      // ESistemas
      modelBuilder.Entity<ESistema>().Ignore(p => p.Coeficiente);
      modelBuilder.Entity<ESistema>().Ignore(p => p.CoeficienteAno);

      // Fornecedores
      modelBuilder.Entity<Fornecedor>().Ignore(p => p.TributarioCap);

      // F.U. das Funcoes
      modelBuilder.Entity<FuFuncao>().Ignore(p => p.Soma);

      // dbo.Funcoes
      modelBuilder.Entity<Funcao>().Ignore(p => p.Vigente);

      // Linhas
      modelBuilder.Entity<Linha>().Ignore(p => p.Descricao);
      modelBuilder.Entity<Linha>().Ignore(p => p.DiasOperacao);
      modelBuilder.Entity<Linha>().Ignore(p => p.Funcao);
      modelBuilder.Entity<Linha>().Ignore(p => p.Extensao);

      // Plano Operacional
      modelBuilder.Entity<Operacional>().Ignore(p => p.PercursoUtil);
      modelBuilder.Entity<Operacional>().Ignore(p => p.PercursoSab);
      modelBuilder.Entity<Operacional>().Ignore(p => p.PercursoDom);

      // Premissas Operacionais
      modelBuilder.Entity<Premissa>().Ignore(p => p.KmProgramado);
      modelBuilder.Entity<Premissa>().Ignore(p => p.FrotaTotal);
      modelBuilder.Entity<Premissa>().Ignore(p => p.PMm);
      modelBuilder.Entity<Premissa>().Ignore(p => p.Equivalencia);

      // Producao Media
      modelBuilder.Entity<ProducaoMedia>().Ignore(p => p.Equivalencia);

      // Sintese Plano Operacional
      modelBuilder.Entity<PSintese>().Ignore(p => p.DiaIdCap);
      modelBuilder.Entity<PSintese>().Ignore(p => p.ViagensSemana);
      modelBuilder.Entity<PSintese>().Ignore(p => p.PercursoSemana);
      modelBuilder.Entity<PSintese>().Ignore(p => p.ViagensMes);
      modelBuilder.Entity<PSintese>().Ignore(p => p.PercursoMes);
      modelBuilder.Entity<PSintese>().Ignore(p => p.ViagensAno);
      modelBuilder.Entity<PSintese>().Ignore(p => p.PercursoAno);

      // Veiculos
      modelBuilder.Entity<Veiculo>().Ignore(p => p.CategoriaCap);

      // VEquipamentos
      modelBuilder.Entity<VEquipamento>().Ignore(p => p.Coeficiente);
      #endregion
    }

    public override int SaveChanges() {
      foreach (EntityEntry entry in ChangeTracker.Entries().Where(entry => 
                                        entry.Entity.GetType().GetProperty("Cadastro") != null)) {
        if (entry.State == EntityState.Added) {
          entry.Property("Cadastro").CurrentValue = DateTime.Now;
        }
        if (entry.State == EntityState.Modified) {
          entry.Property("Cadastro").IsModified = false;
        }
      }
      return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
      foreach (EntityEntry entry in ChangeTracker.Entries().Where(entry => 
                                        entry.Entity.GetType().GetProperty("Cadastro") != null)) {
        if (entry.State == EntityState.Added) {
          entry.Property("Cadastro").CurrentValue = DateTime.Now;
        }
        if (entry.State == EntityState.Modified) {
          entry.Property("Cadastro").IsModified = false;
        }
      }
      return await base.SaveChangesAsync(cancellationToken);
    }
  }
}
