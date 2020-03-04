using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
  public class CstPneusController : ControllerBase {
    private readonly ICstPneuService _custos;
    private readonly IMapper _mapper;

    public CstPneusController(ICstPneuService custos, IMapper mapper) {
      _custos = custos;
      _mapper = mapper;
    }

    // GET: CstPneus
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstPneuDto>>(
                      await _custos.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: CstPneus/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_custos) {
        CstPneu custo = await _custos.GetFirstAsync(c => c.Id == id);
        if (custo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CstPneuDto>(custo));
      }
    }

    // PUT: CstPneus/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CstPneuDto dto) {
      using (_custos) {
        if (_custos.Exists(c => c.Id == id)) {
          CstPneuValidator validator = new CstPneuValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _custos.Update(_mapper.Map<CstPneu>(dto));
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

    // POST: CstPneus
    [HttpPost]
    public async Task<IActionResult> Post(CstPneuDto dto) {
      CstPneu custo = new CstPneu();
      using (_custos) {
        CstPneuValidator validator = new CstPneuValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _custos.Insert(custo = _mapper.Map<CstPneu>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<CstPneuDto>(custo));
    }

    // DELETE: CstPneus/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_custos) {
        CstPneu custo = await _custos.GetByIdAsync(id);
        if (custo == null) {
          return NotFound();
        }
        try {
          await _custos.Delete(custo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstPneuDto>>(
                      await _custos.GetData(
                                _custos.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 1) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstPneuDto>>(
                      await _custos.GetData(
                                _custos.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_custos) {
        return Ok(await _custos.SelectList(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenByDescending(q => q.Ano)
                                         .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_custos) {
        return Ok(await _custos.SelectList(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            _custos.GetExpression(id),
                            c => c.OrderByDescending(q => q.Ano)
                                  .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k = 8) {
      using (_custos) {
        Expression<Func<CstPneu, bool>> filter = _custos.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _custos.Count(filter), _custos.Pages(filter, k)));
      }
    }
  }
}
