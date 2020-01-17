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
using Domain.Extensions;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Api.Controllers {
  [Route("[controller]")]
  [ApiController]
  public class LinhasController : ControllerBase {
    private readonly ILinhaService _linhas;
    private readonly IMapper _mapper;

    public LinhasController(ILinhaService linhas, IMapper mapper) {
      _linhas = linhas;
      _mapper = mapper;
    }

    // GET: Linhas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_linhas) {      
        return Ok(_mapper.Map<IEnumerable<LinhaDto>>(
                      await _linhas.GetData(
                                order: l => l.OrderBy(q => q.EmpresaId).ThenBy(q => q.Prefixo)
                            ).ToListAsync()));
      }
    }

    // GET: Linhas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_linhas) {
        Linha linha = await _linhas.GetFirstAsync(l => l.Id == id);
        if (linha == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<LinhaDto>(linha));
      }
    }

    // PUT: Linhas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, LinhaDto dto) {
      using (_linhas) {
        if (_linhas.Exists(l => l.Id == id)) {
          LinhaValidator validator = new LinhaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _linhas.Update(_mapper.Map<Linha>(dto));
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

    // POST: Linhas
    [HttpPost]
    public async Task<IActionResult> Post(LinhaDto dto) {
      Linha linha = new Linha();
      using (_linhas) {
        LinhaValidator validator = new LinhaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _linhas.Insert(linha = _mapper.Map<Linha>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<LinhaDto>(linha));
    }

    // DELETE: Linhas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_linhas) {
        Linha linha = await _linhas.GetByIdAsync(id);
        if (linha == null) {
          return NotFound();
        }
        try { 
          await _linhas.Delete(linha);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_linhas) {
        return Ok(_mapper.Map<IEnumerable<LinhaDto>>(
                      await _linhas.GetData(
                                _linhas.SearchBy(ForeignKey.EmpresaId, id),
                                l => l.OrderBy(q => q.Prefixo)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}/{id?}")]
    public async Task<IActionResult> PagedList(int p, int k, int? id) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_linhas) {
        return Ok(_mapper.Map<IEnumerable<LinhaDto>>(
                      await _linhas.GetData(
                                _linhas.SearchBy(ForeignKey.EmpresaId, id),
                                l => l.OrderBy(q => q.EmpresaId).ThenBy(q => q.Prefixo)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_linhas) {
        return Ok(await _linhas.SelectList(
                            l => new { l.Id, l.Prefixo, l.Denominacao, l.Descricao },
                            order: l => l.OrderBy(q => q.EmpresaId).ThenBy(q => q.Prefixo)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_linhas) {
        return Ok(await _linhas.SelectList(
                            l => new { l.Id, l.Prefixo, l.Denominacao, l.Descricao },
                            _linhas.SearchBy(ForeignKey.EmpresaId, id),
                            l => l.OrderBy(q => q.Prefixo)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{id?}/{k?}")]
    public IActionResult Pages(int? id, int? k) {
      using (_linhas) {
        return Ok(new KeyValuePair<int, int>(
                          _linhas.Count(_linhas.SearchBy(ForeignKey.EmpresaId, id)),
                          _linhas.Pages(_linhas.SearchBy(ForeignKey.EmpresaId, id), k ?? 16)));
      }
    }
  }
}
