using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class FrotaEtariasController : ControllerBase {
    private readonly IFrotaEtariaService _frotaEtarias;
    private readonly IMapper _mapper;

    public FrotaEtariasController(IFrotaEtariaService frotaEtarias, IMapper mapper) {
      _frotaEtarias = frotaEtarias;
      _mapper = mapper;
    }

    // GET: FrotaEtarias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_frotaEtarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaEtariaDto>>(
                      await _frotaEtarias.ListAsync(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.EtariaId))));
      }
    }

    // GET: FrotaEtarias/5/5
    [HttpGet("{id}/{fx}")]
    public async Task<IActionResult> Get(int id, int fx) {
      using (_frotaEtarias) {
        FrotaEtaria frotaEtaria = await _frotaEtarias.GetFirstAsync(
                                            f => (f.EmpresaId == id) && (f.EtariaId == fx));
        if (frotaEtaria == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FrotaEtariaDto>(frotaEtaria));
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_frotaEtarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaEtariaDto>>(
                      await _frotaEtarias.ListAsync(
                                f => f.EmpresaId == id,
                                f => f.OrderBy(q => q.EtariaId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_frotaEtarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaEtariaDto>>(
                      await _frotaEtarias.PagedListAsync(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.EtariaId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("Average/{id}")]
    public IActionResult Average(int id) {
      using (_frotaEtarias) {
        return Ok(_frotaEtarias.IdadeFrota(p => p.EmpresaId == id));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_frotaEtarias) {
        return Ok(new KeyValuePair<int, int>(_frotaEtarias.Count(),
                                             _frotaEtarias.Pages(size: k ?? 16)));
      }
    }
  }
}
