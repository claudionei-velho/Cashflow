using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

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
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(await _operacoes.ListAsync()));
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
      Operacao operacao = new Operacao();
      using (_operacoes) {
        if (dto == null) {
          return BadRequest();
        }
        await _operacoes.Insert(operacao = _mapper.Map<Operacao>(dto));
      }
      return Ok(_mapper.Map<OperacaoDto>(operacao));
    }

    // DELETE: Operacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_operacoes) {
        Operacao operacao = await _operacoes.GetByIdAsync(id);
        if (operacao == null) {
          return NotFound();
        }
        try {
          await _operacoes.Delete(operacao);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_operacoes) {
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(
                      await _operacoes.ListAsync(p => p.EmpresaId == id)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_operacoes) {
        return Ok(_mapper.Map<IEnumerable<OperacaoDto>>(
                      await _operacoes.PagedListAsync(skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_operacoes) {
        return Ok(await _operacoes.SelectListAsync(
                            p => new { p.Id, p.OpLinha.Denominacao }));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_operacoes) {
        return Ok(await _operacoes.SelectListAsync(
                            p => new { p.Id, p.OpLinha.Denominacao },
                            p => p.EmpresaId == id));
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
