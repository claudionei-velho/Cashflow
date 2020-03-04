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
  public class DominiosController : ControllerBase {
    private readonly IServiceBase<Dominio> _dominios;
    private readonly IMapper _mapper;

    public DominiosController(IServiceBase<Dominio> dominios, IMapper mapper) {
      _dominios = dominios;
      _mapper = mapper;
    }

    // GET: Dominios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_dominios) {
        return Ok(_mapper.Map<IEnumerable<DominioDto>>(
                      await _dominios.GetData().ToListAsync()));
      }
    }

    // GET: Dominios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_dominios) {
        Dominio dominio = await _dominios.GetByIdAsync(id);
        if (dominio == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<DominioDto>(dominio));
      }
    }

    // PUT: Dominios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DominioDto dto) {
      using (_dominios) {
        if (_dominios.Exists(d => d.Id == id)) {
          await _dominios.Update(_mapper.Map<Dominio>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: Dominios
    [HttpPost]
    public async Task<IActionResult> Post(DominioDto dto) {
      Dominio dominio = new Dominio();
      using (_dominios) {
        if (dto == null) {
          return BadRequest();
        }
        await _dominios.Insert(dominio = _mapper.Map<Dominio>(dto));
      }
      return Ok(_mapper.Map<DominioDto>(dominio));
    }

    // DELETE: Dominios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_dominios) {
        Dominio dominio = await _dominios.GetByIdAsync(id);
        if (dominio == null) {
          return NotFound();
        }
        try {
          await _dominios.Delete(dominio);
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
      using (_dominios) {
        return Ok(_mapper.Map<IEnumerable<DominioDto>>(
                      await _dominios.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_dominios) {
        return Ok(await _dominios.SelectList(
                            d => new { d.Id, d.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_dominios) {
        return Ok(new KeyValuePair<int, int>(_dominios.Count(),
                                             _dominios.Pages(size: k ?? 16)));
      }
    }
  }
}
