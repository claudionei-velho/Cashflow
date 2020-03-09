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
  public class UfsController : ControllerBase {
    private readonly IServiceBase<Uf> _ufs;
    private readonly IMapper _mapper;

    public UfsController(IServiceBase<Uf> ufs, IMapper mapper) {
      _ufs = ufs;
      _mapper = mapper;
    }

    // GET: Ufs
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_ufs) {
        return Ok(_mapper.Map<IEnumerable<UfDto>>(
                      await _ufs.ListAsync(order: u => u.OrderBy(p => p.Sigla))));
      }
    }

    // GET: Ufs/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_ufs) {
        Uf uf = await _ufs.GetByIdAsync(id);
        if (uf == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<UfDto>(uf));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_ufs) {
        return Ok(_mapper.Map<IEnumerable<UfDto>>(
                      await _ufs.PagedListAsync(
                                order: u => u.OrderBy(p => p.Sigla),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_ufs) {
        return Ok(await _ufs.SelectListAsync(
                            u => new { u.Id, u.Sigla, u.Estado },
                            order: u => u.OrderBy(p => p.Sigla)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_ufs) {
        return Ok(new KeyValuePair<int, int>(_ufs.Count(),
                                             _ufs.Pages(size: k ?? 16)));
      }
    }
  }
}
