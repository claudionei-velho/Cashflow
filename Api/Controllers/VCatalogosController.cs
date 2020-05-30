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
  public class VCatalogosController : ControllerBase {
    private readonly IVCatalogoService _catalogos;
    private readonly IMapper _mapper;

    public VCatalogosController(IVCatalogoService catalogos, IMapper mapper) {
      _catalogos = catalogos;
      _mapper = mapper;
    }

    // GET: VEquipamentos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_catalogos) {
        return Ok(_mapper.Map<IEnumerable<VCatalogoDto>>(
                      await _catalogos.ListAsync(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes).ThenBy(q => q.Id))));
      }
    }

    // GET: VEquipamentos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_catalogos) {
        VCatalogo catalogo = await _catalogos.GetFirstAsync(c => c.Id == id);
        if (catalogo == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<VCatalogoDto>(catalogo));
      }
    }

    // PUT: VEquipamentos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VCatalogoDto dto) {
      using (_catalogos) {
        if (_catalogos.Exists(c => c.Id == id)) {
          VCatalogoValidator validator = new VCatalogoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _catalogos.Update(_mapper.Map<VCatalogo>(dto));
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

    // POST: VEquipamentos
    [HttpPost]
    public async Task<IActionResult> Post(VCatalogoDto dto) {
      VCatalogo catalogo = new VCatalogo();
      using (_catalogos) {
        VCatalogoValidator validator = new VCatalogoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _catalogos.Insert(catalogo = _mapper.Map<VCatalogo>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<VCatalogoDto>(catalogo));
    }

    // DELETE: VEquipamentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_catalogos) {
        VCatalogo catalogo = await _catalogos.GetByIdAsync(id);
        if (catalogo == null) {
          return NotFound();
        }
        try {
          await _catalogos.Delete(catalogo);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_catalogos) {
        return Ok(_mapper.Map<IEnumerable<VCatalogoDto>>(
                      await _catalogos.ListAsync(
                                c => c.EmpresaId == id,
                                c => c.OrderBy(q => q.EmpresaId)
                                      .ThenByDescending(q => q.Ano)
                                      .ThenByDescending(q => q.Mes).ThenBy(q => q.Id))));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_catalogos) {
        return Ok(_mapper.Map<IEnumerable<VCatalogoDto>>(
                      await _catalogos.PageListAsync(
                                order: c => c.OrderBy(q => q.EmpresaId)
                                             .ThenByDescending(q => q.Ano)
                                             .ThenByDescending(q => q.Mes).ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_catalogos) {
        return Ok(await _catalogos.SelectListAsync(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenByDescending(q => q.Ano)
                                         .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_catalogos) {
        return Ok(await _catalogos.SelectListAsync(
                            c => new { c.Id, c.Empresa.Fantasia, c.Ano, c.Mes, c.CVeiculo.Classe },
                            v => v.EmpresaId == id,
                            c => c.OrderByDescending(q => q.Ano)
                                  .ThenByDescending(q => q.Mes).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_catalogos) {
        return Ok(new KeyValuePair<int, int>(_catalogos.Count(),
                                             _catalogos.Pages(size: k ?? 16)));
      }
    }
  }
}
