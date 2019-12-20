using AutoMapper;

using Api.Models;
using Domain.Models;

namespace Api.Mappings {
  public class CstProfile : Profile {
    public CstProfile() {
      CreateMap<CstChassiDto, CstChassi>().ReverseMap();
      CreateMap<CstCombustivelDto, CstCombustivel>().ReverseMap();
      CreateMap<CstPneuDto, CstPneu>().ReverseMap();
    }
  }
}
