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
  public class ContasController : ControllerBase {
    private readonly IContaService _contas;
    private readonly IMapper _mapper;

    public ContasController(IContaService contas, IMapper mapper) {
      _contas = contas;
      _mapper = mapper;
    }

    // GET: Contas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_contas) {
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.ListAsync(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao))));
      }
    }

    // GET: Contas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_contas) {
        Conta conta = await _contas.GetFirstAsync(c => c.Id == id);
        if (conta == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ContaDto>(conta));
      }
    }

    // PUT: Contas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ContaDto dto) {
      using (_contas) {
        if (_contas.Exists(c => c.Id == id)) {
          ContaValidator validator = new ContaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _contas.Update(_mapper.Map<Conta>(dto));
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

    // POST: Contas
    [HttpPost]
    public async Task<IActionResult> Post(ContaDto dto) {
      Conta conta = new Conta();
      using (_contas) {
        ContaValidator validator = new ContaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _contas.Insert(conta = _mapper.Map<Conta>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<ContaDto>(conta));
    }

    // DELETE: Contas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_contas) {
        Conta conta = await _contas.GetByIdAsync(id);
        if (conta == null) {
          return NotFound();
        }
        try {
          await _contas.Delete(conta);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_contas) {
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.ListAsync(
                                _contas.GetExpression(id),
                                c => c.OrderBy(q => q.Classificacao))));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 8) {
      using (_contas) {
        if (p < 1 || k < 1) {
          return BadRequest();
        }
        return Ok(_mapper.Map<IEnumerable<ContaDto>>(
                      await _contas.PagedListAsync(
                                _contas.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Classificacao), p, k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_contas) {
        return Ok(await _contas.SelectListAsync(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Classificacao)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_contas) {
        return Ok(await _contas.SelectListAsync(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            _contas.GetExpression(id),
                            c => c.OrderBy(q => q.Classificacao)));
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k = 8) {
      using (_contas) {
        Expression<Func<Conta, bool>> filter = _contas.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _contas.Count(filter), _contas.Pages(filter, k)));
      }
    }
  }
}
