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
  public class EInstalacoesController : ControllerBase {
    private readonly IEInstalacaoService _eInstalacoes;
    private readonly IMapper _mapper;

    public EInstalacoesController(IEInstalacaoService eInstalacoes, IMapper mapper) {
      _eInstalacoes = eInstalacoes;
      _mapper = mapper;
    }

    // GET: EInstalacoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_eInstalacoes) {
        return Ok(_mapper.Map<IEnumerable<EInstalacaoDto>>(
                      await _eInstalacoes.GetData(
                                order: e => e.OrderBy(q => q.Instalacao.EmpresaId)
                                             .ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: EInstalacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_eInstalacoes) {
        EInstalacao eInstalacao = await _eInstalacoes.GetFirstAsync(e => e.Id == id);
        if (eInstalacao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EInstalacaoDto>(eInstalacao));
      }
    }

    // PUT: EInstalacoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EInstalacaoDto dto) {
      using (_eInstalacoes) {
        if (_eInstalacoes.Exists(e => e.Id == id)) {
          EInstalacaoValidator validator = new EInstalacaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _eInstalacoes.Update(_mapper.Map<EInstalacao>(dto));
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

    // POST: EInstalacoes
    [HttpPost]
    public async Task<IActionResult> Post(EInstalacaoDto dto) {
      EInstalacao eInstalacao = new EInstalacao();
      using (_eInstalacoes) {
        EInstalacaoValidator validator = new EInstalacaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _eInstalacoes.Insert(eInstalacao = _mapper.Map<EInstalacao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<EInstalacaoDto>(eInstalacao));
    }

    // DELETE: EInstalacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_eInstalacoes) {
        EInstalacao eInstalacao = await _eInstalacoes.GetByIdAsync(id);
        if (eInstalacao == null) {
          return NotFound();
        }
        try { 
          await _eInstalacoes.Delete(eInstalacao);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_eInstalacoes) {
        return Ok(_mapper.Map<IEnumerable<EInstalacaoDto>>(
                      await _eInstalacoes.GetData(
                                e => e.Instalacao.EmpresaId == id,
                                e => e.OrderBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_eInstalacoes) {
        return Ok(_mapper.Map<IEnumerable<EInstalacaoDto>>(
                      await _eInstalacoes.GetData(
                                order: e => e.OrderBy(q => q.Instalacao.EmpresaId)
                                             .ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_eInstalacoes) {
        return Ok(await _eInstalacoes.SelectList(
                            e => new { e.Id, e.Instalacao.Prefixo, e.Instalacao.Denominacao },
                            order: e => e.OrderBy(q => q.Instalacao.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_eInstalacoes) {
        return Ok(await _eInstalacoes.SelectList(
                            e => new { e.Id, e.Instalacao.Prefixo, e.Instalacao.Denominacao },
                            e => e.Instalacao.EmpresaId == id,
                            e => e.OrderBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_eInstalacoes) {
        return Ok(new KeyValuePair<int, int>(_eInstalacoes.Count(),
                                             _eInstalacoes.Pages(size: k ?? 16)));
      }
    }
  }
}
