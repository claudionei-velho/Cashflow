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
  public class TCategoriasController : ControllerBase {
    private readonly ITCategoriaService _tCategorias;
    private readonly IMapper _mapper;

    public TCategoriasController(ITCategoriaService tCategorias, IMapper mapper) {
      _tCategorias = tCategorias;
      _mapper = mapper;
    }

    // GET: TCategorias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_tCategorias) {
        return Ok(_mapper.Map<IEnumerable<TCategoriaDto>>(
                      await _tCategorias.ListAsync(
                                order: v => v.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id))));

      }
    }

    // GET: TCategorias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_tCategorias) {
        TCategoria categoria = await _tCategorias.GetFirstAsync(c => c.Id == id);
        if (categoria == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<TCategoriaDto>(categoria));
      }
    }

    // PUT: TCategorias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TCategoriaDto dto) {
      using (_tCategorias) {
        if (_tCategorias.Exists(c => c.Id == id)) {
          TCategoriaValidator validator = new TCategoriaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _tCategorias.Update(_mapper.Map<TCategoria>(dto));
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

    // POST: TCategorias
    [HttpPost]
    public async Task<IActionResult> Post(TCategoriaDto dto) {
      TCategoria categoria = new TCategoria();
      using (_tCategorias) {
        TCategoriaValidator validator = new TCategoriaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _tCategorias.Insert(categoria = _mapper.Map<TCategoria>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<TCategoriaDto>(categoria));
    }

    // DELETE: TCategorias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_tCategorias) {
        TCategoria categoria = await _tCategorias.GetByIdAsync(id);
        if (categoria == null) {
          return NotFound();
        }
        try {
          await _tCategorias.Delete(categoria);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_tCategorias) {
        return Ok(_mapper.Map<IEnumerable<TCategoriaDto>>(
                      await _tCategorias.ListAsync(c => c.EmpresaId == id)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_tCategorias) {
        return Ok(_mapper.Map<IEnumerable<TCategoriaDto>>(
                      await _tCategorias.PageListAsync(
                                order: v => v.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_tCategorias) {
        return Ok(await _tCategorias.SelectListAsync(
                            c => new { c.Id, c.Denominacao },
                            order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_tCategorias) {
        return Ok(await _tCategorias.SelectListAsync(
                            c => new { c.Id, c.Denominacao },
                            c => c.EmpresaId == id));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_tCategorias) {
        return Ok(new KeyValuePair<int, int>(_tCategorias.Count(),
                                             _tCategorias.Pages(size: k ?? 16)));
      }
    }
  }
}
