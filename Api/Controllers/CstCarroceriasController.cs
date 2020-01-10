﻿using System.Collections.Generic;
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
  public class CstCarroceriasController : ControllerBase {
    private readonly ICstCarroceriaService _custos;
    private readonly IMapper _mapper;

    public CstCarroceriasController(ICstCarroceriaService custos, IMapper mapper) {
      _custos = custos;
      _mapper = mapper;
    }

    // GET: CstChassis
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstCarroceriaDto>>(
                      await _custos.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: CstChassis/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_custos) {
        CstCarroceria custo = await _custos.GetFirstAsync(c => c.Id == id);
        if (custo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CstCarroceriaDto>(custo));
      }
    }

    // PUT: CstChassis/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CstCarroceriaDto dto) {
      using (_custos) {
        if (_custos.Exists(c => c.Id == id)) {
          CstCarroceriaValidator validator = new CstCarroceriaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _custos.Update(_mapper.Map<CstCarroceria>(dto));
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
    public async Task<IActionResult> Post(CstCarroceriaDto dto) {
      using (_custos) {
        CstCarroceriaValidator validator = new CstCarroceriaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _custos.Insert(_mapper.Map<CstCarroceria>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: CstChassis/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_custos) {
        CstCarroceria custo = await _custos.GetByIdAsync(id);
        if (custo == null) {
          return NotFound();
        }
        await _custos.Delete(custo);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstCarroceriaDto>>(
                      await _custos.GetData(
                                c => c.EmpresaId == id,
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_custos) {
        return Ok(_mapper.Map<IEnumerable<CstCarroceriaDto>>(
                      await _custos.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
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
                            c => c.EmpresaId == id,
                            c => c.OrderByDescending(q => q.Ano)
                                  .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_custos) {
        return Ok(new KeyValuePair<int, int>(_custos.Count(),
                                             _custos.Pages(size: k ?? 16)));
      }
    }
  }
}
