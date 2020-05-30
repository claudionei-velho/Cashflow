using System;
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
  public class InstalacoesController : ControllerBase {
    private readonly IInstalacaoService _instalacoes;
    private readonly IMapper _mapper;

    public InstalacoesController(IInstalacaoService instalacoes, IMapper mapper) {
      _instalacoes = instalacoes;
      _mapper = mapper;
    }

    // GET: Instalacoes
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_instalacoes) {
        return Ok(_mapper.Map<IEnumerable<InstalacaoDto>>(
                      await _instalacoes.ListAsync(
                                order: i => i.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id))));
      }
    }

    // GET: Instalacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_instalacoes) {
        Instalacao instalacao = await _instalacoes.GetFirstAsync(i => i.Id == id);
        if (instalacao == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<InstalacaoDto>(instalacao));
      }
    }

    // PUT: Instalacoes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, InstalacaoDto dto) {
      using (_instalacoes) {
        if (_instalacoes.Exists(i => i.Id == id)) {
          InstalacaoValidator validator = new InstalacaoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _instalacoes.Update(_mapper.Map<Instalacao>(dto));
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

    // POST: Instalacoes
    [HttpPost]
    public async Task<IActionResult> Post(InstalacaoDto dto) {
      Instalacao instalacao = new Instalacao();
      using (_instalacoes) {
        InstalacaoValidator validator = new InstalacaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _instalacoes.Insert(instalacao = _mapper.Map<Instalacao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<InstalacaoDto>(instalacao));
    }

    // DELETE: Instalacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_instalacoes) {
        Instalacao instalacao = await _instalacoes.GetByIdAsync(id);
        if (instalacao == null) {
          return NotFound();
        }
        try {
          await _instalacoes.Delete(instalacao);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_instalacoes) {
        return Ok(_mapper.Map<IEnumerable<InstalacaoDto>>(
                      await _instalacoes.ListAsync(
                                i => i.EmpresaId == id,
                                i => i.OrderBy(q => q.Id))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_instalacoes) {
        return Ok(_mapper.Map<IEnumerable<InstalacaoDto>>(
                      await _instalacoes.PageListAsync(
                                order: i => i.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_instalacoes) {
        return Ok(await _instalacoes.SelectListAsync(
                            i => new { i.Id, i.Prefixo, i.Denominacao },
                            order: i => i.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_instalacoes) {
        return Ok(await _instalacoes.SelectListAsync(
                            i => new { i.Id, i.Prefixo, i.Denominacao },
                            i => i.EmpresaId == id,
                            i => i.OrderBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_instalacoes) {
        return Ok(new KeyValuePair<int, int>(_instalacoes.Count(),
                                             _instalacoes.Pages(size: k ?? 16)));
      }
    }
  }
}
