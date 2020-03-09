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
  public class PaisesController : ControllerBase {
    private readonly IServiceBase<Pais> _paises;
    private readonly IMapper _mapper;

    public PaisesController(IServiceBase<Pais> paises, IMapper mapper) {
      _paises = paises;
      _mapper = mapper;
    }

    // GET: Paises
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_paises) {
        return Ok(_mapper.Map<IEnumerable<PaisDto>>(
                      await _paises.ListAsync(order: p => p.OrderBy(q => q.Nome))));
      }
    }

    // GET: Paises/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) {
      using (_paises) {
        Pais pais = await _paises.GetByIdAsync(id);
        if (pais == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PaisDto>(pais));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_paises) {
        return Ok(await _paises.SelectListAsync(
                            p => new { p.Id, p.Nome },
                            order: p => p.OrderBy(q => q.Nome)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_paises) {
        return Ok(_mapper.Map<IEnumerable<PaisDto>>(
                      await _paises.PagedListAsync(
                                order: p => p.OrderBy(q => q.Nome),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_paises) {
        return Ok(new KeyValuePair<int, int>(_paises.Count(),
                                             _paises.Pages(size: k ?? 16)));
      }
    }
  }
}
