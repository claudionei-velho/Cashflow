using System;

using Domain.Lists;

namespace Api.Models {
  public class ChassiDto {
    public int VeiculoId { get; set; }
    public string Fabricante { get; set; }
    public string Modelo { get; set; }
    public string ChassiNo { get; set; }
    public int? Ano { get; set; }
    public DateTime? Aquisicao { get; set; }
    public string Fornecedor { get; set; }
    public string NotaFiscal { get; set; }
    public decimal? Valor { get; set; }
    public string ChaveNfe { get; set; }
    public int? MotorId { get; set; }

    public string MotorCap => new Motor().Items[MotorId ?? 0];

    public string Potencia { get; set; }
    public int? PosMotor { get; set; }

    public string PosMotorCap => new Posicao().Items[PosMotor ?? 0];

    public byte EixosFrente { get; set; }
    public byte EixosTras { get; set; }
    public string PneusFrente { get; set; }
    public string PneusTras { get; set; }
    public int? TransmiteId { get; set; }

    public string TransmiteCap => new Transmissao().Items[TransmiteId ?? 1];

    public int? DirecaoId { get; set; }

    public string DirecaoCap => new Direcao().Items[DirecaoId ?? 1];

    // Navigation Properties
    public VeiculoDto Veiculo { get; private set; }
  }
}
