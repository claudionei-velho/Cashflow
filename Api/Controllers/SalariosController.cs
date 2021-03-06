﻿using System;
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
  public class SalariosController : ControllerBase {
    private readonly ISalarioService _salarios;
    private readonly IMapper _mapper;

    public SalariosController(ISalarioService salarios, IMapper mapper) {
      _salarios = salarios;
      _mapper = mapper;
    }

    // GET: Salarios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_salarios) {
        return Ok(_mapper.Map<IEnumerable<SalarioDto>>(
                      await _salarios.ListAsync(
                                order: s => s.OrderBy(q => q.Funcao.Cargo.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.FuncaoId))));
      }
    }

    // GET: Salarios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_salarios) {
        Salario salario = await _salarios.GetFirstAsync(s => s.Id == id);
        if (salario == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<SalarioDto>(salario));
      }
    }

    // PUT: Salarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SalarioDto dto) {
      using (_salarios) {
        if (_salarios.Exists(s => s.Id == id)) {
          SalarioValidator validator = new SalarioValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _salarios.Update(_mapper.Map<Salario>(dto));
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

    // POST: Salarios
    [HttpPost]
    public async Task<IActionResult> Post(SalarioDto dto) {
      Salario salario = new Salario();
      using (_salarios) {
        SalarioValidator validator = new SalarioValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _salarios.Insert(salario = _mapper.Map<Salario>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<SalarioDto>(salario));
    }

    // DELETE: Salarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_salarios) {
        Salario salario = await _salarios.GetByIdAsync(id);
        if (salario == null) {
          return NotFound();
        }
        try {
          await _salarios.Delete(salario);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_salarios) {
        return Ok(_mapper.Map<IEnumerable<SalarioDto>>(
                      await _salarios.ListAsync(
                                s => s.Funcao.Cargo.EmpresaId == id,
                                s => s.OrderByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes)
                                      .ThenBy(q => q.FuncaoId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_salarios) {
        return Ok(_mapper.Map<IEnumerable<SalarioDto>>(
                      await _salarios.PageListAsync(
                                order: s => s.OrderBy(q => q.Funcao.Cargo.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes)
                                             .ThenBy(q => q.FuncaoId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_salarios) {
        return Ok(await _salarios.SelectListAsync(
                            s => new { s.Id, s.Funcao.Titulo, s.Ano, s.Mes, s.SalBase },
                            order: s => s.OrderBy(q => q.Funcao.Cargo.EmpresaId)
                                         .ThenByDescending(q => q.Ano)
                                         .ThenByDescending(q => q.Mes)
                                         .ThenBy(q => q.FuncaoId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_salarios) {
        return Ok(await _salarios.SelectListAsync(
                            s => new { s.Id, s.Funcao.Titulo, s.Ano, s.Mes, s.SalBase },
                            s => s.Funcao.Cargo.EmpresaId == id,
                            s => s.OrderByDescending(q => q.Ano)
                                  .ThenByDescending(q => q.Mes)
                                  .ThenBy(q => q.FuncaoId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_salarios) {
        return Ok(new KeyValuePair<int, int>(_salarios.Count(),
                                             _salarios.Pages(size: k ?? 16)));
      }
    }
  }
}
