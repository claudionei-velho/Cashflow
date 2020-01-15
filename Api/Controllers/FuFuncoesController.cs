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
  public class FuFuncoesController : ControllerBase {
    private readonly IFuFuncaoService _fuFuncoes;
    private readonly IMapper _mapper;

    public FuFuncoesController(IFuFuncaoService funcoes, IMapper mapper) {
      _fuFuncoes = funcoes;
      _mapper = mapper;
    }

    // GET: FuFuncoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_fuFuncoes) {
        return Ok(_mapper.Map<IEnumerable<FuFuncaoDto>>(
                      await _fuFuncoes.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: FuFuncoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_fuFuncoes) {
        FuFuncao fuFuncao = await _fuFuncoes.GetFirstAsync(f => f.Id == id);
        if (fuFuncao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FuFuncaoDto>(fuFuncao));
      }
    }

    // PUT: FuFuncoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FuFuncaoDto dto) {
      using (_fuFuncoes) {
        if (_fuFuncoes.Exists(f => f.Id == id)) {
          FuFuncaoValidator validator = new FuFuncaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _fuFuncoes.Update(_mapper.Map<FuFuncao>(dto));
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

    // POST: FuFuncoes
    [HttpPost]
    public async Task<IActionResult> Post(FuFuncaoDto dto) {
      FuFuncao fuFuncao = new FuFuncao();
      using (_fuFuncoes) {
        FuFuncaoValidator validator = new FuFuncaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _fuFuncoes.Insert(fuFuncao = _mapper.Map<FuFuncao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<FuFuncaoDto>(fuFuncao));
    }

    // DELETE: FuFuncoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_fuFuncoes) {
        FuFuncao fuFuncao = await _fuFuncoes.GetByIdAsync(id);
        if (fuFuncao == null) {
          return NotFound();
        }
        try { 
          await _fuFuncoes.Delete(fuFuncao);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_fuFuncoes) {
        return Ok(_mapper.Map<IEnumerable<FuFuncaoDto>>(
                      await _fuFuncoes.GetData(f => f.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_fuFuncoes) {
        return Ok(_mapper.Map<IEnumerable<FuFuncaoDto>>(
                      await _fuFuncoes.GetData(
                                order: f => f.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_fuFuncoes) {
        return Ok(await _fuFuncoes.SelectList(
                            f => new { f.Id, f.Ano, f.Mes, f.Funcao.Cargo.Denominacao },
                            order: f => f.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_fuFuncoes) {
        return Ok(await _fuFuncoes.SelectList(
                            f => new { f.Id, f.Ano, f.Mes, f.Funcao.Cargo.Denominacao },
                            f => f.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_fuFuncoes) {
        return Ok(new KeyValuePair<int, int>(_fuFuncoes.Count(),
                                             _fuFuncoes.Pages(size: k ?? 16)));
      }
    }
  }
}
