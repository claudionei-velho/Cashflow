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
  public class EEncargosController : ControllerBase {
    private readonly IEEncargoService _eEncargos;
    private readonly IMapper _mapper;

    public EEncargosController(IEEncargoService eEncargos, IMapper mapper) {
      _eEncargos = eEncargos;
      _mapper = mapper;
    }

    // GET: EEncargos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_eEncargos) {
        return Ok(_mapper.Map<IEnumerable<EEncargoDto>>(
                      await _eEncargos.ListAsync(
                                order: e => e.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.EncargoId))));
      }
    }

    // GET: EEncargos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_eEncargos) {
        EEncargo eEncargo = await _eEncargos.GetFirstAsync(e => e.Id == id);
        if (eEncargo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EEncargoDto>(eEncargo));
      }
    }

    // PUT: EEncargos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EEncargoDto dto) {
      using (_eEncargos) {
        if (_eEncargos.Exists(e => e.Id == id)) {
          EEncargoValidator validator = new EEncargoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _eEncargos.Update(_mapper.Map<EEncargo>(dto));
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

    // POST: EEncargos
    [HttpPost]
    public async Task<IActionResult> Post(EEncargoDto dto) {
      EEncargo eEncargo = new EEncargo();
      using (_eEncargos) {
        EEncargoValidator validator = new EEncargoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _eEncargos.Insert(eEncargo = _mapper.Map<EEncargo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<EEncargoDto>(eEncargo));
    }

    // DELETE: EEncargos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_eEncargos) {
        EEncargo eEncargo = await _eEncargos.GetByIdAsync(id);
        if (eEncargo == null) {
          return NotFound();
        }
        try {
          await _eEncargos.Delete(eEncargo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_eEncargos) {
        return Ok(_mapper.Map<IEnumerable<EEncargoDto>>(
                      await _eEncargos.ListAsync(
                                e => e.EmpresaId == id,
                                e => e.OrderBy(q => q.EncargoId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_eEncargos) {
        return Ok(_mapper.Map<IEnumerable<EEncargoDto>>(
                      await _eEncargos.PageListAsync(
                                order: e => e.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.EncargoId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_eEncargos) {
        return Ok(await _eEncargos.SelectListAsync(
                            e => new { e.Id, e.Encargo.Denominacao },
                            order: e => e.OrderBy(q => q.EmpresaId).ThenBy(q => q.EncargoId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_eEncargos) {
        return Ok(await _eEncargos.SelectListAsync(
                            e => new { e.Id, e.Encargo.Denominacao },
                            e => e.EmpresaId == id,
                            e => e.OrderBy(q => q.EncargoId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_eEncargos) {
        return Ok(new KeyValuePair<int, int>(_eEncargos.Count(),
                                             _eEncargos.Pages(size: k ?? 16)));
      }
    }
  }
}
