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
  public class CentrosController : ControllerBase {
    private readonly ICentroService _centros;
    private readonly IMapper _mapper;

    public CentrosController(ICentroService centros, IMapper mapper) {
      _centros = centros;
      _mapper = mapper;
    }

    // GET: Centros
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao)
                            ).ToListAsync()));
      }
    }

    // GET: Centros/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_centros) {
        Centro centro = await _centros.GetFirstAsync(c => c.Id == id);
        if (centro == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<CentroDto>(centro));
      }
    }

    // PUT: Centros/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CentroDto dto) {
      using (_centros) {
        if (_centros.Exists(c => c.Id == id)) {
          CentroValidator validator = new CentroValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _centros.Update(_mapper.Map<Centro>(dto));
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

    // POST: Centros
    [HttpPost]
    public async Task<IActionResult> Post(CentroDto dto) {
      Centro centro = new Centro();
      using (_centros) {
        CentroValidator validator = new CentroValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _centros.Insert(centro = _mapper.Map<Centro>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<CentroDto>(centro));
    }

    // DELETE: Centros/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_centros) {
        Centro centro = await _centros.GetByIdAsync(id);
        if (centro == null) {
          return NotFound();
        }
        try { 
          await _centros.Delete(centro);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }     
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.GetData(
                                c => c.EmpresaId == id,
                                c => c.OrderBy(q => q.Classificacao)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_centros) {
        return Ok(_mapper.Map<IEnumerable<CentroDto>>(
                      await _centros.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Classificacao)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_centros) {
        return Ok(await _centros.SelectList(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Classificacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_centros) {
        return Ok(await _centros.SelectList(
                            c => new { c.Id, c.Classificacao, c.Denominacao },
                            c => c.EmpresaId == id,
                            c => c.OrderBy(q => q.Classificacao)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_centros) {
        return Ok(new KeyValuePair<int, int>(_centros.Count(),
                                             _centros.Pages(size: k ?? 16)));
      }
    }
  }
}
