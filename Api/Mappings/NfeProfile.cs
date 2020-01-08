﻿using AutoMapper;

using Api.Models;
using Domain.Models;

namespace Api.Mappings {
  public class NfeProfile : Profile {
    public NfeProfile() {
      CreateMap<AnpProdutoDto, AnpProduto>().ReverseMap();
      CreateMap<FornecedorDto, Fornecedor>().ReverseMap();
      CreateMap<ProdutoDto, Produto>().ReverseMap();

      CreateMap<NcmDto, Ncm>().ReverseMap();
      CreateMap<NFiscalDto, NFiscal>().ReverseMap();
      CreateMap<NfReferenciaDto, NfReferencia>().ReverseMap();

      CreateMap<UComercialDto, UComercial>().ReverseMap();
    }
  }
}
