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
  public class NfEntregasController : ControllerBase {
    private readonly INfEntregaService _entregas;
    private readonly IMapper _mapper;

    public NfEntregasController(INfEntregaService referencias, IMapper mapper) {
      _entregas = referencias;
      _mapper = mapper;
    }

    // GET: NfEntregas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_entregas) {
        return Ok(_mapper.Map<IEnumerable<NfEntregaDto>>(
                      await _entregas.ListAsync(order: n => n.OrderBy(q => q.NotaId))));
      }
    }

    // GET: NfEntregas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_entregas) {
        NfEntrega entrega = await _entregas.GetFirstAsync(n => n.Id == id);
        if (entrega == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<NfEntregaDto>(entrega));
      }
    }

    // PUT: NfEntregas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NfEntregaDto dto) {
      using (_entregas) {
        if (_entregas.Exists(n => n.Id == id)) {
          NfEntregaValidator validator = new NfEntregaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _entregas.Update(_mapper.Map<NfEntrega>(dto));
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

    // POST: NfEntregas
    [HttpPost]
    public async Task<IActionResult> Post(NfEntregaDto dto) {
      NfEntrega entrega = new NfEntrega();
      using (_entregas) {
        NfEntregaValidator validator = new NfEntregaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _entregas.Insert(entrega = _mapper.Map<NfEntrega>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<NfEntregaDto>(entrega));
    }

    // DELETE: NfEntregas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_entregas) {
        NfEntrega entrega = await _entregas.GetByIdAsync(id);
        if (entrega == null) {
          return NotFound();
        }
        try {
          await _entregas.Delete(entrega);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_entregas) {
        return Ok(_mapper.Map<IEnumerable<NfEntregaDto>>(
                      await _entregas.ListAsync(
                                n => n.NFiscal.EmpresaId == id,
                                n => n.OrderBy(q => q.NotaId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_entregas) {
        return Ok(_mapper.Map<IEnumerable<NfEntregaDto>>(
                      await _entregas.PageListAsync(
                                order: n => n.OrderBy(q => q.NotaId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_entregas) {
        return Ok(await _entregas.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.NFiscal.ChaveNfe },
                            order: n => n.OrderBy(q => q.NotaId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_entregas) {
        return Ok(await _entregas.SelectListAsync(
                            n => new { n.Id, n.NFiscal.Numero, n.NFiscal.ChaveNfe },
                            n => n.NFiscal.EmpresaId == id,
                            n => n.OrderBy(q => q.NotaId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_entregas) {
        return Ok(new KeyValuePair<int, int>(_entregas.Count(),
                                             _entregas.Pages(size: k ?? 16)));
      }
    }
  }
}
