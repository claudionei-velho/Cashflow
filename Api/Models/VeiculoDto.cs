using System;

using Domain.Lists;

namespace Api.Models {
  public class VeiculoDto {
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Numero { get; set; }
    public string Cor { get; set; }
    public int Classe { get; set; }
    public int? Categoria { get; set; }

    public string CategoriaCap => new Categoria().Items[Categoria ?? 0];

    public string Placa { get; set; }
    public string Renavam { get; set; }
    public string Antt { get; set; }
    public DateTime? Inicio { get; set; }
    public bool Inativo { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public EmpresaDto Empresa { get; private set; }
    public CVeiculoDto CVeiculo { get; private set; }
  }
}
