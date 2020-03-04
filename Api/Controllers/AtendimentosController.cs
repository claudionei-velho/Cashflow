using System;
using System.Collections.Generic;
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
  public class AtendimentosController : ControllerBase {
    private readonly IAtendimentoService _atendimentos;
    private readonly IMapper _mapper;

    public AtendimentosController(IAtendimentoService atendimentos, IMapper mapper) {
      _atendimentos = atendimentos;
      _mapper = mapper;
    }

    // GET: Atendimentos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_atendimentos) {
        return Ok(_mapper.Map<IEnumerable<AtendimentoDto>>(
                      await _atendimentos.GetData(
                                order: a => a.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.LinhaId).ThenBy(q => q.Prefixo)
                            ).ToListAsync()));
      }
    }

    // GET: Atendimentos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_atendimentos) {
        Atendimento atendimento = await _atendimentos.GetFirstAsync(a => a.Id == id);
        if (atendimento == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<AtendimentoDto>(atendimento));
      }
    }

    // PUT: Atendimentos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AtendimentoDto dto) {
      using (_atendimentos) {
        if (_atendimentos.Exists(a => a.Id == id)) {
          AtendimentoValidator validator = new AtendimentoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _atendimentos.Update(_mapper.Map<Atendimento>(dto));
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

    // POST: Atendimentos
    [HttpPost]
    public async Task<IActionResult> Post(AtendimentoDto dto) {
      Atendimento atendimento = new Atendimento();
      using (_atendimentos) {
        AtendimentoValidator validator = new AtendimentoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _atendimentos.Insert(atendimento = _mapper.Map<Atendimento>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<AtendimentoDto>(atendimento));
    }

    // DELETE: Atendimentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_atendimentos) {
        Atendimento atendimento = await _atendimentos.GetByIdAsync(id);
        if (atendimento == null) {
          return NotFound();
        }
        try {
          await _atendimentos.Delete(atendimento);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_atendimentos) {
        return Ok(_mapper.Map<IEnumerable<AtendimentoDto>>(
                      await _atendimentos.GetData(
                                a => a.LinhaId == id,
                                a => a.OrderBy(q => q.Prefixo)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_atendimentos) {
        return Ok(_mapper.Map<IEnumerable<AtendimentoDto>>(
                      await _atendimentos.GetData(
                                order: a => a.OrderBy(q => q.Linha.EmpresaId)
                                             .ThenBy(q => q.LinhaId).ThenBy(q => q.Prefixo)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_atendimentos) {
        return Ok(await _atendimentos.SelectList(
                            a => new { a.Id, a.Prefixo, a.Denominacao },
                            order: a => a.OrderBy(q => q.Linha.EmpresaId)
                                         .ThenBy(q => q.LinhaId).ThenBy(q => q.Prefixo)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_atendimentos) {
        return Ok(await _atendimentos.SelectList(
                            a => new { a.Id, a.Prefixo, a.Denominacao },
                            a => a.Linha.EmpresaId == id,
                            a => a.OrderBy(q => q.Prefixo)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_atendimentos) {
        return Ok(new KeyValuePair<int, int>(_atendimentos.Count(),
                                             _atendimentos.Pages(size: k ?? 16)));
      }
    }
  }
}
