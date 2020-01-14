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
  public class ChassisController : ControllerBase {
    private readonly IChassiService _chassis;
    private readonly IMapper _mapper;

    public ChassisController(IChassiService chassis, IMapper mapper) {
      _chassis = chassis;
      _mapper = mapper;
    }

    // GET: Chassis
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_chassis) {
        return Ok(_mapper.Map<IEnumerable<ChassiDto>>(
                      await _chassis.GetData(
                                order: v => v.OrderBy(q => q.Veiculo.EmpresaId)
                                             .ThenBy(q => q.Veiculo.Numero)
                            ).ToListAsync()));
      }
    }

    // GET: Chassis/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_chassis) {
        Chassi chassi = await _chassis.GetFirstAsync(c => c.VeiculoId == id);
        if (chassi == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ChassiDto>(chassi));
      }
    }

    // PUT: Chassis/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ChassiDto dto) {
      using (_chassis) {
        if (_chassis.Exists(c => c.VeiculoId == id)) {
          ChassiValidator validator = new ChassiValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _chassis.Update(_mapper.Map<Chassi>(dto));
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

    // POST: Chassis
    [HttpPost]
    public async Task<IActionResult> Post(ChassiDto dto) {
      Chassi chassi = new Chassi();
      using (_chassis) {
        ChassiValidator validator = new ChassiValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _chassis.Insert(chassi = _mapper.Map<Chassi>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<ChassiDto>(chassi));
    }

    // DELETE: Chassis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_chassis) {
        Chassi chassi = await _chassis.GetByIdAsync(id);
        if (chassi == null) {
          return NotFound();
        }
        try { 
          await _chassis.Delete(chassi);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }      
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_chassis) {
        return Ok(_mapper.Map<IEnumerable<ChassiDto>>(
                      await _chassis.GetData(
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
      using (_chassis) {
        return Ok(_mapper.Map<IEnumerable<ChassiDto>>(
                      await _chassis.GetData(
                                order: v => v.OrderBy(q => q.Veiculo.EmpresaId)
                                             .ThenBy(q => q.Veiculo.Numero)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_chassis) {
        return Ok(await _chassis.SelectList(
                            c => new { c.VeiculoId, c.Veiculo.Numero },
                            order: c => c.OrderBy(q => q.Veiculo.EmpresaId)
                                         .ThenBy(q => q.Veiculo.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_chassis) {
        return Ok(await _chassis.SelectList(
                            c => new { c.VeiculoId, c.Veiculo.Numero },
                            c => c.Veiculo.EmpresaId == id,
                            c => c.OrderBy(q => q.Veiculo.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_chassis) {
        return Ok(new KeyValuePair<int, int>(_chassis.Count(),
                                             _chassis.Pages(size: k ?? 16)));
      }
    }
  }
}
