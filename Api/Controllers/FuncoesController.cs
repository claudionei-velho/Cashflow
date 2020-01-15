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
  public class FuncoesController : ControllerBase {
    private readonly IFuncaoService _funcoes;
    private readonly IMapper _mapper;

    public FuncoesController(IFuncaoService funcoes, IMapper mapper) {
      _funcoes = funcoes;
      _mapper = mapper;
    }

    // GET: Funcoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_funcoes) {
        return Ok(_mapper.Map<IEnumerable<FuncaoDto>>(
                      await _funcoes.GetData(
                                order: f => f.OrderBy(q => q.Cargo.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }      
    }

    // GET: Funcoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_funcoes) {
        Funcao funcao = await _funcoes.GetFirstAsync(f => f.Id == id);
        if (funcao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<FuncaoDto>(funcao));
      }
    }

    // PUT: Funcoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FuncaoDto dto) {
      using (_funcoes) {
        if (_funcoes.Exists(f => f.Id == id)) {
          FuncaoValidator validator = new FuncaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _funcoes.Update(_mapper.Map<Funcao>(dto));
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

    // POST: Funcoes
    [HttpPost]
    public async Task<IActionResult> Post(FuncaoDto dto) {
      Funcao funcao = new Funcao();
      using (_funcoes) {
        FuncaoValidator validator = new FuncaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _funcoes.Insert(funcao = _mapper.Map<Funcao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<Funcao>(funcao));
    }

    // DELETE: Funcoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_funcoes) {
        Funcao funcao = await _funcoes.GetByIdAsync(id);
        if (funcao == null) {
          return NotFound();
        }
        try { 
          await _funcoes.Delete(funcao);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_funcoes) {
        return Ok(_mapper.Map<IEnumerable<FuncaoDto>>(
                      await _funcoes.GetData(f => f.Cargo.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_funcoes) {
        return Ok(_mapper.Map<IEnumerable<FuncaoDto>>(
                      await _funcoes.GetData(
                                order: f => f.OrderBy(q => q.Cargo.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_funcoes) {
        return Ok(await _funcoes.SelectList(
                            f => new { f.Id, f.CargoId, f.DepartamentoId, f.Titulo },
                            order: f => f.OrderBy(q => q.Cargo.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_funcoes) {
        return Ok(await _funcoes.SelectList(
                            f => new { f.Id, f.CargoId, f.DepartamentoId, f.Titulo },
                            f => f.Cargo.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_funcoes) {
        return Ok(new KeyValuePair<int, int>(_funcoes.Count(),
                                             _funcoes.Pages(size: k ?? 16)));
      }
    }
  }
}
