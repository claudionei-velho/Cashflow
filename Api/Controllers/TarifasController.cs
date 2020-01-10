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
  public class TarifasController : ControllerBase {
    private readonly ITarifaService _tarifas;
    private readonly IMapper _mapper;

    public TarifasController(ITarifaService tarifas, IMapper mapper) {
      _tarifas = tarifas;
      _mapper = mapper;
    }

    // GET: Tarifas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaDto>>(
                      await _tarifas.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: Tarifas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_tarifas) {
        Tarifa tarifa = await _tarifas.GetFirstAsync(t => t.Id == id);
        if (tarifa == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<TarifaDto>(tarifa));
      }
    }

    // PUT: Tarifas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TarifaDto dto) {
      using (_tarifas) {
        if (_tarifas.Exists(s => s.Id == id)) {
          TarifaValidator validator = new TarifaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _tarifas.Update(_mapper.Map<Tarifa>(dto));
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

    // POST: Tarifas
    [HttpPost]
    public async Task<IActionResult> Post(TarifaDto dto) {
      using (_tarifas) {
        TarifaValidator validator = new TarifaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _tarifas.Insert(_mapper.Map<Tarifa>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Tarifas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_tarifas) {
        Tarifa tarifa = await _tarifas.GetByIdAsync(id);
        if (tarifa == null) {
          return NotFound();
        }
        await _tarifas.Delete(tarifa);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaDto>>(
                      await _tarifas.GetData(
                                t => t.EmpresaId == id
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_tarifas) {
        return Ok(_mapper.Map<IEnumerable<TarifaDto>>(
                      await _tarifas.GetData(
                                order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_tarifas) {
        return Ok(await _tarifas.SelectList(
                            t => new { t.Id, t.Referencia, t.Valor },
                            order: t => t.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_tarifas) {
        return Ok(await _tarifas.SelectList(
                            t => new { t.Id, t.Referencia, t.Valor },
                            t => t.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_tarifas) {
        return Ok(new KeyValuePair<int, int>(_tarifas.Count(),
                                             _tarifas.Pages(size: k ?? 16)));
      }
    }
  }
}
