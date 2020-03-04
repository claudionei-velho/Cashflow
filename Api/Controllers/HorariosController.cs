using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
  public class HorariosController : ControllerBase {
    private readonly IHorarioService _horarios;
    private readonly IMapper _mapper;

    public HorariosController(IHorarioService horarios, IMapper mapper) {
      _horarios = horarios;
      _mapper = mapper;
    }

    // GET: Horarios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_horarios) {
        return Ok(_mapper.Map<IEnumerable<HorarioDto>>(
                      await _horarios.GetData(
                                order: h => h.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.Linha.Prefixo).ThenBy(q => q.DiaId)
                                             .ThenBy(q => q.Inicio).ThenBy(q => q.Sentido)
                            ).ToListAsync()));
      }
    }

    // GET: Horarios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_horarios) {
        Horario horario = await _horarios.GetFirstAsync(h => h.Id == id);
        if (horario == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<HorarioDto>(horario));
      }
    }

    // PUT: Horarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, HorarioDto dto) {
      using (_horarios) {
        if (_horarios.Exists(h => h.Id == id)) {
          HorarioValidator validator = new HorarioValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _horarios.Update(_mapper.Map<Horario>(dto));
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

    // POST: Horarios
    [HttpPost]
    public async Task<IActionResult> Post(HorarioDto dto) {
      Horario horario = new Horario();
      using (_horarios) {
        HorarioValidator validator = new HorarioValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _horarios.Insert(horario = _mapper.Map<Horario>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<HorarioDto>(horario));
    }

    // DELETE: Horarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_horarios) {
        Horario horario = await _horarios.GetByIdAsync(id);
        if (horario == null) {
          return NotFound();
        }
        try {
          await _horarios.Delete(horario);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}/{op?}/{ab?}")]
    public async Task<IActionResult> List(int id, int? op = null, string ab = null) {
      Expression<Func<Horario, bool>> condition = h => h.LinhaId == id;
      if (op.HasValue) {
        condition = string.IsNullOrWhiteSpace(ab)
          ? (h => (h.LinhaId == id) && (h.DiaId == op.Value))
          : (Expression<Func<Horario, bool>>)(h => (h.LinhaId == id) &&
                                             (h.DiaId == op.Value) && h.Sentido.Equals(ab));
      }
      else {
        if (!string.IsNullOrWhiteSpace(ab)) {
          condition = h => (h.LinhaId == id) && h.Sentido.Equals(ab);
        }
      }

      using (_horarios) {
        return Ok(_mapper.Map<IEnumerable<HorarioDto>>(
                      await _horarios.GetData(
                                condition,
                                h => h.OrderBy(q => q.DiaId)
                                      .ThenBy(q => q.Inicio).ThenBy(q => q.Sentido)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_horarios) {
        return Ok(_mapper.Map<IEnumerable<HorarioDto>>(
                      await _horarios.GetData(
                                order: h => h.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.Linha.Prefixo).ThenBy(q => q.DiaId)
                                             .ThenBy(q => q.Inicio).ThenBy(q => q.Sentido)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList/{id}/{op?}/{ab?}")]
    public async Task<IActionResult> SelectList(int id, int? op = null, string ab = null) {
      Expression<Func<Horario, bool>> condition = h => h.LinhaId == id;
      if (op.HasValue) {
        if (string.IsNullOrWhiteSpace(ab)) {
          condition = h => h.LinhaId == id && h.DiaId == op.Value;
        }
        else {
          condition = h => h.LinhaId == id && h.DiaId == op.Value && h.Sentido.Equals(ab);
        }
      }
      else {
        if (!string.IsNullOrWhiteSpace(ab)) {
          condition = h => h.LinhaId == id && h.Sentido.Equals(ab);
        }
      }

      using (_horarios) {
        return Ok(await _horarios.SelectList(
                            h => new { h.Id, h.Inicio },
                            condition,
                            h => h.OrderBy(q => q.DiaId)
                                  .ThenBy(q => q.Inicio).ThenBy(q => q.Sentido)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_horarios) {
        return Ok(new KeyValuePair<int, int>(_horarios.Count(),
                                             _horarios.Pages(size: k ?? 16)));
      }
    }
  }
}
