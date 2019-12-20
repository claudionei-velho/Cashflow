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
  public class PremissasController : ControllerBase {
    private readonly IPremissaService _premissas;
    private readonly IMapper _mapper;

    public PremissasController(IPremissaService premissas, IMapper mapper) {
      _premissas = premissas;
      _mapper = mapper;
    }

    // GET: Produtos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_premissas) {
        return Ok(_mapper.Map<IEnumerable<PremissaDto>>(
                      await _premissas.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Produtos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_premissas) {
        Premissa premissa = await _premissas.GetFirstAsync(p => p.Id == id);
        if (premissa == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PremissaDto>(premissa));
      }
    }

    // PUT: Produtos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PremissaDto dto) {
      using (_premissas) {
        if (_premissas.Exists(p => p.Id == id)) {
          PremissaValidator validator = new PremissaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _premissas.Update(_mapper.Map<Premissa>(dto));
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

    // POST: Produtos
    [HttpPost]
    public async Task<IActionResult> Post(PremissaDto dto) {
      using (_premissas) {
        PremissaValidator validator = new PremissaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _premissas.Insert(_mapper.Map<Premissa>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: Produtos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_premissas) {
        Premissa premissa = await _premissas.GetByIdAsync(id);
        if (premissa == null) {
          return NotFound();
        }
        await _premissas.Delete(premissa);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_premissas) {
        return Ok(_mapper.Map<IEnumerable<PlanoDto>>(
          await _premissas.GetData(
                    p => p.EmpresaId == id,
                    p => p.OrderBy(q => q.Id)
                ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_premissas) {
        return Ok(_mapper.Map<IEnumerable<PremissaDto>>(
                      await _premissas.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_premissas) {
        return Ok(await _premissas.SelectList(
                            p => new { p.Id, p.EmpresaId, p.Ano, p.Mes }, 
                            order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_premissas) {
        return Ok(await _premissas.SelectList(
                            p => new { p.Id, p.EmpresaId, p.Ano, p.Mes },
                            p => p.EmpresaId == id,
                            p => p.OrderBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_premissas) {
        return Ok(new KeyValuePair<int, int>(_premissas.Count(),
                                             _premissas.Pages(size: k ?? 16)));
      }
    }
  }
}
