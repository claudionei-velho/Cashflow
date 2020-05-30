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
  public class PSintesesController : ControllerBase {
    private readonly IPSinteseService _sinteses;
    private readonly IMapper _mapper;

    public PSintesesController(IPSinteseService sinteses, IMapper mapper) {
      _sinteses = sinteses;
      _mapper = mapper;
    }

    // GET: PSinteses
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_sinteses) {
        return Ok(_mapper.Map<IEnumerable<PSinteseDto>>(
                      await _sinteses.ListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.DiaId))));
      }
    }

    // GET: PSinteses/5
    [HttpGet("{id}/{dd}")]
    public async Task<IActionResult> Get(int id, int dd) {
      using (_sinteses) {
        PSintese sintese = await _sinteses.GetFirstAsync(
                                     p => p.EmpresaId == id && p.DiaId == dd);
        if (sintese == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PSinteseDto>(sintese));
      }
    }

    // GET: PSinteses/5
    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_sinteses) {
        return Ok(_mapper.Map<IEnumerable<PSinteseDto>>(
                      await _sinteses.ListAsync(
                                p => p.EmpresaId == id,
                                p => p.OrderBy(q => q.DiaId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_sinteses) {
        return Ok(_mapper.Map<IEnumerable<PSinteseDto>>(
                      await _sinteses.PageListAsync(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.DiaId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_sinteses) {
        return Ok(new KeyValuePair<int, int>(_sinteses.Count(),
                                             _sinteses.Pages(size: k ?? 16)));
      }
    }
  }
}
