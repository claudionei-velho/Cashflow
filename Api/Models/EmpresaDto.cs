using System;

namespace Api.Models {
  public class EmpresaDto {
    public int Id { get; set; }
    public string Razao { get; set; }
    public string Fantasia { get; set; }
    public string Cnpj { get; set; }
    public string IEstadual { get; set; }
    public string IMunicipal { get; set; }
    public string Endereco { get; set; }
    public string EnderecoNo { get; set; }
    public string Complemento { get; set; }
    public int? Cep { get; set; }
    public string Bairro { get; set; }
    public string Municipio { get; set; }
    public int? MunicipioId { get; set; }
    public string UfId { get; set; }
    public string PaisId { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public TimeSpan? Inicio { get; set; }
    public TimeSpan? Termino { get; set; }
    public DateTime? Cadastro { get; set; }

    // Navigation Properties
    public MunicipioDto Cidade { get; set; }
    public PaisDto Pais { get; set; }
  }
}
