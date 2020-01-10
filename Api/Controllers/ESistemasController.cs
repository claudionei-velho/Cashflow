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
  public class ESistemasController : ControllerBase {
    private readonly IESistemaService _eSistemas;
    private readonly IServiceBase<Sistema> _sistemas;
    private readonly IMapper _mapper;

    public ESistemasController(IESistemaService eSistemas, IServiceBase<Sistema> sistemas, IMapper mapper) {
      _eSistemas = eSistemas;
      _sistemas = sistemas;
      _mapper = mapper;
    }

    // GET: ESistemas
    [HttpGet]
    public async Task<IActionResult> Get() {
      using (_eSistemas) {
        return Ok(_mapper.Map<IEnumerable<ESistemaDto>>(
                      await _eSistemas.GetData(
                                order: e => e.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.SistemaId)
                            ).ToListAsync()));
      }
    }

    // GET: ESistemas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      using (_eSistemas) {
        ESistema eSistema = await _eSistemas.GetFirstAsync(e => e.Id == id);
        if (eSistema == null) {
          return NotFound();
        }
        return Ok(_mapper.Map<ESistemaDto>(eSistema));
      }
    }

    // PUT: ESistemas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ESistemaDto dto) {
      using (_eSistemas) {
        if (_eSistemas.Exists(e => e.Id == id)) {
          if (string.IsNullOrWhiteSpace(dto.Codigo)) {
            using (_sistemas) {
              dto.Codigo = _sistemas.GetById(dto.SistemaId).Codigo;
            }
          }
          if (string.IsNullOrWhiteSpace(dto.Denominacao)) {
            using (_sistemas) {
              dto.Denominacao = _sistemas.GetById(dto.SistemaId).Denominacao;
            }
          }

          ESistemaValidator validator = new ESistemaValidator();
          try {
            validator.ValidateAndThrow(dto);
            await _eSistemas.Update(_mapper.Map<ESistema>(dto));
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

    // POST: ESistemas
    [HttpPost]
    public async Task<IActionResult> Post(ESistemaDto dto) {
      if (string.IsNullOrWhiteSpace(dto.Codigo)) {
        using (_sistemas) {
          dto.Codigo = _sistemas.GetById(dto.SistemaId).Codigo;
        }
      }
      if (string.IsNullOrWhiteSpace(dto.Denominacao)) {
        using (_sistemas) {
          dto.Denominacao = _sistemas.GetById(dto.SistemaId).Denominacao;
        }
      }

      using (_eSistemas) {
        ESistemaValidator validator = new ESistemaValidator();
        try {
          validator.ValidateAndThrow(dto);
          await _eSistemas.Insert(_mapper.Map<ESistema>(dto));
        }
        catch (ValidationException ex) {
          return BadRequest(ex.Errors);
        }
      }
      return Ok();
    }

    // DELETE: ESistemas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
      using (_eSistemas) {
        ESistema eSistema = await _eSistemas.GetByIdAsync(id);
        if (eSistema == null) {
          return NotFound();
        }
        await _eSistemas.Delete(eSistema);
      }
      return NoContent();
    }

    [HttpGet, Route("List/{id}")]
    public async Task<IActionResult> List(int id) {
      using (_eSistemas) {
        return Ok(_mapper.Map<IEnumerable<ESistemaDto>>(
                      await _eSistemas.GetData(
                                e => e.EmpresaId == id,
                                e => e.OrderBy(q => q.SistemaId)
                            ).ToListAsync()));
      }
    }

    [HttpGet, Route("PagedList/{p}/{k}")]
    public async Task<IActionResult> PagedList(int p, int k) {
      if (p < 1 || k < 1) {
        return BadRequest();
      }
      using (_eSistemas) {
        return Ok(_mapper.Map<IEnumerable<ESistemaDto>>(
                      await _eSistemas.GetData(
                                order: e => e.OrderBy(q => q.EmpresaId)
                                             .ThenBy(q => q.SistemaId)
                            ).Skip((p - 1) * k).Take(k).ToListAsync()));
      }
    }

    [HttpGet, Route("SelectList")]
    public async Task<IActionResult> SelectList() {
      using (_eSistemas) {
        return Ok(await _eSistemas.SelectList(
                            e => new { e.Id, e.Codigo, e.Denominacao },
                            order: e => e.OrderBy(q => q.EmpresaId).ThenBy(q => q.SistemaId)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("SelectList/{id}")]
    public async Task<IActionResult> SelectList(int id) {
      using (_eSistemas) {
        return Ok(await _eSistemas.SelectList(
                            e => new { e.Id, e.Codigo, e.Denominacao },
                            e => e.EmpresaId == id,
                            e => e.OrderBy(q => q.SistemaId)
                        ).ToListAsync());
      }
    }

    [HttpGet, Route("Pages/{k?}")]
    public IActionResult Pages(int? k) {
      using (_eSistemas) {
        return Ok(new KeyValuePair<int, int>(_eSistemas.Count(),
                                             _eSistemas.Pages(size: k ?? 16)));
      }
    }
  }
}
