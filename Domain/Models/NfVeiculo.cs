using Domain.Lists;

namespace Domain.Models {
  public class NfVeiculo {
    public int Id { get; private set; }
    public int NotaId { get; private set; }
    public int ItemId { get; private set; }
    public string ChassiNo { get; private set; }
    public string CorId { get; private set; }
    public string Cor { get; private set; }
    public string MotorCv { get; private set; }
    public string Cilindrada { get; private set; }
    public decimal? Liquido { get; private set; }
    public decimal? Bruto { get; private set; }
    public string Serial { get; private set; }
    public int CombustivelId { get; private set; }
    public string CombustivelCap => new Combustivel().Items[CombustivelId];

    public string MotorNo { get; private set; }
    public decimal? Tracao { get; private set; }
    public decimal? EntreEixos { get; private set; }
    public int? AnoModelo { get; private set; }
    public int? AnoFabrica { get; private set; }
    public string Pintura { get; private set; }
    public int TVeiculoId { get; private set; }
    public string TVeiculoCap => new TVeiculo().Items[TVeiculoId];

    public int EVeiculoId { get; private set; }
    public string EVeiculoCap => new EVeiculo().Items[EVeiculoId];

    public char CondicaoVin { get; private set; }
    public int CondicaoId { get; private set; }
    public string CondicaoCap => new VCondicao().Items[CondicaoId];

    public int? Modelo { get; private set; }
    public int? CorDenatran { get; private set; }
    public string CorDenatranCap => new Cor().Items[CorDenatran ?? 0];

    public int Lotacao { get; private set; }
    public int RestricaoId { get; private set; }
    public string RestricaoCap => new Restricao().Items[RestricaoId];

    // Navigation Properties
    public NFiscal NFiscal { get; private set; }
  }
}
