using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using FluentValidation;

using Api.Models;
using Api.Models.Validations;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class FrotaHorasController : ControllerBase {
    private readonly IFrotaHoraService _frotas;
    private readonly IMapper _mapper;

    public FrotaHorasController(IFrotaHoraService frotas, IMapper mapper) {
      _frotas = frotas;
      _mapper = mapper;
    }

    // GET: FrotaHoras
    [HttpGet]
    public async Task<IActionResult> Get() {
      
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaHoraDto>>(
                      await _frotas.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.HorarioId)
                            ).ToListAsync()));
      }      
    }

    // GET: FrotaHoras/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_frotas) {
        FrotaHora frota = await _frotas.GetByIdAsync(id);
        if (frota == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FrotaHoraDto>(frota));
      }
    }

    // PUT: FrotaHoras/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FrotaHoraDto dto) {
      using (_frotas) {
        if (_frotas.Exists(f => f.Id == id)) {
          FrotaHoraValidator validator = new FrotaHoraValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _frotas.Update(_mapper.Map<FrotaHora>(dto));
          }
          catch (ValidationException ex) {
            return BadRequest(ex.Errors);
          }
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: FrotaHoras
    [HttpPost]
    public async Task<IActionResult> Post(FrotaHoraDto dto) {
      using (_frotas) {
        FrotaHoraValidator validator = new FrotaHoraValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _frotas.Insert(_mapper.Map<FrotaHora>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: FrotaHoras/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_frotas) {
        FrotaHora frota = await _frotas.GetByIdAsync(id);
        if (frota == null) {
          return NotFound();
        }
        await _frotas.Delete(frota);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaHoraDto>>(
                      await _frotas.GetData(
                                f => f.EmpresaId == id,
                                f => f.OrderByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.HorarioId)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("List/{id}/{year}/{month}")]
    public async Task<IActionResult> List(int id, int year, int month) {
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaHoraDto>>(
                      await _frotas.GetData(
                                f => (f.EmpresaId == id) && (f.Ano == year) && (f.Mes == month),
                                f => f.OrderBy(q => q.HorarioId)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaHoraDto>>(
                      await _frotas.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.HorarioId)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_frotas) {
        return Ok(new KeyValuePair<int, int>(_frotas.Count(),
                                             _frotas.Pages(size: k ?? 16)));
      }
    }
  }
}
