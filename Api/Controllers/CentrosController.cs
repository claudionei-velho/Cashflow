using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
  public class CentrosController : ControllerBase {
    private readonly ICentroService _centros;
    private readonly IMapper _mapper;

    public CentrosController(ICentroService centros, IMapper mapper) {
      _centros = centros;
      _mapper = mapper;
    }

    // GET: Centros
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.ListAsync(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao))));
      }
    }

    // GET: Centros/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_centros) {
        Centro centro = await _centros.GetFirstAsync(c => c.Id == id);
        if (centro == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CentroDto>(centro));
      }
    }

    // PUT: Centros/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CentroDto dto) {
      using (_centros) {
        if (_centros.Exists(c => c.Id == id)) {
          CentroValidator validator = new CentroValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _centros.Update(_mapper.Map<Centro>(dto));
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

    // POST: Centros
    [HttpPost]
    public async Task<IActionResult> Post(CentroDto dto) {
      Centro centro = new Centro();
      using (_centros) {
        CentroValidator validator = new CentroValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _centros.Insert(centro = _mapper.Map<Centro>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<CentroDto>(centro));
    }

    // DELETE: Centros/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_centros) {
        Centro centro = await _centros.GetByIdAsync(id);
        if (centro == null) {
          return NotFound();
        }
        try {
          await _centros.Delete(centro);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.ListAsync(
                                _centros.GetExpression(id),
                                c => c.OrderBy(q => q.Classificacao))));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 8) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.PageListAsync(
                                _centros.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenBy(q => q.Classificacao), p, k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_centros) {
        return Ok(await _centros.SelectListAsync(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Classificacao)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_centros) {
        return Ok(await _centros.SelectListAsync(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            _centros.GetExpression(id),
                            c => c.OrderBy(q => q.Classificacao)));
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k = 8) {
      using (_centros) {
        Expression<Func<Centro, bool>> filter = _centros.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _centros.Count(filter), _centros.Pages(filter, k)));
      }
    }
  }
}
