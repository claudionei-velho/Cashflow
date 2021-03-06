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
  public class EConsorciosController : ControllerBase {
    private readonly IEConsorcioService _eConsorcios;
    private readonly IMapper _mapper;

    public EConsorciosController(IEConsorcioService eConsorcios, IMapper mapper) {
      _eConsorcios = eConsorcios;
      _mapper = mapper;
    }

    // GET: EConsorcios
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_eConsorcios) {
        return Ok(_mapper.Map<IEnumerable<EConsorcioDto>>(await _eConsorcios.ListAsync()));
      }
    }

    // GET: EConsorcios/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_eConsorcios) {
        EConsorcio eConsorcio = await _eConsorcios.GetFirstAsync(c => c.Id == id);
        if (eConsorcio == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EConsorcioDto>(eConsorcio));
      }
    }

    // PUT: EConsorcios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EConsorcioDto dto) {
      using (_eConsorcios) {
        if (_eConsorcios.Exists(c => c.Id == id)) {
          EConsorcioValidator validator = new EConsorcioValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _eConsorcios.Update(_mapper.Map<EConsorcio>(dto));
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

    // POST: EConsorcios
    [HttpPost]
    public async Task<IActionResult> Post(EConsorcioDto dto) {
      EConsorcio eConsorcio = new EConsorcio();
      using (_eConsorcios) {
        EConsorcioValidator validator = new EConsorcioValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _eConsorcios.Insert(eConsorcio = _mapper.Map<EConsorcio>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<EConsorcioDto>(eConsorcio));
    }

    // DELETE: EConsorcios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_eConsorcios) {
        EConsorcio eConsorcio = await _eConsorcios.GetByIdAsync(id);
        if (eConsorcio == null) {
          return NotFound();
        }
        try {
          await _eConsorcios.Delete(eConsorcio);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
        return NoContent();
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_eConsorcios) {
        return Ok(_mapper.Map<IEnumerable<EConsorcioDto>>(
                      await _eConsorcios.ListAsync(
                                _eConsorcios.GetExpression(id),
                                c => c.OrderByDescending(q => q.Ratio))));
      }
    }

    [HttpGet, Route("PagedList/{id?}/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int? id, int p = 1, int k = 8) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_eConsorcios) {
        return Ok(_mapper.Map<IEnumerable<EConsorcioDto>>(
                      await _eConsorcios.PageListAsync(
                                _eConsorcios.GetExpression(id),
                                c => c.OrderBy(q => q.ConsorcioId).ThenByDescending(q => q.Ratio), p, k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_eConsorcios) {
        return Ok(await _eConsorcios.SelectListAsync(
                            c => new { c.Id, c.Consorcio.Razao, c.Empresa.Fantasia },
                            order: c => c.OrderBy(q => q.ConsorcioId).ThenByDescending(q => q.Ratio)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_eConsorcios) {
        return Ok(await _eConsorcios.SelectListAsync(
                            c => new { c.Id, c.Consorcio.Razao, c.Empresa.Fantasia },
                            _eConsorcios.GetExpression(id),
                            c => c.OrderByDescending(q => q.Ratio)));
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int k) {
      using (_eConsorcios) {
        Expression<Func<EConsorcio, bool>> filter = _eConsorcios.GetExpression(id);
        return Ok(new KeyValuePair<int, int>(
                          _eConsorcios.Count(filter), _eConsorcios.Pages(filter, k)));
      }
    }
  }
}
