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
  public class PCoeficientesController : ControllerBase {
    private readonly IPCoeficienteService _pCoeficientes;
    private readonly IMapper _mapper;

    public PCoeficientesController(IPCoeficienteService pCoeficientes, IMapper mapper) {
      _pCoeficientes = pCoeficientes;
      _mapper = mapper;
    }

    // GET: Planos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_pCoeficientes) {
        return Ok(_mapper.Map<IEnumerable<PCoeficienteDto>>(
                      await _pCoeficientes.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Planos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_pCoeficientes) {
        PCoeficiente pCoeficiente = await _pCoeficientes.GetFirstAsync(p => p.Id == id);
        if (pCoeficiente == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<PCoeficienteDto>(pCoeficiente));
      }
    }

    // PUT: Planos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PCoeficienteDto dto) {
      using (_pCoeficientes) {
        if (_pCoeficientes.Exists(p => p.Id == id)) {
          PCoeficienteValidator validator = new PCoeficienteValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _pCoeficientes.Update(_mapper.Map<PCoeficiente>(dto));
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
    public async Task<IActionResult> Post(PCoeficienteDto dto) {
      using (_pCoeficientes) {
        PCoeficienteValidator validator = new PCoeficienteValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _pCoeficientes.Insert(_mapper.Map<PCoeficiente>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Planos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_pCoeficientes) {
        PCoeficiente pCoeficiente = await _pCoeficientes.GetByIdAsync(id);
        if (pCoeficiente == null) {
          return NotFound();
        }
        await _pCoeficientes.Delete(pCoeficiente);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_pCoeficientes) {
        return Ok(_mapper.Map<IEnumerable<PCoeficienteDto>>(
                      await _pCoeficientes.GetData(p => p.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_pCoeficientes) {
        return Ok(_mapper.Map<IEnumerable<PCoeficienteDto>>(
                      await _pCoeficientes.GetData(
                                order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_pCoeficientes) {
        return Ok(await _pCoeficientes.SelectList(
                            p => new { p.Id, p.Empresa.Fantasia, p.Ano, p.Mes },
                            order: p => p.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_pCoeficientes) {
        return Ok(await _pCoeficientes.SelectList(
                            p => new { p.Id, p.Empresa.Fantasia, p.Ano, p.Mes },
                            p => p.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_pCoeficientes) {
        return Ok(new KeyValuePair<int, int>(_pCoeficientes.Count(),
                                             _pCoeficientes.Pages(size: k ?? 16)));
      }
    }
  }
}