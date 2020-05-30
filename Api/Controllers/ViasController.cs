using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class ViasController : ControllerBase {
    private readonly IServiceBase<Via> _vias;
    private readonly IMapper _mapper;

    public ViasController(IServiceBase<Via> vias, IMapper mapper) {
      _vias = vias;
      _mapper = mapper;
    }

    // GET: Vias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_vias) {
        return Ok(_mapper.Map<IEnumerable<ViaDto>>(await _vias.ListAsync()));
      }
    }

    // GET: Vias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_vias) {
        Via via = await _vias.GetByIdAsync(id);
        if (via == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ViaDto>(via));
      }
    }

    // PUT: Vias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ViaDto dto) {
      using (_vias) {
        if (_vias.Exists(v => v.Id == id)) {
          await _vias.Update(_mapper.Map<Via>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: Vias
    [HttpPost]
    public async Task<IActionResult> Post(ViaDto dto) {
      Via via = new Via();
      using (_vias) {
        if (dto == null) {
          return BadRequest();
        }
        await _vias.Insert(via = _mapper.Map<Via>(dto));
      }
      return Ok(_mapper.Map<ViaDto>(via));
    }

    // DELETE: Vias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_vias) {
        Via via = await _vias.GetByIdAsync(id);
        if (via == null) {
          return NotFound();
        }
        try {
          await _vias.Delete(via);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_vias) {
        return Ok(_mapper.Map<IEnumerable<ViaDto>>(await _vias.PageListAsync(skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_vias) {
        return Ok(await _vias.SelectListAsync(v => new { v.Id, v.Denominacao }));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_vias) {
        return Ok(new KeyValuePair<int, int>(_vias.Count(),
                                             _vias.Pages(size: k ?? 16)));
      }
    }
  }
}
