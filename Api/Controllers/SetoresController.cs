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
  public class SetoresController : ControllerBase {
    private readonly ICollectionorService _setores;
    private readonly IMapper _mapper;

    public SetoresController(ICollectionorService setores, IMapper mapper) {
      _setores = setores;
      _mapper = mapper;
    }

    // GET: Setores
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_setores) {
        return Ok(_mapper.Map<IEnumerable<SetorDto>>(
                      await _setores.ListAsync(
                                order: s => s.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id))));
      }
    }

    // GET: Setores/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_setores) {
        Setor setor = await _setores.GetFirstAsync(s => s.Id == id);
        if (setor == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<SetorDto>(setor));
      }
    }

    // PUT: Setores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, SetorDto dto) {
      using (_setores) {
        if (_setores.Exists(s => s.Id == id)) {
          SetorValidator validator = new SetorValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _setores.Update(_mapper.Map<Setor>(dto));
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

    // POST: Setores
    [HttpPost]
    public async Task<IActionResult> Post(SetorDto dto) {
      Setor setor = new Setor();
      using (_setores) {
        SetorValidator validator = new SetorValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _setores.Insert(setor = _mapper.Map<Setor>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok(_mapper.Map<SetorDto>(setor));
    }

    // DELETE: Setores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_setores) {
        Setor setor = await _setores.GetByIdAsync(id);
        if (setor == null) {
          return NotFound();
        }
        try {
          await _setores.Delete(setor);
        }
        catch (Exception ex) {
          return BadRequest(ex.Message);
        }
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_setores) {
        return Ok(_mapper.Map<IEnumerable<SetorDto>>(
                      await _setores.ListAsync(s => s.EmpresaId == id)));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_setores) {
        return Ok(_mapper.Map<IEnumerable<SetorDto>>(
                      await _setores.PageListAsync(
                                order: s => s.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.Id),
                                skip: p, take: k)));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_setores) {
        return Ok(await _setores.SelectListAsync(
                            s => new { s.Id, s.Codigo, s.Denominacao },
                            order: s => s.OrderBy(q => q.EmpresaId).ThenBy(q => q.Id)));
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_setores) {
        return Ok(await _setores.SelectListAsync(
                            s => new { s.Id, s.Codigo, s.Denominacao },
                            s => s.EmpresaId == id));
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_setores) {
        return Ok(new KeyValuePair<int, int>(_setores.Count(),
                                             _setores.Pages(size: k ?? 16)));
      }
    }
  }
}
