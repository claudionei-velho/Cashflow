﻿using System;
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
  public class CstChassiController : ControllerBase {
    private readonly ICstChassiService _custos;
    private readonly IMapper _mapper;

    public CstChassiController(ICstChassiService custos, IMapper mapper) {
      _custos = custos;
      _mapper = mapper;
    }

    // GET: CstChassis
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstChassiDto>>(
                      await _custos.ListAsync(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes).ThenBy(q => q.Id))));
      }
    }

    // GET: CstChassis/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_custos) {
        CstChassi custo = await _custos.GetFirstAsync(c => c.Id == id);
        if (custo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CstChassiDto>(custo));
      }
    }

    // PUT: CstChassis/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CstChassiDto dto) {
      using (_custos) {
        if (_custos.Exists(c => c.Id == id)) {
          CstChassiValidator validator = new CstChassiValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _custos.Update(_mapper.Map<CstChassi>(dto));
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

    // POST: CstChassis
    [HttpPost]
    public async Task<IActionResult> Post(CstChassiDto dto) {
      CstChassi custo = new CstChassi();
      using (_custos) {
        CstChassiValidator validator = new CstChassiValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _custos.Insert(custo = _mapper.Map<CstChassi>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<CstChassiDto>(custo));
    }

    // DELETE: CstChassis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_custos) {
        CstChassi custo = await _custos.GetByIdAsync(id);
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
        return Ok(_mapper.Map<IEnumerable<CstChassiDto>>(
                      await _custos.ListAsync(
                                _custos.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id))));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 8) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstChassiDto>>(
                      await _custos.PageListAsync(
                                _custos.GetExpression(id),
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id), p, k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_custos) {
        return Ok(await _custos.SelectListAsync(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenByDescending(q => q.Ano)
                                         .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_custos) {
        return Ok(await _custos.SelectListAsync(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            _custos.GetExpression(id),
                            c => c.OrderByDescending(q => q.Ano)
                                  .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k = 8) {
      using (_custos) {
        Expression<Func<CstChassi, bool>> filter = _custos.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _custos.Count(filter), _custos.Pages(filter, k)));
      }
    }
  }
}
