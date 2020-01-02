using AutoMapper;

using Api.Models;
using Domain.Models;

namespace Api.Mappings {
  public class DboProfile : Profile {
    public DboProfile() {
      CreateMap<BaciaDto, Bacia>().ReverseMap();

      CreateMap<CargoDto, Cargo>().ReverseMap();
      CreateMap<CentroDto, Centro>().ReverseMap();
      CreateMap<ClassLinhaDto, ClassLinha>().ReverseMap();
      CreateMap<ContaDto, Conta>().ReverseMap();

      CreateMap<DepartamentoDto, Departamento>().ReverseMap();
      CreateMap<DominioDto, Dominio>().ReverseMap();

      CreateMap<EEncargoDto, EEncargo>().ReverseMap();
      CreateMap<EmpresaDto, Empresa>().ReverseMap();
      CreateMap<EncargoDto, Encargo>().ReverseMap();
      CreateMap<ESistemaDto, ESistema>().ReverseMap();

      CreateMap<FInstalacaoDto, FInstalacao>().ReverseMap();
      CreateMap<FuncaoDto, Funcao>().ReverseMap();

      CreateMap<LoteDto, Lote>().ReverseMap();
      CreateMap<MunicipioDto, Municipio>().ReverseMap();
      CreateMap<OpLinhaDto, OpLinha>().ReverseMap();
      CreateMap<PaisDto, Pais>().ReverseMap();      
      CreateMap<RhIndiceDto, RhIndice>().ReverseMap();

      CreateMap<SalarioDto, Salario>().ReverseMap();
      CreateMap<SetorDto, Setor>().ReverseMap();
      CreateMap<SistemaDto, Sistema>().ReverseMap();
      CreateMap<SistDespesaDto, SistDespesa>().ReverseMap();
      CreateMap<SistFuncaoDto, SistFuncao>().ReverseMap();

      CreateMap<TurnoDto, Turno>().ReverseMap();
      CreateMap<UfDto, Uf>().ReverseMap();
    }
  }
}
