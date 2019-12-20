﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Api.Models;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class OperacoesController : ControllerBase {
    private readonly IOperacaoService _operacoes;
    private readonly IMapper _mapper;

    public OperacoesController(IOperacaoService operacoes, IMapper mapper) {
      _operacoes = operacoes;
      _mapper = mapper;
    }

    // GET: Operacoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_operacoes) {
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(
                      await _operacoes.GetData().ToListAsync()));
      }      
    }

    // GET: Operacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_operacoes) {
        Operacao operacao = await _operacoes.GetFirstAsync(o => o.Id == id);
        if (operacao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<OperacaoDto>(operacao));
      }
    }

    // PUT: Operacoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, OperacaoDto dto) {
      using (_operacoes) {
        if (_operacoes.Exists(o => o.Id == id)) {
          await _operacoes.Update(_mapper.Map<Operacao>(dto));
        }
        else {
          return BadRequest();
        }
      }
      return Ok();
    }

    // POST: Operacoes
    [HttpPost]
    public async Task<IActionResult> Post(OperacaoDto dto) {
      using (_operacoes) {
        if (dto == null) {
          return BadRequest();
        }
        await _operacoes.Insert(_mapper.Map<Operacao>(dto));
      }
      return Ok();
    }

    // DELETE: Operacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_operacoes) {
        Operacao operacao = await _operacoes.GetByIdAsync(id);
        if (operacao == null) {
          return NotFound();
        }
        await _operacoes.Delete(operacao);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_operacoes) {
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(
                      await _operacoes.GetData(p => p.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_operacoes) {
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(
                      await _operacoes.GetData().Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_operacoes) {
        return Ok(await _operacoes.SelectList(
                            p => new { p.Id, p.OpLinha.Denominacao }
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_operacoes) {
        return Ok(await _operacoes.SelectList(
                            p => new { p.Id, p.OpLinha.Denominacao }, 
                            p => p.EmpresaId == id
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_operacoes) {
        return Ok(new KeyValuePair<int, int>(_operacoes.Count(),
                                             _operacoes.Pages(size: k ?? 16)));
      }
    }
  }
}
