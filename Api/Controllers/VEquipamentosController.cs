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
                      await _vEquipamentos.GetData(
                                order: v => v.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).ToListAsync()));
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
      using (_vEquipamentos) {
        VEquipamentoValidator validator = new VEquipamentoValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _vEquipamentos.Insert(_mapper.Map<VEquipamento>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }        
      }
      return Ok();
    }

    // DELETE: VEquipamentos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_vEquipamentos) {
        VEquipamento vEquipamento = await _vEquipamentos.GetByIdAsync(id);
        if (vEquipamento == null) {
          return NotFound();
        }
        await _vEquipamentos.Delete(vEquipamento);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_vEquipamentos) {
        return Ok(_mapper.Map<IEnumerable<VEquipamentoDto>>(
                      await _vEquipamentos.GetData(v => v.EmpresaId == id).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_vEquipamentos) {
        return Ok(_mapper.Map<IEnumerable<VEquipamentoDto>>(
                      await _vEquipamentos.GetData(
                                order: v => v.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_vEquipamentos) {
        return Ok(await _vEquipamentos.SelectList(
                            v => new { v.Id, v.Denominacao, v.Unidade },
                            order: c => c.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_vEquipamentos) {
        return Ok(await _vEquipamentos.SelectList(
                            v => new { v.Id, v.Denominacao, v.Unidade },
                            v => v.EmpresaId == id
                        ).ToListAsync());
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
