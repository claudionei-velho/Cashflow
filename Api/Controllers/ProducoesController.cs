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
  public class ProducoesController : ControllerBase {
    private readonly IProducaoService _producoes;
    private readonly IMapper _mapper;

    public ProducoesController(IProducaoService producoes, IMapper mapper) {
      _producoes = producoes;
      _mapper = mapper;
    }

    // GET: Producoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoDto>>(
                      await _producoes.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano).ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.TarifariaId)
                            ).ToListAsync()));
      }      
    }

    // GET: Producoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_producoes) {
        Producao producao = await _producoes.GetFirstAsync(p => p.Id == id);
        if (producao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ProducaoDto>(producao));
      }
    }
    
    // PUT: Producoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProducaoDto dto) {
      using (_producoes) {
        if (_producoes.Exists(p => p.Id == id)) {
          ProducaoValidator validator = new ProducaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _producoes.Update(_mapper.Map<Producao>(dto));
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

    // POST: Producoes
    [HttpPost]
    public async Task<IActionResult> Post(ProducaoDto dto) {
      using (_producoes) {
        ProducaoValidator validator = new ProducaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _producoes.Insert(_mapper.Map<Producao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: Producoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_producoes) {
        Producao producao = await _producoes.GetByIdAsync(id);
        if (producao == null) {
          return NotFound();
        }
        await _producoes.Delete(producao);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoDto>>(
                      await _producoes.GetData(
                                p => p.EmpresaId == id,
                                p => p.OrderByDescending(q => q.Ano).ThenByDescending(q => q.Mes)
                                      .ThenBy(q => q.TarifariaId)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_producoes) {
        return Ok(_mapper.Map<IEnumerable<ProducaoDto>>(
                      await _producoes.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano).ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.TarifariaId)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_producoes) {
        return Ok(await _producoes.SelectList(
                            p => new { p.Id, p.EmpresaId, p.Ano, p.Mes, p.TCategoria.Denominacao },
                            order: p => p.OrderBy(q => q.EmpresaId)
                                         .ThenByDescending(q => q.Ano).ThenByDescending(q => q.Mes)
                                         .ThenBy(q => q.TarifariaId)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_producoes) {
        return Ok(await _producoes.SelectList(
                            p => new { p.Id, p.EmpresaId, p.Ano, p.Mes, p.TCategoria.Denominacao },
                            p => p.EmpresaId == id,
                            p => p.OrderByDescending(q => q.Ano).ThenByDescending(q => q.Mes)
                                  .ThenBy(q => q.TarifariaId)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_producoes) {
        return Ok(new KeyValuePair<int, int>(_producoes.Count(),
                                             _producoes.Pages(size: k ?? 16)));
      }
    }
  }
}
