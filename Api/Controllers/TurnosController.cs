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

using Newtonsoft.Json.Linq;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class TurnosController : ControllerBase {
    private readonly ITurnoService _turnos;
    private readonly IMapper _mapper;

    public TurnosController(ITurnoService turnos, IMapper mapper) {
      _turnos = turnos;
      _mapper = mapper;
    }

    // GET: Turnos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_turnos) {
        return Ok(_mapper.Map<IEnumerable<TurnoDto>>(
                      await _turnos.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Turnos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_turnos) {
        Turno turno = await _turnos.GetFirstAsync(t => t.Id == id);
        if (turno == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<TurnoDto>(turno));
      }
    }

    // PUT: Turnos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TurnoDto dto) {
      using (_turnos) {
        if (_turnos.Exists(t => t.Id == id)) {
          TurnoValidator validator = new TurnoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _turnos.Update(_mapper.Map<Turno>(dto));
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

    // POST: Turnos
    [HttpPost]
    public async Task<IActionResult> Post(TurnoDto dto) {
      using (_turnos) {
        TurnoValidator validator = new TurnoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _turnos.Insert(_mapper.Map<Turno>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: Turnos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_turnos) {
        Turno turno = await _turnos.GetByIdAsync(id);
        if (turno == null) {
          return NotFound();
        }
        await _turnos.Delete(turno);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_turnos) {
        return Ok(_mapper.Map<IEnumerable<TurnoDto>>(
                      await _turnos.GetData(t => t.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_turnos) {
        return Ok(_mapper.Map<IEnumerable<TurnoDto>>(
                      await _turnos.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_turnos) {
        return Ok(await _turnos.SelectList(
                            c => new { c.Id, c.Denominacao },
                            order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_turnos) {
        return Ok(await _turnos.SelectList(
                            c => new { c.Id, c.Denominacao },
                            c => c.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_turnos) {
        return Ok(new KeyValuePair<int, int>(_turnos.Count(),
                                             _turnos.Pages(size: k ?? 16)));
      }
    }
  }
}
