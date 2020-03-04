using System;
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
  public class FxEtariasController : ControllerBase {
    private readonly IServiceBase<FxEtaria> _fxEtarias;
    private readonly IMapper _mapper;

    public FxEtariasController(IServiceBase<FxEtaria> fxEtarias, IMapper mapper) {
      _fxEtarias = fxEtarias;
      _mapper = mapper;
    }

    // GET: FxEtarias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_fxEtarias) {
        return Ok(_mapper.Map<IEnumerable<FxEtariaDto>>(
                      await _fxEtarias.GetData().ToListAsync()));
      }
    }

    // GET: FxEtarias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_fxEtarias) {
        FxEtaria fxEtaria = await _fxEtarias.GetFirstAsync(f => f.Id == id);
        if (fxEtaria == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FxEtariaDto>(fxEtaria));
      }
    }

    // PUT: FxEtarias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FxEtariaDto dto) {
      using (_fxEtarias) {
        if (_fxEtarias.Exists(f => f.Id == id)) {
          await _fxEtarias.Update(_mapper.Map<FxEtaria>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: FxEtarias
    [HttpPost]
    public async Task<IActionResult> Post(FxEtariaDto dto) {
      FxEtaria fxEtaria = new FxEtaria();
      using (_fxEtarias) {
        if (dto == null) {
          return BadRequest();
        }
        await _fxEtarias.Insert(fxEtaria = _mapper.Map<FxEtaria>(dto));
      }
      return Ok(_mapper.Map<FxEtariaDto>(fxEtaria));
    }

    // DELETE: FxEtarias/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<FxEtaria>> Delete(int id) {
      using (_fxEtarias) {
        FxEtaria fxEtaria = await _fxEtarias.GetByIdAsync(id);
        if (fxEtaria == null) {
          return NotFound();
        }
        try {
          await _fxEtarias.Delete(fxEtaria);
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
      using (_fxEtarias) {
        return Ok(_mapper.Map<IEnumerable<FxEtariaDto>>(
                      await _fxEtarias.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_fxEtarias) {
        return Ok(await _fxEtarias.SelectList(
                            f => new { f.Id, f.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_fxEtarias) {
        return Ok(new KeyValuePair<int, int>(_fxEtarias.Count(),
                                             _fxEtarias.Pages(size: k ?? 16)));
      }
    }
  }
}
