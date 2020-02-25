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
  public class NfReferenciasController : ControllerBase {
    private readonly INfReferenciaService _referencias;
    private readonly IMapper _mapper;

    public NfReferenciasController(INfReferenciaService referencias, IMapper mapper) {
      _referencias = referencias;
      _mapper = mapper;
    }

    // GET: NfReferencias
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_referencias) {      
        return Ok(_mapper.Map<IEnumerable<NfReferenciaDto>>(
                      await _referencias.GetData(
                                order: n => n.OrderBy(q => q.NotaId).ThenBy(q => q.Numero)
                            ).ToListAsync()));
      }
    }

    // GET: NfReferencias/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_referencias) {
        NfReferencia nota = await _referencias.GetFirstAsync(n => n.Id == id);
        if (nota == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<NfReferenciaDto>(nota));
      }
    }

    // PUT: NfReferencias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NfReferenciaDto dto) {
      using (_referencias) {
        if (_referencias.Exists(n => n.Id == id)) {
          NfReferenciaValidator validator = new NfReferenciaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _referencias.Update(_mapper.Map<NfReferencia>(dto));
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

    // POST: NfReferencias
    [HttpPost]
    public async Task<IActionResult> Post(NfReferenciaDto dto) {
      NfReferencia nota = new NfReferencia();
      using (_referencias) {
        NfReferenciaValidator validator = new NfReferenciaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _referencias.Insert(nota = _mapper.Map<NfReferencia>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<NfReferenciaDto>(nota));
    }

    // DELETE: NfReferencias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_referencias) {
        NfReferencia nota = await _referencias.GetByIdAsync(id);
        if (nota == null) {
          return NotFound();
        }
        try { 
          await _referencias.Delete(nota);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_referencias) {
        return Ok(_mapper.Map<IEnumerable<NfReferenciaDto>>(
                      await _referencias.GetData(
                                n => n.NFiscal.EmpresaId == id,
                                n => n.OrderBy(q => q.NotaId).ThenBy(q => q.Numero)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_referencias) {
        return Ok(_mapper.Map<IEnumerable<NfReferenciaDto>>(
                      await _referencias.GetData(
                                order: n => n.OrderBy(q => q.NotaId).ThenBy(q => q.Numero)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_referencias) {
        return Ok(await _referencias.SelectList(
                            n => new { n.Id, n.Numero, n.ChaveNfeRef },
                            order: n => n.OrderBy(q => q.NotaId).ThenBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_referencias) {
        return Ok(await _referencias.SelectList(
                            n => new { n.Id, n.Numero, n.ChaveNfeRef },
                            n => n.NFiscal.EmpresaId == id,
                            n => n.OrderBy(q => q.NotaId).ThenBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public ActionResult<KeyValuePair<int, int>> Pages(int? k) {
      using (_referencias) {
        return new KeyValuePair<int, int>(_referencias.Count(),
                                          _referencias.Pages(size: k ?? 8));
      }
    }
  }
}
