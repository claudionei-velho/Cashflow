namespace Domain.Models {
  public class Operacional {
    public int EmpresaId { get; private set; }
    public int LinhaId { get; private set; }
    public int? AtendimentoId { get; private set; }
    public string Prefixo { get; private set; }
    public string Denominacao { get; private set; }
    public string Sentido { get; private set; }
    public string DiaOperacao { get; private set; }
    public string Funcao { get; private set; }
    public decimal? Extensao { get; private set; }

    public int? ViagensUtil { get; private set; }
    public decimal? PercursoUtil => Extensao * ViagensUtil;

    public int? ViagensSab { get; private set; }
    public decimal? PercursoSab => Extensao * ViagensSab;

    public int? ViagensDom { get; private set; }
    public decimal? PercursoDom => Extensao * ViagensDom;

    // Navigation Properties
    public Atendimento Atendimento { get; private set; }
    public Empresa Empresa { get; private set; }
    public Linha Linha { get; private set; }    
  }
}
