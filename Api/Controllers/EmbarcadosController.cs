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
  public class EmbarcadosController : ControllerBase {
    private readonly IEmbarcadoService _embarcados;
    private readonly IMapper _mapper;

    public EmbarcadosController(IEmbarcadoService embarcados, IMapper mapper) {
      _embarcados = embarcados;
      _mapper = mapper;
    }

    // GET: Embarcados
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_embarcados) {
        return Ok(_mapper.Map<IEnumerable<EmbarcadoDto>>(
                      await _embarcados.ListAsync(
                                order: e => e.OrderBy(q => q.VeiculoId)
                                             .ThenBy(q => q.EquipamentoId))));
      }
    }

    // GET: Embarcados/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_embarcados) {
        Embarcado embarcado = await _embarcados.GetFirstAsync(e => e.Id == id);
        if (embarcado == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EmbarcadoDto>(embarcado));
      }
    }

    // PUT: Embarcados/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EmbarcadoDto dto) {
      using (_embarcados) {
        if (_embarcados.Exists(e => e.Id == id)) {
          EmbarcadoValidator validator = new EmbarcadoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _embarcados.Update(_mapper.Map<Embarcado>(dto));
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

    // POST: Embarcados
    [HttpPost]
    public async Task<IActionResult> Post(EmbarcadoDto dto) {
      Embarcado embarcado = new Embarcado();
      using (_embarcados) {
        EmbarcadoValidator validator = new EmbarcadoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _embarcados.Insert(embarcado = _mapper.Map<Embarcado>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<EmbarcadoDto>(embarcado));
    }

    // DELETE: Embarcados/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_embarcados) {
        Embarcado embarcado = await _embarcados.GetByIdAsync(id);
        if (embarcado == null) {
          return NotFound();
        }
        try {
          await _embarcados.Delete(embarcado);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_embarcados) {
        return Ok(_mapper.Map<IEnumerable<EmbarcadoDto>>(
                      await _embarcados.ListAsync(
                                e => e.VeiculoId == id,
                                e => e.OrderBy(q => q.EquipamentoId))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_embarcados) {
        return Ok(_mapper.Map<IEnumerable<EmbarcadoDto>>(
                      await _embarcados.PageListAsync(
                                order: e => e.OrderBy(q => q.VeiculoId)
                                             .ThenBy(q => q.EquipamentoId),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_embarcados) {
        return Ok(await _embarcados.SelectListAsync(
                            e => new { e.Id, e.Veiculo.Numero, e.Equipamento.Denominacao },
                            order: e => e.OrderBy(q => q.VeiculoId).ThenBy(q => q.EquipamentoId)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_embarcados) {
        return Ok(await _embarcados.SelectListAsync(
                            e => new { e.Id, e.Veiculo.Numero, e.Equipamento.Denominacao },
                            e => e.VeiculoId == id,
                            e => e.OrderBy(q => q.EquipamentoId)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_embarcados) {
        return Ok(new KeyValuePair<int, int>(_embarcados.Count(),
                                             _embarcados.Pages(size: k ?? 16)));
      }
    }
  }
}
