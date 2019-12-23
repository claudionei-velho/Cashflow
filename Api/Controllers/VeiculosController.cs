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
  public class VeiculosController : ControllerBase {
    private readonly IVeiculoService _veiculos;
    private readonly IMapper _mapper;

    public VeiculosController(IVeiculoService veiculos, IMapper mapper) {
      _veiculos = veiculos;
      _mapper = mapper;
    }

    // GET: Veiculos
    [HttpGet]
    public async Task<IActionResult> Get() {      
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<VeiculoDto>>(
                      await _veiculos.GetData(
                                order: v => v.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Numero)
                            ).ToListAsync()));
      }      
    }

    // GET: Veiculos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_veiculos) {
        Veiculo veiculo = await _veiculos.GetFirstAsync(v => v.Id == id);
        if (veiculo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<VeiculoDto>(veiculo));
      }
    }

    // PUT: Veiculos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VeiculoDto dto) {
      using (_veiculos) {
        if (_veiculos.Exists(v => v.Id == id)) {
          VeiculoValidator validator = new VeiculoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _veiculos.Update(_mapper.Map<Veiculo>(dto));
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

    // POST: Veiculos
    [HttpPost]
    public async Task<IActionResult> Post(VeiculoDto dto) {
      using (_veiculos) {
        VeiculoValidator validator = new VeiculoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _veiculos.Insert(_mapper.Map<Veiculo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Veiculos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_veiculos) {
        Veiculo veiculo = await _veiculos.GetByIdAsync(id);
        if (veiculo == null) {
          return NotFound();
        }
        await _veiculos.Delete(veiculo);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<VeiculoDto>>(
                      await _veiculos.GetData(
                                v => v.EmpresaId == id,
                                v => v.OrderBy(q => q.Numero)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_veiculos) {
        return Ok(_mapper.Map<IEnumerable<VeiculoDto>>(
                      await _veiculos.GetData(
                                order: v => v.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Numero)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_veiculos) {
        return Ok(await _veiculos.SelectList(
                            v => new { v.Id, v.Numero },
                            order: v => v.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_veiculos) {
        return Ok(await _veiculos.SelectList(
                            v => new { v.Id, v.Numero },
                            v => v.EmpresaId == id,
                            v => v.OrderBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("AddChassi/{id}")]
    public async Task<ActionResult> AddChassi(int id) {
      using (_veiculos) {
        return Ok(await _veiculos.GetNoChassi(
                            v => v.EmpresaId == id,
                            v => v.OrderBy(q => q.Numero)
                        ).Select(v => new { v.Id, v.Numero }).ToListAsync());
      }
    }

    [HttpGet, Route("AddCarroceria/{id}")]
    public async Task<IActionResult> AddCarroceria(int id) {
      using (_veiculos) {
        return Ok(await _veiculos.GetNoCarroceria(
                            v => v.EmpresaId == id, 
                            v => v.OrderBy(q => q.Numero)
                        ).Select(v => new { v.Id, v.Numero }).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_veiculos) {
        return Ok(new KeyValuePair<int, int>(_veiculos.Count(),
                                             _veiculos.Pages(size: k ?? 16)));
      }
    }
  }
}