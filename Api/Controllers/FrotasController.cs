using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using FluentValidation;

using Api.Models;
using Api.Models.Validations;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class FrotasController : ControllerBase {
    private readonly IFrotaService _frotas;
    private readonly IMapper _mapper;

    public FrotasController(IFrotaService frotas, IMapper mapper) {
      _frotas = frotas;
      _mapper = mapper;
    }

    // GET: Frotas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaDto>>(
                      await _frotas.ListAsync(
                                order: f => f.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id))));
      }
    }

    // GET: Frotas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_frotas) {
        Frota frota = await _frotas.GetFirstAsync(f => f.Id == id);
        if (frota == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FrotaDto>(frota));
      }
    }

    // PUT: Frotas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FrotaDto dto) {
      using (_frotas) {
        if (_frotas.Exists(f => f.Id == id)) {
          FrotaValidator validator = new FrotaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _frotas.Update(_mapper.Map<Frota>(dto));
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

    // POST: Frotas
    [HttpPost]
    public async Task<IActionResult> Post(FrotaDto dto) {
      Frota frota = new Frota();
      using (_frotas) {
        FrotaValidator validator = new FrotaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _frotas.Insert(frota = _mapper.Map<Frota>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<FrotaDto>(frota));
    }

    // DELETE: Frotas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_frotas) {
        Frota frota = await _frotas.GetByIdAsync(id);
        if (frota == null) {
          return NotFound();
        }
        try {
          await _frotas.Delete(frota);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaDto>>(
                      await _frotas.ListAsync(
                                f => f.EmpresaId == id,
                                f => f.OrderBy(q => q.Id))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_frotas) {
        return Ok(_mapper.Map<IEnumerable<FrotaDto>>(
                      await _frotas.PagedListAsync(
                                order: f => f.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_frotas) {
        return Ok(await _frotas.SelectListAsync(
                            f => new { f.Id, f.CVeiculo.Classe, f.FxEtaria.Denominacao },
                            f => f.EmpresaId == id,
                            f => f.OrderBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_frotas) {
        return Ok(new KeyValuePair<int, int>(_frotas.Count(),
                                             _frotas.Pages(size: k ?? 16)));
      }
    }
  }
}
