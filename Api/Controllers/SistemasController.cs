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
  public class SistemasController : ControllerBase {
    private readonly IServiceBase<Sistema> _sistemas;
    private readonly IMapper _mapper;

    public SistemasController(IServiceBase<Sistema> sistemas, IMapper mapper) {
      _sistemas = sistemas;
      _mapper = mapper;
    }

    // GET: Sistemas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_sistemas) {
        return Ok(_mapper.Map<IEnumerable<SistemaDto>>(
                      await _sistemas.GetData().ToListAsync()));
      }
    }

    // GET: Sistemas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_sistemas) {
        Sistema sistema = await _sistemas.GetByIdAsync(id);
        if (sistema == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<SistemaDto>(sistema));
      }
    }

    // PUT: Sistemas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SistemaDto dto) {
      using (_sistemas) {
        if (_sistemas.Exists(s => s.Id == id)) {
          await _sistemas.Update(_mapper.Map<Sistema>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: Sistemas
    [HttpPost]
    public async Task<IActionResult> Post(SistemaDto dto) {
      Sistema sistema = new Sistema();
      using (_sistemas) {
        if (dto == null) {
          return BadRequest();
        }
        await _sistemas.Insert(sistema = _mapper.Map<Sistema>(dto));
      }
      return Ok(_mapper.Map<Sistema>(sistema));
    }

    // DELETE: Sistemas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_sistemas) {
        Sistema sistema = await _sistemas.GetByIdAsync(id);
        if (sistema == null) {
          return NotFound();
        }
        try { 
          await _sistemas.Delete(sistema);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_sistemas) {
        return Ok(await _sistemas.SelectList(
                            s => new { s.Id, s.Codigo, s.Denominacao },
                            order: s => s.OrderBy(q => q.Codigo)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_sistemas) {
        return Ok(_mapper.Map<IEnumerable<SistemaDto>>(
                      await _sistemas.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_sistemas) {
        return Ok(new KeyValuePair<int, int>(_sistemas.Count(),
                                             _sistemas.Pages(size: k ?? 16)));
      }
    }
  }
}
