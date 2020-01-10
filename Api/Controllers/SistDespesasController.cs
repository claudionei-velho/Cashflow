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
  public class SistDespesasController : ControllerBase {
    private readonly ISistDespesaService _dSistemas;
    private readonly IMapper _mapper;

    public SistDespesasController(ISistDespesaService dSistemas, IMapper mapper) {
      _dSistemas = dSistemas;
      _mapper = mapper;
    }

    // GET: SistDespesas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_dSistemas) {      
        return Ok(_mapper.Map<IEnumerable<SistDespesaDto>>(
                      await _dSistemas.GetData(
                                order: d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).ToListAsync()));
      }
    }

    // GET: SistDespesas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_dSistemas) {
        SistDespesa dSistema = await _dSistemas.GetFirstAsync(d => d.Id == id);
        if (dSistema == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<SistDespesaDto>(dSistema));
      }
    }

    // PUT: SistDespesas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SistDespesaDto dto) {
      using (_dSistemas) {
        if (_dSistemas.Exists(d => d.Id == id)) {
          SistDespesaValidator validator = new SistDespesaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _dSistemas.Update(_mapper.Map<SistDespesa>(dto));
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

    // POST: SistDespesas
    [HttpPost]
    public async Task<IActionResult> Post(SistDespesaDto dto) {
      using (_dSistemas) {
        SistDespesaValidator validator = new SistDespesaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _dSistemas.Insert(_mapper.Map<SistDespesa>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: SistDespesas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_dSistemas) {
        SistDespesa dSistema = await _dSistemas.GetByIdAsync(id);
        if (dSistema == null) {
          return NotFound();
        }
        await _dSistemas.Delete(dSistema);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_dSistemas) {
        return Ok(_mapper.Map<IEnumerable<SistDespesaDto>>(
                      await _dSistemas.GetData(
                                d => d.ESistema.EmpresaId == id,
                                d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_dSistemas) {
        return Ok(_mapper.Map<IEnumerable<SistDespesaDto>>(
                      await _dSistemas.GetData(
                                order: d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_dSistemas) {
        return Ok(await _dSistemas.SelectList(
                            d => new { d.Id, d.ESistema.Codigo, d.Conta.Denominacao },
                            order: d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_dSistemas) {
        return Ok(await _dSistemas.SelectList(
                            d => new { d.Id, d.ESistema.Codigo, d.Conta.Denominacao },
                            d => d.ESistema.EmpresaId == id,
                            d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_dSistemas) {
        return Ok(new KeyValuePair<int, int>(_dSistemas.Count(),
                                             _dSistemas.Pages(size: k ?? 16)));
      }
    }
  }
}
