using System;
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
  public class ECVeiculosController : ControllerBase {
    private readonly IECVeiculoService _ecVeiculos;
    private readonly IMapper _mapper;

    public ECVeiculosController(IECVeiculoService ecVeiculos, IMapper mapper) {
      _ecVeiculos = ecVeiculos;
      _mapper = mapper;
    }

    // GET: ECVeiculos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_ecVeiculos) {
        return Ok(_mapper.Map<IEnumerable<ECVeiculoDto>>(
                      await _ecVeiculos.GetData().ToListAsync()));
      }
    }

    // GET: ECVeiculos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_ecVeiculos) {
        ECVeiculo ecVeiculo = await _ecVeiculos.GetFirstAsync(v => v.Id == id);
        if (ecVeiculo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ECVeiculoDto>(ecVeiculo));
      }
    }

    // PUT: ECVeiculos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ECVeiculoDto dto) {
      using (_ecVeiculos) {
        if (_ecVeiculos.Exists(v => v.Id == id)) {
          ECVeiculoValidator validator = new ECVeiculoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _ecVeiculos.Update(_mapper.Map<ECVeiculo>(dto));
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

    // POST: ECVeiculos
    [HttpPost]
    public async Task<IActionResult> Post(ECVeiculoDto dto) {
      ECVeiculo ecVeiculo = new ECVeiculo();
      using (_ecVeiculos) {
        ECVeiculoValidator validator = new ECVeiculoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _ecVeiculos.Insert(ecVeiculo = _mapper.Map<ECVeiculo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<ECVeiculoDto>(ecVeiculo));
    }

    // DELETE: ECVeiculos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_ecVeiculos) {
        ECVeiculo ecVeiculo = await _ecVeiculos.GetByIdAsync(id);
        if (ecVeiculo == null) {
          return NotFound();
        }
        try {
          await _ecVeiculos.Delete(ecVeiculo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_ecVeiculos) {
        return Ok(_mapper.Map<IEnumerable<ECVeiculoDto>>(
                      await _ecVeiculos.GetData(v => v.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_ecVeiculos) {
        return Ok(_mapper.Map<IEnumerable<ECVeiculoDto>>(
                      await _ecVeiculos.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_ecVeiculos) {
        return Ok(await _ecVeiculos.SelectList(
                            v => new { v.Id, v.CVeiculo.Classe }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_ecVeiculos) {
        return Ok(await _ecVeiculos.SelectList(
                           v => new { v.Id, v.CVeiculo.Classe },
                           v => v.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_ecVeiculos) {
        return Ok(new KeyValuePair<int, int>(_ecVeiculos.Count(),
                                             _ecVeiculos.Pages(size: k ?? 16)));
      }
    }
  }
}
