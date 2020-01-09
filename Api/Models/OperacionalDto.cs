namespace Api.Models {
  public class OperacionalDto {
    public int EmpresaId { get; set; }
    public int LinhaId { get; set; }
    public int? AtendimentoId { get; set; }
    public string Prefixo { get; set; }
    public string Denominacao { get; set; }
    public string Sentido { get; set; }
    public string DiaOperacao { get; set; }
    public string Funcao { get; set; }
    public decimal? Extensao { get; set; }

    public int? ViagensUtil { get; set; }
    public decimal? PercursoUtil => Extensao * ViagensUtil;

    public int? ViagensSab { get; set; }
    public decimal? PercursoSab => Extensao * ViagensSab;

    public int? ViagensDom { get; set; }
    public decimal? PercursoDom => Extensao * ViagensDom;

    // Navigation Properties
    public AtendimentoDto Atendimento { get; private set; }
    public EmpresaDto Empresa { get; private set; }
    public LinhaDto Linha { get; private set; }
  }
}
