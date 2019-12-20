using System;
using System.Collections.Generic;

namespace Domain.Models {
  public class Instalacao {
    public int Id { get; private set; }
    public int EmpresaId { get; private set; }
    public string Prefixo { get; private set; }
    public string Denominacao { get; private set; }
    public string Endereco { get; private set; }
    public string EnderecoNo { get; private set; }
    public string Complemento { get; private set; }
    public int? Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Municipio { get; private set; }
    public string UfId { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public DateTime? Cadastro { get; private set; }

    // Navigation Properties
    public Empresa Empresa { get; private set; }

    public ICollection<EInstalacao> EInstalacoes { get; private set; }
  }
}
