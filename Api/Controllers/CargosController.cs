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
  public class CargosController : ControllerBase {
    private readonly ICargoService _cargos;
    private readonly IMapper _mapper;

    public CargosController(ICargoService cargos, IMapper mapper) {
      _cargos = cargos;
      _mapper = mapper;
    }

    // GET: Cargos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_cargos) {
        return Ok(_mapper.Map<IEnumerable<CargoDto>>(
                      await _cargos.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: Cargos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_cargos) {
        Cargo cargo = await _cargos.GetFirstAsync(c => c.Id == id);
        if (cargo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CargoDto>(cargo));
      }
    }

    // PUT: Cargos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CargoDto dto) {
      using (_cargos) {
        if (_cargos.Exists(c => c.Id == id)) {
          CargoValidator validator = new CargoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _cargos.Update(_mapper.Map<Cargo>(dto));
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

    // POST: Cargos
    [HttpPost]
    public async Task<IActionResult> Post(CargoDto dto) {
      Cargo cargo = new Cargo();
      using (_cargos) {
        CargoValidator validator = new CargoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _cargos.Insert(cargo = _mapper.Map<Cargo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<CargoDto>(cargo));
    }

    // DELETE: Cargos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_cargos) {
        Cargo cargo = await _cargos.GetByIdAsync(id);
        if (cargo == null) {
          return NotFound();
        }
        try {
          await _cargos.Delete(cargo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_cargos) {
        return Ok(_mapper.Map<IEnumerable<CargoDto>>(
                      await _cargos.GetData(
                                _cargos.GetExpression(id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 8) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_cargos) {
        return Ok(_mapper.Map<IEnumerable<CargoDto>>(
                      await _cargos.GetData(
                                _cargos.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Denominacao)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_cargos) {
        return Ok(await _cargos.SelectList(
                            c => new { c.Id, c.Codigo, c.Titulo },
                            order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Denominacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_cargos) {
        return Ok(await _cargos.SelectList(
                            c => new { c.Id, c.Codigo, c.Titulo },
                            _cargos.GetExpression(id),
                            c => c.OrderBy(q => q.Denominacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k = 8) {
      using (_cargos) {
        Expression<Func<Cargo, bool>> filter = _cargos.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _cargos.Count(filter), _cargos.Pages(filter, k)));
      }
    }
  }
}
