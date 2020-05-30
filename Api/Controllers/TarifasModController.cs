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
  public class TarifasModController : ControllerBase {
    private readonly ITarifaModService _tarifas;
    private readonly IMapper _mapper;

    public TarifasModController(ITarifaModService tarifas, IMapper mapper) {
      _tarifas = tarifas;
      _mapper = mapper;
    }

    // GET: TarifasMod
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaModDto>>(
                      await _tarifas.ListAsync(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id))));
      }
    }

    // GET: TarifasMod/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_tarifas) {
        TarifaMod tarifa = await _tarifas.GetFirstAsync(t => t.Id == id);
        if (tarifa == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<TarifaModDto>(tarifa));
      }
    }

    // GET: TarifasMod/5
    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaModDto>>(
                      await _tarifas.ListAsync(p => p.EmpresaId == id)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaModDto>>(
                      await _tarifas.PageListAsync(
                                order: t => t.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_tarifas) {
        return Ok(await _tarifas.SelectListAsync(
                            t => new { t.Id, t.Denominacao, t.Tarifa },
                            order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_tarifas) {
        return Ok(await _tarifas.SelectListAsync(
                            t => new { t.Id, t.Denominacao, t.Tarifa },
                            t => t.EmpresaId == id,
                            t => t.OrderBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_tarifas) {
        return Ok(new KeyValuePair<int, int>(_tarifas.Count(),
                                             _tarifas.Pages(size: k ?? 16)));
      }
    }
  }
}
