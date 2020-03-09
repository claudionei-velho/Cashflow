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
  public class DepartamentosController : ControllerBase {
    private readonly IDepartamentoService _departamentos;
    private readonly IMapper _mapper;

    public DepartamentosController(IDepartamentoService departamentos, IMapper mapper) {
      _departamentos = departamentos;
      _mapper = mapper;
    }

    // GET: Departamentos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_departamentos) {
        return Ok(_mapper.Map<IEnumerable<DepartamentoDto>>(
                      await _departamentos.ListAsync(
                                order: d => d.OrderBy(q => q.Setor.EmpresaId)
                                             .ThenBy(q => q.SetorId).ThenBy(q => q.Id))));
      }
    }

    // GET: Departamentos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_departamentos) {
        Departamento departamento = await _departamentos.GetFirstAsync(d => d.Id == id);
        if (departamento == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<DepartamentoDto>(departamento));
      }
    }

    // PUT: Departamentos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, DepartamentoDto dto) {
      using (_departamentos) {
        if (_departamentos.Exists(d => d.Id == id)) {
          DepartamentoValidator validator = new DepartamentoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _departamentos.Update(_mapper.Map<Departamento>(dto));
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

    // POST: Departamentos
    [HttpPost]
    public async Task<IActionResult> Post(DepartamentoDto dto) {
      Departamento departamento = new Departamento();
      using (_departamentos) {
        DepartamentoValidator validator = new DepartamentoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _departamentos.Insert(departamento = _mapper.Map<Departamento>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<DepartamentoDto>(departamento));
    }

    // DELETE: Departamentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_departamentos) {
        Departamento departamento = await _departamentos.GetByIdAsync(id);
        if (departamento == null) {
          return NotFound();
        }
        try {
          await _departamentos.Delete(departamento);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_departamentos) {
        return Ok(_mapper.Map<IEnumerable<DepartamentoDto>>(
                      await _departamentos.ListAsync(
                                d => d.Setor.EmpresaId == id,
                                d => d.OrderBy(q => q.Setor.EmpresaId)
                                      .ThenBy(q => q.SetorId).ThenBy(q => q.Id))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_departamentos) {
        return Ok(_mapper.Map<IEnumerable<DepartamentoDto>>(
                      await _departamentos.PagedListAsync(
                                order: d => d.OrderBy(q => q.Setor.EmpresaId)
                                             .ThenBy(q => q.SetorId).ThenBy(q => q.Id), 
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_departamentos) {
        return Ok(await _departamentos.SelectListAsync(
                            d => new { d.Id, d.Codigo, d.Denominacao },
                            order: d => d.OrderBy(q => q.Setor.EmpresaId)
                                         .ThenBy(q => q.SetorId).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_departamentos) {
        return Ok(await _departamentos.SelectListAsync(
                            d => new { d.Id, d.Codigo, d.Denominacao },
                            d => d.Setor.EmpresaId == id,
                            d => d.OrderBy(q => q.SetorId).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_departamentos) {
        return Ok(new KeyValuePair<int, int>(_departamentos.Count(),
                                             _departamentos.Pages(size: k ?? 16)));
      }
    }
  }
}
