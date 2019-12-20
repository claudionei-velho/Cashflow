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
                      await _instalacoes.GetData(
                                order: i => i.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    // GET: Instalacoes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_instalacoes) {
        Instalacao instalacao = await _instalacoes.GetByIdAsync(id);
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
      using (_instalacoes) {
        InstalacaoValidator validator = new InstalacaoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _instalacoes.Insert(_mapper.Map<Instalacao>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: Instalacoes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_instalacoes) {
        Instalacao instalacao = await _instalacoes.GetByIdAsync(id);
        if (instalacao == null) {
          return NotFound();
        }
        await _instalacoes.Delete(instalacao);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_instalacoes) {
        return Ok(_mapper.Map<IEnumerable<InstalacaoDto>>(
                      await _instalacoes.GetData(
                                i => i.EmpresaId == id,
                                i => i.OrderBy(q => q.Id)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_instalacoes) {
        return Ok(_mapper.Map<IEnumerable<InstalacaoDto>>(
                      await _instalacoes.GetData(
                                order: i => i.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_instalacoes) {
        return Ok(await _instalacoes.SelectList(
                            i => new { i.Id, i.Prefixo, i.Denominacao },
                            order: i => i.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_instalacoes) {
        return Ok(await _instalacoes.SelectList(
                            i => new { i.Id, i.Prefixo, i.Denominacao },
                            i => i.EmpresaId == id,
                            i => i.OrderBy(q => q.Id)
                        ).ToListAsync());
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
