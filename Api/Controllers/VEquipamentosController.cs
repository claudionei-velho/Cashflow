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
  public class VEquipamentosController : ControllerBase {
    private readonly IVEquipamentoService _vEquipamentos;
    private readonly IMapper _mapper;

    public VEquipamentosController(IVEquipamentoService vEquipamentos, IMapper mapper) {
      _vEquipamentos = vEquipamentos;
      _mapper = mapper;
    }

    // GET: VEquipamentos
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_vEquipamentos) {
        return Ok(_mapper.Map<IEnumerable<VEquipamentoDto>>(
                      await _vEquipamentos.ListAsync(
                                order: v => v.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id))));
      }
    }

    // GET: VEquipamentos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_vEquipamentos) {
        VEquipamento vEquipamento = await _vEquipamentos.GetFirstAsync(e => e.Id == id);
        if (vEquipamento == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<VEquipamentoDto>(vEquipamento));
      }
    }

    // PUT: VEquipamentos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, VEquipamentoDto dto) {
      using (_vEquipamentos) {
        if (_vEquipamentos.Exists(v => v.Id == id)) {
          VEquipamentoValidator validator = new VEquipamentoValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _vEquipamentos.Update(_mapper.Map<VEquipamento>(dto));
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
    public async Task<IActionResult> Post(VEquipamentoDto dto) {
      VEquipamento vEquipamento = new VEquipamento();
      using (_vEquipamentos) {
        VEquipamentoValidator validator = new VEquipamentoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _vEquipamentos.Insert(vEquipamento = _mapper.Map<VEquipamento>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<VEquipamento>(vEquipamento));
    }

    // DELETE: VEquipamentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_vEquipamentos) {
        VEquipamento vEquipamento = await _vEquipamentos.GetByIdAsync(id);
        if (vEquipamento == null) {
          return NotFound();
        }
        try {
          await _vEquipamentos.Delete(vEquipamento);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_vEquipamentos) {
        return Ok(_mapper.Map<IEnumerable<VEquipamentoDto>>(
                      await _vEquipamentos.ListAsync(v => v.EmpresaId == id)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_vEquipamentos) {
        return Ok(_mapper.Map<IEnumerable<VEquipamentoDto>>(
                      await _vEquipamentos.PagedListAsync(
                                order: v => v.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_vEquipamentos) {
        return Ok(await _vEquipamentos.SelectListAsync(
                            v => new { v.Id, v.Denominacao, v.Unidade },
                            order: c => c.OrderBy(q => q.EmpresaId)
                                         .ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_vEquipamentos) {
        return Ok(await _vEquipamentos.SelectListAsync(
                            v => new { v.Id, v.Denominacao, v.Unidade },
                            v => v.EmpresaId == id));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_vEquipamentos) {
        return Ok(new KeyValuePair<int, int>(_vEquipamentos.Count(),
                                             _vEquipamentos.Pages(size: k ?? 16)));
      }
    }
  }
}
