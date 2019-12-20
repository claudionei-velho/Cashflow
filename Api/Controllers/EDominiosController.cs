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
  public class EDominiosController : ControllerBase {
    private readonly IEDominioService _eDominios;
    private readonly IMapper _mapper;

    public EDominiosController(IEDominioService eDominios, IMapper mapper) {
      _eDominios = eDominios;
      _mapper = mapper;
    }

    // GET: EDominios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_eDominios) {
        return Ok(_mapper.Map<IEnumerable<EDominioDto>>(
                      await _eDominios.GetData().ToListAsync()));
      }
    }

    // GET: EDominios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_eDominios) {
        EDominio eDominio = await _eDominios.GetFirstAsync(d => d.Id == id);
        if (eDominio == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EDominioDto>(eDominio));
      }
    }

    // PUT: EDominios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EDominioDto dto) {
      using (_eDominios) {
        if (_eDominios.Exists(d => d.Id == id)) {
          await _eDominios.Update(_mapper.Map<EDominio>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: EDominios
    [HttpPost]
    public async Task<IActionResult> Post(EDominioDto dto) {
      using (_eDominios) {
        if (dto == null) {
          return BadRequest();          
        }
        await _eDominios.Insert(_mapper.Map<EDominio>(dto));
      }
      return Ok();
    }

    // DELETE: EDominios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_eDominios) {
        EDominio eDominio = await _eDominios.GetByIdAsync(id);
        if (eDominio == null) {
          return NotFound();
        }
        await _eDominios.Delete(eDominio);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_eDominios) {
        return Ok(_mapper.Map<IEnumerable<EDominioDto>>(
                      await _eDominios.GetData(d => d.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_eDominios) {
        return Ok(_mapper.Map<IEnumerable<EDominioDto>>(
                      await _eDominios.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_eDominios) {
        return Ok(await _eDominios.SelectList(
                            d => new { d.Id, d.Dominio.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_eDominios) {
        return Ok(await _eDominios.SelectList(
                            d => new { d.Id, d.Dominio.Denominacao }, 
                            d => d.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_eDominios) {
        return Ok(new KeyValuePair<int, int>(_eDominios.Count(),
                                             _eDominios.Pages(size: k ?? 16)));
      }
    }
  }
}
