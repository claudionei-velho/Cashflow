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
  public class EmpresasController : ControllerBase {
    private readonly IEmpresaService _empresas;
    private readonly IMapper _mapper;

    public EmpresasController(IEmpresaService empresas, IMapper mapper) {
      _empresas = empresas;
      _mapper = mapper;
    }

    // GET: Empresas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_empresas) {
        return Ok(_mapper.Map<IEnumerable<EmpresaDto>>(
                      await _empresas.ListAsync(
                                order: e => e.OrderBy(q => q.Fantasia))));
      }
    }

    // GET: Empresas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_empresas) {
        Empresa empresa = await _empresas.GetFirstAsync(e => e.Id == id);
        if (empresa == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<EmpresaDto>(empresa));
      }
    }

    // PUT: Empresas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, EmpresaDto dto) {
      using (_empresas) {
        if (_empresas.Exists(e => e.Id == id)) {
          EmpresaValidator validator = new EmpresaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _empresas.Update(_mapper.Map<Empresa>(dto));
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

    // POST: Empresas
    [HttpPost]
    public async Task<IActionResult> Post(EmpresaDto dto) {
      Empresa empresa = new Empresa();
      using (_empresas) {
        EmpresaValidator validator = new EmpresaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _empresas.Insert(empresa = _mapper.Map<Empresa>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<EmpresaDto>(empresa));
    }

    // DELETE: Empresas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_empresas) {
        Empresa empresa = await _empresas.GetByIdAsync(id);
        if (empresa == null) {
          return NotFound();
        }
        try {
          await _empresas.Delete(empresa);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{mid}")]
    public async Task<IActionResult> List(int mid) {
      using (_empresas) {
        return Ok(_mapper.Map<IEnumerable<EmpresaDto>>(
                      await _empresas.ListAsync(
                                e => e.MunicipioId == mid,
                                e => e.OrderBy(q => q.Fantasia))));
      }
    }

    [HttpGet, Route("PagedList/{p?}/{k?}")]
    public async Task<IActionResult> PagedList(int p = 1, int k = 8) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_empresas) {
        return Ok(_mapper.Map<IEnumerable<EmpresaDto>>(
                      await _empresas.PagedListAsync(
                                order: e => e.OrderBy(q => q.Fantasia),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_empresas) {
        return Ok(await _empresas.SelectListAsync(
                            e => new { e.Id, e.Fantasia, e.Cnpj },
                            order: e => e.OrderBy(q => q.Fantasia)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public ActionResult<KeyValuePair<int, int>> Pages(int? k) {
      using (_empresas) {
        return new KeyValuePair<int, int>(_empresas.Count(),
                                          _empresas.Pages(size: k ?? 8));
      }
    }
  }
}
