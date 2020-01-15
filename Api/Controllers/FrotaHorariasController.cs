using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class FrotaHorariasController : ControllerBase {
    private readonly IFrotaHorariaService _frotaHorarias;
    private readonly IMapper _mapper;

    public FrotaHorariasController(IFrotaHorariaService frotaHorarias, IMapper mapper) {
      _frotaHorarias = frotaHorarias;
      _mapper = mapper;
    }

    // GET: FrotaHorarias
    [HttpGet]
    public async Task<IActionResult> Get() {      
      using (_frotaHorarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaHorariaDto>>(
                      await _frotaHorarias.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Hora)
                            ).ToListAsync()));
      }
    }

    // GET: FrotaHorarias/5/5
    [HttpGet("{id}/{hr}")]
    public async Task<IActionResult> Get(int id, int hr) {
      using (_frotaHorarias) {
        FrotaHoraria frotaHoraria = await _frotaHorarias.GetFirstAsync(
                                              f => (f.EmpresaId == id) && (f.Hora == hr));
        if (frotaHoraria == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FrotaHorariaDto>(frotaHoraria));
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_frotaHorarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaHorariaDto>>(
                      await _frotaHorarias.GetData(
                                f => f.EmpresaId == id, 
                                f => f.OrderBy(q => q.Hora)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_frotaHorarias) {
        return Ok(_mapper.Map<IEnumerable<FrotaHorariaDto>>(
                      await _frotaHorarias.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId).ThenBy(q => q.Hora)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("Average/{id}")]
    public IActionResult Average(int id) {
      using (_frotaHorarias) {
        return Ok(_frotaHorarias.MediaHoras(p => p.EmpresaId == id));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_frotaHorarias) {
        return Ok(new KeyValuePair<int, int>(_frotaHorarias.Count(),
                                             _frotaHorarias.Pages(size: k ?? 16)));
      }
    }
  }
}
