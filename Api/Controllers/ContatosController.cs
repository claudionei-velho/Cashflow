﻿using System;
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
  public class ContatosController : ControllerBase {
    private readonly IContatoService _contatos;
    private readonly IMapper _mapper;

    public ContatosController(IContatoService contatos, IMapper mapper) {
      _contatos = contatos;
      _mapper = mapper;
    }

    // GET: Contatos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_contatos) {
        return Ok(_mapper.Map<IEnumerable<ContatoDto>>(
                      await _contatos.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Nome)
                            ).ToListAsync()));
      }
    }

    // GET: Contatos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_contatos) {
        Contato contato = await _contatos.GetFirstAsync(c => c.Id == id);
        if (contato == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ContatoDto>(contato));
      }
    }

    // PUT: Contatos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ContatoDto dto) {
      using (_contatos) {
        if (_contatos.Exists(c => c.Id == id)) {
          ContatoValidator validator = new ContatoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _contatos.Update(_mapper.Map<Contato>(dto));
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

    // POST: Contatos
    [HttpPost]
    public async Task<IActionResult> Post(ContatoDto dto) {
      Contato contato = new Contato();
      using (_contatos) {
        ContatoValidator validator = new ContatoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _contatos.Insert(contato = _mapper.Map<Contato>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok(_mapper.Map<ContatoDto>(contato));
    }

    // DELETE: Contatos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_contatos) {
        Contato contato = await _contatos.GetByIdAsync(id);
        if (contato == null) {
          return NotFound();
        }
        try { 
          await _contatos.Delete(contato);
          return NoContent();
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }      
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_contatos) {
        return Ok(_mapper.Map<IEnumerable<ContatoDto>>(
                      await _contatos.GetData(
                                c => c.EmpresaId == id, c => c.OrderBy(q => q.Nome)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_contatos) {
        return Ok(_mapper.Map<IEnumerable<ContatoDto>>(
                      await _contatos.GetData(
                                order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Nome)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_contatos) {
        return Ok(await _contatos.SelectList(
                            c => new { c.Id, c.Empresa.Fantasia, c.Nome },
                            order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Nome)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_contatos) {
        return Ok(await _contatos.SelectList(
                            c => new { c.Id, c.Empresa.Fantasia, c.Nome },
                            c => c.EmpresaId == id,
                            c => c.OrderBy(q => q.Nome)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_contatos) {
        return Ok(new KeyValuePair<int, int>(_contatos.Count(),
                                             _contatos.Pages(size: k ?? 16)));
      }
    }
  }
}
