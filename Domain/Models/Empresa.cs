using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Empresa {
    public int Id { get; private set; }
    public string Razao { get; private set; }
    public string Fantasia { get; private set; }
    public string Cnpj { get; private set; }
    public string IEstadual { get; private set; }
    public string IMunicipal { get; private set; }
    public string Endereco { get; private set; }
    public string EnderecoNo { get; private set; }
    public string Complemento { get; private set; }
    public int? Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Municipio { get; private set; }
    public int MunicipioId { get; private set; }
    public string UfId { get; private set; }
    public string PaisId { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public TimeSpan? Inicio { get; private set; }
    public TimeSpan? Termino { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Municipio Cidade { get; private set; }
    public Pais Pais { get; private set; }

    public ICollection<Cargo> Cargos { get; private set; }
    public ICollection<Centro> Centros { get; private set; }
    public ICollection<CLinha> CLinhas { get; private set; }
    public ICollection<Conta> Contas { get; private set; }
    public ICollection<CstCarroceria> CstCarrocerias { get; private set; }
    public ICollection<CstChassi> CstChassis { get; private set; }
    public ICollection<CstCombustivel> CstCombustiveis { get; private set; }
    public ICollection<CstPneu> CstPneus { get; private set; }
    public ICollection<ECVeiculo> ECVeiculos { get; private set; }
    public ICollection<EDominio> EDominios { get; private set; }
    public ICollection<EEncargo> EEncargos { get; private set; }
    public ICollection<ESistema> ESistemas { get; private set; }
    public ICollection<Frota> Frotas { get; private set; }
    public ICollection<FrotaEtaria> FrotaEtarias { get; private set; }
    public ICollection<FrotaHora> FrotaHoras { get; private set; }
    public ICollection<FuFuncao> FuFuncoes { get; private set; }
    public ICollection<Instalacao> Instalacoes { get; private set; }
    public ICollection<Linha> Linhas { get; private set; }
    public ICollection<Operacao> Operacoes { get; private set; }
    public ICollection<Operacional> Operacionais { get; private set; }
    public ICollection<PCoeficiente> PCoeficientes { get; private set; }
    public ICollection<PCombustivel> PCombustiveis { get; private set; }
    public ICollection<Premissa> Premissas { get; private set; }
    public ICollection<Producao> Producoes { get; private set; }
    public ICollection<ProducaoMedia> ProducoesMedias { get; private set; }
    public ICollection<PSintese> PSinteses { get; private set; }
    public ICollection<Setor> Setores { get; private set; }
    public ICollection<Tarifa> Tarifas { get; private set; }
    public ICollection<TarifaMod> TarifasMod { get; private set; }
    public ICollection<TCategoria> TCategorias { get; private set; }
    public ICollection<Turno> Turnos { get; private set; }
    public ICollection<VCatalogo> VCatalogos { get; private set; }
    public ICollection<Veiculo> Veiculos { get; private set; }
    public ICollection<VEquipamento> VEquipamentos { get; private set; }
  }
}
