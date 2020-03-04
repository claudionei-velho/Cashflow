using System;

using Domain.Lists;

namespace Domain.Models {
  public class Chassi {
    public int VeiculoId { get; private set; }
    public string Fabricante { get; private set; }
    public string Modelo { get; private set; }
    public string ChassiNo { get; private set; }
    public int? Ano { get; private set; }
    public DateTime? Aquisicao { get; private set; }
    public string Fornecedor { get; private set; }
    public string NotaFiscal { get; private set; }
    public decimal? Valor { get; private set; }
    public string ChaveNfe { get; private set; }
    public int? MotorId { get; private set; }
    public string MotorCap {
      get {
        return new Motor().Items[MotorId ?? 0];
      }
    }

    public string Potencia { get; private set; }
    public int? PosMotor { get; private set; }
    public string PosMotorCap {
      get {
        return new Posicao().Items[PosMotor ?? 0];
      }
    }

    public byte EixosFrente { get; private set; }
    public byte EixosTras { get; private set; }
    public string PneusFrente { get; private set; }
    public string PneusTras { get; private set; }
    public int? TransmiteId { get; private set; }
    public string TransmiteCap {
      get {
        return new Transmissao().Items[TransmiteId ?? 1];
      }
    }

    public int? DirecaoId { get; private set; }
    public string DirecaoCap {
      get {
        return new Direcao().Items[DirecaoId ?? 1];
      }
    }

    // Navigation Properties
    public Veiculo Veiculo { get; private set; }
  }
}
