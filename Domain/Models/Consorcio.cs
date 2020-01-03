using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Consorcio {
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
    public Municipio Municipio { get; private set; }
    public Pais Pais { get; private set; }

    public ICollection<EConsorcio> EConsorcios { get; private set; }
  }
}
