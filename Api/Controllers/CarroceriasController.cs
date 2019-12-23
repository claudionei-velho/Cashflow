﻿using System.Collections.Generic;
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
  public class CarroceriasController : ControllerBase {
    private readonly ICarroceriaService _carrocerias;
    private readonly IMapper _mapper;

    public CarroceriasController(ICarroceriaService carrocerias, IMapper mapper) {
      _carrocerias = carrocerias;
      _mapper = mapper;
    }

    // GET: Carrocerias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_carrocerias) {
        return Ok(_mapper.Map<IEnumerable<CarroceriaDto>>(
                      await _carrocerias.GetData(
                                order: v => v.OrderBy(q => q.Veiculo.EmpresaId)
                                             .ThenBy(q => q.Veiculo.Numero)
                            ).ToListAsync()));
      }
    }

    // GET: Carrocerias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_carrocerias) {
        Carroceria carroceria = await _carrocerias.GetFirstAsync(c => c.VeiculoId == id);
        if (carroceria == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<Carroceria>(carroceria));
      }
    }

    // PUT: Carrocerias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CarroceriaDto dto) {
      using (_carrocerias) {
        if (_carrocerias.Exists(c => c.VeiculoId == id)) {
          CarroceriaValidator validator = new CarroceriaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _carrocerias.Update(_mapper.Map<Carroceria>(dto));
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

    // POST: Carrocerias
    [HttpPost]
    public async Task<IActionResult> Post(CarroceriaDto dto) {
      using (_carrocerias) {
        CarroceriaValidator validator = new CarroceriaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _carrocerias.Insert(_mapper.Map<Carroceria>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Carrocerias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_carrocerias) {
        Carroceria carroceria = await _carrocerias.GetByIdAsync(id);
        if (carroceria == null) {
          return NotFound();
        }
        await _carrocerias.Delete(carroceria);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_carrocerias) {
        return Ok(_mapper.Map<IEnumerable<CarroceriaDto>>(
                      await _carrocerias.GetData(
                                c => c.Veiculo.EmpresaId == id,
                                c => c.OrderBy(q => q.Veiculo.Numero)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_carrocerias) {
        return Ok(_mapper.Map<IEnumerable<CarroceriaDto>>(
                      await _carrocerias.GetData(
                                order: v => v.OrderBy(q => q.Veiculo.EmpresaId)
                                             .ThenBy(q => q.Veiculo.Numero)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_carrocerias) {
        return Ok(await _carrocerias.SelectList(
                            c => new { c.VeiculoId, c.Veiculo.Numero },
                            order: c => c.OrderBy(q => q.Veiculo.EmpresaId)
                                         .ThenBy(q => q.Veiculo.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_carrocerias) {
        return Ok(await _carrocerias.SelectList(
                            c => new { c.VeiculoId, c.Veiculo.Numero },
                            c => c.Veiculo.EmpresaId == id,
                            c => c.OrderBy(q => q.Veiculo.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_carrocerias) {
        return Ok(new KeyValuePair<int, int>(_carrocerias.Count(),
                                             _carrocerias.Pages(size: k ?? 16)));
      }
    }
  }
}