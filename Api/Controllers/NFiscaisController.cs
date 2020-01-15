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
  public class NFiscaisController : ControllerBase {
    private readonly INFiscalService _notas;
    private readonly IMapper _mapper;

    public NFiscaisController(INFiscalService notas, IMapper mapper) {
      _notas = notas;
      _mapper = mapper;
    }

    // GET: NFiscais
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_notas) {      
        return Ok(_mapper.Map<IEnumerable<NFiscalDto>>(
                      await _notas.GetData(
                                order: n => n.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.FornecedorId).ThenBy(q => q.Numero)
                            ).ToListAsync()));
      }
    }

    // GET: NFiscais/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_notas) {
        NFiscal nota = await _notas.GetFirstAsync(n => n.Id == id);
        if (nota == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<NFiscalDto>(nota));
      }
    }

    // PUT: NFiscais/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, NFiscalDto dto) {
      using (_notas) {
        if (_notas.Exists(n => n.Id == id)) {
          NFiscalValidator validator = new NFiscalValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _notas.Update(_mapper.Map<NFiscal>(dto));
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

    // POST: NFiscais
    [HttpPost]
    public async Task<IActionResult> Post(NFiscalDto dto) {
      NFiscal nota = new NFiscal();
      using (_notas) {
        NFiscalValidator validator = new NFiscalValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _notas.Insert(nota = _mapper.Map<NFiscal>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<NFiscal>(nota));
    }

    // DELETE: NFiscais/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_notas) {
        NFiscal nota = await _notas.GetByIdAsync(id);
        if (nota == null) {
          return NotFound();
        }
        try { 
          await _notas.Delete(nota);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_notas) {
        return Ok(_mapper.Map<IEnumerable<NFiscalDto>>(
                      await _notas.GetData(
                                n => n.EmpresaId == id,
                                n => n.OrderBy(q => q.FornecedorId).ThenBy(q => q.Numero)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_notas) {
        return Ok(_mapper.Map<IEnumerable<NFiscalDto>>(
                      await _notas.GetData(
                                order: n => n.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.FornecedorId).ThenBy(q => q.Numero)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_notas) {
        return Ok(await _notas.SelectList(
                            n => new { n.Id, n.Numero, n.ChaveNfe },
                            order: n => n.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.FornecedorId).ThenBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_notas) {
        return Ok(await _notas.SelectList(
                            n => new { n.Id, n.Numero, n.ChaveNfe },
                            n => n.EmpresaId == id,
                            n => n.OrderBy(q => q.FornecedorId).ThenBy(q => q.Numero)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_notas) {
        return Ok(new KeyValuePair<int, int>(_notas.Count(),
                                             _notas.Pages(size: k ?? 16)));
      }
    }
  }
}
