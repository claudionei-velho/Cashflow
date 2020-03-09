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
  public class OperacionaisController : ControllerBase {
    private readonly IOperacionalService _operacionais;
    private readonly IMapper _mapper;

    public OperacionaisController(IOperacionalService operacionais, IMapper mapper) {
      _operacionais = operacionais;
      _mapper = mapper;
    }

    // GET: Operacionais
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_operacionais) {
        return Ok(_mapper.Map<IEnumerable<OperacionalDto>>(
                      await _operacionais.ListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.LinhaId)
                                             .ThenBy(q => q.Prefixo).ThenBy(q => q.Sentido))));
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_operacionais) {
        return Ok(_mapper.Map<IEnumerable<OperacionalDto>>(
                      await _operacionais.ListAsync(
                                p => p.EmpresaId == id,
                                p => p.OrderBy(q => q.LinhaId)
                                      .ThenBy(q => q.Prefixo).ThenBy(q => q.Sentido))));
      }
    }

    [HttpGet, Route("List/{id}/{ln}")]
    public async Task<IActionResult> List(int id, int ln) {
      using (_operacionais) {
        return Ok(_mapper.Map<IEnumerable<OperacionalDto>>(
                      await _operacionais.ListAsync(
                                p => (p.EmpresaId == id) && (p.LinhaId == ln),
                                p => p.OrderBy(q => q.Prefixo)
                                      .ThenBy(q => q.Sentido))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_operacionais) {
        return Ok(_mapper.Map<IEnumerable<OperacionalDto>>(
                      await _operacionais.PagedListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.LinhaId)
                                             .ThenBy(q => q.Prefixo).ThenBy(q => q.Sentido),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_operacionais) {
        return Ok(new KeyValuePair<int, int>(_operacionais.Count(),
                                             _operacionais.Pages(size: k ?? 16)));
      }
    }
  }
}
