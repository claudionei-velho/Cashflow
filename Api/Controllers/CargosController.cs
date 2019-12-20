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
        Cargo cargo = await _cargos.GetByIdAsync(id);
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
        if (_cargos.Exists(t => t.Id == id)) {
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
      using (_cargos) {
        CargoValidator validator = new CargoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _cargos.Insert(_mapper.Map<Cargo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Cargos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_cargos) {
        Cargo cargo = await _cargos.GetByIdAsync(id);
        if (cargo == null) {
          return NotFound();
        }
        await _cargos.Delete(cargo);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_cargos) {
        return Ok(_mapper.Map<IEnumerable<CargoDto>>(
                      await _cargos.GetData(t => t.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_cargos) {
        return Ok(_mapper.Map<IEnumerable<CargoDto>>(
                      await _cargos.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_cargos) {
        return Ok(await _cargos.SelectList(
                            c => new { c.Id, c.Codigo, c.Titulo },
                            order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_cargos) {
        return Ok(await _cargos.SelectList(
                            c => new { c.Id, c.Codigo, c.Titulo },
                            c => c.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_cargos) {
        return Ok(new KeyValuePair<int, int>(_cargos.Count(),
                                             _cargos.Pages(size: k ?? 16)));
      }
    }
  }
}
