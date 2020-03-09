using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class ProducoesMediasController : ControllerBase {
    private readonly IProducaoMediaService _producoes;
    private readonly IMapper _mapper;

    public ProducoesMediasController(IProducaoMediaService producoes, IMapper mapper) {
      _producoes = producoes;
      _mapper = mapper;
    }

    // GET: ProducoesMedias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoMediaDto>>(
                      await _producoes.ListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenBy(q => q.TarifariaId))));
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoMediaDto>>(
                      await _producoes.ListAsync(
                                p => p.EmpresaId == id,
                                p => p.OrderByDescending(q => q.Ano)
                                      .ThenBy(q => q.TarifariaId))));
      }
    }

    [HttpGet, Route("List/{id}/{year}")]
    public async Task<IActionResult> Get(int id, int year) {
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoMediaDto>>(
                      await _producoes.ListAsync(
                                p => (p.EmpresaId == id) && (p.Ano == year),
                                p => p.OrderBy(q => q.TarifariaId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoMediaDto>>(
                      await _producoes.PagedListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenBy(q => q.TarifariaId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_producoes) {
        return Ok(new KeyValuePair<int, int>(_producoes.Count(),
                                             _producoes.Pages(size: k ?? 16)));
      }
    }
  }
}
