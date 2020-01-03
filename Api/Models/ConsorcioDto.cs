using System;

namespace Api.Models {
  public class ConsorcioDto {
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
    public int MunicipioId { get; private set; }
    public string UfId { get; private set; }
    public string PaisId { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public MunicipioDto Municipio { get; private set; }
    public PaisDto Pais { get; private set; }
  }
}
