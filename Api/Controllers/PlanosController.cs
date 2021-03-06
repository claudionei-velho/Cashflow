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
  public class PlanosController : ControllerBase {
    private readonly IPlanoService _planos;
    private readonly IMapper _mapper;

    public PlanosController(IPlanoService planos, IMapper mapper) {
      _planos = planos;
      _mapper = mapper;
    }

    // GET: Planos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_planos) {
        return Ok(_mapper.Map<IEnumerable<PlanoDto>>(
                      await _planos.ListAsync(
                                order: p => p.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.Linha.Prefixo)
                                             .ThenBy(q => q.Atendimento.Prefixo))));
      }
    }

    // GET: Planos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_planos) {
        Plano plano = await _planos.GetFirstAsync(p => p.Id == id);
        if (plano == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PlanoDto>(plano));
      }
    }

    // PUT: Planos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PlanoDto dto) {
      using (_planos) {
        if (_planos.Exists(p => p.Id == id)) {
          PlanoValidator validator = new PlanoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _planos.Update(_mapper.Map<Plano>(dto));
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

    // POST: Planos
    [HttpPost]
    public async Task<IActionResult> Post(PlanoDto dto) {
      Plano plano = new Plano();
      using (_planos) {
        PlanoValidator validator = new PlanoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _planos.Insert(plano = _mapper.Map<Plano>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<PlanoDto>(plano));
    }

    // DELETE: Planos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_planos) {
        Plano plano = await _planos.GetByIdAsync(id);
        if (plano == null) {
          return NotFound();
        }
        try {
          await _planos.Delete(plano);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_planos) {
        return Ok(_mapper.Map<IEnumerable<PlanoDto>>(
                      await _planos.ListAsync(
                                p => p.Linha.EmpresaId == id,
                                p => p.OrderBy(q => q.Linha.Prefixo)
                                      .ThenBy(q => q.Atendimento.Prefixo))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_planos) {
        return Ok(_mapper.Map<IEnumerable<PlanoDto>>(
                      await _planos.PageListAsync(
                                order: p => p.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.Linha.Prefixo)
                                             .ThenBy(q => q.Atendimento.Prefixo),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_planos) {
        return Ok(await _planos.SelectListAsync(
                            p => new { p.Id, p.Linha.Prefixo, p.Linha.Denominacao },
                            order: p => p.OrderBy(q => q.Linha.EmpresaId)
                                         .ThenBy(q => q.Linha.Prefixo)
                                         .ThenBy(q => q.Atendimento.Prefixo)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_planos) {
        return Ok(await _planos.SelectListAsync(
                            p => new { p.Id, p.Linha.Prefixo, p.Linha.Denominacao },
                            p => p.Linha.EmpresaId == id,
                            p => p.OrderBy(q => q.Linha.Prefixo)
                                  .ThenBy(q => q.Atendimento.Prefixo)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_planos) {
        return Ok(new KeyValuePair<int, int>(_planos.Count(),
                                             _planos.Pages(size: k ?? 16)));
      }
    }
  }
}
