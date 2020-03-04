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
  public class SistFuncoesController : ControllerBase {
    private readonly ISistFuncaoService _fSistemas;
    private readonly ISalarioService _salarios;
    private readonly IMapper _mapper;

    public SistFuncoesController(ISistFuncaoService fSistemas, ISalarioService salarios, IMapper mapper) {
      _fSistemas = fSistemas;
      _salarios = salarios;
      _mapper = mapper;
    }

    // GET: SistFuncoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_fSistemas) {
        return Ok(_mapper.Map<IEnumerable<SistFuncaoDto>>(
                      await _fSistemas.GetData(
                                order: f => f.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).ToListAsync()));

      }
    }

    // GET: SistFuncoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_fSistemas) {
        SistFuncao funcao = await _fSistemas.GetFirstAsync(f => f.Id == id);
        if (funcao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<SistFuncaoDto>(funcao));
      }
    }

    // PUT: SistFuncoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SistFuncaoDto dto) {
      using (_fSistemas) {
        if (_fSistemas.Exists(f => f.Id == id)) {
          if (dto.SalBase <= 0m) {
            using (_salarios) {
              dto.SalBase = _salarios.GetFirst(f => f.FuncaoId == dto.FuncaoId).SalBase;
            }
          }

          SistFuncaoValidator validator = new SistFuncaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _fSistemas.Update(_mapper.Map<SistFuncao>(dto));
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

    // POST: SistFuncoes
    [HttpPost]
    public async Task<IActionResult> Post(SistFuncaoDto dto) {
      if (dto.SalBase <= 0m) {
        using (_salarios) {
          dto.SalBase = _salarios.GetFirst(f => f.FuncaoId == dto.FuncaoId).SalBase;
        }
      }

      SistFuncao funcao = new SistFuncao();
      using (_fSistemas) {
        SistFuncaoValidator validator = new SistFuncaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _fSistemas.Insert(funcao = _mapper.Map<SistFuncao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<SistFuncaoDto>(funcao));
    }

    // DELETE: SistFuncoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_fSistemas) {
        SistFuncao funcao = await _fSistemas.GetByIdAsync(id);
        if (funcao == null) {
          return NotFound();
        }
        try {
          await _fSistemas.Delete(funcao);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_fSistemas) {
        return Ok(_mapper.Map<IEnumerable<SistFuncaoDto>>(
                      await _fSistemas.GetData(
                                f => f.ESistema.EmpresaId == id,
                                f => f.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_fSistemas) {
        return Ok(_mapper.Map<IEnumerable<SistFuncaoDto>>(
                      await _fSistemas.GetData(
                                order: d => d.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_fSistemas) {
        return Ok(await _fSistemas.SelectList(
                            f => new { f.Id, f.ESistema.Codigo, f.Funcao.Titulo },
                            order: f => f.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_fSistemas) {
        return Ok(await _fSistemas.SelectList(
                            f => new { f.Id, f.ESistema.Codigo, f.Funcao.Titulo },
                            f => f.ESistema.EmpresaId == id,
                            f => f.OrderBy(q => q.SistemaId).ThenBy(q => q.Item)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_fSistemas) {
        return Ok(new KeyValuePair<int, int>(_fSistemas.Count(),
                                             _fSistemas.Pages(size: k ?? 16)));
      }
    }
  }
}
