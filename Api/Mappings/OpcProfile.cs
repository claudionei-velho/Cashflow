using AutoMapper;

using Api.Models;
using Domain.Models;

namespace Api.Mappings {
  public class OpcProfile : Profile {
    public OpcProfile() {
      CreateMap<AtendimentoDto, Atendimento>().ReverseMap();
      
      CreateMap<CarroceriaDto, Carroceria>().ReverseMap();
      CreateMap<ChassiDto, Chassi>().ReverseMap();
      CreateMap<CLinhaDto, CLinha>().ReverseMap();
      CreateMap<CVeiculoDto, CVeiculo>().ReverseMap();

      CreateMap<DepreciacaoDto, Depreciacao>().ReverseMap();

      CreateMap<ECVeiculoDto, ECVeiculo>().ReverseMap();
      CreateMap<EDominioDto, EDominio>().ReverseMap();
      CreateMap<EInstalacaoDto, EInstalacao>().ReverseMap();
      CreateMap<EmbarcadoDto, Embarcado>().ReverseMap();

      CreateMap<FrotaDto, Frota>().ReverseMap();
      CreateMap<FrotaEtariaDto, FrotaEtaria>().ReverseMap();
      CreateMap<FrotaHoraDto, FrotaHora>().ReverseMap();
      CreateMap<FuFuncaoDto, FuFuncao>().ReverseMap();
      CreateMap<FxEtariaDto, FxEtaria>().ReverseMap();

      CreateMap<HorarioDto, Horario>().ReverseMap();
      CreateMap<InstalacaoDto, Instalacao>().ReverseMap();
      CreateMap<LinhaDto, Linha>().ReverseMap();

      CreateMap<OperacaoDto, Operacao>().ReverseMap();
      CreateMap<OperacionalDto, Operacional>().ReverseMap();

      CreateMap<PCoeficienteDto, PCoeficiente>().ReverseMap();
      CreateMap<PCombustivelDto, PCombustivel>().ReverseMap();
      CreateMap<PlanoDto, Plano>().ReverseMap();
      CreateMap<PremissaDto, Premissa>().ReverseMap();
      CreateMap<ProducaoDto, Producao>().ReverseMap();
      CreateMap<ProducaoMediaDto, ProducaoMedia>().ReverseMap();
      CreateMap<PSinteseDto, PSintese>().ReverseMap();

      CreateMap<TarifaDto, Tarifa>().ReverseMap();
      CreateMap<TarifaModDto, TarifaMod>().ReverseMap();
      CreateMap<TCategoriaDto, TCategoria>().ReverseMap();

      CreateMap<VCatalogoDto, VCatalogo>().ReverseMap();
      CreateMap<VeiculoDto, Veiculo>().ReverseMap();
      CreateMap<VEquipamentoDto, VEquipamento>().ReverseMap();
      CreateMap<ViaDto, Via>().ReverseMap();
    }
  }
}
